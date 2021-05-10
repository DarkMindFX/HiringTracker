
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

const CandidatesDal = require("../dal/CadidatesDal")

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

    onFirstNameChanged(event) {

    }

    onMiddleNameChanged(event) {

    }

    onLastNameChanged(event) {

    }

    onPhoneChanged(event) {

    }

    onEmailChanged(event) {

    }

    onCVLinkChanged(event) {

    }

    onSkillChanged(updatedSkill) {

        let updatedState = this.state;

        let skill = updatedState.position._skills.find( s => { return s.id == updatedSkill.id; } );

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
        let idx = updatedState.position._skills.findIndex( s => { return s.id == id; } );
        if(idx >= 0) {
            updatedState.candidate._skills.splice(idx, 1);
            this.setState(updatedState);
        }
    }

    onSaveClicked() {

    }

    onDeleteClicked() {

    }

    onDeleteCancel() {

    }

    onDeleteConfirm() {

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
                                            value={this.state.position._fname}
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
                                            value={this.state.position._mname}
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
                                            value={this.state.position._lname}
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
                                            value={this.state.position._email}
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
                                            value={this.state.position._phone}
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
                                            value={this.state.position._cvlink}
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

    _getPositionSkills() {
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