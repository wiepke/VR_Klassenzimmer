import React from 'react'
import { goodBehaviours, badBehaviours, impulseArray, themeArray } from '../../constants/behaviour'
import BehaviourButton from './BehaviourButton'
import ImpulseButton from './ImpulseButton'
import ThemeButton from './ThemeButton'
//import KeyBinding from './keyBinding'

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
      <div>
        <h3>Impulse</h3>
        <div className='btn-group'>
          {impulseArray.map(b =>
            <ImpulseButton key={b} impulse={b} />)}
        </div>
      </div>
      <div>
        <h3>Themes</h3>
        <div className='btn-group'>
          {themeArray.map(b =>
            <ThemeButton key={b} theme={b} />)}
        </div>
      </div>
	  
    </div>
	
  )
}

export default BehaviourControls