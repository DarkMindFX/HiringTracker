


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
const SkillProficienciesDal = require('../dal/SkillProficienciesDal');
const { SkillProficiencyDto } = require('hrt.dto')

const constants = require('../constants');
const { v4: uuidv4 } = require('uuid');

class SkillProficiencyPage extends React.Component {

    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);

        this.state = { 
            operation: this.props.match.params.operation,
            id: this.props.match.params.id ? parseInt(this.props.match.params.id) : null,
            canEdit: this.props.match.params.operation ? (this.props.match.params.operation.toLowerCase() == 'new' || 
                                                          this.props.match.params.operation.toLowerCase() == 'edit' ? true : false) : false,
            skillproficiency: this._createEmptySkillProficiencyObj(),

            showDeleteConfirm: false,
            showError: false,
            showSuccess: false,
            error: null,
            success: null
        };

        this.onNameChanged = this.onNameChanged.bind(this);

        this.onSaveClicked = this.onSaveClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);
        this.onDeleteCancel = this.onDeleteCancel.bind(this);
        this.onDeleteConfirm = this.onDeleteConfirm.bind(this);

        this._getSkillProficiency = this._getSkillProficiency.bind(this);


        this.onNameChanged = this.onNameChanged.bind(this);


    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            			obj._getSkillProficiency().then( () => {} );
			
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
        updatedState.skillproficiency.Name = newVal;

        this.setState(updatedState);
    }



    onSaveClicked() {

        console.log("Saving SkillProficiency: ", this.state.skillproficiency);
        
        const reqSkillProficiency = new SkillProficiencyDto();
        reqSkillProficiency.ID = this.state.id;
        reqSkillProficiency.Name = this.state.skillproficiency.Name;

        console.log("Saving SkillProficiency: ", reqSkillProficiency); 
        
        let dalSkillProficiencies = new SkillProficienciesDal();

        let obj = this;

        function upsertSkillProficiencyThen(response) {
            const updatedState = obj.state;

            if(response.status == constants.HTTP_OK || response.status == constants.HTTP_Created) {
                updatedState.showSuccess = true;
                updatedState.showError = false;
                if(response.status == constants.HTTP_Created) {
                    updatedState.id = response.data.ID;
                    updatedState.success = `SkillProficiency was created. ID: ${updatedState.id}`;
                }
                else {
                    updatedState.success = `SkillProficiency was updated`;                
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
            dalSkillProficiencies.updateSkillProficiency(reqSkillProficiency)
                                    .then( (res) => { upsertSkillProficiencyThen(res); } )
                                    .catch( (err) => { upsertCatch(err); });
        }
        else {
            dalSkillProficiencies.insertSkillProficiency(reqSkillProficiency)
                                    .then( (res) => { upsertSkillProficiencyThen(res); } )
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
        
        let dalSkillProficiencies = new SkillProficienciesDal();
        let obj = this;

        dalSkillProficiencies.deleteSkillProficiency(this.state.id).then( (response) => {
            if(response.status == constants.HTTP_OK) {
                obj.props.history.push("/skillproficiencies");                
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
                            <td style={{width: 450}}></td>
                            <td>
                                <Button variant="contained" color="primary"
                                        onClick={ () => this.onSaveClicked() }>Save</Button>

                                <Button variant="contained" color="secondary"
                                        style={styleDeleteBtn}
                                        onClick={ () => this.onDeleteClicked() }>Delete</Button>

                                <Button variant="contained" component={Link} to="/skillproficiencies">Cancel</Button>
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
                                            value={this.state.skillproficiency.Name}
                                            onChange={ (event) => { this.onNameChanged(event) } }
                                            />
                                
                            </td>
                        </tr> 
                       

                    </tbody>
                </table>

                <Dialog open={this.state.showDeleteConfirm} onClose={() => { this.onDeleteCancel() }} aria-labelledby="form-dialog-title">
                    <DialogTitle id="form-dialog-title">Delete SkillProficiency</DialogTitle>
                    <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this SkillProficiency?
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

    _createEmptySkillProficiencyObj() {
        let skillproficiency = new SkillProficiencyDto();

        return skillproficiency;
    }

    async _getSkillProficiency()
    {
        if(this.state.id) {
            let updatedState = this.state;
                  
            let dalSkillProficiencies = new SkillProficienciesDal();
            let response = await dalSkillProficiencies.getSkillProficiency(this.state.id);

            if(response.status == constants.HTTP_OK)
            {
                updatedState.skillproficiency = response.data;                
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

    



    _showError(updatedState, response) {
        var error = JSON.parse(response.data.response);
        updatedState.showError = true;
        updatedState.error = error.Message;
    }

    _redirectToLogin()
    {
        this._pageHelper.redirectToLogin(`/skillproficiency/${this.state.operation}` + (this.state.id ? `/${this.state.id}` : ``));        
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

export default withRouter(SkillProficiencyPage);