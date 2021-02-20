import { createSlice } from '@reduxjs/toolkit'
import { act } from 'react-dom/test-utils'
import { emit } from '../websocket/websocketSlice'

// TO BE DONE: See if amount of reducers and states can be minimized
export const replaySlice = createSlice({
  name: 'replayManager',
  initialState: { replayFiles: [], currentReplay: '', currentStudentPerspective : null, currentRecordingTime: 0, currentReplayTime: 0, lengthReplay: 0, isRecording: false, isLoading: false, tabToggle: false },
  reducers: {
    init: (state, action) => {
      return action.payload
    },
    requestReplayFiles: (state, action) => {
      emit({ type: 'requestReplays' })
    },
    startRecording: (state, action) => {
      state.isRecording = true
      emit({ type: 'startRecording' })
    },
    stopRecording: (state, action) => {
      state.isRecording = false
      state.currentRecordingTime = 0
      emit({ type: 'stopRecording' })
    },
    toggleTab: (state, action) => {
      state.tabToggle = action.payload
    },
    setIsLoading: (state, action) => {
      state.isLoading = action.payload
    },
    setLengthReplay: (state, action) => {
      state.lengthReplay = action.payload
    },
    updateReplayFiles: (state, action) => {
      state.replayFiles = action.payload
    },
    updateCurrentReplay: (state, action) => {
      state.currentReplay = action.payload
    },
    tickCurrentRecordingTime: (state, action) => {
      state.currentRecordingTime += 1
    },
    tickCurrentReplayTime: (state, action) => {
      if(state.currentReplayTime < state.lengthReplay)
      {
        state.currentReplayTime += 1
      }
    },
    updateCurrentReplayTime: (state, action) => {
      state.currentReplayTime = parseInt(action.payload)
      emit({ type: 'updateReplayTime', startTime : state.currentReplayTime})
    },
    startLoading: (state, action) => {
      state.isLoading = true
      state.currentReplayTime = 0
      emit({ type: 'startLoading', currentReplay: action.payload })
    },
    pauseLoading: (state, action) => {
      state.isLoading = false
      emit({ type: 'pauseLoading'})
    },
    continueLoading: (state, action) => {
      state.isLoading = true
      emit({ type: 'continueLoading'})
    },
    stopLoading: (state, action) => {
      state.isLoading = false
      state.currentReplayTime = 0
      emit({ type: 'stopLoading' })
    },
    setCurrentStudentPerspective: (state, action) => {
      state.currentStudentPerspective = action.payload.name
      emit({ type: 'switchPerspective', id: action.payload.id }) // id used to find gameobject in unity
    },
    resetPerspective: (state, action) => {
      state.currentStudentPerspective = null
      emit({ type: 'resetPerspective'})
    }
  }
})

export const {
  init, startRecording, stopRecording, toggleTab, setIsLoading, updateReplayFiles, setCurrentStudentPerspective, setLengthReplay,
  startLoading, updateCurrentReplay, tickCurrentRecordingTime, tickCurrentReplayTime, updateCurrentReplayTime, stopLoading, requestReplayFiles, pauseLoading, continueLoading, resetPerspective
} = replaySlice.actions;

export const selectReplayFiles = ({ replay }) => replay.replayFiles
export const selectCurrentReplay = ({ replay }) => replay.currentReplay
export const getRecordingState = ({ replay }) => replay.isRecording
export const getCurrentRecordingTime = ({ replay }) => replay.currentRecordingTime
export const getCurrentReplayTime = ({ replay }) => replay.currentReplayTime
export const getLengthReplay = ({replay}) => replay.lengthReplay
export const getLoadingState = ({ replay }) => replay.isLoading
export const getToggleState = ({ replay }) => replay.tabToggle
export const getCurrentStudentPerspective = ({ replay }) => replay.currentStudentPerspective

export default replaySlice.reducer

