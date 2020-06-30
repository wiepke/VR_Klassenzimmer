import { createSlice } from '@reduxjs/toolkit'
import testData from './test.json'

export const scenarioSlice = createSlice({
  name: 'scenario',
  initialState: testData, //[],
  reducers: {

  }
})

export const selectTimeSorted = ({ scenario }) => scenario.events.sort((a, b) => a.time - b.time)
export const selectIdSorted = ({ scenario }) => scenario.events.sort((a, b) => a.id - b.id)

export default scenarioSlice.reducer
