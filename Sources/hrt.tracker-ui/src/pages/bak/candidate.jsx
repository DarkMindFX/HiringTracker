
import React from "react"
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
import Proposal from "../components/Proposal";
import { HTTP_OK, HTTP_Unauthorized } from "../constants";

const PageHelper = require("../helpers/PageHelper"); 
const CandidatesDal = require("../dal/CandidatesDal")
const ProposalsDal = require("../dal/ProposalsDal")
const CandidateSkillsDal = require("../dal/CandidateSkillsDal")
const SkillsDal = require('../dal/SkillsDal');
const SkillProficienciesDal = require('../dal/SkillProficienciesDal');
const { CandidateDto, CandidateSkillDto, SkillDto, SkillProficiencyDto } = require('hrt.dto')

const constants = require('../constants');
const { v4: uuidv4 } = require('uuid');

class CandidatePage extends React.Component {

    _skills = null;
    _proficiences = null;
    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);

        this.state = {
            operation: this.props.operation,

            id: this.props.id ? parseInt(this.props.id) : null,
            canEdit: this.props.operation ? (this.props.operation.toLowerCase() == 'new' || 
                                                          this.props.operation.toLowerCase() == 'edit' ? true : false) : false,
            candidate: this._createEmptyCandidateObj(),

            showDeleteConfirm: false,
            showProposalDialog: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null
        }

        
        this.onFirstNameChanged = this.onFirstNameChanged.bind(this);
        this.onMiddleNameChanged = this.onMiddleNameChanged.bind(this);
        this.onLastNameChanged = this.onLastNameChanged.bind(this);
        this.onEmailChanged = this.onEmailChanged.bind(this);
        this.onPhoneChanged = this.onPhoneChanged.bind(this);
        this.onCVLinkChanged = this.onCVLinkChanged.bind(this);
        
        this.onProposeClicked = this.onProposeClicked.bind(this);
        this.onProposeCompleted = this.onProposeCompleted.bind(this);
        this.onSkillAdded = this.onSkillAdded.bind(this);
        this.onSkillChanged = this.onSkillChanged.bind(this);
        this.onSkillDeleted = this.onSkillDeleted.bind(this);
    }

    componentDidMount(event) {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);

        if(token != null) {

            let obj = this;

            obj._getSkills().then( () => {

                obj._getSkillProficiencies().then( () => {

                    if(this.state.id) {
                        let dalCand = new CandidatesDal();
                        let dalCandSkills = new CandidateSkillsDal();
                        let obj = this;

                        dalCand.getCandidate(this.state.id).then( (resCand) => {
                            let updatedState = obj.state;

                            if(resCand.status == constants.HTTP_OK) {
                                updatedState.candidate = resCand.data;

                                updatedState.showError = false;
                                updatedState.error = null;

                                obj.setState(updatedState); 

                                dalCandSkills.getCandidateSkillsByCandidate(obj.state.id).then( (resSkills) => {
                                    let updatedState = obj.state;

                                    if(resSkills.status == constants.HTTP_OK) {   
                                        const skills = resSkills.data;                     
                                        updatedState.candidate.Skills = skills.map(s => { s.id = uuidv4(); return s; });
                                        updatedState.showError = false;
                                        updatedState.error = null;
                                        
                                    } 
                                    else {
                                        var error = JSON.parse(resSkills.data.response);
                                        updatedState.showError = true;
                                        updatedState.error = error.Message;
                                    }

                                    obj.setState(updatedState);
                                }).catch( (err) => {
                                    console.log('Error when getting candidate skills:', err);
                                });
                            }
                            else if(resCand.status == constants.HTTP_Unauthorized) {
                                this._redirectToLogin();
                            }
                            else {
                                var error = JSON.parse(resCand.data.response);
                                updatedState.showError = true;
                                updatedState.error = error.Message;                     
                            }
                        });
                    }

                });
            });
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();
        }
    }

    onProposeClicked()
    {
        let updatedState = this.state;
        updatedState.showProposalDialog = true;
        this.setState(updatedState);
    }

    onProposeCompleted(proposal)
    {
        if(proposal)
        {
            let updatedState = this.state;
            updatedState.showProposalDialog = false;

            var dalProposals = new ProposalsDal();

            if(proposal.ID == null)
            {
                dalProposals.insertProposal( proposal ).then( (resp) => {

                    if(resp.status == constants.HTTP_Created)
                    {
                        updatedState.showSuccess = true;
                        updatedState.success = `Candidate proposed to the position: Proposal ID ${resp.data.ID}`;                                     
                    }
                    else
                    {
                        var error = JSON.parse(resp.data.response);
                        updatedState.showError = true;
                        updatedState.error = error.Message;                     
                    }

                    this.setState(updatedState);
                });
            }
            else
            {
                dalProposals.updateProposal( proposal ).then( (resp) => {

                    if(resp.status == constants.HTTP_OK)
                    {
                        updatedState.showSuccess = true;
                        updatedState.success = `Candidate proposal updated: Proposal ID ${resp.data.ID}`;                                     
                    }
                    else
                    {
                        var error = JSON.parse(resp.data.response);
                        updatedState.showError = true;
                        updatedState.error = error.Message;                     
                    }

                    this.setState(updatedState);
                });                
            }
        }   
    }

    onFirstNameChanged(event) {
        const newFName = event.target.value;
        let updatedState = this.state;
        updatedState.candidate.FirstName = newFName;

        this.setState(updatedState);
    }

    onMiddleNameChanged(event) {
        const newMName = event.target.value;
        let updatedState = this.state;
        updatedState.candidate.MiddleName = newMName;

        this.setState(updatedState);
    }

    onLastNameChanged(event) {
        const newLName = event.target.value;
        let updatedState = this.state;
        updatedState.candidate.LastName = newLName;

        this.setState(updatedState);
    }

    onPhoneChanged(event) {
        const newPhone = event.target.value;
        let updatedState = this.state;
        updatedState.candidate.Phone = newPhone;

        this.setState(updatedState);
    }

    onEmailChanged(event) {
        const newEmail = event.target.value;
        let updatedState = this.state;
        updatedState.candidate.Email = newEmail; 

        this.setState(updatedState);
    }

    onCVLinkChanged(event) {
        const newCVLink = event.target.value;
        let updatedState = this.state;
        updatedState.candidate.CVLink = newCVLink;

        this.setState(updatedState);
    }

    onSkillChanged(updatedSkill) {

        let updatedState = this.state;

        let skill = updatedState.candidate.Skills.find( s => { return s.id == updatedSkill.id; } );

        if(skill != null) {
            skill.SkillID = updatedSkill.SkillID;
            skill.SkillProficiencyID = updatedSkill.SkillProficiencyID;

            this.setState(updatedState);
        }   
    }   

    onSkillAdded(newSkill) {
        let updatedState = this.state;
        let newSkillRec = new CandidateSkillDto();
         
        newSkillRec.id = newSkill.id;
        newSkillRec.CandidateID = this.id;
        newSkillRec.SkillID = newSkill.SkillID;
        newSkillRec.SkillProficiencyID = newSkill.SkillProficiencyID;
        
        updatedState.candidate.Skills.push(newSkillRec)

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

        const reqCandidate = new CandidateDto();
        reqCandidate.ID = this.state.id;
        reqCandidate.FirstName = this.state.candidate.FirstName;
        reqCandidate.MiddleName = this.state.candidate.MiddleName;
        reqCandidate.LastName = this.state.candidate.LastName;
        reqCandidate.Email = this.state.candidate.Email;
        reqCandidate.Phone = this.state.candidate.Phone;
        reqCandidate.CVLink = this.state.candidate.CVLink;

        let reqCandidateSkills = this.state.candidate.Skills.map( ps => { 
            var dto = new CandidateSkillDto();
            dto.SkillID = ps.SkillID;
            dto.SkillProficiencyID = ps.SkillProficiencyID;
            return dto;
        });

        console.log("Saving Candidate: ", reqCandidate);   
        console.log("Saving Skills: ", reqCandidateSkills); 

        let dalCand = new CandidatesDal();

        let obj = this;

        function upsertCandidateThen(result) {
            const updatedState = obj.state;

            if(result.status == constants.HTTP_OK || result.status == constants.HTTP_Created) {
                updatedState.showSuccess = true;
                updatedState.showError = false;
                if(result.status == constants.HTTP_Created) {
                    updatedState.id = result.data.ID;
                    updatedState.success = `Candidate was created. ID: ${updatedState.id}`;
                }
                else {
                    updatedState.success = `Candidate was updated`;                
                }

                obj.setState(updatedState);
            
                let dalCandidateSkills = new CandidateSkillsDal();
                dalCandidateSkills.setCandidateSkills(obj.state.id ? obj.state.id : result.data.ID, reqCandidateSkills)
                                .then( (res) => { upsertSkillsThen(res) } )
                                .catch( (res) => { upsertCatch(res) } );
            }
            else {
                updatedState.showSuccess = false;
                updatedState.showError = true;
                updatedState.error = result.data.Message;  
                
                obj.setState(updatedState);
            }

            
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

        function upsertCatch(err) {
            const updatedState = obj.state;
            const errMsg = `Error: ${err}`
            updatedState.showSuccess = false;
            updatedState.showError = true;
            updatedState.error = errMsg; 
            obj.setState(updatedState);
        }

        if(this.state.id != null) {
            dalCand.updateCandidate(reqCandidate)
                                    .then( (res) => { upsertCandidateThen(res); } )
                                    .catch( (err) => { upsertCatch(err); });
        }
        else {
            dalCand.insertCandidate(reqCandidate)
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
        let dalCand = new CandidatesDal();
        let obj = this;

        dalCand.deleteCandidate(this.state.id).then( (res) => {
            if(res.status == constants.HTTP_OK) {
                obj.props.history.push("/candidates");                
            }
            else {
                const updatedState = obj.state;
                updatedState.showSuccess = false;
                updatedState.showError = true;
                updatedState.error = res.data.Message; 
                updatedState.showDeleteConfirm = false;
                obj.setState(updatedState);               
            }
        });
    }

    onProposeCancel()
    {
        const updatedState = this.state;
        updatedState.showProposalDialog = false;
        this.setState(updatedState);        
    }

    

    render() {
        let skills = this._getCandidateSkills();

        const styleError = {
            display: this.state.showError ? "block" : "none"
        }

        const styleSuccess = {
            display: this.state.showSuccess ? "block" : "none"
        }   
        
        const styleDeleteBtn = {
            display: this.state.id ? "block" : "none"
        }

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

                                <Button variant="contained" 
                                        onClick={ () => this.onProposeClicked() }>Propose</Button>

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
                                
                                <TextField  id="firstName" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="First Name" 
                                            value={this.state.candidate.FirstName}
                                            onChange={ (event) => { this.onFirstNameChanged(event) } }
                                            />
                                
                            </td>
                        </tr>                    
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="middleName" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Middle Name" 
                                            value={this.state.candidate.MiddleName}
                                            onChange={ (event) => { this.onMiddleNameChanged(event) } }
                                            />
                            </td>
                        </tr>
                        <tr>
                            <td colSpan={1}>
                                <TextField  id="lastName" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Last Name" 
                                            value={this.state.candidate.LastName}
                                            onChange={ (event) => { this.onLastNameChanged(event) } }
                                            />
                            </td>
                        </tr>
                        <tr>
                            <td colSpan={1}>
                                <TextField  id="email" 
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
                            <td colSpan={1}>
                                <TextField  id="phone" 
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
                            <td colSpan={1}>
                                <TextField  id="cvlink" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Link to CV" 
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
                                    showMustHave = { false }
                                    dictSkills = { this._skills }
                                    dictProficiencies = { this._proficiences }
                                    onSkillAdded = { this.onSkillAdded }
                                    onSkillChanged = { this.onSkillChanged }
                                    onSkillDeleted = { this.onSkillDeleted }
                                 />
                            </td>
                        </tr>
                    </tbody>
                </table>

                <Dialog open={this.state.showProposalDialog} onClose={() => { this.onDeleteCancel() }} aria-labelledby="form-dialog-title">
                    <DialogTitle id="form-dialog-title">Position / Candidate Assignment</DialogTitle>
                    <DialogContent>
                        <DialogContentText>
                            Choose position to which candidate should be proposed
                        </DialogContentText>  
                        <Proposal 
                            CandidateID={ this.state.id }
                            canEdit={ this.state.canEdit } 
                            onCompleted = { this.onProposeCompleted }
                            />                  
                    </DialogContent>
                </Dialog>

                <Dialog open={this.state.showDeleteConfirm} onClose={() => { this.onProposeCancel() }} aria-labelledby="form-dialog-title">
                    <DialogTitle id="form-dialog-title">Delete Candidate</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this candidate?
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
        )
    }

    _createEmptyCandidateObj() {
        let cand = new CandidateDto();
        cand.Skills = [];

        return cand;
    }

    async _getSkills()
    {
        if(this._skills == null)
        {            
            let dalSkills = new SkillsDal();
            let resp = await dalSkills.getSkills();

            if(resp.status == HTTP_OK)
            {
                this._skills = {};

                for(let s in resp.data)
                {
                    this._skills[ resp.data[s].ID ] = resp.data[s];
                }
            }
            else if(resp.status == HTTP_Unauthorized)
            {
                this._redirectToLogin();
            }
        }
    }

    async _getSkillProficiencies()
    {
        if(this._proficiences == null)
        {            
            let dalSkillProficiences = new SkillProficienciesDal();
            let resp = await dalSkillProficiences.getSkillProficiencies();
               
            if(resp.status == HTTP_OK)
            {
                this._proficiences = {};

                for(let s in resp.data)
                {
                    this._proficiences[ resp.data[s].ID ] = resp.data[s];
                }
            }
            else if(resp.status == HTTP_Unauthorized)
            {
                this._redirectToLogin();
            }
        }
    }

    _getCandidateSkills() {
        let skills = [];

        if(this.state.candidate.Skills ) {    

            let obj = this;
            
            this.state.candidate.Skills.forEach( s => {

                let skill = {
                    id: s.id,
                    SkillID: s.SkillID, 
                    SkillProficiencyID: s.SkillProficiencyID, 
                };
                skills.push(skill);
            })
        }  
      
        return skills;
    }

    _redirectToLogin()
    {
        this._pageHelper.redirectToLogin(`/candidate/${this.state.operation}` + (this.state.id ? `/${this.state.id}` : ``))
    }
}

export default withRouter(CandidatePage);