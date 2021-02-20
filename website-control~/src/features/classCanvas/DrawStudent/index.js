import React from 'react'

import { useSelector } from 'react-redux'
import { selectStudentStyle, unityToCanvas } from '../canvasSlice'
import { useCanvas } from '../Canvas'
import Behaviour from './Behaviour'
import Name from './Name'

const DrawStudent = ({ student: { x, z, name, behaviour, selected } }) => {
  const style = useSelector(selectStudentStyle)
  const ctx = useCanvas()
  const u2c = useSelector(unityToCanvas)

  if (ctx == null || !style) return null;

  const { x: X, z: Z } = u2c(x, z)

  return (
    <>
      <Behaviour behaviour={behaviour} x={X} z={Z} selected={selected} />
      <Name name={name} x={X} z={Z} selected={selected} behaviour={behaviour} />
    </>
  )
}

export default DrawStudent
