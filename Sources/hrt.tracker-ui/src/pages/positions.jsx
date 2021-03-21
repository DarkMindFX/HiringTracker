
import React from "react";
import { DataGrid, GridRowsProp, GridColDef } from '@material-ui/data-grid';

const PositionsDal = require('../dal/PositionsDal')

class PositionsPage extends React.Component {

    _columns = null;

    constructor(props) {
        super(props);

        this.state = { positions: [] };
        this._initColumns();
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

    componentDidMount() {

        let dalPos = new PositionsDal();
        let obj = this;

        dalPos.getPositions().then( function(ps) {
            let newState = { positions: ps };
            obj.setState(newState)
        } );
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

    render() {
        let records = this._getRecords();
        console.log(records);
        return (
        <div style={{ height: 500, width: '100%' }}>
            <h3>Positions</h3>
            <DataGrid columns={this._columns} rows={records}/>        
        </div>
        );
    }
};

export default PositionsPage;