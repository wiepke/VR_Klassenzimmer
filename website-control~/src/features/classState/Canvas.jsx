import React, { useEffect, useState } from 'react'

const CanvasContext = React.createContext(null)
const FrameContext = React.createContext(0)

export const useCanvas = () => {
  React.useContext(FrameContext);
  const renderingContext = React.useContext(CanvasContext);
  return renderingContext;
}

export const useAnimation = (initialValue, valueUpdater) => {
  const animatedValue = React.useRef(initialValue);
  animatedValue.current = valueUpdater(animatedValue.current)
  return animatedValue.current
}

const Canvas = props => {
  // Use Ref for DOM access
  const canvasRef = React.useRef(null)

  const [ renderingContext, setRenderingContext ] = useState(null)
  const [ frameCount, setFrameCount ] = useState(0)

  const { width, height } = props

  useEffect(() => {
    const context2d = canvasRef.current.getContext("2d")

    setRenderingContext(context2d)

    // TODO: only redraw when necessary (i.e. add a hook?)
    const frameId = requestAnimationFrame(() => {
      setFrameCount(frameCount + 1)
    })

    return () => cancelAnimationFrame(frameId)
  }, [frameCount, setFrameCount])

  // Redraw on layout change
  React.useLayoutEffect(() => {
    setFrameCount(0)
  }, [width, height])

  // clear frame
  if (renderingContext !== null) {
    renderingContext.clearRect(0, 0, width, height)
  }

  const onClick = ({ clientX, clientY }) => {
    // TODO: somehow get this to be available in a more elegant way
    const { left, top } = document.getElementById('classCanvas').getBoundingClientRect()
    props.onClick(clientX - left, clientY - top)
  }

  return (
    <CanvasContext.Provider value={renderingContext}>
      <FrameContext.Provider value={frameCount}>
        <canvas
          id={props.id} ref={canvasRef} height={props.height} width={props.width} onClick={onClick}
        />
        {props.children}
      </FrameContext.Provider>
    </CanvasContext.Provider>
  )
}

export default Canvas
