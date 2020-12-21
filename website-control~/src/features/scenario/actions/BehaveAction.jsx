import React from 'react'
import { useDispatch } from 'react-redux'
import { behaviourColors, allBehaviours } from '../../../constants/behaviour'
import { setBehaviour, setStudents } from '../scenarioSlice'
import Action from './Action'

const BehaveAction = ({ event: { time, id, action: { payload } } }) => {
  const dispatch = useDispatch()

  const selectBehaviour = behaviour => () => dispatch(setBehaviour({ id, behaviour }))

  const editStudents = e => dispatch(setStudents({ id, students: e.target.value }))

  const procStudents = () => {
    // Post processing, remove duplicates and sort lexicographically
    const students = [...new Set(payload.students)].sort()
    dispatch(setStudents({ id, students: students.join(",") }))
  }

  return (
    <div>
      <Action id={id} title="Behave" time={time} />

      <div className="float-left mr-4">
        <label htmlFor={`${id}-Students`} className="text-muted">
          Affected Students:
        </label>
        <input
          if={`${id}-Students`} className="form-control"
          value={payload.students.join(",")}
          onChange={editStudents} onBlur={procStudents}/>
      </div>

      <div className="float-left">
        <label className="text-muted" htmlFor={`${id}-Time`}>
          Behaviour
        </label>
        <br />
        <div className="btn-group">
          <button
            className={`btn btn-${behaviourColors[payload.behaviour]} dropdown-toggle`}
            data-toggle="dropdown"
          >
            {payload.behaviour}
          </button>
          <div className="dropdown-menu">
            {allBehaviours.map((b, i) => (
              <button
                className={`dropdown-item alert-${behaviourColors[b]}`} key={i}
                onClick={selectBehaviour(b)}
              >
                {b}
              </button>
            ))}
          </div>
        </div>
      </div>
    </div>
  )
}

export default BehaveAction
