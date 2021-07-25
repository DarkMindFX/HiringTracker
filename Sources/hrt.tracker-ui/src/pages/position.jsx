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

const PositionsDal = require('../dal/PositionsDal');
const PositionSkillsDal = require('../dal/PositionSkillsDal');
const SkillsDal = require('../dal/SkillsDal');
const SkillProficienciesDal = require('../dal/SkillProficienciesDal');
const { PositionDto, PositionSkillDto } = require('hrt.dto')

const constants = require('../constants');
const { v4: uuidv4 } = require('uuid');

class PositionPage extends React.Component {

    _skills = null;
    _proficiences = null;

    constructor(props) {
        super(props);

        this.state = { 
            operation: this.props.match.params.operation,
            id: this.props.match.params.id ? parseInt(this.props.match.params.id) : null,
            canEdit: this.props.match.params.operation ? (this.props.match.params.operation.toLowerCase() == 'new' || 
                                                          this.props.match.params.operation.toLowerCase() == 'edit' ? true : false) : false,
            position: this._createEmptyPositionObj(),

            showDeleteConfirm: false,
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
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);
        this._getSkills = this._getSkills.bind(this);
        this._getSkillProficiencies = this._getSkillProficiencies.bind(this);
    }

    componentDidMount() {

        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        if(token != null) {

            let dalPos = new PositionsDal();
            let obj = this;

            obj._getSkills().then( () => {

                obj._getSkillProficiencies().then( () => {

                    if(obj.state.id) {

                        dalPos.getPosition(obj.state.id).then( async (resPos) => {
                            let updatedState = obj.state;

                            if(resPos.status == constants.HTTP_OK) {
           
                                updatedState.position = resPos.data;
                                updatedState.showError = false;
                                updatedState.error = null;

                                obj.setState(updatedState); 

                                let dalPosSkills = new PositionSkillsDal();

                                dalPosSkills.getPositionSkillsByPosition(obj.state.id).then( (resSkills) => {

                                    let updatedState = obj.state;                            

                                    if(resSkills.status == constants.HTTP_OK) {   
                                        const skills = resSkills.data;                     
                                        updatedState.position.Skills = skills.map(s => { s.id = uuidv4(); return s; });
                                        updatedState.showError = false;
                                        updatedState.error = null;
                                    } 
                                    else {
                                        updatedState.showError = true;
                                        updatedState.error = resPos.data.Message;
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
                                updatedState.error = resPos.data.Message;                    
                            }

                            obj.setState(updatedState);

                        }).catch( (err) => {
                            console.log('Error when getting position:', err);
                        })
                    }
                });
            });
        }
        else {
            console.log('No token - need to login')
            this.props.history.push(`/login?ret=/position/${this.state.operation}` + (this.state.id ? `/${this.state.id}` : ``))
        }
    }

    onStatusChanged(event) {

        let updatedState = this.state;        

        let newStatusId = parseInt(event.target.value);
       
        updatedState.position.StatusID = newStatusId;
        updatedState.position.StatusName = constants.POSITION_STATUSES.find( s => { return s.statusID == newStatusId } ).name;

        this.setState(updatedState);
    }    

    onSkillChanged(updatedSkill) {

        let updatedState = this.state;

        let skill = updatedState.position.Skills.find( s => { return s.id == updatedSkill.id; } );

        if(skill != null) {
            skill.SkillID = updatedSkill.SkillID;
            skill.SkillProficiencyID = updatedSkill.SkillProficiencyID;
            skill.IsMandatory = updatedSkill.IsMandatory;

            this.setState(updatedState);
        }  
        
        console.log("onSkillChanged", this.state.position.Skills);
    }   

    onSkillAdded(newSkill) {
        let updatedState = this.state;
        let newSkillRec = new PositionSkillDto();
         
        newSkillRec.id = newSkill.id;
        newSkillRec.PositionID = this.state.id;
        newSkillRec.SkillID = newSkill.SkillID;
        newSkillRec.SkillProficiencyID = newSkill.SkillProficiencyID;
        newSkillRec.IsMandatory = newSkill.IsMandatory;
        
        updatedState.position.Skills.push(newSkillRec);

        this.setState(updatedState);

    }

    onSkillDeleted(id) {
        let updatedState = this.state;
        let idx = updatedState.position.Skills.findIndex( s => { return s.id == id; } );
        if(idx >= 0) {
            updatedState.position.Skills.splice(idx, 1);
            this.setState(updatedState);
        }
    }

    onTitleChanged(event) {
        const newTitle = event.target.value;

        let updatedState = this.state;
        updatedState.position.Title = newTitle;
        this.setState(updatedState);
    }

    onShortDescChanged(event) {
        const newShortDesc = event.target.value;

        let updatedState = this.state;
        updatedState.position.ShortDesc = newShortDesc;
        this.setState(updatedState);
    }

    onDescChanged(event) {
        const newDesc = event.target.value;

        let updatedState = this.state;
        updatedState.position.Description = newDesc;
        this.setState(updatedState);
    }

    onSaveClicked() {

        console.log("Saving position: ", this.state.position);
        
        const reqPosition = new PositionDto();
        reqPosition.ID = this.state.id;
        reqPosition.Title = this.state.position.Title;
        reqPosition.ShortDesc = this.state.position.ShortDesc;
        reqPosition.Description = this.state.position.Description;
        reqPosition.StatusID = this.state.position.StatusID;

        let reqPositionSkills = this.state.position.Skills.map( ps => { 
            var dto = new PositionSkillDto();
            dto.SkillID = ps.SkillID;
            dto.SkillProficiencyID = ps.SkillProficiencyID;
            dto.IsMandatory = ps.IsMandatory;
            return dto;
        });        

        console.log("Saving Position: ", reqPosition);   
        console.log("Saving Skills: ", reqPositionSkills);         
        
        let dalPos = new PositionsDal();

        let obj = this;

        function upsertPositionThen(result) {
            const updatedState = obj.state;

            if(result.status == constants.HTTP_OK || result.status == constants.HTTP_Created) {
                updatedState.showSuccess = true;
                updatedState.showError = false;
                if(result.status == constants.HTTP_Created) {
                    updatedState.id = result.data.ID;
                    updatedState.success = `Position was created. ID: ${updatedState.id}`;
                }
                else {
                    updatedState.success = `Position was updated`;                
                }

                obj.setState(updatedState);
            
                let dalPositionSkills = new PositionSkillsDal();
                dalPositionSkills.setPositionSkills(obj.state.id ? obj.state.id : result.data.ID, reqPositionSkills)
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
            dalPos.updatePosition(reqPosition)
                                    .then( (res) => { upsertPositionThen(res); } )
                                    .catch( (err) => { upsertCatch(err); });
        }
        else {
            dalPos.insertPosition(reqPosition)
                                    .then( (res) => { upsertPositionThen(res); } )
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
        
        let dalPos = new PositionsDal();
        let obj = this;

        dalPos.deletePosition(this.state.id).then( (res) => {
            if(res.status == constants.HTTP_OK) {
                obj.props.history.push("/positions");                
            }
            else {
                const updatedState = obj.state;
                updatedState.showSuccess = false;
                updatedState.showError = true;
                updatedState.error = res.data.Message; 
                updatedState.showDeleteConfirm = false;
                obj.setState(updatedState);               
            }
        })
    }

    render() {

        let skills = this._getPositionSkills();

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

                                <Button variant="contained" component={Link} to="/positions">Cancel</Button>
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
                                
                                <TextField  id="title" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Position Title" 
                                            value={this.state.position.Title}
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
                                            value={this.state.position.ShortDesc}
                                            onChange={ (event) => { this.onShortDescChanged(event) } }
                                            /></td>
                        </tr>
                        <tr>
                            <td colSpan={1}>
                                <TextField      key="cbStatus" 
                                                fullWidth
                                                select 
                                                label="Status" 
                                                value={ this.state.position.StatusID ? this.state.position.StatusID : Object.keys(constants.POSITION_STATUSES)[0] }
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
                                            defaultValue={this.state.position.Description}
                                            onChange={ (event) => { this.onDescChanged(event) } }
                                            rows="10"/>
                            </td>
                        </tr>
                        <tr>
                            <td colSpan={4}>
                                
                                <SkillsList id="positionSkills"
                                    skills={ skills }
                                    canEdit={ this.state.canEdit }
                                    showMustHave = { true }
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

                <Dialog open={this.state.showDeleteConfirm} onClose={() => { this.onDeleteCancel() }} aria-labelledby="form-dialog-title">
                    <DialogTitle id="form-dialog-title">Delete Position</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this position?
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

    _createEmptyPositionObj() {
        let pos = new PositionDto();
        pos.Skills = [];
        pos.StatusID = 1;

        return pos;
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

    _getPositionSkills() {
        let skills = [];

        if(this.state.position.Skills ) {    

            let obj = this;
            
            this.state.position.Skills.forEach( s => {

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

    _redirectToLogin()
    {
        this.props.history.push(`/login?ret=/position/${this.state.operation}` + (this.state.id ? `/${this.state.id}` : ``))        
    }
}

export default withRouter(PositionPage);