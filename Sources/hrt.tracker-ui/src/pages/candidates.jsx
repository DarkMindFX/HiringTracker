

import React from "react";
import { Link, withRouter } from 'react-router-dom'
import { DataGrid, GridRowsProp, GridColDef } from '@material-ui/data-grid';
import Alert from '@material-ui/lab/Alert';
import { Button } from '@material-ui/core';
import constants from "../constants";

const CandidatesDal = require('../dal/CandidatesDal')

class CandidatesPage extends React.Component {

    _columns = null;

    constructor(props) {
        super(props);

        this.state = { 
            candidates: [],
            showError: false,
            error: null
        };
        this._initColumns();

        this.onRowClick = this.onRowClick.bind(this);
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
            { field: 'CreatedBy', headerName: 'Create By', width: 250 }
        ]        
    }

    _getRecords() {
        let records = [];

        let cs = this.state.candidates;

        for(let c in cs) {
            let r = {
                id: cs[c]._candidateId,
                FirstName: cs[c]._fname,
                MiddleName: cs[c]._mname,
                LastName: cs[c]._lname,
                Email: cs[c]._email,
                Phone: cs[c]._phone,
                CreatedDate: cs[c]._createdDate,
                CreatedBy: cs[c]._createdBy._fname + ' ' + cs[c]._createdBy._lname
            };

            records.push(r);
        }

        return records;
    }
}

export default withRouter(CandidatesPage);