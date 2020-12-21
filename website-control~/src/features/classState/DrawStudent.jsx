import { useSelector } from 'react-redux'
import { selectStudentStyle, unityToCanvas } from './canvasSlice'
import { toHex, behaviourColors } from '../../constants/behaviour'
import { useCanvas } from './Canvas'

const DrawStudent = ({ student: { x, z, name, behaviour, selected } }) => {
  const style = useSelector(selectStudentStyle)
  const ctx = useCanvas()
  const u2c = useSelector(unityToCanvas)

  if (ctx != null && style) {
    const { indicator, fontSize, typeface, border } = style
    const { x: X, z: Z } = u2c(x, z)

    const squareOff = indicator / 2
    const innerOff = squareOff - border

    const color = toHex[behaviourColors[behaviour]]

    // Draw border
    ctx.fillStyle = selected ? "#ff00ff" : "#000000"
    ctx.fillRect(X - squareOff, Z - squareOff, indicator, indicator)

    const textX = X + indicator * .75
    const textZ = Z + indicator - fontSize

    // Name
    ctx.font = fontSize + "px " + typeface
    ctx.fillText(name, textX, textZ)

    // Color represents action type
    ctx.fillStyle = color

    const innerSize = indicator - 2 * border
    ctx.fillRect(X - innerOff, Z - innerOff, innerSize, innerSize)

    ctx.fillStyle = '#a0a0a0'
    ctx.font = (fontSize - 4) + "px " + typeface
    ctx.fillText(behaviour, textX, textZ + fontSize - 2)
  }

  return null
}

export default DrawStudent
