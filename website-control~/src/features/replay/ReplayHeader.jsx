import React, { useEffect } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { statusSelector } from '../websocket/websocketSlice'
import "./styling.css"

import {
  requestReplayFiles, startRecording, stopRecording, startLoading, stopLoading, toggleTab, pauseLoading, continueLoading, setCurrentStudentPerspective,
  selectReplayFiles, updateCurrentReplay, selectCurrentReplay, getCurrentReplayTime, updateCurrentReplayTime, tickCurrentRecordingTime, tickCurrentReplayTime, getRecordingState, getCurrentRecordingTime,
  getLoadingState, getToggleState, getCurrentStudentPerspective, resetPerspective, getLengthReplay
} from "./replaySlice"

import { selectStudents } from '../classState/studentsSlice'



// https://code.labstack.com/HVdZZYqH
function formatTime(time) {
  // Hours, minutes and seconds
  var hrs = ~~(time / 3600);
  var mins = ~~((time % 3600) / 60);
  var secs = ~~time % 60;

  // Output like "1:01" or "4:03:59" or "123:03:59"
  var ret = "";
  if (hrs > 0) {
    ret += "" + hrs + ":" + (mins < 10 ? "0" : "");
  }
  ret += "" + mins + ":" + (secs < 10 ? "0" : "");
  ret += "" + secs;
  return ret;
}

const ReplayHeader = () => {
  let tabToggle = useSelector(getToggleState)
  let isRecording = useSelector(getRecordingState)

  let dispatch = useDispatch()

  return (
    <>
      <div className="row">
        <nav aria-label="Page navigation example">
          <ul className="pagination">
            <li className={tabToggle ? 'page-item' : 'page-item active'}><a className="page-link" href="#/Replay"
              onClick={!isRecording ? () => dispatch(toggleTab(false)) : null}>Record</a></li>
            <li className={tabToggle ? 'page-item active' : 'page-item'}><a className="page-link" href="#/Replay"
              onClick={!isRecording ? () => dispatch(toggleTab(true)) : null}>Replay</a></li>
          </ul>
        </nav>

        {tabToggle ?
          <LoadHeader />
          :
          <RecordHeader />
        }
      </div>
    </>
  )
}

const RecordHeader = () => {
  let dispatch = useDispatch()
  let isRecording = useSelector(getRecordingState)
  let currentRecordingTime = useSelector(getCurrentRecordingTime)

  let timerID
  useEffect(() => {
    if (isRecording) {
      timerID = setInterval(() => {
        dispatch(tickCurrentRecordingTime())
      }, 1000);
    }
    return () => clearInterval(timerID);
  }, [isRecording]);


  return (
    <>
      <div className="container">
        <button className="btn btn-primary mr-5" disabled={isRecording} onClick={() => dispatch(startRecording())}>
          Start Recording
      </button >
        <button className="btn btn-primary mr-5" disabled={!isRecording} onClick={() => dispatch(stopRecording())}>
          Stop Recording
     </button>
      </div>

      <div>
        {isRecording ?
          <h2 className="row mt-5 ml-3">Recording time: {formatTime(currentRecordingTime)}</h2>
          :
          null
        }


      </div>
    </>
  )
}

const LoadHeader = () => {
  let dispatch = useDispatch()

  let replayData = useSelector(selectReplayFiles)
  let currentReplay = useSelector(selectCurrentReplay)
  let currentReplayTime = useSelector(getCurrentReplayTime)
  let lengthReplay = useSelector(getLengthReplay)
  let isLoading = useSelector(getLoadingState)
  let statusWebsocket = useSelector(statusSelector)
  let allStudents = useSelector(selectStudents)
  let currentStudentPerspective = useSelector(getCurrentStudentPerspective)

  let timerID
  useEffect(() => {
    if (isLoading) {
      timerID = setInterval(() => {
        dispatch(tickCurrentReplayTime())
      }, 1000);
    }
    return () => clearInterval(timerID);
  }, [isLoading]);

  useEffect(() => {
    if (statusWebsocket !== 'closed') {
      dispatch(requestReplayFiles())
    }
  }, []);

  return (
    <div className="container">

      {replayData ?
        <div className="btn-group mr-5">
          <button
            type="button" className="btn btn-primary dropdown-toggle" data-toggle="dropdown"
            aria-haspopup="true" aria-expanded="false" id="replaySelectionButton"
          >
            {currentReplay ?
              currentReplay
              :
              "Select a replay"
            }
          </button>
          <div className="dropdown-menu" aria-labelledby="replaySelectionButton">
            {replayData.map((element, i) => (
              <button key={i} className="dropdown-item" type="button" onClick={() => dispatch(updateCurrentReplay(element))}>
                {element}
              </button>
            ))}
          </div>
        </div>
        :
        null
      }

      <button className="btn btn-primary mr-5" disabled={!currentReplay} onClick={() => dispatch(startLoading(currentReplay))}>
        Load Replay
          </button>

      <button className="btn-group mr-5 btn btn-danger" disabled={!isLoading} onClick={() => dispatch(stopLoading())}>
        Stop loading
          </button>


      <div className="row mt-5 ml-1">
        {isLoading ?
          <img className="icon" src={require('./PauseIcon.png')}
            onClick={currentReplay ? () => dispatch(pauseLoading()) : null}></img>
          :
          <img className="icon" src={require('./ResumeIcon.png')}
            onClick={currentReplay ? () => dispatch(continueLoading()) : null}></img>
        }

        
        <input type="range" min={0} max={lengthReplay} value={currentReplayTime} onChange={ev => dispatch(updateCurrentReplayTime(ev.target.value))} />
        {formatTime(currentReplayTime)}

      </div>

      <div className="btn-group mt-5">
        <h4>Switch perspective:</h4>
      </div>

      <div className="btn-group mt-5">
      </div>

      <div className="row">
        <div className="btn-group mt-1 ml-3">
          {allStudents ?
            <div>
              <button
                type="button" className="btn btn-primary dropdown-toggle" data-toggle="dropdown"
                aria-haspopup="true" aria-expanded="false" id="studentSelectionButton"
              >
                {currentStudentPerspective ?
                  currentStudentPerspective
                  :
                  "Select a student"
                }
              </button>

              <div className="dropdown-menu" aria-labelledby="studentSelectionButton">
                {Object.keys(allStudents).map((element, i) => (
                  <button key={allStudents[i]['name']} className="dropdown-item" type="button" onClick={() => dispatch(setCurrentStudentPerspective(allStudents[i]))}>
                    {allStudents[i]['name']}
                  </button>
                ))}
              </div>
              <button className="btn btn-primary mr-5" disabled={!currentStudentPerspective} onClick={() => dispatch(resetPerspective())}>
                Reset perspective
            </button>
            </div>
            :
            null
          }
        </div>
      </div>
    </div >
  )
}


export default ReplayHeader


