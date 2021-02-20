export const behaviourColors = {}
export const impulseColor = {}
export const themeColor = {}

export const toHex = {
  'light': '#F7F7f7',
  'success': '#5CB85C',
  'info': '#5BC0DE',
  'warning': '#F0AD4E',
  'danger': '#D9534F',
  'primary': '#0275D8'
}

export const NEUTRAL = 'light'
export const neutral = ['Idle']
neutral.forEach(b => behaviourColors[b] = NEUTRAL)

export const GOOD = 'success'
export const good = ['Writing', 'RaiseArm', 'AskQuestion', 'PlakatPartner']
good.forEach(b => behaviourColors[b] = GOOD)

export const WARNING = 'warning'
export const warning = ['Eating', 'Drinking', 'Playing', 'Staring', 'Tapping']
warning.forEach(b => behaviourColors[b] = WARNING)

export const DANGER = 'danger'
export const bad = ['Hitting', 'Throwing', 'Chatting']
bad.forEach(b => behaviourColors[b] = DANGER)

export const IMPULSE = 'info'
export const impulses = ['positive', 'neutral', 'negative', 'evoke']
impulses.forEach(b => impulseColor[b] = IMPULSE)
impulses.forEach(b => behaviourColors[b] = IMPULSE)

export const THEME_SELECTION = 'primary'
export const themes = ['Bismarck', 'Israel', 'Tag der Befreiung']
themes.forEach(b => themeColor[b] = THEME_SELECTION)
themes.forEach(b => behaviourColors[b] = THEME_SELECTION)

// Export behaviour labels in two different categories (maybe use map here instead?)
export const goodBehaviours = [ ...neutral, ...good ]
export const badBehaviours = [ ...warning, ...bad ]
export const impulseArray = [ ...impulses ]
export const themeArray = [ ...themes ]
export const allBehaviours = [ ...goodBehaviours, ...badBehaviours ]
