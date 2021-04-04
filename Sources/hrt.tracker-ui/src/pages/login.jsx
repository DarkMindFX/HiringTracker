import React from 'react';
import { TextField } from '@material-ui/core';
import { Link } from 'react-router-dom'
import { Button } from '@material-ui/core';
import Alert from '@material-ui/lab/Alert';
import constants from '../constants';
const UsersDal = require('../dal/UsersDal')

class RegisterPage extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            login: null,
            password: null,
            showError: false,
            error: null
        }

        this.onLoginChanged = this.onLoginChanged.bind(this);
        this.onPasswordChanged = this.onPasswordChanged.bind(this);
        this.onLoginClicked = this.onLoginClicked.bind(this);
    }

    onLoginChanged(event) {
        let login = event.target.value;

        let updatedState = this.state;
        updatedState.login = login;

        this.setState(updatedState);
    }

    onPasswordChanged(event) {
        let pwd = event.target.value;

        let updatedState = this.state;
        updatedState.password = pwd;

        this.setState(updatedState);
    }

    onLoginClicked() {

        console.log(`Loggiing in as ${this.state.login} / ${this.state.password}`)

        const dal = new UsersDal();
        dal.login(this.state.login, this.state.password).then( (res) => {

            let updatedState = this.state;
            if(res.status == constants.HTTP_OK) {
                console.log('Login SUCCESS', res.data._token)
                updatedState.showError = false;
                updatedState.error = null;
            }
            else {
                updatedState.showError = true;
                updatedState.error = res.data._message;
            }

            this.setState(updatedState);

        });

    }

    render() {

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
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <TextField  id="login" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Login" 
                                            defaultValue={this.state.login}
                                            onChange={ (event) => { this.onLoginChanged(event) } }
                                            />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <TextField  id="password" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Password" 
                                            defaultValue={this.state.password}
                                            onChange={ (event) => { this.onPasswordChanged(event) } }
                                            />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <Button variant="contained" 
                                        color="primary"
                                        onClick = { this.onLoginClicked }>
                                    Login
                                </Button>
                                <Button variant="contained" component={Link} to="/" >Cancel</Button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        )
    }
}

export default RegisterPage; 