
import React from "react";
import { Link, withRouter } from 'react-router-dom'
import { DataGrid, GridRowsProp, GridColDef } from '@material-ui/data-grid';
import Alert from '@material-ui/lab/Alert';
import { Button } from '@material-ui/core';
import constants from "../constants";

const PositionsDal = require('../dal/PositionsDal')
const PositionStatusesDal = require('../dal/PositionStatusesDal')
const UsersDal = require('../dal/UsersDal')

class PositionsPage extends React.Component {

    _columns = null;
    _positionStatuses = null;
    _users = null;

    constructor(props) {
        super(props);

        this.state = { 
            positions: [],
            showError: false,
            error: null
        };
        this._initColumns();

        this.onRowClick = this.onRowClick.bind(this);
        this._getPositionStatuses = this._getPositionStatuses.bind(this);
        this._getUsers = this._getUsers.bind(this);
        this._getRecords = this._getRecords.bind(this);
    }


    componentDidMount() {

        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);

        if(token != null) {
            this._getPositionStatuses().then( () => {
                this._getUsers().then( () => {

                    let dalPos = new PositionsDal();
                    let obj = this;

                    dalPos.getPositions().then( function(ps) {
                        let updatedState = obj.state;
                        
                        if(ps.status == constants.HTTP_OK) {
                            updatedState.positions = ps.data;
                            updatedState.showError = false;
                            updatedState.error = null;
                        }
                        else if(ps.status == constants.HTTP_Unauthorized) {
                            console.log('Unauth - need to login')
                            obj.props.history.push("/login?ret=/positions");
                        }
                        else {
                            updatedState.showError = true;
                            updatedState.error = ps.data._message;
                        }
                        obj.setState(updatedState)
                    });
                });
            });
        }
        else {
            console.log('No token - need to login')
            this.props.history.push(`/login?ret=/positions`)
        }
    }  
    
    onRowClick(event) {
        const row = event.row;
        if(row) {
            this.props.history.push(`/position/edit/${row.id}`);
        }

    }

    render() {
        let records = this._getRecords();

        const styleError = {
            display: this.state.showError ? "block" : "none"
        }

        return (
            <div style={{ height: 500, width: '100%' }}>
                <h3>Positions</h3>                
                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                <DataGrid columns={this._columns} rows={records}  onRowClick={ this.onRowClick }/>
                <Button variant="contained" component={Link} color="primary" size="small" to="/position/new" >+ Position</Button>        
            </div>
        );
    }

    _initColumns() {
        this._columns = [
            { field: 'id', headerName: 'ID', width: 100 },
            { field: 'Title', headerName: 'Title', width: 250 },
            { field: 'ShortDesc', headerName: 'Summary', width: 350 },
            { field: 'Status', headerName: 'Status', width: 150 },
            { field: 'CreatedDate', headerName: 'Created On', width: 250 },
            { field: 'CreatedBy', headerName: 'Created By', width: 250 },
        ]        
    }

    _getRecords() {
        let rows = [];
        
        let ps = this.state.positions;

        for(let p in ps) {

            let userCreated = this._users[ps[p].CreatedByID];

            let r = {
                id:  ps[p].ID,
                Title: ps[p].Title,
                ShortDesc: ps[p].ShortDesc,                
                Status: this._positionStatuses[ ps[p].StatusID ].Name,
                CreatedDate: ps[p].CreatedDate,
                CreatedBy: userCreated.FirstName + ' ' + userCreated.LastName
            };

            rows.push(r);
        }

        return rows;
    }

    async _getPositionStatuses() {
        this._positionStatuses = {};
        let statusesDal = new PositionStatusesDal();
        let statuses = await statusesDal.getPositionStatuses();

        for(let s in statuses.data)
        {
             this._positionStatuses[statuses.data[s].ID] = statuses.data[s];           
        }
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
};

export default withRouter(PositionsPage);