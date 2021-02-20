import { useSelector } from 'react-redux';
import { selectStudentStyle } from '../canvasSlice';
import { useCanvas } from '../Canvas';

const Name = ({ x, z, name, behaviour, selected }) => {
  const ctx = useCanvas();
  const { typeface, fontSize, indicator } = useSelector(selectStudentStyle);

  ctx.fillStyle = selected ? "#ff00ff" : "#000000";

  // Draw border
  const textX = x + indicator * .75;
  const textZ = z + indicator - fontSize;

  // Name
  ctx.font = fontSize + "px " + typeface;
  ctx.fillText(name, textX, textZ);

  ctx.fillStyle = '#a0a0a0';
  ctx.font = (fontSize - 4) + "px " + typeface;
  ctx.fillText(behaviour, textX, textZ + fontSize - 2);

  return null;
};

export default Name