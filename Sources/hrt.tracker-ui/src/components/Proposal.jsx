
import 'date-fns';
import DateFnsUtils from '@date-io/date-fns';
import React from 'react';
import { Button } from '@material-ui/core';
import { MuiPickersUtilsProvider, KeyboardDatePicker  } from '@material-ui/pickers';
import Select from '@material-ui/core/Select';
import constants from "../constants";


const CandidatesDal = require('../dal/CandidatesDal');
const PositionsDal = require('../dal/PositionsDal');
const ProposalStepsDal = require('../dal/ProposalStepsDal');
const ProposalStatusesDal = require('../dal/ProposalStatusesDal');
const ProposalDto = require("hrt.dto/src/ProposalDto");


class Proposal extends React.Component
{
    constructor(props) {
        super(props);

        let updatedState = {
            proposalID : props.ProposalID,
            candidateID : props.CandidateID,
            positionID : props.PositionID,
            canChangeCandidate: props.CanChangeCandidate,
            canChangePosition: props.CanChangePosition,
            canEdit: props.canEdit ?  props.canEdit : false,

            onCompleted: props.onCompleted,

            statuses: [],
            steps: [],
            positions: [],
            candidates: [],
            proposal: this._createEmptyProposalObj()
        }

        if(!props.ProposalID)
        {
            updatedState.proposal.CandidateID = props.CandidateID;
            updatedState.proposal.PositionID = props.PositionID;            
        }

        console.log("Initial state", updatedState);


        this.state = updatedState;

        this.onCandidateChanged = this.onCandidateChanged.bind(this);
        this.onPositionChanged = this.onPositionChanged.bind(this);
        this.onStatusChanged = this.onStatusChanged.bind(this);
        this.onCurrentStepChanged = this.onCurrentStepChanged.bind(this);
        this.onNextStepChanged = this.onNextStepChanged.bind(this);
    }

    onCandidateChanged(event)
    {
        let updatedState = this.state;        

        let newCandId = parseInt(event.target.value);

        updatedState.proposal.CandidateID = newCandId;
        this.setState(updatedState);
    }

    onPositionChanged(event)
    {
        let updatedState = this.state;        

        let newPosId = parseInt(event.target.value);

        updatedState.proposal.CandidateID = newPosId;

        this.setState(updatedState);
    }

    onStatusChanged(event)
    {  
        let updatedState = this.state;        

        let newStatusId = parseInt(event.target.value);

        updatedState.proposal.StatusID = newStatusId;
        this.setState(updatedState);      
    }
  
    onCurrentStepChanged(event)
    {   
        let updatedState = this.state;        

        let newCurrStepId = parseInt(event.target.value);

        updatedState.proposal.CurrentStepID = newCurrStepId;
        this.setState(updatedState);       
    }

    onNextStepChanged(event)
    {  
        let updatedState = this.state;        

        let newNextStepId = parseInt(event.target.value);

        updatedState.proposal.NextStepId = newNextStepId;
        this.setState(updatedState);         
    }

    onDueDateChanged(date)
    {    
        let updatedState = this.state;
        updatedState.proposal.DueDate = date;
        this.setState(updatedState);
    }

    onConfirmClicked()
    {
         this.state.onCompleted(this.state.proposal);
    }

    onCancelClicked()
    {
        this.state.onCompleted();
    }

    componentDidMount() {
        let obj = this;
        var dalPos = new PositionsDal();
        var dalSteps = new ProposalStepsDal();
        var dalCandidates = new CandidatesDal();
        var dalStatuses = new ProposalStatusesDal();

        // populating positions
        dalPos.getPositions().then( function(ps) {
            let updatedState = obj.state;
            
            if(ps.status == constants.HTTP_OK) {
                updatedState.positions = ps.data;
                if(!updatedState.proposal.PositionID && ps.data.length > 0) {
                    
                    updatedState.proposal.PositionID = ps.data[0].ID;
                }
                obj.setState(updatedState);
                
            }
            
        });

        // populating positions
        dalCandidates.getCandidates().then( function(cs) {
            let updatedState = obj.state;
            
            if(cs.status == constants.HTTP_OK) {
                updatedState.candidates = cs.data;
                if(!updatedState.proposal.CandidateID && cs.data.length > 0) {
                    
                    updatedState.proposal.CandidateID = cs.data[0].ID;
                }
                obj.setState(updatedState);
            }
        });

        
        // populating steps
        dalSteps.getProposalSteps().then( function(pcs) {
            let updatedState = obj.state;

            if(pcs.status == constants.HTTP_OK) {
                updatedState.steps = pcs.data;
                if(updatedState.proposal.CurrentStepID == 0 && pcs.data.length > 0)
                {
                    updatedState.proposal.CurrentStepID = pcs.data[0].ID;               
                }
                if(updatedState.proposal.NextStepId == 0 && pcs.data[0].length > 0)
                {
                    updatedState.proposal.NextStepId = pcs.data[1].ID;               
                }
            }
            
            obj.setState(updatedState)
        } );

        
        // populating statuses
        dalStatuses.getProposalStatuses().then( function(pcs) {
            let updatedState = obj.state;

            if(pcs.status == constants.HTTP_OK) {
                updatedState.statuses = pcs.data;
                if(!updatedState.proposal.StatusID)
                {
                    updatedState.proposal.StatusID = pcs.data[0].ID;               
                }
            }
            
            obj.setState(updatedState)
        } );
        
    }

    render() {

        let styleEdit = {
            display: this.state.canEdit ? "block": "none"
        };
        let styleView = {
            display: !this.state.canEdit ? "block": "none"
        };  
        

        return (
            <div>
                <div key={"divEdit" + this.state.ID} style={styleEdit}>
                        <table>
                            <tbody>
                            <tr>
                                <td>Candidate</td>
                                <td>
                                <Select
                                    key="cbCandidate"                     
                                    value={this.state.proposal.CandidateID ? this.state.proposal.CandidateID : ""}
                                    onChange = { (event) => this.onCandidateChanged(event) }
                                   >   
                                        {
                                            this.state.candidates.map( (p) => (
                                                <option id={p.ID} key={p.ID} value={p.ID}>
                                                    {p.FirstName + " " + p.LastName}
                                                </option>
                                            ))
                                        }                                   
                                </Select>
                                </td>
                            </tr>
                            <tr>
                                <td>Position</td>
                                <td>
                                <Select
                                    key="cbPosition"                     
                                    value={this.state.proposal.PositionID ? this.state.proposal.PositionID : ""}
                                    onChange = { (event) => this.onPositionChanged(event) }
                                   >           
                                        {
                                            this.state.positions.map( (p) => (
                                                <option id={p.ID} key={p.ID} value={p.ID}>
                                                    {p.Title}
                                                </option>
                                            ))
                                        }                         
                                </Select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Proposed
                                </td>
                                <td>
                                    { (this.state.proposal.Proposed.getMonth() + 1) + "/" 
                                        + this.state.proposal.Proposed.getDate() + "/" 
                                        + this.state.proposal.Proposed.getFullYear() + " " 
                                        + this.state.proposal.Proposed.getHours() + ":"
                                    + this.state.proposal.Proposed.getMinutes() } 
                                </td>
                            </tr>
                            <tr>
                                <td>Status</td>
                                <td>
                                <Select                   
                                    key="cbStatus"                     
                                    value={this.state.proposal.StatusID}
                                    onChange = { (event) => this.onStatusChanged(event) }
                                   >   
                                        {
                                            Object.values(this.state.statuses).map( (p) => (
                                                <option id={p.ID} key={p.ID} value={p.ID}>
                                                    {p.Name}
                                                </option>
                                            ))
                                        }                                 
                                </Select>
                                </td>
                            </tr>
                            <tr>
                                <td>Current Step</td>
                                <td>
                                <Select                  
                                    key="cbCurrentStep"                     
                                    value={this.state.proposal.CurrentStepId}
                                    onChange = { (event) => this.onCurrentStepChanged(event) }
                                   >   
                                        {
                                            Object.values(this.state.steps).map( (p) => (
                                                <option id={p.ID} key={p.ID} value={p.ID}>
                                                    {p.Name}
                                                </option>
                                            ))
                                        }                                 
                                </Select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Due Date
                                </td>
                                <td>
                                <MuiPickersUtilsProvider utils={DateFnsUtils}>
                                    <KeyboardDatePicker
                                        disableToolbar
                                        variant="inline"
                                        format="MM/dd/yyyy"
                                        margin="normal"
                                        id="date-picker-inline"
                                        value={ this.state.proposal.DueDate }
                                        onChange={ (date) => this.onDueDateChanged(date)}
                                        KeyboardButtonProps={{
                                            'aria-label': 'change date',
                                        }}
                                        />
                                </MuiPickersUtilsProvider>
                                </td>
                            </tr>
                            <tr>
                                <td>Next Step</td>
                                <td>
                                <Select
                                    key="cbNextStep"                     
                                    value={this.state.proposal.NextStepID}
                                    onChange = { (event) => this.onNextStepChanged(event) }
                                   >   
                                        {
                                            Object.values(this.state.steps).map( (p) => (
                                                <option id={p.ID} key={p.ID} value={p.ID}>
                                                    {p.Name}
                                                </option>
                                            ))
                                        }                                   
                                </Select>
                                </td>
                            </tr>
                            <tr>
                            <td colspan="2">
                                <Button variant="contained" color="primary"
                                        onClick={ () => this.onConfirmClicked() }>Confirm</Button>

                                <Button variant="contained" 
                                        onClick={ () => this.onCancelClicked() }>Cancel</Button>
                            </td>
                            </tr>
                        </tbody>
                        </table>
                </div>
            </div>
        )
    }

    _createEmptyProposalObj()
    {
        let proposal = new ProposalDto();

        var now = new Date();

        proposal.Proposed = now;
        proposal.DueDate = now;
        
        return proposal;
        
    }
}

export default Proposal;