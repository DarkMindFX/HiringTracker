
import React from 'react';
import { DataGrid } from '@material-ui/data-grid';
import { Button } from '@material-ui/core';
import constants from "../constants";

const ProposalStepsDal = require('../dal/ProposalStepsDal');
const ProposalStatusesDal = require('../dal/ProposalStatusesDal');

class CandidateProposals extends React.Component
{
    _columns = null;
    _statuses = null;
    _steps = null;


    constructor(props) {
        super(props);

        let updatedState = {
            candidateID: props.candidateID,
            canDelete: props.canDelete,
            canEdit: props.canEdit,
            onProposalClick: props.onProposalClick,
            onProposalDelete: props.onProposalDelete
        }

        this.state = updatedState;
    }

    componentDidMount() {

        var dalSteps = new ProposalStepsDal();
        var dalStatuses = new ProposalStatusesDal();

        // populating steps
        dalSteps.getProposalSteps().then( function(pcs) {
            let updatedState = obj.state;

            if(pcs.status == constants.HTTP_OK) {
                updatedState.steps = {};
                for(i in pcs.data)
                {
                    updatedState.steps[pcs.data[i].ID] = pcs.data[i];  
                }              
            }
            
            obj.setState(updatedState)
        });

        
        // populating statuses
        dalStatuses.getProposalStatuses().then( function(pcs) {
            let updatedState = obj.state;

            if(pcs.status == constants.HTTP_OK) {
                updatedState.statuses = {};
                for(i in pcs.data)
                {
                    updatedState.statuses[pcs.data[i].ID] = pcs.data[i];  
                } 
            }
            
            obj.setState(updatedState)
        } );

    }

    render() {

        return (
            <div style={{ height: 500, width: '100%' }}>
                <DataGrid columns={this._columns} rows={records}  onRowClick={ this.onRowClick }/>                
            </div>
        );

    }

    async _getProposalSteps() {

    }
}

export default CandidateProposals;

