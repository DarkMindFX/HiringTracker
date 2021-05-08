

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
            positions: [],
            showError: false,
            error: null
        };
        this._initColumns();

        this.onRowClick = this.onRowClick.bind(this);
    }

    onRowClick(event) {
    }

    componentDidMount() {
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
                <Button variant="contained" component={Link} color="primary" size="small" to="/position/new" >+ Position</Button>        
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

        return records;
    }
}

export default withRouter(CandidatesPage);