export const goodBehaviours = ['idle', 'writing', 'raiseArm', 'question']

export const badBehaviours = [
  'eating', 'drinking', 'lethargic', 'playing', 'starring', 'hitting', 'throwing', 'chatting'
]

const NEUTRAL = 'light'
const WARNING = 'warning'
const DANGER = 'danger'
const GOOD = 'success'

export const behaviourColors = {
  idle: NEUTRAL,
  eating: WARNING,
  drinking: WARNING,
  lethargic: WARNING,
  playing: WARNING,
  starring: WARNING,
  hitting: DANGER,
  throwing: DANGER,
  chatting: DANGER,
  writing: GOOD,
  raiseArm: GOOD, // TODO another category for students requesting for teachers attention?
  question: GOOD
}
