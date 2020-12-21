import React from 'react'
//import StudentState from './StudentState'
import ToggleAll from './ToggleAll'
import BehaviourControls from './BehaviourControls'
import ClassCanvas from './ClassCanvas'

const ClassState = () => {
  return (
    <div>
      {/* Currently being replaced
      <div className='row row-cols-6'>
        {students.map(s => <div className="col"><StudentState key={s.id} student={s} /></div>)}
      </div>
      */}

      <div className='row'>
        <ClassCanvas />
      </div>
      <div className='float-right'><ToggleAll /></div>
      <BehaviourControls />
    </div>
  )
}

export default ClassState
