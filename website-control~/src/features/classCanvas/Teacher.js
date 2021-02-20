import { useSelector } from 'react-redux'
import { selectTeacherStyle, translateTeacherPosition } from './canvasSlice'
import { useCanvas } from './Canvas'
import { teacher } from '../websocket/websocketSlice'

const rotate = (x, z, angle) => ([
  Math.cos(angle) * x - Math.sin(angle) * z,
  Math.sin(angle) * x + Math.cos(angle) * z
])

const DrawTeacher = () => {
  const ctx = useCanvas()
  const style = useSelector(selectTeacherStyle)
  const translate = useSelector(translateTeacherPosition)

  const pos = translate(teacher.getTeacherPos())

  if (ctx === null || !pos) return null

  const { x, z, viewX, viewZ } = pos;
  const { size, color, viewAngle, coneLength, coneColor } = style

  ctx.strokeStyle = coneColor
  ctx.beginPath()
  ctx.moveTo(x, z)

  const [x1, z1] = rotate(viewX, viewZ, viewAngle / 2)
  const [x2, z2] = rotate(viewX, viewZ, -viewAngle / 2)

  ctx.lineTo(x + x1 * coneLength, z + z1 * coneLength)
  ctx.lineTo(x + x2 * coneLength, z + z2 * coneLength)
  ctx.lineTo(x, z)
  ctx.stroke()

  ctx.fillStyle = color
  ctx.fillRect(x - size / 2, z - size / 2, size, size)

  return null
}


export default DrawTeacher
