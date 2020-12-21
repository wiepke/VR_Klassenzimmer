import React from 'react'
import ScenarioHeader from '../features/scenario/ScenarioHeader'
import ScheduledActions from '../features/scenario/actions/ScheduledActions'

const Scenario = () => (
  <div className="row">
    <ScenarioHeader />
    <div className="mt-3 container-fluid">
      <ScheduledActions />
    </div>
  </div>
)

export default Scenario
