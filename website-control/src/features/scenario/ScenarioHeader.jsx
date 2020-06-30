import React from 'react'

const ScenarioHeader = () => {
  return (
    <div className="col-12 mb-3 mt-4">
      <button className="btn btn-primary mr-2">Import</button>
      <button className="btn btn-primary mr-4">Export</button>
      <button className="btn btn-info mr-2 disabled">Undo</button>
      <button className="btn btn-info mr-4 disabled">Redo</button>
      <button className="float-right btn btn-success">Run Scenario</button>
    </div>
  )
}

export default ScenarioHeader
