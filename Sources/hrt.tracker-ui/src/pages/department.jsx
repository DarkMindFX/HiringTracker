


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
const DepartmentsDal = require('../dal/DepartmentsDal');

const UsersDal = require('../dal/UsersDal');
const { DepartmentDto } = require('hrt.dto')

const constants = require('../constants');
const { v4: uuidv4 } = require('uuid');

class DepartmentPage extends React.Component {

    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);

        this.state = { 
            operation: this.props.match.params.operation,
            id: this.props.match.params.id ? parseInt(this.props.match.params.id) : null,
            canEdit: this.props.match.params.operation ? (this.props.match.params.operation.toLowerCase() == 'new' || 
                                                          this.props.match.params.operation.toLowerCase() == 'edit' ? true : false) : false,
            department: this._createEmptyDepartmentObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null
        };

        this.onNameChanged = this.onNameChanged.bind(this);
        this.onUUIDChanged = this.onUUIDChanged.bind(this);
        this.onParentIDChanged = this.onParentIDChanged.bind(this);
        this.onManagerIDChanged = this.onManagerIDChanged.bind(this);

        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);

        this._getDepartment = this._getDepartment.bind(this);

        this._getDepartments = this._getDepartments.bind(this);
        this._getUsers = this._getUsers.bind(this);


    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            			obj._getDepartments().then( () => {
			obj._getUsers().then( () => {
			obj._getDepartment().then( () => {} );
			});});
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
    }

    onNameChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.department.Name = newVal;

        this.setState(updatedState);
    }

    onUUIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.department.UUID = newVal;

        this.setState(updatedState);
    }

    onParentIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.department.ParentID = newVal;

        this.setState(updatedState);
    }

    onManagerIDChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = parseInt(event.target.value);
        updatedState.department.ManagerID = newVal;

        this.setState(updatedState);
    }



    onSaveClicked() {

        console.log("Saving Department: ", this.state.department);
        
        const reqDepartment = new DepartmentDto();
        reqDepartment.ID = this.state.id;
        reqDepartment.Name = this.state.department.Name;
        reqDepartment.UUID = this.state.department.UUID;
        reqDepartment.ParentID = this.state.department.ParentID;
        reqDepartment.ManagerID = this.state.department.ManagerID;

        console.log("Saving Department: ", reqDepartment); 
        
        let dalDepartments = new DepartmentsDal();

        let obj = this;

        function upsertDepartmentThen(response) {
            const updatedState = obj.state;

            if(response.status == constants.HTTP_OK || response.status == constants.HTTP_Created) {
                updatedState.showSuccess = true;
                updatedState.showError = false;
                if(response.status == constants.HTTP_Created) {
                    updatedState.id = response.data.ID;
                    updatedState.success = `Department was created. ID: ${updatedState.id}`;
                }
                else {
                    updatedState.success = `Department was updated`;                
                }

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

        if(this.state.id != null) {
            dalDepartments.updateDepartment(reqDepartment)
                                    .then( (res) => { upsertDepartmentThen(res); } )
                                    .catch( (err) => { upsertCatch(err); });
        }
        else {
            dalDepartments.insertDepartment(reqDepartment)
                                    .then( (res) => { upsertDepartmentThen(res); } )
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
        
        let dalDepartments = new DepartmentsDal();
        let obj = this;

        dalDepartments.deleteDepartment(this.state.id).then( (response) => {
            if(response.status == constants.HTTP_OK) {
                obj.props.history.push("/departments");                
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

        const styleError = {
            display: this.state.showError ? "block" : "none"
        }

        const styleSuccess = {
            display: this.state.showSuccess ? "block" : "none"
        }   
        
        const styleDeleteBtn = {
            display: this.state.id ? "block" : "none"
        }

        const lstParentIDsFields = ["Name"];
        const lstParentIDs = this._prepareOptionsList( this.state.departments 
                                                                    ? Object.values(this.state.departments) : null, 
                                                                    lstParentIDsFields,
                                                                    true );
        const lstManagerIDsFields = ["FirstName", "LastName"];
        const lstManagerIDs = this._prepareOptionsList( this.state.users 
                                                                    ? Object.values(this.state.users) : null, 
                                                                    lstManagerIDsFields,
                                                                    false );
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

                                <Button variant="contained" component={Link} to="/departments">Cancel</Button>
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
                                <TextField  id="Name" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Name" 
                                            value={this.state.department.Name}
                                            onChange={ (event) => { this.onNameChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="UUID" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="UUID" 
                                            value={this.state.department.UUID}
                                            onChange={ (event) => { this.onUUIDChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbParentID" 
                                            fullWidth
                                            select 
                                            label="ParentID" 
                                            value={ (this.state.department && this.state.department.ParentID) ? 
                                                        this.state.department.ParentID : '-1' }
                                                        onChange={ (event) => this.onParentIDChanged(event) }>
                                        {
                                            lstParentIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  key="cbManagerID" 
                                            fullWidth
                                            select 
                                            label="ManagerID" 
                                            value={ (this.state.department && this.state.department.ManagerID) ? 
                                                        this.state.department.ManagerID : '-1' }
                                                        onChange={ (event) => this.onManagerIDChanged(event) }>
                                        {
                                            lstManagerIDs 
                                        }
                                </TextField>

                                
                            </td>
                        </tr> 
                       

                    </tbody>
                </table>

                <Dialog open={this.state.showDeleteConfirm} onClose={() => { this.onDeleteCancel() }} aria-labelledby="form-dialog-title">
                    <DialogTitle id="form-dialog-title">Delete Department</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this Department?
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

    _createEmptyDepartmentObj() {
        let department = new DepartmentDto();

        return department;
    }

    async _getDepartment()
    {
        let updatedState = this.state;
                  
        let dalDepartments = new DepartmentsDal();
        let response = await dalDepartments.getDepartment(this.state.id);

        if(response.status == constants.HTTP_OK)
        {
            updatedState.department = response.data;                
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

    async _getDepartments() {
        let updatedState = this.state;
        updatedState.departments = {};
        let dalDepartments = new DepartmentsDal();
        let response = await dalDepartments.getDepartments();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.departments[response.data[s].ID] = response.data[s];             
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

    



    _showError(updatedState, response) {
        var error = JSON.parse(response.data.response);
        updatedState.showError = true;
        updatedState.error = error.Message;
    }

    _redirectToLogin()
    {
        this._pageHelper.redirectToLogin(`/department/${this.state.operation}` + (this.state.id ? `/${this.state.id}` : ``));        
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

export default withRouter(DepartmentPage);