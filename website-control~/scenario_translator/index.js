const csv = require('csv-parser')
const fs = require('fs')

const from = process.argv[2]
const to = process.argv[3]

const data = { title: to, description: '', seed: 0, events: [] }

const translate = {
  breathing: "Idle",
  playing: "Playing",
  writing: "Writing",
  eat: "Eating",
  chatting: "Chatting",
  letargic: "Staring",
  hit: "Hitting",
  throw: "Throwing"
}

fs.createReadStream(from).pipe(csv()).on('data', row => {
  data.events.push({
    id: row.id,
    action: {
      type: 'students/behave',
      payload: { students: [ row.student ], behaviour: translate[row.behaviour] }
    },
    state: {},
    time: row.time
  })
}).on('end', () => {
  if (!to) console.error("No location given!")
  else fs.writeFileSync(to, JSON.stringify(data))
})
