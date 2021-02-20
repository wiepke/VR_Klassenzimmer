import React from 'react'
import Canvas from './Canvas'
import DrawStudent from './DrawStudent/'
import Teacher from './Teacher'
import { wasClicked } from './canvasSlice'
import { useSelector, useDispatch } from 'react-redux'
import { selectStudents, toggle } from '../classState/studentsSlice'

const ClassCanvas = () => {
  const dispatch = useDispatch()
  const students = useSelector(selectStudents)
  const clickCheck = useSelector(wasClicked)

  const onClick = (x, z) => {
    const clicked = students.find(s => clickCheck(s, x, z))
    if (clicked) dispatch(toggle(clicked.id))
  }

  return (
    <Canvas id="classCanvas" width={800} height={600} onClick={onClick}>
      <Teacher />
      {students.map(s => <DrawStudent key={s.id} student={s} />)}
    </Canvas>
  );
}

export default ClassCanvas
