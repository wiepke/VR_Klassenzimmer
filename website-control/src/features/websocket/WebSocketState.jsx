import React, { useEffect } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { statusSelector, errorSelector, initSocket } from './websocketSlice'

const status2type = {
  closed: 'info', connected: 'success', warning: 'warning', error: 'error'
}

const WebSocketState = () => {
  const dispatch = useDispatch()
  const status = useSelector(statusSelector)
  const error = useSelector(errorSelector)

  const msg = {
    connected: 'Verbindung aufgebaut',
    closed: 'Verbindung geschlossen',
    warning: error, error: error
  }

  useEffect(() => {
    initSocket()(dispatch)
  })

  return (
    <div className={`alert alert-${status2type[status]}`}>
      {msg[status]}
    </div>
  )
}

export default WebSocketState
