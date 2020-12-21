import { configureStore } from '@reduxjs/toolkit'
import studentsReducer from '../features/classState/studentsSlice'
import websocketReducer from '../features/websocket/websocketSlice'
import scenarioReducer from '../features/scenario/scenarioSlice'
import canvasReducer from '../features/classState/canvasSlice'
import scheduleReducer from '../features/schedule/scheduleSlice'

export default configureStore({
  reducer: {
    students: studentsReducer,
    websocket: websocketReducer,
    scenario: scenarioReducer,
    canvas: canvasReducer,
    schedule: scheduleReducer
  },
});
