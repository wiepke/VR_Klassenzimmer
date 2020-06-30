const csv = require('csv-parser')
const fs = require('fs')

const from = process.argv[2]
const to = process.argv[3]

const data = { title: to, description: '', seed: 0, events: [] }
let i = 0

fs.createReadStream(from).pipe(csv()).on('data', row => {
  data.events.push({
    id: i,
    action: { type: 'behave', students: [ row.student ] },
    state: {},
    time: row.time
  })
}).on('end', () => {
  if (!to) console.error("No location given!")
  else fs.writeFileSync(to, JSON.stringify(data))
})
