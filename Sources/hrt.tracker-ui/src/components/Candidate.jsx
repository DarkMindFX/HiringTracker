


import React from 'react';
import { Link, withRouter  } from 'react-router-dom'
import { TextField } from '@material-ui/core';
import { Button } from '@material-ui/core';
import Alert from '@material-ui/lab/Alert';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import SkillsList from '../components/SkillsList';



const PageHelper = require("../helpers/PageHelper");
const CandidatesDal = require('../dal/CandidatesDal');
const CandidateSkillsDal = require('../dal/CandidateSkillsDal');
const SkillsDal = require('../dal/SkillsDal');
const SkillProficienciesDal = require('../dal/SkillProficienciesDal');
const UsersDal = require('../dal/UsersDal');
const { CandidateDto, CandidateSkillDto } = require('hrt.dto')

const constants = require('../constants');
const { v4: uuidv4 } = require('uuid');

class Candidate extends React.Component {

    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(props);

        this.state = { 
            operation: props.operation,
            id: props.id ? parseInt(props.id) : null,
            canEdit: props.operation ? (props.operation.toLowerCase() == 'new' || 
                                                          props.operation.toLowerCase() == 'edit' ? true : false) : false,
            candidate: this._createEmptyCandidateObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null
        };

        this.onFirstNameChanged = this.onFirstNameChanged.bind(this);
        this.onMiddleNameChanged = this.onMiddleNameChanged.bind(this);
        this.onLastNameChanged = this.onLastNameChanged.bind(this);
        this.onEmailChanged = this.onEmailChanged.bind(this);
        this.onPhoneChanged = this.onPhoneChanged.bind(this);
        this.onCVLinkChanged = this.onCVLinkChanged.bind(this);
        this.onSkillAdded = this.onSkillAdded.bind(this);
        this.onSkillChanged = this.onSkillChanged.bind(this);
        this.onSkillDeleted = this.onSkillDeleted.bind(this);

        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);

        this._getCandidate = this._getCandidate.bind(this);

        this._getUsers = this._getUsers.bind(this);

    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
                obj._getUsers().then( () => {
                    if(this.state.id) {                        
			            obj._getCandidate().then( () => {
                            obj._getSkills().then( () => {
                                obj._getSkillProficiencies().then( () => {
                                    obj._getCandidateSkills().then( () => {} );
                                })
                            })
                        } );
                    }
			});
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
    }

    onFirstNameChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.candidate.FirstName = newVal;

        this.setState(updatedState);
    }

    onMiddleNameChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.candidate.MiddleName = newVal;

        this.setState(updatedState);
    }

    onLastNameChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.candidate.LastName = newVal;

        this.setState(updatedState);
    }

    onEmailChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.candidate.Email = newVal;

        this.setState(updatedState);
    }

    onPhoneChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.candidate.Phone = newVal;

        this.setState(updatedState);
    }

    onCVLinkChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.candidate.CVLink = newVal;

        this.setState(updatedState);
    }

    onSkillChanged(updatedSkill) {

        let updatedState = this.state;

        let skill = updatedState.candidate.Skills.find( s => { return s.id == updatedSkill.id; } );

        if(skill != null) {
            skill.SkillID = updatedSkill.SkillID;
            skill.SkillProficiencyID = updatedSkill.SkillProficiencyID;
            skill.IsMandatory = updatedSkill.IsMandatory;

            this.setState(updatedState);
        }  
        
        console.log("onSkillChanged", this.state.candidate.Skills);
    }   

    onSkillAdded(newSkill) {
        let updatedState = this.state;
        let newSkillRec = new CandidateSkillDto();
         
        newSkillRec.id = newSkill.id;
        newSkillRec.CandidateID = this.state.id;
        newSkillRec.SkillID = newSkill.SkillID;
        newSkillRec.SkillProficiencyID = newSkill.SkillProficiencyID;
        newSkillRec.IsMandatory = newSkill.IsMandatory;
        
        updatedState.candidate.Skills.push(newSkillRec);

        this.setState(updatedState);

    }

    onSkillDeleted(id) {
        let updatedState = this.state;
        let idx = updatedState.candidate.Skills.findIndex( s => { return s.id == id; } );
        if(idx >= 0) {
            updatedState.candidate.Skills.splice(idx, 1);
            this.setState(updatedState);
        }
    }



    onSaveClicked() {

        console.log("Saving Candidate: ", this.state.candidate);
        
        const reqCandidate = new CandidateDto();
        reqCandidate.ID = this.state.id;
        reqCandidate.FirstName = this.state.candidate.FirstName;
        reqCandidate.MiddleName = this.state.candidate.MiddleName;
        reqCandidate.LastName = this.state.candidate.LastName;
        reqCandidate.Email = this.state.candidate.Email;
        reqCandidate.Phone = this.state.candidate.Phone;
        reqCandidate.CVLink = this.state.candidate.CVLink;
        reqCandidate.CreatedByID = this.state.candidate.CreatedByID;
        reqCandidate.CreatedDate = this.state.candidate.CreatedDate;
        reqCandidate.ModifiedByID = this.state.candidate.ModifiedByID;
        reqCandidate.ModifiedDate = this.state.candidate.ModifiedDate;

        console.log("Saving Candidate: ", reqCandidate); 
        
        let dalCandidates = new CandidatesDal();

        let obj = this;

        function upsertCandidateThen(response) {
            const updatedState = obj.state;

            if(response.status == constants.HTTP_OK || response.status == constants.HTTP_Created) {
                updatedState.showSuccess = true;
                updatedState.showError = false;
                if(response.status == constants.HTTP_Created) {
                    updatedState.id = response.data.ID;
                    updatedState.success = `Candidate was created. ID: ${updatedState.id}`;
                }
                else {
                    updatedState.success = `Candidate was updated`;                
                }

                // saving skills
                let reqCandidateSkills = obj.state.candidate.Skills.map( ps => { 
                    var dto = new CandidateSkillDto();
                    dto.SkillID = ps.SkillID;
                    dto.SkillProficiencyID = ps.SkillProficiencyID;
                    return dto;
                });

                let dalCandidateSkills = new CandidateSkillsDal();
                dalCandidateSkills.setCandidateSkills(updatedState.id, reqCandidateSkills)
                                .then( (res) => { upsertSkillsThen(res) } )
                                .catch( (res) => { upsertCatch(res) } );

                obj.setState(updatedState);
            }
            else {
                obj._showError(updatedState, response); 
                
                obj.setState(updatedState);
            }
        }  

        function upsertCatch(err) {
            const updatedState = obj.state;
            const errMsg = `Error: ${err}`
            updatedState.showSuccess = false;
            updatedState.showError = true;
            updatedState.error = errMsg; 
            obj.setState(updatedState);
        }

        function upsertSkillsThen(result) {
            const updatedState = obj.state;

            console.log("upsertSkillsThen", result);

            if(result.status == constants.HTTP_OK || result.status == constants.HTTP_Created) {
                updatedState.showSuccess = true;
                updatedState.showError = false;
            }
            else {
                updatedState.showSuccess = false;
                updatedState.showError = true;
                updatedState.error = result.data.Message;          
            }

            obj.setState(updatedState);
        }

        if(this.state.id != null) {
            dalCandidates.updateCandidate(reqCandidate)
                                    .then( (res) => { upsertCandidateThen(res); } )
                                    .catch( (err) => { upsertCatch(err); });
        }
        else {
            dalCandidates.insertCandidate(reqCandidate)
                                    .then( (res) => { upsertCandidateThen(res); } )
                                    .catch( (err) => { upsertCatch(err); });        
        }
        
    }

    onDeleteClicked() {
        const updatedState = this.state;
        updatedState.showDeleteConfirm = true;
        this.setState(updatedState);
    }

    onDeleteCancel() {
        const updatedState = this.state;
        updatedState.showDeleteConfirm = false;
        this.setState(updatedState);
    }

    onDeleteConfirm() {  
        
        let dalCandidates = new CandidatesDal();
        let obj = this;

        dalCandidates.deleteCandidate(this.state.id).then( (response) => {
            if(response.status == constants.HTTP_OK) {
                obj.props.history.push("/candidates");                
            }
            else {
                const updatedState = obj.state;
                updatedState.showDeleteConfirm = false;
                obj._showError(updatedState, response);                
                obj.setState(updatedState);               
            }
        })
    }

    render() {

        let skills = this._getCandidateSkillsAsList();

        const styleError = {
            display: this.state.showError ? "block" : "none"
        }

        const styleSuccess = {
            display: this.state.showSuccess ? "block" : "none"
        }   
        
        const styleDeleteBtn = {
            display: this.state.id ? "block" : "none"
        }

        const lstCreatedByIDsFields = ["FirstName", "LastName"];
        const lstCreatedByIDs = this._prepareOptionsList( this.state.users 
                                                                    ? Object.values(this.state.users) : null, 
                                                                    lstCreatedByIDsFields,
                                                                    false );
        const lstModifiedByIDsFields = ["FirstName", "LastName"];
        const lstModifiedByIDs = this._prepareOptionsList( this.state.users 
                                                                    ? Object.values(this.state.users) : null, 
                                                                    lstModifiedByIDsFields,
                                                                    true );
        return (
            <div>
                 <table>
                    <tbody>
                        <tr>
                            <td style={{width: 450}}></td>
                            <td>
                                <Button variant="contained" color="primary"
                                        onClick={ () => this.onSaveClicked() }>Save</Button>

                                <Button variant="contained" color="secondary"
                                        style={styleDeleteBtn}
                                        onClick={ () => this.onDeleteClicked() }>Delete</Button>

                                <Button variant="contained" component={Link} to="/candidates">Cancel</Button>
                            </td>
                        </tr>
                        <tr>
                            <td colSpan={2}>
                                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                                <Alert severity="success" style={styleSuccess}>Success! {this.state.success}</Alert>                                
                            </td>
                        </tr> 
    
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="FirstName" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="FirstName" 
                                            value={this.state.candidate.FirstName}
                                            onChange={ (event) => { this.onFirstNameChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="MiddleName" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="MiddleName" 
                                            value={this.state.candidate.MiddleName}
                                            onChange={ (event) => { this.onMiddleNameChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="LastName" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="LastName" 
                                            value={this.state.candidate.LastName}
                                            onChange={ (event) => { this.onLastNameChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="Email" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Email" 
                                            value={this.state.candidate.Email}
                                            onChange={ (event) => { this.onEmailChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="Phone" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Phone" 
                                            value={this.state.candidate.Phone}
                                            onChange={ (event) => { this.onPhoneChanged(event) } }
                                            />
                                
                            </td>
                        </tr>    
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="CVLink" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="CVLink" 
                                            value={this.state.candidate.CVLink}
                                            onChange={ (event) => { this.onCVLinkChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
   
                        
                        <tr>
                            <td colSpan={4}>
                                
                                <SkillsList id="candidateSkills"
                                    skills={ skills }
                                    canEdit={ this.state.canEdit }
                                    showMustHave = { true }
                                    dictSkills = { this.state.skills }
                                    dictProficiencies = { this.state.skillProficiences }
                                    onSkillAdded = { this.onSkillAdded }
                                    onSkillChanged = { this.onSkillChanged }
                                    onSkillDeleted = { this.onSkillDeleted }
                                />
                                
                            </td>
                        </tr>

                    </tbody>
                </table>

                <Dialog open={this.state.showDeleteConfirm} onClose={() => { this.onDeleteCancel() }} aria-labelledby="form-dialog-title">
                    <DialogTitle id="form-dialog-title">Delete Candidate</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this Candidate?
                    </DialogContentText>                    
                    </DialogContent>
                    <DialogActions>
                    <Button onClick={() => { this.onDeleteCancel() }} color="primary">
                        Cancel
                    </Button>
                    <Button onClick={() => { this.onDeleteConfirm() }} color="primary">
                        Delete
                    </Button>
                    </DialogActions>
                </Dialog>
            </div>

        );
    }

    _createEmptyCandidateObj() {
        let candidate = new CandidateDto();

        return candidate;
    }

    async _getCandidate()
    {
        if(this.state.id) {
            let updatedState = this.state;
                  
            let dalCandidates = new CandidatesDal();
            let response = await dalCandidates.getCandidate(this.state.id);

            if(response.status == constants.HTTP_OK)
            {
                updatedState.candidate = response.data;                
            }
            else if(response.status == constants.HTTP_Unauthorized)
            {
                this._redirectToLogin();
            }
            else 
            {
                this._showError(updatedState, response);
            }
        
            this.setState(updatedState);    
        }
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

    
    async _getCandidateSkills()
    {
        let updatedState = this.state;

        if(updatedState.candidate)
        {
                    
            let dalCandidateSkills = new CandidateSkillsDal();
            let response = await dalCandidateSkills.getCandidateSkillsByCandidate(this.state.id);

            if(response.status == constants.HTTP_OK)
            {
                const skills = response.data;                     
                updatedState.candidate.Skills = skills.map(s => { s.id = uuidv4(); return s; });                
            }
            else if(response.status == constants.HTTP_Unauthorized)
            {
                this._redirectToLogin();
            }
            else 
            {
                this._showError(updatedState, response);
            }
        }        
        this.setState(updatedState);        
    }

    async _getSkills()
    {
        let updatedState = this.state;
        if(updatedState.skills == null)
        {            
            let dalSkills = new SkillsDal();
            let response = await dalSkills.getSkills();

            if(response.status == constants.HTTP_OK)
            {
                updatedState.skills = {};

                for(let s in response.data)
                {
                    updatedState.skills[ response.data[s].ID ] = response.data[s];
                }
            }
            else if(response.status == constants.HTTP_Unauthorized)
            {
                this._redirectToLogin();
            }
            else 
            {
                this._showError(updatedState, response);
            }
        }
        this.setState(updatedState);
    }

    async _getSkillProficiencies()
    {
        let updatedState = this.state;
        if(updatedState.skillProficiences == null)
        {            
            let dalSkillProficiencies = new SkillProficienciesDal();
            let response = await dalSkillProficiencies.getSkillProficiencies();

            if(response.status == constants.HTTP_OK)
            {
                updatedState.skillProficiences = {};

                for(let s in response.data)
                {
                    updatedState.skillProficiences[ response.data[s].ID ] = response.data[s];
                }
            }
            else if(response.status == constants.HTTP_Unauthorized)
            {
                this._redirectToLogin();
            }
            else 
            {
                this._showError(updatedState, response);
            }
        }
        this.setState(updatedState);
    }

    _getCandidateSkillsAsList() {
        let skills = [];

        if(this.state.candidate.Skills ) {    

            let obj = this;
            
            this.state.candidate.Skills.forEach( s => {

                let skill = {
                    id: s.id,
                    SkillID: s.SkillID, 
                    SkillProficiencyID: s.SkillProficiencyID, 
                    IsMandatory: s.IsMandatory
                };
                skills.push(skill);
            })
        }  
      
        return skills;
    }


    _showError(updatedState, response) {
        var error = JSON.parse(response.data.response);
        updatedState.showError = true;
        updatedState.error = error.Message;
    }

    _redirectToLogin()
    {
        this._pageHelper.redirectToLogin(`/candidate/${this.state.operation}` + (this.state.id ? `/${this.state.id}` : ``));        
    }

    _prepareOptionsList(objs, fields, hasEmptyVal) 
    {
        var lst = [];
        
        if(hasEmptyVal) {
            lst.push( <option key='-1' value='-1'>[Empty]</option> );
        }

        if(objs) {
            
            lst.push(
                objs.map( (i) => {
                    let optionText = "";
                    for(let f in fields) {
                        optionText += i[fields[f]] + (f + 1 < fields.length ? " " : "");
                    }

                    return(
                        <option key={i.ID} value={i.ID}>
                            { optionText }
                        </option>
                    )
                })
            )
        }

        return lst;
    }
}

export default Candidate;