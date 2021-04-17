import React from 'react';
import { Link, withRouter } from 'react-router-dom'
import { Button } from '@material-ui/core';
import Alert from '@material-ui/lab/Alert';
import UserDetails from '../components/UserDetails'
import constants from '../constants';

const UsersDal = require('../dal/UsersDal')

const { UserDto } = require('hrt.dto')

class RegisterPage extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            user: this._createEmptyUserObj(),
            error: null,
            showError: false,
            showSuccess: false
        }

        this.onRegisterClicked = this.onRegisterClicked.bind(this);
        this.onUserDataChanged = this.onUserDataChanged.bind(this);
    }

    onUserDataChanged(updatedUser) {

        let updatedState = this.state;
        updatedState.user = updatedUser;

        this.setState(updatedState);
    }

    onRegisterClicked() {
        console.log('Registering user: ', this.state.user)

        const dal = new UsersDal();
        dal.registerUser(this.state.user).then( (res) => {
            let updatedState = this.state;
            if(res.status == constants.HTTP_OK) {
                
                updatedState.showSuccess = true;
                updatedState.showError = false;
                updatedState.error = null;
            }
            else {                
                updatedState.showSuccess = false;
                updatedState.showError = true;
                updatedState.error = res.data._message;                
            }
            this.setState(updatedState);
        } )

    }

    render() {

        const styleSuccess = {
            display: this.state.showSuccess ? "block" : "none"
        }

        const styleError = {
            display: this.state.showError ? "block" : "none"
        }

        return (
            <div>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <Alert severity="error" style={styleError}>Error: {this.state.error}</Alert>
                                <Alert severity="success" style={styleSuccess}>
                                    New user with login {this.state.user.Login} successfully created.
                                    You can now login - <Button variant="contained" component={Link} color="primary" size="small" to="/login" >Login</Button> 
                                </Alert>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <UserDetails user={this.state.user} onUserDataChanged={this.onUserDataChanged} />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <Button variant="contained" 
                                        color="primary"
                                        onClick = { this.onRegisterClicked }>
                                    Register
                                </Button>
                                <Button variant="contained" component={Link} to="/" >Cancel</Button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        )
    }

    _createEmptyUserObj() {

        let user = new UserDto();
        user.Login = null;
        user.Email = null;
        user.Pwd = null;
        user.FirstName = null;
        user.LastName = null;
        user.Description = null;

        return user;

    }
}

export default withRouter(RegisterPage);