

import React from "react";
import { Link, withRouter } from 'react-router-dom'
import { DataGrid } from '@material-ui/data-grid';
import Alert from '@material-ui/lab/Alert';
import { Button } from '@material-ui/core';
import constants from "../constants";

const CandidatesDal = require('../dal/CandidatesDal')
const UsersDal = require('../dal/UsersDal')

class CandidatesPage extends React.Component {

    _columns = null;
    _users = null;

    constructor(props) {
        super(props);

        this.state = { 
            candidates: [],
            showError: false,
            error: null
        };
        this._initColumns();

        this.onRowClick = this.onRowClick.bind(this);
        this._getUsers = this._getUsers.bind(this);
    }

    onRowClick(event) {
        const row = event.row;
        if(row) {
            this.props.history.push(`/candidate/edit/${row.id}`);
        }

    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {

            this._getUsers();

            let dalCands = new CandidatesDal();
            let obj = this;

            dalCands.getCandidates().then( function(cs) {
                let updatedState = obj.state;

                if(cs.status == constants.HTTP_OK){
                    updatedState.candidates = cs.data;
                    updatedState.showError = false;
                    updatedState.error = null;
                }
                else if(cs.status == constants.HTTP_Unauthorized) {
                    console.log('Unauth - need to login')
                    obj.props.history.push("/login?ret=/candidates");
                }
                else {
                    updatedState.showError = true;
                    updatedState.error = cs.data._message;
                }
                obj.setState(updatedState) 

            })
        }
        else {
            console.log('No token - need to login')
            this.props.history.push(`/login?ret=/candidates`)            
        }
    }

    render() {
        let records = this._getRecords();

        const styleError = {
            display: this.state.showError ? "block" : "none"
        }

        return (
            <div style={{ height: 500, width: '100%' }}>
                <h3>Candidates</h3>                
                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                <DataGrid columns={this._columns} rows={records}  onRowClick={ this.onRowClick }/>
                <Button variant="contained" component={Link} color="primary" size="small" to="/candidate/new" >+ Candidate</Button>        
            </div>
        );
    }

    _initColumns() {
        this._columns = [
            { field: 'id', headerName: 'ID', width: 100 },
            { field: 'FirstName', headerName: 'First Name', width: 250 },
            { field: 'MiddleName', headerName: 'Middle Name', width: 250 },
            { field: 'LastName', headerName: 'Last Name', width: 350 },
            { field: 'Email', headerName: 'Email', width: 150 },
            { field: 'Phone', headerName: 'Phone', width: 150 },
            { field: 'CreatedDate', headerName: 'Created On', width: 250 },
            { field: 'CreatedBy', headerName: 'Created By', width: 250 }
        ]        
    }

    _getRecords() {
        let records = [];

        let cs = this.state.candidates;

        for(let c in cs) {

            let userCreated = this._users[cs[c].CreatedByID];

            let r = {
                id: cs[c].ID,
                FirstName: cs[c].FirstName,
                MiddleName: cs[c].MiddleName,
                LastName: cs[c].LastName,
                Email: cs[c].Email,
                Phone: cs[c].Phone,
                CreatedDate: cs[c].CreatedDate,
                CreatedBy: userCreated.FirstName + ' ' + userCreated.LastName
            };

            records.push(r);
        }

        return records;
    }

    async _getUsers() {
        this._users = {};
        let usersDal = new UsersDal();
        let users = await usersDal.getUsers();

        for(let s in users.data)
        {
             this._users[users.data[s].ID] = users.data[s];             
        }

    }
}

export default withRouter(CandidatesPage);