import { INIT_STUDENTS, STOP_STUDENT } from 'actions/students'

export const messageTypes = [INIT_STUDENTS, STOP_STUDENT].reduce(
  (acc, msg) => ({ ...acc, [msg]: msg }),
  {}
)

export const uri = 'ws://localhost:10000'
