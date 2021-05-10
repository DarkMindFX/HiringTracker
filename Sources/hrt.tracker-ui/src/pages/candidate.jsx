
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
}