import React from 'react'
import { useSelector, useDispatch } from 'react-redux'
import {
  selectTitle, setTitle, jsonify, selectSchedule, loadScenario, clearScenario 
} from './scenarioSlice'
import { runSchedule, updateTimer, selectTimer } from '../schedule/scheduleSlice'
import Templates from "./default"
import ImportFile from "./ImportFile"

const ScenarioHeader = () => {
  let dispatch = useDispatch()

  let title = useSelector(selectTitle)
  let json = useSelector(jsonify)
  let schedule = useSelector(selectSchedule)
  let prevTimer = useSelector(selectTimer)

  // TODO: might not work for larger files?
  const tohref = () => "data:text/json;charset=utf-8," + encodeURIComponent(json)

  const run = () => {
    clearInterval(prevTimer) // ensure only one timer is running at a time
    const timer = setInterval(() => { dispatch(updateTimer()) }, 1000)
    dispatch(runSchedule({ events: schedule, timer }))
  }

  const loadJson = json => () => dispatch(loadScenario(json))

  return (
    <>
      <div className="col-12 mb-3 mt-4">
        {/* TODO add sometime in the future
          <button className="btn btn-info mr-2 disabled">Undo</button>
        <button className="btn btn-info mr-4 disabled">Redo</button>*/}

        {/* TODO add dropdown with selection of default schedules */}

        <div className="btn-group mr-2">
          <button
            type="button" className="btn btn-primary dropdown-toggle" data-toggle="dropdown"
            aria-haspopup="true" aria-expanded="false" id="scenarioSelectionButton"
          >
            Scenario Templates
          </button>
          <div className="dropdown-menu" aria-labelledby="scenarioSelectionButton">
            {Templates.map((json, i) => (
              <button key={i} className="dropdown-item" type="button" onClick={loadJson(json)}>
                {json.title}
              </button>
            ))}
          </div>
        </div>

        <a className="btn btn-primary mr-5" href={tohref()} download="export.json">Export</a>
        <button className="btn btn-danger" onClick={() => dispatch(clearScenario())}>Clear Scenario</button>

        <button className="float-right btn btn-success" onClick={run}>
          Run Scenario
        </button>
        <div className="mt-2">
          <ImportFile />
        </div>
      </div>
      <div className="col-12">
        <input
          className="form-control form-control-lg"
          value={title} onChange={e => dispatch(setTitle(e.target.value))}
          placeholder="Scenario Title"
        />
      </div>
    </>
  )
}

export default ScenarioHeader
