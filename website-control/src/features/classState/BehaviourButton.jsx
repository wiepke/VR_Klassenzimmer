import React from 'react'
import PropTypes from 'prop-types'
import { behaviourColors } from '../../constants/behaviour'
import { useDispatch } from 'react-redux'
import { triggerBehaviour } from './studentsSlice'

const BehaviourButton = ({ behaviour }) => {
  const dispatch = useDispatch()

  return (
    <button
      className={`btn btn-${behaviourColors[behaviour]}`} type='button' key={behaviour}
      onClick={() => dispatch(triggerBehaviour(behaviour))}
    >
      {behaviour}
    </button>
  )
}

BehaviourButton.propTypes = {
  behaviour: PropTypes.string.isRequired,
  onClick: PropTypes.func
}

export default BehaviourButton
