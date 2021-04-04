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
import PositionsPage from './pages/positions';
import PositionPage from './pages/position';
import RegisterPage from './pages/register';

class App extends React.Component {

  render() {
    return (
      <Router>
       {/*All our Routes goes here!*/}
       <Route exact path="/" component={MainPage} />
       <Route exact path="/positions" component={PositionsPage} />
       <Route exact path="/position/:operation/:id?" component={PositionPage} />
       <Route exact path="/register" component={RegisterPage} />
      </Router>
    );
  }
}

export default App;
