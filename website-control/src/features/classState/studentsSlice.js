import { createSlice } from '@reduxjs/toolkit'
import { emit } from '../websocket/websocketSlice'

export const studentsSlice = createSlice({
  name: 'students',
  initialState: [],
  reducers: {
    init: (state, action) => {
      return action.payload
    },
    toggle: (state, action) => {
      return state.map(
        s => action.payload === s.id ? { ...s, selected: !s.selected } : s
      )
    },
    selectAll: (state, action) => {
      return state.map(s => ({ ...s, selected: action.payload }))
    },
    triggerBehaviour: (state, action) => {
      emit({ // This seems a bit iffy to put into the reducer, but whatever
        type: 'behave',
        students: state.filter(s => s.selected).map(s => s.id), behaviour: action.payload
      })

      return state.map(
        s => s.selected ? { ...s, behaviour: action.payload, selected: false } : s
      )
    },
    syncBehaviour: (state, action) => {
      return state.map(
        s => s.id === action.payload.id ? { ...s, behaviour: action.payload.behaviour } : s
      )
    }
  }
})

export const { init, toggle, selectAll, triggerBehaviour, syncBehaviour } = studentsSlice.actions;

export const selectStudents = ({ students }) => students

export default studentsSlice.reducer
