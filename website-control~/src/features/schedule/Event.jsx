import React from 'react'

const Event = ({ time, type }) => (
  <div className="d-flex flex-row justify-content-between">
    <span className="lead">{type}</span>
    <span className="text-muted">{time}</span>
  </div>
)

export default Event
