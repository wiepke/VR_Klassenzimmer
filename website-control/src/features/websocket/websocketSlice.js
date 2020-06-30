import { createSlice } from '@reduxjs/toolkit'
import { init, syncBehaviour } from '../classState/studentsSlice'

export const websocketSlice = createSlice({
  name: 'websocket',
  initialState: {
    status: 'closed', // TODO other interesting states?
    error: ''
  },
  reducers: {
    connectionState: (state, action) => ({ ...state, status: action.payload }),
    socketError: (state, action) => ({ ...state, error: action.payload })
  }
})

export const { connectionState, socketError } = websocketSlice.actions

const toSocketAction = action =>
  `${action.type};${JSON.stringify({ ...action, type: undefined })}`

let socket

export const emit = action => {
  console.log(`[socket] emit action of type ${action.type}`)
  socket.send(toSocketAction(action))
}

export const requestBootstrapping = () => {
  emit({ type: 'bootstrap' })
}

const messageHandlers = dispatch => ({
  bootstrap: ({ students }) => dispatch(init(students)),
  behave: ({ student, behaviour }) => dispatch(syncBehaviour({ id: student, behaviour }))
})

const handleMessage = (action, dispatch) => {
  const handler = messageHandlers(dispatch)[action.type]

  if (handler) {
    handler(action.payload)
  } else {
    dispatch(socketError('Unbekannter Aktionstyp ' + action.type))
    dispatch(connectionState('warning'))
  }
}

export const initSocket = () => dispatch => {
  if (socket) return // Don't run this twice

  socket = new WebSocket("ws://localhost:10000/SockServer")

  socket.onopen = e => {
    console.log("[socket] Connection established!")
    dispatch(connectionState('connected'))
    requestBootstrapping()
  }

  socket.onmessage = e => {
    const action = JSON.parse(e.data)
    handleMessage(action, dispatch)
  }

  socket.onerror = e => {
    dispatch(connectionState('error'))
    dispatch(socketError(e.message))
  }

  socket.onclose = e => {
    dispatch(connectionState('closed'))
  }
}

export const statusSelector = ({ websocket }) => websocket.status
export const errorSelector = ({ websocket }) => websocket.error

export default websocketSlice.reducer
