
import 'date-fns';
import DateFnsUtils from '@date-io/date-fns';
import React from 'react';
import { Button } from '@material-ui/core';
import { MuiPickersUtilsProvider, KeyboardDatePicker  } from '@material-ui/pickers';
import FormControl from '@material-ui/core/FormControl';
import Checkbox from '@material-ui/core/Checkbox';
import MenuItem from '@material-ui/core/MenuItem';
import Select from '@material-ui/core/Select';
import constants from "../constants";

const UtilsDal = require('../dal/UtilsDal')
const CandidatesDal = require('../dal/CandidatesDal')
const PositionsDal = require('../dal/PositionsDal')



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
            updatedState.proposal._candidateId = props.CandidateID;
            updatedState.proposal._positionId = props.PositionID;            
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

        updatedState.proposal._candidateId = newCandId;
        this.setState(updatedState);
    }

    onPositionChanged(event)
    {
        let updatedState = this.state;        

        let newPosId = parseInt(event.target.value);

        updatedState.proposal._positionId = newPosId;

        this.setState(updatedState);
    }

    onStatusChanged(event)
    {  
        let updatedState = this.state;        

        let newStatusId = parseInt(event.target.value);

        updatedState.proposal._statusId = newStatusId;
        this.setState(updatedState);      
    }
  
    onCurrentStepChanged(event)
    {   
        let updatedState = this.state;        

        let newCurrStepId = parseInt(event.target.value);

        updatedState.proposal._currentStepId = newCurrStepId;
        this.setState(updatedState);       
    }

    onNextStepChanged(event)
    {  
        let updatedState = this.state;        

        let newNextStepId = parseInt(event.target.value);

        updatedState.proposal._nextStepId = newNextStepId;
        this.setState(updatedState);         
    }

    onDueDateChanged(date)
    {    
        let updatedState = this.state;
        updatedState.proposal._dueDate = date;
        this.setState(updatedState);
    }

    onProposeClicked()
    {
         this.state.onCompleted();
    }

    onCancelClicked()
    {
        this.state.onCompleted();
    }

    componentDidMount() {
        let obj = this;
        var dalPos = new PositionsDal();
        var dalUtils = new UtilsDal();
        var dalCandidates = new CandidatesDal();

        // populating positions
        dalPos.getPositions().then( function(ps) {
            let updatedState = obj.state;
            
            if(ps.status == constants.HTTP_OK) {
                updatedState.positions = ps.data;
                if(!updatedState.proposal._positionId && ps.data.length > 0) {
                    
                    updatedState.proposal._positionId = ps.data[0]._positionId;
                }
                obj.setState(updatedState);
                
            }
            
        });

        // populating positions
        dalCandidates.getCandidates().then( function(cs) {
            let updatedState = obj.state;
            
            if(cs.status == constants.HTTP_OK) {
                updatedState.candidates = cs.data;
                if(!updatedState.proposal._candidateId && cs.data.length > 0) {
                    
                    updatedState.proposal._candidateId = cs.data[0]._candidateId;
                }
                obj.setState(updatedState);
                
            }
            
        });

        
        // populating steps
        dalUtils.getPositionCandidateStepsAsTable().then( function(pcs) {
            let updatedState = obj.state;

            updatedState.steps = pcs;
            if(updatedState.proposal._currentStepId == 0 && Object.values(pcs).length > 0)
            {
                updatedState.proposal._currentStepId = Object.values(pcs)[0].StepID;               
            }
            if(updatedState.proposal._nextStepId == 0 && Object.values(pcs).length > 0)
            {
                updatedState.proposal._nextStepId = Object.values(pcs)[1].StepID;               
            }
            
            obj.setState(updatedState)
        } );

        
        // populating statuses
        dalUtils.getPositionCandidateStatusesAsTable().then( function(pcs) {
            let updatedState = obj.state;

            updatedState.statuses = pcs;
            if(!updatedState.proposal._statusId)
            {
                updatedState.proposal._statusId = Object.values(pcs)[0].StatusID;               
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
                <div key={"divEdit" + this.state.SkillID} style={styleEdit}>
                        <table>
                            <tbody>
                            <tr>
                                <td>Candidate</td>
                                <td>
                                <Select
                                    key="cbCandidate"                     
                                    value={this.state.proposal._candidateId ? this.state.proposal._candidateId : ""}
                                    onChange = { (event) => this.onCandidateChanged(event) }
                                   >   
                                        {
                                            this.state.candidates.map( (p) => (
                                                <option id={p._candidateId} key={p._candidateId} value={p._candidateId}>
                                                    {p._fname + " " + p._lname}
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
                                    value={this.state.proposal._positionId ? this.state.proposal._positionId : ""}
                                    onChange = { (event) => this.onPositionChanged(event) }
                                   >           
                                        {
                                            this.state.positions.map( (p) => (
                                                <option id={p._positionId} key={p._positionId} value={p._positionId}>
                                                    {p._title}
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
                                    { (this.state.proposal._proposed.getMonth() + 1) + "/" 
                                        + this.state.proposal._proposed.getDate() + "/" 
                                        + this.state.proposal._proposed.getFullYear() + " " 
                                        + this.state.proposal._proposed.getHours() + ":"
                                        + this.state.proposal._proposed.getMinutes() } 
                                </td>
                            </tr>
                            <tr>
                                <td>Status</td>
                                <td>
                                <Select                   
                                    key="cbStatus"                     
                                    value={this.state.proposal._statusId}
                                    onChange = { (event) => this.onStatusChanged(event) }
                                   >   
                                        {
                                            Object.values(this.state.statuses).map( (p) => (
                                                <option id={p.StatusID} key={p.StatusID} value={p.StatusID}>
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
                                    value={this.state.proposal._currentStepId}
                                    onChange = { (event) => this.onCurrentStepChanged(event) }
                                   >   
                                        {
                                            Object.values(this.state.steps).map( (p) => (
                                                <option id={p.StepID} key={p.StepID} value={p.StepID}>
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
                                        value={ this.state.proposal._dueDate }
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
                                    value={this.state.proposal._nextStepId}
                                    onChange = { (event) => this.onNextStepChanged(event) }
                                   >   
                                        {
                                            Object.values(this.state.steps).map( (p) => (
                                                <option id={p.StepID} key={p.StepID} value={p.StepID}>
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
                                        onClick={ () => this.onProposeClicked() }>Propose</Button>

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
        let proposal = {}

        var now = new Date();


        proposal._proposalId = null;
        proposal._positionId = null;
        proposal._candidateId = null;
        proposal._statusId = 0;
        proposal._currentStepId = 0;
        proposal._nextStepId = 0;
        proposal._proposed = now;
        proposal._dueDate = null;
        
        return proposal;
        
    }
}

export default Proposal;