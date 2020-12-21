const { app, BrowserWindow } = require('electron')
const url = require('url')
const path = require('path')

const startURL = process.env.ELECTRON_START_URL || url.format({
  pathname: path.join(__dirname, '/../build/index.html'),
  protocol: 'file:',
  slashes: true
})

const createWindow = () => {
  const win = new BrowserWindow({
    width: 1024, height: 786,
    webPreferences: { nodeIntegration: true }
  })

  win.loadURL(startURL)

}

app.whenReady().then(createWindow)

app.on('window-all-closed', () => {
  if (process.platform !== 'darwin') app.quit()
})

app.on('active', () => {
  if (BrowserWindow.getAllWindows().length === 0) createWindow()
})
