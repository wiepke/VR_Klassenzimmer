import { createSlice } from '@reduxjs/toolkit'
import { behave } from '../classState/studentsSlice'

function CreateUUID() {
  return ([1e7]+-1e3+-4e3+-8e3+-1e11).replace(/[018]/g, c => {
    const rand = c ^ crypto.getRandomValues(new Uint8Array(1))[0]
    return ((rand & 15) >> c / 4).toString(16)
  })
}

const initialState = { title: "", description: "", seed: 0, events: [] }

export const scenarioSlice = createSlice({
  name: 'scenario',
  initialState: initialState,
  reducers: {
    loadScenario: (state, action) => action.payload,
    clearScenario: (state, action) => initialState,
    setTitle: (state, action) => {
      state.title = action.payload
    },
    deleteEvent: (state, action) => {
      state.events = state.events.filter(e => e.id !== action.payload)
    },
    setTime: (state, { payload: { id, time } }) => {
      const t = parseInt((time || "0").replace(/\D/g, ''), 10)
      state.events = state.events.map(e => e.id === id ? { ...e, time: t } : e)
    },

    // TODO: Below are actions/reducers which NEED to be abstracted for future expansions
    setStudents: (state, { payload: { id, students } }) => {
      const studentsProcessed = students.split(",").map(s => s.trim().toUpperCase())
      state.events = state.events.map(e => e.id === id
        ? { ...e, action: { ...e.action, payload: { ...e.action.payload, students: studentsProcessed } } } : e
      )
    },
    setBehaviour: (state, { payload: { id, behaviour } }) => {
      state.events = state.events.map(e => e.id === id
        ? { ...e, action: { ...e.action, payload: { ...e.action.payload, behaviour } } } : e
      )
    },
    // TODO rename
    sortStudents: (state, action) => {
      state.events = state.events.sort((a, b) => a.time - b.time)
    },

    // TODO just give the desired initial action state instead
    newEvent: (state, action) => {
      state.events = [
        {
          id: CreateUUID(),
          action: behave({ students: [], behaviour: "Idle" }),
          state: {},
          time: 0
        },
        ...state.events
      ]
    }
  }
})

export const selectEvents = ({ scenario }) => scenario.events
export const jsonify = ({ scenario }) => JSON.stringify(scenario)
export const selectTitle = ({ scenario }) => scenario.title
export const selectSchedule = ({ scenario }) => scenario.events
  .map((e, i) => ({ time: e.time, action: e.action, id: i }))
  .sort((e1, e2) => e1.time - e2.time)

export const {
  setTime, setBehaviour, setStudents, sortStudents, setTitle, deleteEvent, newEvent, loadScenario,
  clearScenario
} = scenarioSlice.actions

export default scenarioSlice.reducer
