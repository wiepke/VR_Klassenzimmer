import React from 'react'
import ClassState from '../features/classState/ClassState'
import WebSocketState from '../features/websocket/WebSocketState'
import Schedule from '../features/schedule/Schedule'

const Dashboard = () => (
  <div className="row">
    <div className="col-9">
      <div className={`
        d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3
        border-bottom
        `}
      >
        <h1 className='h2 row'>Classroom</h1>
        <WebSocketState />
      </div>
      <ClassState />
    </div>
    <div className="col-3 pt-3">
      <Schedule />
    </div>
  </div>
)

export default Dashboard
