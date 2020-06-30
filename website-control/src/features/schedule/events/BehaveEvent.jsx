import React from 'react'
import { behaviourColors } from '../../../constants/behaviour'

const BehaveEvent = ({ eventData: { payload } }) => {
  const { behaviour, students, remainingTime } = payload
  
  return (
    <button className={`list-group-item ${behaviourColors[behaviour]}`}>
      <span className="float-left">{behaviour}, In: {remainingTime}</span>
    </button>
  )
}

export default BehaveEvent
