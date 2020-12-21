import React from 'react'
import { useDispatch } from 'react-redux'
import { loadScenario } from './scenarioSlice'

const ImportFile = () => {
  const dispatch = useDispatch()

  const readJson = e => e.target.files[0].text()
    .then(content => dispatch(loadScenario(JSON.parse(content))))

  return (
    <div className="input-group mr-2">
      <div className="custom-file" style={{maxWidth: "300px"}}>
        <input
          type="file" className="custom-file-input" id="fileImport"
          onChange={readJson}
        />
        <label className="custom-file-label" forhtml="fileImport">Choose file to import</label>
      </div>
    </div>
  )
}

export default ImportFile
