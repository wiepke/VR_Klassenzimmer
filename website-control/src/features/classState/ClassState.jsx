import React from 'react'
import StudentState from './StudentState'
import ToggleAll from './ToggleAll'
import BehaviourControls from './BehaviourControls'
import { useSelector } from 'react-redux'
import { selectStudents } from './studentsSlice'

const ClassState = () => {
  const students = useSelector(selectStudents)

  return (
    <div>
      <div className='row row-cols-6'>
        {students.map(s => <div className="col"><StudentState key={s.id} student={s} /></div>)}
      </div>
      <div className='float-right'><ToggleAll /></div>
      <BehaviourControls />
    </div>
  )
}

export default ClassState
