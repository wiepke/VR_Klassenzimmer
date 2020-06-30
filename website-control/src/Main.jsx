import React from 'react'
import { Dashboard, Scenario } from './pages/'
import './sidebar.css'
import { Link, Switch, Route, BrowserRouter as Router, useRouteMatch } from 'react-router-dom'

const NavBar = () => (
  <nav className='navbar navbar-dark sticky-top bg-dark flex-md-nowrap p-0 shadow'>
    <Link className='navbar-brand col-md-3 col-lg-2 mr-0 px-3' to='/'>VR Classroom</Link>
    <button
      className='navbar-toggler position-absolute d-md-none collapsed'
      type='button'
      data-toggle='collapse'
      data-target='#sidebarMenu'
      aria-controls='sidebarMenu'
      aria-expanded='false'
      aria-label='Toggle navigation'
    >
      <span className='navbar-toggler-icon' />
    </button>
  </nav>
)

const NavLink = ({ to, children }) => {
  const match = useRouteMatch()

  // TODO: Highlight active link
  //
  return (
    <li className="nav-item">
      <Link className={`nav-link`} to={to}>{children}</Link>
    </li>
  )
}

const SidebarMenu = () => (
  <nav id='sidebarMenu' className='col-md-3 col-lg-2 d-md-block bg-light sidebar collapse'>
    <div className='sidebar-sticky pt-3'>
      <ul className='nav flex-column'>
        <NavLink to="/">Classroom Controls</NavLink>
        <NavLink to="/scenario">Scenario</NavLink>
      </ul>
    </div>
  </nav>
)

const Main = () => (
  <Router>
    <NavBar />
    <div className='container-fluid'>
      <div className='row'>
        <SidebarMenu />
        <main className='col-md-9 ml-sm-auto col-lg-10 px-md-4' role='main'>
          <Switch>
            <Route path="/" exact><Dashboard /></Route>
            <Route path="/scenario" exact><Scenario /></Route>
          </Switch>
        </main>
      </div>
    </div>
  </Router>
)

export default Main
