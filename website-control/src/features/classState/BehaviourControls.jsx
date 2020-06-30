import React from 'react'
import { goodBehaviours, badBehaviours } from '../../constants/behaviour'
import BehaviourButton from './BehaviourButton'

const BehaviourControls = () => {
  return (
    <div>
      <div className="mb-4">
        <h3>Good behaviours</h3>
        <div className='btn-group'>
          {goodBehaviours.map(
            b => <BehaviourButton key={b} behaviour={b} />)}
        </div>
      </div>
      <div>
        <h3>Bad behaviours</h3>
        <div className='btn-group'>
          {badBehaviours.map(b =>
            <BehaviourButton key={b} behaviour={b} />)}
        </div>
      </div>
    </div>
  )
}

export default BehaviourControls
