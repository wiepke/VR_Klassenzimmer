import React from 'react'
import { useSelector } from 'react-redux'
import { selectEvents } from './scheduleSlice'
import { BehaveEvent } from './events/'
import Dispatcher from './Dispatcher'

const Schedule = () => {
  const events = useSelector(selectEvents)

  return (
    <div>
      <Dispatcher />
      <h3>Events</h3>
      <ul className="list-group">
        {events.map((e, i) => <BehaveEvent data={e} key={i}/>)}
      </ul>
    </div>
  )
}

export default Schedule
