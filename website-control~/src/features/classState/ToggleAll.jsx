import React from 'react'
import { useDispatch } from 'react-redux'
import { selectAll } from './studentsSlice'

const ToggleAll = () => {
  const dispatch = useDispatch()
  const toggle = b => dispatch(selectAll(b))
  
  return (
    <div className='btn-group' role='group' aria-label='Alle Lerner aus oder abwählen'>
      <button
        className='btn btn-outline-primary' type='button' onClick={() => toggle(false)}
      >
        Alle abwählen
      </button>
      <button className='btn btn-primary' type='button' onClick={() => toggle(true)}>
        Alle auswählen
      </button>
    </div>
  )
}

export default ToggleAll
