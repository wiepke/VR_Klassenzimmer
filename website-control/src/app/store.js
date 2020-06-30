import { configureStore } from '@reduxjs/toolkit'
import studentsReducer from '../features/classState/studentsSlice'
import websocketReducer from '../features/websocket/websocketSlice'
import scenarioReducer from '../features/scenario/scenarioSlice'

export default configureStore({
  reducer: {
    students: studentsReducer,
    websocket: websocketReducer,
    scenario: scenarioReducer
  },
});
