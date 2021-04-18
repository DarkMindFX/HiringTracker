import React from 'react';
import { Link, withRouter  } from 'react-router-dom'
import { TextField } from '@material-ui/core';
import { Button } from '@material-ui/core';
import Alert from '@material-ui/lab/Alert';
import SkillsList from '../components/SkillsList';

const PositionsDal = require('../dal/PositionsDal');
const { PositionDto, PositionUpsertDto, PositionStatusDto, PositionSkillDto, SkillDto, SkillProficiencyDto } = require('hrt.dto')

const constants = require('../constants');
const { v4: uuidv4 } = require('uuid');

class PositionPage extends React.Component {

    constructor(props) {
        super(props);

        this.state = { 
            operation: this.props.match.params.operation,
            id: this.props.match.params.id ? parseInt(this.props.match.params.id) : null,
            canEdit: this.props.match.params.operation ? (this.props.match.params.operation.toLowerCase() == 'new' || 
                                                          this.props.match.params.operation.toLowerCase() == 'edit' ? true : false) : false,
            position: this._createEmptyPositionObj(),

            showError: false,
            showSuccess: false,
            error: null,
            success: null
        };

        this.onSkillAdded = this.onSkillAdded.bind(this);
        this.onSkillChanged = this.onSkillChanged.bind(this);
        this.onSkillDeleted = this.onSkillDeleted.bind(this);
        this.onStatusChanged = this.onStatusChanged.bind(this);
        this.onTitleChanged = this.onTitleChanged.bind(this);
        this.onShortDescChanged = this.onShortDescChanged.bind(this);
        this.onDescChanged = this.onDescChanged.bind(this);
        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
    }

    onStatusChanged(event) {

        let updatedState = this.state;        

        let newStatusId = parseInt(event.target.value);
       
        updatedState.position._status._statusId = newStatusId;
        updatedState.position._status._name = constants.POSITION_STATUSES.find( s => { return s.statusID == newStatusId } ).name;

        console.log(updatedState);

        this.setState(updatedState);
    }    

    componentDidMount() {

        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {

            if(this.state.id) {
                let dalPos = new PositionsDal();
                let obj = this;

                dalPos.getPosition(this.state.id).then( (resPos) => {
                    let updatedState = obj.state;

                    if(resPos.status == constants.HTTP_OK) {

                        updatedState.position = resPos.data;
                        updatedState.showError = false;
                        updatedState.error = null;

                        obj.setState(updatedState); 

                        dalPos.getPositionSkills(obj.state.id).then( (resSkills) => {

                            let updatedState = obj.state;

                            if(resSkills.status == constants.HTTP_OK) {   
                                const skills = resSkills.data;                     
                                updatedState.position._skills = skills.map(s => { s.id = uuidv4(); return s; });
                                updatedState.showError = false;
                                updatedState.error = null;
                                
                            } 
                            else {
                                updatedState.showError = true;
                                updatedState.error = resPos.data._message;
                            }

                            obj.setState(updatedState);

                        }).catch( (err) => {
                            console.log('Error when getting position skills:', err);
                        })
                    }
                    else if(resPos.status == constants.HTTP_Unauthorized) {
                        obj.props.history.push("/login?ret=/positions");
                    }
                    else {
                        updatedState.showError = true;
                        updatedState.error = resPos.data._message;                    
                    }

                    obj.setState(updatedState);

                }).catch( (err) => {
                    console.log('Error when getting position:', err);
                })
            }
        }
        else {
            console.log('No token - need to login')
            this.props.history.push(`/login?ret=/position/${this.state.operation}` + (this.state.id ? `/${this.state.id}` : ``))
        }
    }

    onSkillChanged(updatedSkill) {

        let updatedState = this.state;

        let skill = updatedState.position._skills.find( s => { return s.id == updatedSkill.id; } );

        if(skill != null) {
            skill._skill._skillId = updatedSkill.SkillID;
            skill._skill._name = "";
            skill._proficiency._id = updatedSkill.ProficiencyID;
            skill._proficiency._name = "";
            skill._isMandatory = updatedSkill.IsMandatory;

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
            },
            _isMandatory: newSkill.IsMandatory
        }
        updatedState.position._skills.push(newSkillRec)

        this.setState(updatedState);

    }

    onSkillDeleted(id) {
        let updatedState = this.state;
        let idx = updatedState.position._skills.findIndex( s => { return s.id == id; } );
        if(idx >= 0) {
            updatedState.position._skills.splice(idx, 1);
            this.setState(updatedState);
        }
    }

    onTitleChanged(event) {
        const newTitle = event.target.value;

        let updatedState = this.state;
        updatedState.position._title = newTitle;
        this.setState(updatedState);
    }

    onShortDescChanged(event) {
        const newShortDesc = event.target.value;

        let updatedState = this.state;
        updatedState.position._shortDesc = newShortDesc;
        this.setState(updatedState);
    }

    onDescChanged(event) {
        const newDesc = event.target.value;

        let updatedState = this.state;
        updatedState.position._desc = newDesc;
        this.setState(updatedState);
    }

    onSaveClicked() {

        const req = new PositionUpsertDto();
        req.Position = new PositionDto();
        req.Position.PositionID = this.state.id;
        req.Position.Title = this.state.position._title;
        req.Position.ShortDesc = this.state.position._shortDesc;
        req.Position.Desc = this.state.position._desc;
        req.Position.Status = new PositionStatusDto();
        req.Position.Status.StatusID = this.state.position._status._statusId;

        req.Skills = [];

        this.state.position._skills.forEach( s => {

            const sp = new PositionSkillDto();
            sp.Skill = new SkillDto();
            sp.Skill.SkillID = s._skill._skillId;
            sp.Proficiency = new SkillProficiencyDto();
            sp.Proficiency.ProficiencyID = s._proficiency._id
            sp.IsMandatory = s._isMandatory;

            req.Skills.push(sp);

        } );

        let dalPos = new PositionsDal();
        let result = null;

        let obj = this;
        

        function upsertThen(result) {
            const updatedState = obj.state;

            if(result.status == constants.HTTP_OK || result.status == constants.HTTP_Created) {
                updatedState.showSuccess = true;
                updatedState.showError = false;
                if(result.status == constants.HTTP_Created) {
                    updatedState.id = parseInt(result._positionId);
                    updatedState.success = `Position was created. Position ID: ${updatedState.id}`;
                }
                else {
                    updatedState.success = `Position was updated`;                
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
            dalPos.updatePosition(req)
                                    .then( (res) => { upsertThen(res); } )
                                    .catch( (err) => { upsertCatch(err); });
        }
        else {
            dalPos.addPosition(req)
                                    .then( (res) => { upsertThen(res); } )
                                    .catch( (err) => { upsertCatch(err); });        
        }
    }

    onDeleteClicked() {
    }

    render() {

        let skills = this._getPositionSkills();

        const styleError = {
            display: this.state.showError ? "block" : "none"
        }

        const styleSuccess = {
            display: this.state.showSuccess ? "block" : "none"
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
                                        onClick={ () => this.onDeleteClicked() }>Delete</Button>
                                <Button variant="contained" component={Link} to="/positions">Cancel</Button>
                            </td>
                        </tr>
                        <tr>
                            <td colSpan={2}>
                                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                                <Alert severity="success" style={styleSuccess}>Success! {this.state.error}</Alert>
                                
                            </td>
                        </tr>                    
                        <tr>                            
                            <td colSpan={2}>
                                
                                <TextField  id="title" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Position Title" 
                                            value={this.state.position._title}
                                            onChange={ (event) => { this.onTitleChanged(event) } }
                                            />
                                
                            </td>
                        </tr>                    
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="shortDesc" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Short Description" 
                                            value={this.state.position._shortDesc}
                                            onChange={ (event) => { this.onShortDescChanged(event) } }
                                            /></td>
                        </tr>
                        <tr>
                            <td colSpan={1}>
                                <TextField      key="cbStatus" 
                                                fullWidth
                                                select 
                                                label="Status" 
                                                value={ this.state.position._status ? this.state.position._status._statusId : Object.keys(constants.POSITION_STATUSES)[0] }
                                                onChange={ (event) => this.onStatusChanged(event) }>
                                        {
                                            constants.POSITION_STATUSES.map( (status) => (
                                                <option key={status.statusID} value={status.statusID}>
                                                    {status.name}
                                                </option>
                                            ) )
                                        }
                                </TextField>
                            </td>
                        </tr>
                        <tr>
                            <td colSpan={4}>Position Details</td>
                        </tr>
                        <tr>
                            <td colSpan={4}>
                                <TextField  id="desc" 
                                            type="text" 
                                            variant="filled" 
                                            multiline 
                                            fullWidth
                                            defaultValue={this.state.position._desc}
                                            onChange={ (event) => { this.onDescChanged(event) } }
                                            rows="10"/>
                            </td>
                        </tr>
                        <tr>
                            <td colSpan={4}>
                                <SkillsList id="positionSkills"
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
            </div>

        );
    }

    _createEmptyPositionObj() {
        let pos = {
            _title: "",
            _shortDesc: "",
            _desc: "",
            _status: { _statusId: 1,  _name: "Draft"},
            _skills: []
        }

        return pos;
    }

    _getPositionSkills() {
        let skills = [];

        if( this.state.position._skills ) {
            
            this.state.position._skills.forEach( s => {
                let skill = {
                    id: s.id,
                    SkillID: s._skill._skillId, 
                    ProficiencyID: s._proficiency._id, 
                    IsMandatory: s._isMandatory
                };
                skills.push(skill);
            })
        }  
      
        return skills;
    }
}

export default withRouter(PositionPage);