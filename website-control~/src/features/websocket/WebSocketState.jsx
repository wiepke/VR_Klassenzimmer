import React, { useEffect } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { statusSelector, errorSelector, initSocket, isConnectingSelector } from './websocketSlice'

const status2type = {
  closed: 'info', connected: 'success', warning: 'warning', error: 'error'
}

const WebSocketState = () => {
  const dispatch = useDispatch()
  const status = useSelector(statusSelector)
  const error = useSelector(errorSelector)
  const connecting = useSelector(isConnectingSelector)

  const msg = {
    connected: 'Verbindung aufgebaut',
    closed: 'Verbindung geschlossen',
    warning: error, error: error
  }

  const connect = (retry) => initSocket(retry)(dispatch)

  useEffect(() => {
    connect()
  })

  return (
    <button
      className={`btn btn-${status2type[status]} ${status === 'connected' && 'disabled'}`}
      onClick={() => connect(true)}
      disabled={connecting || status === 'connected'}>
      {connecting ? (<div className='spinner-border' role='status' />) : (
        <span>{msg[status]} {status !== 'connected' && '(Retry Connection)'}</span>
      )}
    </button>
  )
}

export default WebSocketState
