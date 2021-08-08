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
const PositionsDal = require('../dal/PositionsDal');
const DepartmentsDal = require('../dal/DepartmentsDal');
const PositionStatusesDal = require('../dal/PositionStatusesDal');
const PositionSkillsDal = require('../dal/PositionSkillsDal');
const SkillsDal = require('../dal/SkillsDal');
const SkillProficienciesDal = require('../dal/SkillProficienciesDal');
const { PositionDto, PositionSkillDto } = require('hrt.dto')

const constants = require('../constants');
const { v4: uuidv4 } = require('uuid');

class PositionPage extends React.Component {

    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);

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
        this.onDepartmentChanged = this.onDepartmentChanged.bind(this);
        this.onStatusChanged = this.onStatusChanged.bind(this);
        this.onTitleChanged = this.onTitleChanged.bind(this);
        this.onShortDescChanged = this.onShortDescChanged.bind(this);
        this.onDescChanged = this.onDescChanged.bind(this);
        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);

        this._getDepartments = this._getDepartments.bind(this);
        this._getPositionStatuses = this._getPositionStatuses.bind(this);
        this._getPosition = this._getPosition.bind(this);
        this._getPositionSkills = this._getPositionSkills.bind(this);
        this._getSkills = this._getSkills.bind(this);
        this._getSkillProficiencies = this._getSkillProficiencies.bind(this);
    }

    componentDidMount() {

        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        if(token != null) {

            let obj = this;

            obj._getDepartments().then( () => {
                console.log("Departments", this.state.departments);
                obj._getPositionStatuses().then( () => {
                    if(obj.state.id) {   
                        obj._getPosition().then( () => {
                            obj._getSkills().then( () => {
                                obj._getSkillProficiencies().then( () => {
                                    obj._getPositionSkills().then( () => {} );
                                })
                            })
                        })                     
                    }
                })
            })
        }
        else {
            console.log('No token - need to login')
            this.props.history.push(`/login?ret=/position/${this.state.operation}` + (this.state.id ? `/${this.state.id}` : ``))
        }
    }

    onStatusChanged(event) {

        let updatedState = this.state;        

        let newStatusId = parseInt(event.target.value);
       
        updatedState.position.StatusID = newStatusId > 0 ? newStatusId : null;

        this.setState(updatedState);
    }  
    
    onDepartmentChanged(event) {

        let updatedState = this.state;        

        let newDepartmentId = parseInt(event.target.value);
       
        updatedState.position.DepartmentID = newDepartmentId > 0 ? newDepartmentId : null;

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
                var error = JSON.parse(result.data.response);
                updatedState.showError = true;
                updatedState.error = error.Message;   
                
                obj.setState(updatedState);
            }

            
        }        

        function upsertSkillsThen(result) {
            const updatedState = obj.state;

            if(result.status == constants.HTTP_OK || result.status == constants.HTTP_Created) {
                updatedState.showSuccess = true;
                updatedState.showError = false;
            }
            else {
                var error = JSON.parse(result.data.response);
                updatedState.showError = true;
                updatedState.error = error.Message;          
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

        let skills = this._getPositionSkillsAsList();

        const styleError = {
            display: this.state.showError ? "block" : "none"
        }

        const styleSuccess = {
            display: this.state.showSuccess ? "block" : "none"
        }   
        
        const styleDeleteBtn = {
            display: this.state.id ? "block" : "none"
        }

        var lstDepartments = this._prepareOptionsList( this.state.departments ? Object.values(this.state.departments) : null, true );
        
        var lstPositionStatuses = this._prepareOptionsList( this.state.positionstatuses ? Object.values(this.state.positionstatuses) : null, false );
        
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
                                <TextField      key="cbDepartment" 
                                                fullWidth
                                                select 
                                                label="Department" 
                                                value={ (this.state.position && this.state.position.DepartmentID) ? this.state.position.DepartmentID : '-1' }
                                                onChange={ (event) => this.onDepartmentChanged(event) }>
                                        {
                                            lstDepartments 
                                        }
                                </TextField>
                            </td>
                        </tr>
                        <tr>
                            <td colSpan={1}>
                                <TextField      key="cbStatus" 
                                                fullWidth
                                                select 
                                                label="Status" 
                                                value={ (this.state.position && this.state.position.StatusID) ? this.state.position.StatusID : Object.keys(this.state.positionstatuses)[0] }
                                                onChange={ (event) => this.onStatusChanged(event) }>
                                        {
                                            lstPositionStatuses
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

    async _getPosition()
    {
        let updatedState = this.state;
                  
        let dalPositions = new PositionsDal();
        let response = await dalPositions.getPosition(this.state.id);

        if(response.status == constants.HTTP_OK)
        {
            updatedState.position = response.data;                
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

    async _getPositionSkills()
    {
        let updatedState = this.state;

        if(updatedState.position)
        {
                    
            let dalPositionSkills = new PositionSkillsDal();
            let response = await dalPositionSkills.getPositionSkillsByPosition(this.state.id);

            if(response.status == constants.HTTP_OK)
            {
                const skills = response.data;                     
                updatedState.position.Skills = skills.map(s => { s.id = uuidv4(); return s; });                
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

    async _getDepartments()
    {
        let updatedState = this.state;
        if(updatedState.departments == null)
        {            
            let dalDepartments = new DepartmentsDal();
            let response = await dalDepartments.getDepartments();

            if(response.status == constants.HTTP_OK)
            {
                updatedState.departments = {};

                for(let s in response.data)
                {
                    updatedState.departments[ response.data[s].ID ] = response.data[s];
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

    async _getPositionStatuses()
    {
        let updatedState = this.state;
        if(updatedState.positionstatuses == null)
        {            
            let dalPositionStatuses = new PositionStatusesDal();
            let response = await dalPositionStatuses.getPositionStatuses();

            if(response.status == constants.HTTP_OK)
            {
                updatedState.positionstatuses = {};

                for(let s in response.data)
                {
                    updatedState.positionstatuses[ response.data[s].ID ] = response.data[s];
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

    _getPositionSkillsAsList() {
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

    _showError(updatedState, response) {
        var error = JSON.parse(response.data.response);
        updatedState.showError = true;
        updatedState.error = error.Message;
    }

    _redirectToLogin()
    {
        this._pageHelper.redirectToLogin(`/position/${this.state.operation}` + (this.state.id ? `/${this.state.id}` : ``));        
    }

    _prepareOptionsList(objs, hasEmptyVal) 
    {
        var lst = [];
        
        if(hasEmptyVal) {
            lst.push( <option key='-1' value='-1'>[Empty]</option> );
        }

        if(objs) {
            lst.push(
                objs.map( (i) => (
                    <option key={i.ID} value={i.ID}>
                        {i.Name}
                    </option>
                ))
            )
        }

        return lst;
    }
}

export default withRouter(PositionPage);