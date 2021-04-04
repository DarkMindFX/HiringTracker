
import React from "react";
import { Link } from "react-router-dom"
//Functional Component 
class MainPage extends React.Component {

  render() {
    return (
      <div>

        <h3>Welcome To Hiring Tracker</h3>
        <div>
          <Link to="/positions">Positions</Link>
        </div>
        <div>
          <Link to="/position/new">Add Position</Link>
        </div>
        <div>
          <Link to="/register">Register User</Link>
        </div>
      </div>
    );
  }
};

export default MainPage;