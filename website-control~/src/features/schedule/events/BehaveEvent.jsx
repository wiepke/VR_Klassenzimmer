import React from 'react'
import { behaviourColors } from '../../../constants/behaviour'
import Event from '../Event'

const BehaveEvent = ({ data: { time, action: { payload: { behaviour, students } } } }) => {
  return (
    <li className={`list-group-item list-group-item-${behaviourColors[behaviour]}`}>
      <Event time={time} type="Behaviour Event" />
      {behaviour}, {students.length} Students
    </li>
  )
}

export default BehaveEvent
