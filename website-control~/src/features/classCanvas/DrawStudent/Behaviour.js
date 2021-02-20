import { useSelector } from 'react-redux';
import { selectStudentStyle } from '../canvasSlice';
import { toHex, behaviourColors } from '../../../constants/behaviour';
import { useCanvas } from '../Canvas';

const Behaviour = ({ x, z, behaviour, selected }) => {
  const ctx = useCanvas();
  const { indicator, border } = useSelector(selectStudentStyle);

  const squareOff = indicator / 2;

  x -= squareOff;
  z -= squareOff;

  ctx.fillStyle = selected ? "#ff00ff" : "#000000";
  ctx.fillRect(x, z, indicator, indicator);

  x += border;
  z += border;

  ctx.fillStyle = toHex[behaviourColors[behaviour]];
  const innerSize = indicator - 2 * border;

  ctx.fillRect(x, z, innerSize, innerSize);

  return null;
};

export default Behaviour