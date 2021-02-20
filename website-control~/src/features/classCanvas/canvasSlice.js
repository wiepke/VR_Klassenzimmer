import { createSlice } from '@reduxjs/toolkit'

const initialCanvas = {
  // Canvas Dimensions
  width: 800, height: 600,

  // Determines padding at borders to allocate (reduce screen real-estate for class)
  padding: { top: 20, bottom: 80, left: 20, right: 80 },

  // Size of the room (used for transforming Unity space into [0,1]^2 space)
  realSpace: { xMin: 0, xMax: 0, zMin: 0, zMax: 0 },

  // Parameters for the representation of students in the canvas
  studentStyle: {
    indicator: 16, border: 1, fontSize: 14, typeface: 'Arial'
  },

  // Some parameters codifying the teachers representation on screen
  teacherStyle: {
    size: 20, color: '#ff0000',
    viewAngle: 45, coneLength: 50, coneColor: '#ff0000'
  }
}

export const canvasSlice = createSlice({
  name: 'canvas',
  initialState: initialCanvas,
  reducers: {
    init: (state, action) => {
      // TODO: It would be better to use studentsSlice.init here as a extraReducer,
      // however this causes a obscure lexical error.
      const xs = action.payload.map(({x}) => x)
      const zs = action.payload.map(({z}) => -z) // z axis is inverted

      state.realSpace = {
        xMin: Math.min(...xs), xMax: Math.max(...xs),
        zMin: Math.min(...zs), zMax: Math.max(...zs)
      }
    }
  }
})

/** Transforms a unity position into the canvas's coordinate system. */
export const unityToCanvas = ({ canvas }) => (x, z) => {
  const {
    realSpace: { xMin, xMax, zMin, zMax },
    width, height,
    padding: { left, right, top, bottom }
  } = canvas

  // Shrink space of the classroom on screen slightly
  const w = width - left - right, h = height - top - bottom
  z = -z; // Flip z axis

  return ({ x: left + (x - xMin) / (xMax - xMin) * w, z: right + (z - zMin) / (zMax - zMin) * h })
}

/** Checks for a given student, whether a click was on their indicator. */
export const wasClicked = state => (student, x, z) => {
  const off = state.canvas.studentStyle.indicator / 2;
  const { x: ux, z: uz } = unityToCanvas(state)(student.x, student.z)

  return x >= ux - off && x <= ux + off && z >= uz - off && z <= uz + off
}

/** Exports student style. */
export const selectStudentStyle = ({ canvas: { studentStyle } }) => studentStyle

export const selectTeacherStyle = ({ canvas: { teacherStyle } }) => teacherStyle

export const translateTeacherPosition = state => pos => {
  if (pos) {
    const toCoords = unityToCanvas(state)
    const { x, z } = toCoords(pos.x, pos.z)
    let { x: viewX, z: viewZ } = toCoords(pos.facingX, pos.facingZ)

    viewX = x - viewX; viewZ = z - viewZ
    const length = Math.sqrt(viewX * viewX + viewZ * viewZ)
    return { x, z, viewX: viewX / length, viewZ: viewZ / length }
  }
  return undefined
}

export const { init } = canvasSlice.actions

export default canvasSlice.reducer
