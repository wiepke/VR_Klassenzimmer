import { createSlice } from '@reduxjs/toolkit'

export const scheduleSlice = createSlice({
  name: "schedule",
  initialState: { events: [], timer: undefined, toDispatch: [] },
  reducers: {
    runSchedule: (state, { payload }) => ({
      events: payload.events, timer: payload.timer, toDispatch: []
    }),
    updateTimer: (state, action) => {
      const events = state.events.map(e => ({ ...e, time: e.time - 1 }))
      return {
        ...state, events: events.filter(e => e.time > 0),
        toDispatch: [ ...state.toDispatch, ...events.filter(e => e.time <= 0).map(e => e.action) ]
      }
    },
    clearDispatch: (state, action) => ({ ...state, toDispatch: [] })
  }
})

export const selectEvents = ({ schedule }) => schedule.events
export const selectTimer = ({ schedule }) => schedule.timer
export const selectToDispatch = ({ schedule }) => schedule.toDispatch

export const { runSchedule, updateTimer, clearDispatch } = scheduleSlice.actions

export default scheduleSlice.reducer
