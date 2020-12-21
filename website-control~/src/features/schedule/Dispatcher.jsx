import { useDispatch, useSelector } from 'react-redux'
import { selectToDispatch, clearDispatch } from './scheduleSlice'

const Dispatcher = () => {
  const dispatch = useDispatch()
  const toDispatch = useSelector(selectToDispatch)

  toDispatch.forEach(action => dispatch(action))

  if (toDispatch.length > 0) dispatch(clearDispatch())

  return null;
}

export default Dispatcher
