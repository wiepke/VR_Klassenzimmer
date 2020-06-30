import React from 'react'
import { useSelector } from 'react-redux'
import { selectTimeSorted } from '../scenarioSlice'
import Action from './Action'

const ScheduledActions = () => {
  const schedule = useSelector(selectTimeSorted)

  return (
    <div>
      {schedule.map(s => <Action action={s} />)}
    </div>
  )
}

export default ScheduledActions
