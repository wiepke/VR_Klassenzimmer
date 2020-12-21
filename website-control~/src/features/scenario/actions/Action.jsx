import React from 'react'

import { useDispatch } from 'react-redux'
import { setTime, deleteEvent } from '../scenarioSlice'

const Action = ({ id, time, title }) => {
  const dispatch = useDispatch()
  const editTime = e => dispatch(setTime({ id, time: e.target.value }))

  return (
    <div>
      <div className="d-flex flex-row mb-2 justify-content-between">
        <span className="input-group mr-4" style={{maxWidth: "100px"}}>
          <input if={`${id}-Time`} value={time} onChange={editTime} className="form-control"/>
          <div className="input-group-append">
            <div className="input-group-text">s</div>
          </div>
        </span>

        <span className="h5">{title} Event</span>
        <button
          className="close" type="button" aria-label="Delete"
          onClick={() => dispatch(deleteEvent(id))}
        >
          &times;
        </button>
      </div>
    </div>
  )
}

export default Action
