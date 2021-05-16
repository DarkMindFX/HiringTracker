
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
import constants from "../constants";
const { v4: uuidv4 } = require('uuid');

const CandidatesDal = require("../dal/CandidatesDal")
const { CandidateDto, CandidateUpsertDto, CandidateSkillDto, SkillDto, SkillProficiencyDto } = require('hrt.dto')

class CandidatePage extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            operation: this.props.match.params.operation,

            id: this.props.match.params.id ? parseInt(this.props.match.params.id) : null,
            canEdit: this.props.match.params.operation ? (this.props.match.params.operation.toLowerCase() == 'new' || 
                                                          this.props.match.params.operation.toLowerCase() == 'edit' ? true : false) : false,
            candidate: this._createEmptyCandidateObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null
        }

        this.onSkillAdded = this.onSkillAdded.bind(this);
        this.onSkillChanged = this.onSkillChanged.bind(this);
        this.onSkillDeleted = this.onSkillDeleted.bind(this);
        this.onFirstNameChanged = this.onFirstNameChanged.bind(this);
        this.onMiddleNameChanged = this.onMiddleNameChanged.bind(this);
        this.onLastNameChanged = this.onLastNameChanged.bind(this);
        this.onEmailChanged = this.onEmailChanged.bind(this);
        this.onPhoneChanged = this.onPhoneChanged.bind(this);
        this.onCVLinkChanged = this.onCVLinkChanged.bind(this);
    }

    componentDidMount(event) {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {

            if(this.state.id) {
                let dalCand = new CandidatesDal();
                let obj = this;

                dalCand.getCandidate(this.state.id).then( (resCand) => {
                    let updatedState = obj.state;

                    if(resCand.status == constants.HTTP_OK) {
                        updatedState.candidate = resCand.data;

                        updatedState.showError = false;
                        updatedState.error = null;

                        obj.setState(updatedState); 

                        dalCand.getCandidateSkills(obj.state.id).then( (resSkills) => {
                            let updatedState = obj.state;

                            if(resSkills.status == constants.HTTP_OK) {   
                                const skills = resSkills.data;                     
                                updatedState.candidate._skills = skills.map(s => { s.id = uuidv4(); return s; });
                                updatedState.showError = false;
                                updatedState.error = null;
                                
                            } 
                            else {
                                updatedState.showError = true;
                                updatedState.error = resCand.data._message;
                            }

                            obj.setState(updatedState);
                        });
                    }
                    else if(resCand.status == constants.HTTP_Unauthorized) {
                        obj.props.history.push("/login?ret=/positions");
                    }
                    else {
                        updatedState.showError = true;
                        updatedState.error = resCand.data._message;                    
                    }
                });
            }
        }
        else {
            console.log('No token - need to login')
            this.props.history.push(`/login?ret=/candidate/${this.state.operation}` + (this.state.id ? `/${this.state.id}` : ``))
        }
    }

    onFirstNameChanged(event) {
        const newFName = event.target.value;
        let updatedState = this.state;
        updatedState.candidate._fname = newFName;

        this.setState(updatedState);
    }

    onMiddleNameChanged(event) {
        const newMName = event.target.value;
        let updatedState = this.state;
        updatedState.candidate._mname = newMName;

        this.setState(updatedState);
    }

    onLastNameChanged(event) {
        const newLName = event.target.value;
        let updatedState = this.state;
        updatedState.candidate._lname = newLName;

        this.setState(updatedState);
    }

    onPhoneChanged(event) {
        const newPhone = event.target.value;
        let updatedState = this.state;
        updatedState.candidate._phone = newPhone;

        this.setState(updatedState);
    }

    onEmailChanged(event) {
        const newEmail = event.target.value;
        let updatedState = this.state;
        updatedState.candidate._email = newEmail;

        this.setState(updatedState);
    }

    onCVLinkChanged(event) {
        const newCVLink = event.target.value;
        let updatedState = this.state;
        updatedState.candidate._cvlink = newCVLink;

        this.setState(updatedState);
    }

    onSkillChanged(updatedSkill) {

        let updatedState = this.state;

        let skill = updatedState.candidate._skills.find( s => { return s.id == updatedSkill.id; } );

        if(skill != null) {
            skill._skill._skillId = updatedSkill.SkillID;
            skill._skill._name = "";
            skill._proficiency._id = updatedSkill.ProficiencyID;
            skill._proficiency._name = "";

            this.setState(updatedState);
        }        
    }   

    onSkillAdded(newSkill) {
        let updatedState = this.state;
        let newSkillRec = {
            id: newSkill.id,
            _skill: {
                _skillId: newSkill.SkillID
            },
            _proficiency: {
                _id: newSkill.ProficiencyID
            }
        }
        updatedState.candidate._skills.push(newSkillRec)

        this.setState(updatedState);

    }

    onSkillDeleted(id) {
        let updatedState = this.state;
        let idx = updatedState.candidate._skills.findIndex( s => { return s.id == id; } );
        if(idx >= 0) {
            updatedState.candidate._skills.splice(idx, 1);
            this.setState(updatedState);
        }
    }

    onSaveClicked() {
        const req = new CandidateUpsertDto();
        req.Candidate = new CandidateDto();
        req.Candidate.CandidateID = this.state.id;
        req.Candidate.FirstName = this.state.candidate._fname;
        req.Candidate.MiddleName = this.state.candidate._mname;
        req.Candidate.LastName = this.state.candidate._lname;
        req.Candidate.Email = this.state.candidate._email;
        req.Candidate.Phone = this.state.candidate._phone;
        req.Candidate.CVLink = this.state.candidate._cvlink;

        req.Skills = [];

        this.state.candidate._skills.forEach( s => {

            const sp = new CandidateSkillDto();
            sp.Skill = new SkillDto();
            sp.Skill.SkillID = s._skill._skillId;
            sp.Proficiency = new SkillProficiencyDto();
            sp.Proficiency.ProficiencyID = s._proficiency._id
            sp.IsMandatory = s._isMandatory;

            req.Skills.push(sp);

        });

        let dalCand = new CandidatesDal();

        let obj = this;

        function upsertThen(result) {
            const updatedState = obj.state;

            if(result.status == constants.HTTP_OK || result.status == constants.HTTP_Created) {
                updatedState.showSuccess = true;
                updatedState.showError = false;
                if(result.status == constants.HTTP_Created) {
                    updatedState.id = parseInt(result._candidateId);
                    updatedState.success = `Candidate was created. Candidate ID: ${updatedState.id}`;
                }
                else {
                    updatedState.success = `Candidate was updated`;                
                }
            }
            else {
                updatedState.showSuccess = false;
                updatedState.showError = true;
                updatedState.error = result.data._message;          
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

        console.log("Upserting: ", req);

        if(this.state.id != null) {
            dalCand.updateCandidate(req)
                                    .then( (res) => { upsertThen(res); } )
                                    .catch( (err) => { upsertCatch(err); });
        }
        else {
            dalCand.addCandidate(req)
                                    .then( (res) => { upsertThen(res); } )
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
                obj.props.history.push("/login?ret=/candidates");                
            }
            else {
                const updatedState = obj.state;
                updatedState.showSuccess = false;
                updatedState.showError = true;
                updatedState.error = res.data._message; 
                updatedState.showDeleteConfirm = false;
                obj.setState(updatedState);               
            }
        });
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
                                            value={this.state.candidate._fname}
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
                                            value={this.state.candidate._mname}
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
                                            value={this.state.candidate._lname}
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
                                            value={this.state.candidate._email}
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
                                            value={this.state.candidate._phone}
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
                                            value={this.state.candidate._cvlink}
                                            onChange={ (event) => { this.onCVLinkChanged(event) } }
                                            />
                            </td>
                        </tr>
                        
                        <tr>
                            <td colSpan={4}>
                                <SkillsList id="candidateSkills"
                                    skills={ skills }
                                    canEdit={ this.state.canEdit }
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
        let cand = {
            _fname: "",
            _lname: "",
            _mname: "",
            _phone: "",
            _email: "",
            _cvlink: "",
            _skills: []
        }

        return cand;
    }

    _getCandidateSkills() {
        let skills = [];

        if( this.state.candidate._skills ) {
            
            this.state.candidate._skills.forEach( s => {
                let skill = {
                    id: s.id,
                    SkillID: s._skill._skillId, 
                    ProficiencyID: s._proficiency._id
                };
                skills.push(skill);
            })
        }  
      
        return skills;
    }
}

export default withRouter(CandidatePage);