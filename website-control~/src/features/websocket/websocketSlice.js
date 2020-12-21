import { createSlice } from '@reduxjs/toolkit'
import { init, syncBehaviour } from '../classState/studentsSlice'
import { init as canvasInit } from '../classState/canvasSlice'

export const websocketSlice = createSlice({
  name: 'websocket',
  initialState: {
    status: 'closed', // TODO other interesting states?
    error: '',
    connecting: false
  },
  reducers: {
    connectionState: (state, action) => ({ ...state, status: action.payload, connecting: false }),
    socketError: (state, action) => ({ ...state, error: action.payload }),
    tryConnect: (state, action) => ({ ...state, connecting: true })
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

class Teacher {
  Teacher() {
    this.pos = undefined
  }

  setTeacherPos(pos) {
    this.pos = pos
  }

  getTeacherPos() {
    return this.pos
  }
}

// Use class instead of redux to drastically reduce amount of actions
export const teacher = new Teacher()

const messageHandlers = dispatch => ({
  bootstrap: ({ students }) => { dispatch(init(students)); dispatch(canvasInit(students)) },
  behave: ({ student, behaviour }) => dispatch(syncBehaviour({ id: student, behaviour })),
  syncTeacher: pos => teacher.setTeacherPos(pos)
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

export const initSocket = (retry = false) => dispatch => {
  if (socket && !retry) return // Don't run this twice

  socket = new WebSocket("ws://localhost:10000/SockServer")
  dispatch(tryConnect())

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

export const { tryConnect } = websocketSlice.actions

export const statusSelector = ({ websocket }) => websocket.status
export const errorSelector = ({ websocket }) => websocket.error
export const isConnectingSelector = ({ websocket }) => websocket.connecting

export default websocketSlice.reducer
