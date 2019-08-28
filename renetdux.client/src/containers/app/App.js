import React from 'react';
import { connect } from 'react-redux';
import { BrowserRouter as Router, Route, Link } from 'react-router-dom';
import { requireAuthentication } from '../../components/AuthenticatedComponent';
import Home from '../home/Home';
import Registration from '../registration/Registration';
import Login from '../login/Login';
import Users from '../users/Users';
import { logout } from '../../slices/authSlice';
import './App.scss';

const App = ({ auth, logout }) => {
  const { isAuthenticated } = auth;

  return (
    <Router>
      <div className="app">
        { isAuthenticated ? 
          <div className="nav-bar">
            <div className="nav-bar__item"><Link to="/">Home</Link></div>
            <div className="nav-bar__item"><Link to="/users">Users</Link></div>
            <div className="nav-bar__item"><button onClick={() => { logout(); }}>Log out</button></div>
          </div>
        :
          <div className="nav-bar">
            <div className="nav-bar__item"><Link to="/">Home</Link></div>
            <div className="nav-bar__item"><Link to="/login">Login</Link></div>
            <div className="nav-bar__item"><Link to="/register">Sign up</Link></div>
          </div>
        }

        <div className="content">
          <Route exact path="/" component={Home} />
          <Route path="/login" component={Login} />
          <Route path="/register" component={Registration} />
          <Route path="/users" component={requireAuthentication(Users)} />
        </div>
      </div>
    </Router>
  );
}

const mapStateToProps = state => ({
  auth: state.auth
})

const mapDispatch = { logout }

export default connect(mapStateToProps, mapDispatch)(App);