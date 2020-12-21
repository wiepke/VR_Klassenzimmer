import React from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { selectEvents, newEvent, sortStudents } from '../scenarioSlice'
import BehaveAction from './BehaveAction'

const ScheduledActions = () => {
  const schedule = useSelector(selectEvents)
  const dispatch = useDispatch()

  return (
    <div>
      <button className="btn btn-primary my-3 mr-2" onClick={() => dispatch(newEvent())}>
        Add Event
      </button>

      <button className="btn btn-primary my-3" onClick={() => dispatch(sortStudents())}>
        Sort events
      </button>

      <ul className="list-group" style={{maxWidth: "800px"}}>
        {schedule.map(
          (s, i) => <li className="list-group-item" key={i}><BehaveAction event={s} /></li>
      )}
      </ul>
    </div>
  )
}

export default ScheduledActions
