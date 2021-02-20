import { configureStore } from '@reduxjs/toolkit'
import studentsReducer from '../features/classState/studentsSlice'
import websocketReducer from '../features/websocket/websocketSlice'
import scenarioReducer from '../features/scenario/scenarioSlice'
import canvasReducer from '../features/classCanvas/canvasSlice'
import scheduleReducer from '../features/schedule/scheduleSlice'
import replayReducer from '../features/replay/replaySlice'


export default configureStore({
  reducer: {
    students: studentsReducer,
    websocket: websocketReducer,
    scenario: scenarioReducer,
    canvas: canvasReducer,
    schedule: scheduleReducer,
    replay : replayReducer
  },
});
