import { useEffect } from 'react'
import Mousetrap from 'mousetrap'
import { useDispatch } from 'react-redux'
import { triggerImpulse } from './features/classState/studentsSlice'

const Hotkeys = () => {
	const dispatch = useDispatch()
  useEffect(() => {
    Mousetrap.bind("q", () => { dispatch(triggerImpulse("positive")) })
	Mousetrap.bind("w", () => { dispatch(triggerImpulse("neutral")) })
	Mousetrap.bind("e", () => { dispatch(triggerImpulse("negative")) })
	Mousetrap.bind("r", () => { dispatch(triggerImpulse("evoke")) })
  })

  return null
}

export default Hotkeys
