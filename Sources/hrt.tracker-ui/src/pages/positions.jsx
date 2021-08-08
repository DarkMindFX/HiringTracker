



import React from "react";
import { Link, withRouter } from 'react-router-dom'
import { DataGrid } from '@material-ui/data-grid';
import Alert from '@material-ui/lab/Alert';
import { Button } from '@material-ui/core';
import constants from "../constants";

const PageHelper = require("../helpers/PageHelper");
const PositionsDal = require('../dal/PositionsDal');
const DepartmentsDal = require('../dal/DepartmentsDal');
const PositionStatusesDal = require('../dal/PositionStatusesDal');
const UsersDal = require('../dal/UsersDal');


class PositionsPage extends React.Component {

    _columns = null;
    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);

        this.state = { 
            positions: [],
            showError: false,
            error: null
        };
        this._initColumns();
       
        this._getDepartments = this._getDepartments.bind(this);
        this._getPositionStatuses = this._getPositionStatuses.bind(this);
        this._getUsers = this._getUsers.bind(this);
        this._getPositions = this._getPositions.bind(this);
        this._redirectToLogin = this._redirectToLogin.bind(this);

        this.onRowClick = this.onRowClick.bind(this);
    }

    onRowClick(event) {
        const row = event.row;
        if(row) {
            this.props.history.push(`/position/edit/${row.id}`);
        }

    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            			obj._getDepartments().then( () => {
			obj._getPositionStatuses().then( () => {
			obj._getUsers().then( () => {
			obj._getPositions().then( () => {} );
			});});});
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
    }

    render() {
        let records = this._getRecords();

        const styleError = {
            display: this.state.showError ? "block" : "none"
        }

        return (
            <div style={{ height: 500, width: '100%' }}>
                <h1>Positions</h1>                
                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                <DataGrid columns={this._columns} rows={records}  onRowClick={ this.onRowClick }/>
                <Button variant="contained" component={Link} color="primary" size="small" to="/position/new" >+ Position</Button>        
            </div>
        );
    }

    _initColumns() {
        this._columns = [
                { field: 'ID', headerName: 'ID', width: 250 },
                { field: 'DepartmentID', headerName: 'DepartmentID', width: 250 },
                { field: 'Title', headerName: 'Title', width: 250 },
                { field: 'ShortDesc', headerName: 'ShortDesc', width: 250 },
                { field: 'Description', headerName: 'Description', width: 250 },
                { field: 'StatusID', headerName: 'StatusID', width: 250 },
                { field: 'CreatedDate', headerName: 'CreatedDate', width: 250 },
                { field: 'CreatedByID', headerName: 'CreatedByID', width: 250 },
                { field: 'ModifiedDate', headerName: 'ModifiedDate', width: 250 },
                { field: 'ModifiedByID', headerName: 'ModifiedByID', width: 250 },
       
        ]        
    }

    _getRecords() {
        let records = [];

        let cs = Object.values(this.state.positions);

        for(let c in cs) {

            let r = {
                id: cs[c].ID,
                ID: cs[c].ID,
                DepartmentID: cs[c].DepartmentID,
                Title: cs[c].Title,
                ShortDesc: cs[c].ShortDesc,
                Description: cs[c].Description,
                StatusID: cs[c].StatusID,
                CreatedDate: cs[c].CreatedDate,
                CreatedByID: cs[c].CreatedByID,
                ModifiedDate: cs[c].ModifiedDate,
                ModifiedByID: cs[c].ModifiedByID,

            };

            records.push(r);
        }

        return records;
    }

    async _getDepartments() {
        let updatedState = this.state;
        updatedState.departments = {};
        let dalDepartments = new DepartmentsDal();
        let response = await dalDepartments.getDepartments();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.departments[response.data[s].ID] = response.data[s];             
            }
        }
        else if(response.status == constants.HTTP_Unauthorized) {
            this._redirectToLogin();            
        }
        else {
            this._showError(updatedState, response);                        
        }

        this.setState(updatedState);
    }
    async _getPositionStatuses() {
        let updatedState = this.state;
        updatedState.positionstatuses = {};
        let dalPositionStatuses = new PositionStatusesDal();
        let response = await dalPositionStatuses.getPositionStatuses();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.positionstatuses[response.data[s].ID] = response.data[s];             
            }
        }
        else if(response.status == constants.HTTP_Unauthorized) {
            this._redirectToLogin();            
        }
        else {
            this._showError(updatedState, response);                        
        }

        this.setState(updatedState);
    }
    async _getUsers() {
        let updatedState = this.state;
        updatedState.users = {};
        let dalUsers = new UsersDal();
        let response = await dalUsers.getUsers();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.users[response.data[s].ID] = response.data[s];             
            }
        }
        else if(response.status == constants.HTTP_Unauthorized) {
            this._redirectToLogin();            
        }
        else {
            this._showError(updatedState, response);                        
        }

        this.setState(updatedState);
    }
    

    async _getPositions() {
        let updatedState = this.state;
        updatedState.positions = {};
        let dalPositions = new PositionsDal();
        let response = await dalPositions.getPositions();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.positions[response.data[s].ID] = response.data[s];             
            }
        }
        else if(response.status == constants.HTTP_Unauthorized) {
            this._redirectToLogin();            
        }
        else {
            this._showError(updatedState, response);                        
        }

        this.setState(updatedState);
    }

    _showError(updatedState, response) {
        var error = JSON.parse(response.data.response);
        updatedState.showError = true;
        updatedState.error = error.Message;
    }

    _redirectToLogin()
    {        
        this._pageHelper.redirectToLogin(`/positions`);
    }
}

export default withRouter(PositionsPage);