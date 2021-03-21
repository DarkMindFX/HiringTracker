
import React from "react";
import { Link } from "react-router-dom"
//Functional Component 
class MainPage extends React.Component {

  render() {
    return (
      <div>

        <h3>Welcome To Hiring Tracker</h3>
        <Link to="/positions">Positions</Link>
      </div>
    );
  }
};

export default MainPage;