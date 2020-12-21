import React from 'react'
import PropTypes from 'prop-types'
import { themeColor } from '../../constants/behaviour'
import { useDispatch } from 'react-redux'
import { triggerTheme } from './studentsSlice'

const ThemeButton = ({ theme }) => {
  const dispatch = useDispatch()

  return (
    <button
      className={`btn btn-${themeColor[theme]}`} type='button' key={theme}
      onClick={() => dispatch(triggerTheme(theme))}
    >
      {theme}
    </button>
  )
}

ThemeButton.propTypes = {
  theme: PropTypes.string.isRequired,
  onClick: PropTypes.func
}

export default ThemeButton