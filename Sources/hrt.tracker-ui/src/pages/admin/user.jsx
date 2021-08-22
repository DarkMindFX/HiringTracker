


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


const PageHelper = require("../../helpers/PageHelper");
const UsersDal = require('../../dal/UsersDal');
const { UserDto } = require('hrt.dto')

const constants = require('../../constants');
const { v4: uuidv4 } = require('uuid');

class UserPage extends React.Component {

    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);

        let paramOperation = this.props.match.params.operation;
        let paramId = this.props.match.params.id;
        let rooPath = '/admin';

        this.state = { 
            operation:  paramOperation,
            id:         paramId ? parseInt(paramId) : null,
            canEdit:    paramOperation ? ( paramOperation.toLowerCase() == 'new' || 
                                        paramOperation.toLowerCase() == 'edit' ? true : false) : false,
            user:       this._createEmptyUserObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null,
            urlEntities: `${rooPath}/users`,
            urlThis: `${rooPath}/user/${paramOperation}` + (paramId ? `/${paramId}` : ``)
        };

        this.onLoginChanged = this.onLoginChanged.bind(this);
        this.onFirstNameChanged = this.onFirstNameChanged.bind(this);
        this.onLastNameChanged = this.onLastNameChanged.bind(this);
        this.onEmailChanged = this.onEmailChanged.bind(this);
        this.onDescriptionChanged = this.onDescriptionChanged.bind(this);

        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);

        this._getUser = this._getUser.bind(this);
        this._validateForm = this._validateForm.bind(this);
        this._showError = this._showError.bind(this);


        this.onLoginChanged = this.onLoginChanged.bind(this);
        this.onFirstNameChanged = this.onFirstNameChanged.bind(this);
        this.onLastNameChanged = this.onLastNameChanged.bind(this);
        this.onEmailChanged = this.onEmailChanged.bind(this);
        this.onDescriptionChanged = this.onDescriptionChanged.bind(this);
    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            			obj._getUser().then( () => {} );
			
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
    }

    onLoginChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.user.Login = newVal;

        this.setState(updatedState);
    }

    onFirstNameChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.user.FirstName = newVal;

        this.setState(updatedState);
    }

    onLastNameChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.user.LastName = newVal;

        this.setState(updatedState);
    }

    onEmailChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.user.Email = newVal;

        this.setState(updatedState);
    }

    onDescriptionChanged(event) {

        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.user.Description = newVal;

        this.setState(updatedState);
    }

    onNewPasswordChanged(event) {
        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.user.NewPassword = newVal;

        this.setState(updatedState);      
    }

    onConfirmPasswordChanged(event) { 
        let updatedState = this.state;
        let newVal = null;
        newVal = event.target.value
        updatedState.user.ConfirmPassword = newVal;

        this.setState(updatedState);             
    }

    onSaveClicked() {

        console.log("Saving User: ", this.state.user);
        
        if(this._validateForm()) {
            const reqUser = new UserDto();
            reqUser.ID = this.state.id;
            reqUser.Login = this.state.user.Login;
            reqUser.FirstName = this.state.user.FirstName;
            reqUser.LastName = this.state.user.LastName;
            reqUser.Email = this.state.user.Email;
            reqUser.Description = this.state.user.Description;
            reqUser.Password = this.state.user.NewPassword && this.state.user.NewPassword == this.state.user.ConfirmPassword ?
                                    this.state.user.NewPassword : null;

            console.log("Saving User: ", reqUser); 
            
            let dalUsers = new UsersDal();

            let obj = this;

            function upsertUserThen(response) {
                const updatedState = obj.state;

                if(response.status == constants.HTTP_OK || response.status == constants.HTTP_Created) {
                    updatedState.showSuccess = true;
                    updatedState.showError = false;
                    if(response.status == constants.HTTP_Created) {
                        updatedState.id = response.data.ID;
                        updatedState.success = `User was created. ID: ${updatedState.id}`;
                    }
                    else {
                        updatedState.success = `User was updated`;                
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
                dalUsers.updateUser(reqUser)
                                        .then( (res) => { upsertUserThen(res); } )
                                        .catch( (err) => { upsertCatch(err); });
            }
            else {
                dalUsers.insertUser(reqUser)
                                        .then( (res) => { upsertUserThen(res); } )
                                        .catch( (err) => { upsertCatch(err); });        
            }

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
        
        let dalUsers = new UsersDal();
        let obj = this;

        dalUsers.deleteUser(this.state.id).then( (response) => {
            if(response.status == constants.HTTP_OK) {
                obj.props.history.push(this.urlEntities);                
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

        return (
            <div>
                 <table>
                    <tbody>
                        <tr>
                            <td style={{width: 450}}>
                                <h2>User: {this.state.user.FirstName + ' ' + this.state.user.LastName}</h2>
                            </td>
                            <td>
                                <Button variant="contained" color="primary"
                                        onClick={ () => this.onSaveClicked() }>Save</Button>

                                <Button variant="contained" color="secondary"
                                        style={styleDeleteBtn}
                                        onClick={ () => this.onDeleteClicked() }>Delete</Button>

                                <Button variant="contained" component={Link} to={this.state.urlEntities}>Cancel</Button>
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
                                <TextField  id="Login" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Login" 
                                            value={this.state.user.Login}
                                            onChange={ (event) => { this.onLoginChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="FirstName" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="FirstName" 
                                            value={this.state.user.FirstName}
                                            onChange={ (event) => { this.onFirstNameChanged(event) } }
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
                                            value={this.state.user.LastName}
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
                                            value={this.state.user.Email}
                                            onChange={ (event) => { this.onEmailChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="Description" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Description" 
                                            value={this.state.user.Description}
                                            onChange={ (event) => { this.onDescriptionChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="NewPassword" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="New Password" 
                                            value={this.state.user.NewPassword}
                                            onChange={ (event) => { this.onNewPasswordChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
   
                        <tr>
                            <td colSpan={2}>
                                <TextField  id="ConfirmPassword" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Confirm Password" 
                                            value={this.state.user.ConfirmPassword}
                                            onChange={ (event) => { this.onConfirmPasswordChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
                       

                    </tbody>
                </table>

                <Dialog open={this.state.showDeleteConfirm} onClose={() => { this.onDeleteCancel() }} aria-labelledby="form-dialog-title">
                    <DialogTitle id="form-dialog-title">Delete User</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this User?
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

    _createEmptyUserObj() {
        let user = new UserDto();

        return user;
    }

    async _getUser()
    {
        if(this.state.id) {
            let updatedState = this.state;
                  
            let dalUsers = new UsersDal();
            let response = await dalUsers.getUser(this.state.id);

            if(response.status == constants.HTTP_OK)
            {
                updatedState.user = response.data;                
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

    _validateForm() {
        let updatedState = this.state;
        let isValid = true;
        if(this.state.user.NewPassword && this.state.user.NewPassword != this.state.user.ConfirmPassword) {
            isValid = false;
            updatedState.showError = true;
            updatedState.error = "Check your 'New Password' and 'Confirm Password' - values are not equal";            
        }
        if(!this.state.user.NewPassword && !this.state.id) {
            isValid = false;
            updatedState.showError = true;
            updatedState.error = "Password is required for new user";
        }

        if(isValid) {
            updatedState.showError = false;
        }
        
        this.setState(updatedState);
        
        return isValid;
    }

    _showError(updatedState, response) {
        var error = JSON.parse(response.data.response);
        updatedState.showError = true;
        updatedState.error = error.Message;
    }

    _redirectToLogin()
    {
        this._pageHelper.redirectToLogin(this.state.urlThis);        
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

export default withRouter(UserPage);