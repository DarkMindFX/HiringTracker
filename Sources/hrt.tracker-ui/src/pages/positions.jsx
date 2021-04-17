
import React from "react";
import { Link, withRouter } from 'react-router-dom'
import { DataGrid, GridRowsProp, GridColDef } from '@material-ui/data-grid';
import Alert from '@material-ui/lab/Alert';
import { Button } from '@material-ui/core';
import constants from "../constants";

const PositionsDal = require('../dal/PositionsDal')

class PositionsPage extends React.Component {

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


    componentDidMount() {

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
                obj.props.history.push("/login?ret=/positions");
            }
            else {
                updatedState.showError = true;
                updatedState.error = ps.data._message;
            }
            obj.setState(updatedState)
        });
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
            { field: 'CreatedBy', headerName: 'Create By', width: 250 },
        ]        
    }

    _getRecords() {
        let rows = [];
        
        let ps = this.state.positions;

        for(let p in ps) {
            let r = {
                id:  ps[p]._positionId,
                Title: ps[p]._title,
                ShortDesc: ps[p]._shortDesc,                
                Status: ps[p]._status._name,
                CreatedDate: ps[p]._createdDate,
                CreatedBy: ps[p]._createdBy._fname + ' ' + ps[p]._createdBy._lname
            };

            rows.push(r);
        }

        return rows;
    }
};

export default withRouter(PositionsPage);