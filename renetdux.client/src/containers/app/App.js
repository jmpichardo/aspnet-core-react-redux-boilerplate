import React from 'react';
import { BrowserRouter as Router, Route, Link } from 'react-router-dom';
import Home from '../home/Home';
import Registration from '../registration/Registration';
import Login from '../login/Login';
import './App.scss';

const App = () => {
  return (
    <Router>
      <div className="app">
        <div className="nav-bar">
          <div className="nav-bar__item"><Link to="/">Home</Link></div>
          <div className="nav-bar__item"><Link to="/login">Login</Link></div>
          <div className="nav-bar__item"><Link to="/register">Sign up</Link></div>
        </div>

        <div className="content">
          <Route exact path="/" component={Home} />
          <Route path="/login" component={Login} />
          <Route path="/register" component={Registration} />
        </div>
      </div>
    </Router>
  );
}

export default App;
