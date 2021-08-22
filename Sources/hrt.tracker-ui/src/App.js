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


import DepartmentsPage from './pages/admin/departments';
import DepartmentPage from './pages/admin/department';
import InterviewStatusPage from './pages/admin/interviewstatus';
import InterviewStatusesPage from './pages/admin/interviewstatuses';
import InterviewTypePage from './pages/admin/interviewtype';
import InterviewTypesPage from './pages/admin/interviewtypes';
import PositionStatusPage from './pages/admin/positionstatus';
import PositionStatusesPage from './pages/admin/positionstatuses';
import ProposalStatusPage from './pages/admin/proposalstatus';
import ProposalStatusesPage from './pages/admin/proposalstatuses';
import ProposalStepPage from './pages/admin/proposalstep';
import ProposalStepsPage from './pages/admin/proposalsteps';
import RolePage from './pages/admin/role';
import RolesPage from './pages/admin/roles';
import SkillPage from './pages/admin/skill';
import SkillsPage from './pages/admin/skills';
import SkillProficiencyPage from './pages/admin/skillproficiency';
import SkillProficienciesPage from './pages/admin/skillproficiencies';
import UserPage from './pages/admin/user';
import UsersPage from './pages/admin/users';


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
       

       {/*Admin pages*/}
       <Route exact path="/admin/departments" component={DepartmentsPage} />
       <Route exact path="/admin/department/:operation/:id?" component={DepartmentPage} />
       <Route exact path="/admin/users" component={UsersPage} />
       <Route exact path="/admin/user/:operation/:id?" component={UserPage} />
       <Route exact path="/admin/skills" component={SkillsPage} />
       <Route exact path="/admin/slill/:operation/:id?" component={SkillPage} />
       <Route exact path="/admin/skillproficiencies" component={SkillProficienciesPage} />
       <Route exact path="/admin/slillproficiency/:operation/:id?" component={SkillProficiencyPage} />
       <Route exact path="/admin/roles" component={RolesPage} />
       <Route exact path="/admin/role/:operation/:id?" component={RolePage} />
       <Route exact path="/admin/interviewstatuses" component={InterviewStatusesPage} />
       <Route exact path="/admin/interviewstatus/:operation/:id?" component={InterviewStatusPage} />
       <Route exact path="/admin/interviewtypes" component={InterviewTypesPage} />
       <Route exact path="/admin/interviewtype/:operation/:id?" component={InterviewTypePage} />
       <Route exact path="/admin/positionstatuses" component={PositionStatusesPage} />
       <Route exact path="/admin/positionstatus/:operation/:id?" component={PositionStatusPage} />
       <Route exact path="/admin/proposalstatuses" component={ProposalStatusesPage} />
       <Route exact path="/admin/proposalstatus/:operation/:id?" component={ProposalStatusPage} />
       <Route exact path="/admin/proposalsteps" component={ProposalStepsPage} />
       <Route exact path="/admin/proposalstep/:operation/:id?" component={ProposalStepPage} />
      </Router>
    );
  }
}

export default App;
