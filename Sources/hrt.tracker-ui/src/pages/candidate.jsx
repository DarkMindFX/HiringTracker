


import React from 'react';
import PropTypes from 'prop-types';
import { withRouter  } from 'react-router-dom'
import AppBar from '@material-ui/core/AppBar';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import Typography from '@material-ui/core/Typography';
import Box from '@material-ui/core/Box';

import Candidate from '../components/Candidate';
import CandidateComments from '../components/CandidateComments';

const PageHelper = require("../helpers/PageHelper");
const constants = require('../constants');

function TabPanel(props) {
    const { children, value, index, ...other } = props;
  
    return (
      <div
        role="tabpanel"
        hidden={value !== index}
        id={`simple-tabpanel-${index}`}
        aria-labelledby={`simple-tab-${index}`}
        {...other}
      >
        {value === index && (
          <Box p={3}>
            <Typography>{children}</Typography>
          </Box>
        )}
      </div>
    );
  }
  
  TabPanel.propTypes = {
    children: PropTypes.node,
    index: PropTypes.any.isRequired,
    value: PropTypes.any.isRequired,
  };
  
  function a11yProps(index) {
    return {
      id: `simple-tab-${index}`,
      'aria-controls': `simple-tabpanel-${index}`,
    };
  }



class CandidatePage extends React.Component {

    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);

        this.state = { 
            operation: this.props.match.params.operation,
            id: this.props.match.params.id ? parseInt(this.props.match.params.id) : null,
            canEdit: this.props.match.params.operation ? (this.props.match.params.operation.toLowerCase() == 'new' || 
                                                          this.props.match.params.operation.toLowerCase() == 'edit' ? true : false) : false,
            currentTab: 0
        };

        this.onTabChanged = this.onTabChanged.bind(this);
    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token == null) {
            console.log('No token - need to login')
            this._redirectToLogin();                 
        }
    }

    onTabChanged(event, newValue) {
        let updatedState = this.state;
        updatedState.currentTab = newValue;
        this.setState(updatedState);
    }

    render() {        
        
        return (
            <div>
  
                <Tabs value={this.state.currentTab} onChange={this.onTabChanged} >
                    <Tab label="Candidate Details" {...a11yProps(0)}  />
                    <Tab label="Proposals" {...a11yProps(1)}  />
                    <Tab label="Interviews" {...a11yProps(2)}  />
                    <Tab label="Comments" {...a11yProps(3)}  />
                </Tabs>

                <TabPanel value={this.state.currentTab} index={0}>
                        <Candidate 
                            id = {this.state.id}
                            operation = {this.state.operation}
                        />
                </TabPanel>
                <TabPanel value={this.state.currentTab} index={1}>
                        List of proposed positions
                </TabPanel>
                <TabPanel value={this.state.currentTab} index={2}>
                        List of interviews
                </TabPanel>
                <TabPanel value={this.state.currentTab} index={3}>
                  <CandidateComments 
                    id = {this.state.id}
                  />
                </TabPanel>
            </div>
        );
    }

    _redirectToLogin()
    {
        this._pageHelper.redirectToLogin(`/candidate/${this.state.operation}` + (this.state.id ? `/${this.state.id}` : ``));        
    }    
}

export default withRouter(CandidatePage);