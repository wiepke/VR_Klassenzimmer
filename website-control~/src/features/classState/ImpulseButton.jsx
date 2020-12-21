import React from 'react'
import PropTypes from 'prop-types'
import { impulseColor } from '../../constants/behaviour'
import { useDispatch } from 'react-redux'
import { triggerImpulse } from './studentsSlice'

const ImpulseButton = ({ impulse }) => {
  const dispatch = useDispatch()

  return (
    <button
      className={`btn btn-${impulseColor[impulse]}`} type='button' key={impulse}
      onClick={() => dispatch(triggerImpulse(impulse))}
    >
      {impulse}
    </button>
  )
}

ImpulseButton.propTypes = {
  impulse: PropTypes.string.isRequired,
  onClick: PropTypes.func
}

export default ImpulseButton