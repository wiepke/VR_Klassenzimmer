import React from 'react'
import ScenarioHeader from '../features/scenario/ScenarioHeader'
import ScheduledActions from '../features/scenario/schedule/ScheduledActions'

const Scenario = () => (
  <div className="row">
    <ScenarioHeader />
    <div className="mt-3">
      <ScheduledActions />
    </div>
  </div>
)

export default Scenario
