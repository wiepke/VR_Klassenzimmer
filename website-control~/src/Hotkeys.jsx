import { useEffect } from 'react'
import Mousetrap from 'mousetrap'

const Hotkeys = () => {
  useEffect(() => {
    Mousetrap.bind("q", () => { console.log("Key 'q' was pressed") })
  })

  return null
}

export default Hotkeys
