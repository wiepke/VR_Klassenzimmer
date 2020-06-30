import React from 'react'

const Action = ({ action: { action, state, type, time, id }}) => {

  return (
    <div>
      {/* Common header for all actions */}
      <label htmlFor={`type${id}`}>Event Type: </label>
      <input id={`type${id}`}></input>
      <label htmlFor={`timeIn${id}`}>Invoke in </label>
      <input id={`timeIn${id}`} type="text" />

      {/* Select appropriate action depending on type */}
    </div>
  )
}

export default Action
