import React from 'react'
import { BehaveEvent } from './events/'

const Event = ({ e }) => {
  const component = {
    behave: BehaveEvent
  }[e.type]

  return component({ eventData: e })
}

const Schedule = () => {
  const events = [
    {
      id: 0,
      time: 10,
      action: {
        type: 'behave',
        payload: {
          students: ['0R', '0L', '1L', '2R'],
          behaviour: 'eating'
        }
      }
    }
  ]

  return (
    <div>
      <h3>Events</h3>
      <ul className="list-group">
        { /* TODO: use padding to increase distance between events according to delay? */ }
        {events.map(e => <Event e={e.action} time={e.time} key={e.id} />)}
      </ul>
    </div>
  )
}

export default Schedule
