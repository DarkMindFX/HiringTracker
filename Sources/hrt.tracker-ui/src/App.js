import logo from './logo.svg';
import './App.css';

import {
  BrowserRouter as Router,
  Route,
  Switch,
  Link,
  Redirect
} from 'react-router-dom';

import React from 'react';

import MainPage from './pages';
import CandidatesPage from './pages/candidates';
import CandidatePage from './pages/candidate';
import PositionsPage from './pages/positions';
import PositionPage from './pages/position';
import RegisterPage from './pages/register';
import LogingPage from './pages/login';
import LogoutPage from './pages/logout';

class App extends React.Component {

  render() {
    return (
      <Router>
       {/*All our Routes goes here!*/}
       <Route exact path="/" component={MainPage} />
       <Route exact path="/positions" component={PositionsPage} />
       <Route exact path="/position/:operation/:id?" component={PositionPage} />
       <Route exact path="/register" component={RegisterPage} />
       <Route exact path="/login" component={LogingPage} />
       <Route exact path="/logout" component={LogoutPage} />
       <Route exact path="/candidates" component={CandidatesPage} />
       <Route exact path="/candidate/:operation/:id?" component={CandidatePage} />
      </Router>
    );
  }
}

export default App;
