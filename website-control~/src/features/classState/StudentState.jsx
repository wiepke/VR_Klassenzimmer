import React from 'react'
import PropTypes from 'prop-types'
import { useDispatch } from 'react-redux'
import { behaviourColors } from '../../constants/behaviour'
import { toggle } from './studentsSlice'

const StudentState = ({ student: { id, name, behaviour, selected } }) => {
  const dispatch = useDispatch()

  return (
    <div className='mb-2'>
      <ul className='card list-group'>
        <li className='list-group-item'>
          <button
            className={`font-weight-bolder btn btn-${!selected ? 'outline-' : ''}primary`}
            type='button'
            onClick={() => dispatch(toggle(id))}
          >
            {name}
          </button>
        </li>
        <li className={`list-group-item list-group-item-${behaviourColors[behaviour]}`}>
          <span>{behaviour}</span>
        </li>
      </ul>
    </div>
  )
}

StudentState.propTypes = {
  student: PropTypes.shape({
    name: PropTypes.string,
    id: PropTypes.string,
    behaviour: PropTypes.string,
    selected: PropTypes.bool
  }).isRequired
}

export default StudentState
