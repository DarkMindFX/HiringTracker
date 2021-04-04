import React from 'react';
import { TextField } from '@material-ui/core';
import { Button } from '@material-ui/core';

const { UserDto } = require('hrt.dto')


class UserDetails extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            user: props.user,
            onUserDataChanged: props.onUserDataChanged
        }

        this.onLoginChanged = this.onLoginChanged.bind(this);
        this.onEmailChanged = this.onEmailChanged.bind(this);
        this.onFirstNameChanged = this.onFirstNameChanged.bind(this);
        this.onLastNameChanged = this.onLastNameChanged.bind(this);
        this.onPwdChanged = this.onPwdChanged.bind(this);
        this.onDescChanged = this.onDescChanged.bind(this);
    }

    onLoginChanged(event) {
        const login = event.target.value;

        const updateUser = this.state.user;
        updateUser.Login = login;

        this.state.onUserDataChanged(updateUser);
    }

    onEmailChanged(event) {
        const email = event.target.value;

        const updateUser = this.state.user;
        updateUser.Email = email;

        this.state.onUserDataChanged(updateUser);

    }

    onFirstNameChanged(event) {
        const fname = event.target.value;

        const updateUser = this.state.user;
        updateUser.FirstName = fname;

        this.state.onUserDataChanged(updateUser);
    }

    onLastNameChanged(event) {
        const lname = event.target.value;

        const updateUser = this.state.user;
        updateUser.LastName = lname;

        this.state.onUserDataChanged(updateUser);
    }

    onPwdChanged(event) {
        const pwd = event.target.value;

        const updateUser = this.state.user;
        updateUser.Pwd = pwd;

        this.state.onUserDataChanged(updateUser);

    }

    onDescChanged(event) {
        const desc = event.target.value;

        const updateUser = this.state.user;
        updateUser.Description = desc;

        this.state.onUserDataChanged(updateUser);
    }

    static getDerivedStateFromProps(props, state) {
        if(props.user != state.user ||
           props.user.Login != state.user.Login ||
           props.user.Email != state.user.Email ||
           props.user.FirstName != state.user.FirstName ||
           props.user.LastName != state.user.LastName ||
           props.user.Pwd != state.user.Pwd ||
           props.user.Description != state.user.Description) {

            let updatedState = {
                onUserDataChanged: props.onUserDataChanged
            }

            let user = new UserDto();
            user.Login = props.Login;
            user.Email = props.Email;
            user.FirstName = props.FirstName;
            user.LastName = props.LastName;
            user.Pwd = props.Pwd;
            user.Description = props.Description;

            updatedState.user = user;

            return updatedState;

        }
        else {
            return null;
        }
    }

    render() {
        return (
            <div>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TextField  id="login" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Login" 
                                            defaultValue={this.state.user.Login}
                                            onChange={ (event) => { this.onLoginChanged(event) } }
                                            />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <TextField  id="email" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Email" 
                                            defaultValue={this.state.user.Email}
                                            onChange={ (event) => { this.onEmailChanged(event) } }
                                            />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <TextField  id="firstName" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="First Name" 
                                            defaultValue={this.state.user.FirstName}
                                            onChange={ (event) => { this.onFirstNameChanged(event) } }
                                            />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <TextField  id="lastName" 
                                            fullWidth
                                            type="text" 
                                            variant="filled" 
                                            label="Last Name" 
                                            defaultValue={this.state.user.LastName}
                                            onChange={ (event) => { this.onLastNameChanged(event) } }
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
                                            defaultValue={this.state.user.Pwd}
                                            onChange={ (event) => { this.onPwdChanged(event) } }
                                            />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <TextField  id="userDesc" 
                                            fullWidth
                                            multiline
                                            rows="10"
                                            type="text" 
                                            variant="filled" 
                                            label="User Short Description" 
                                            defaultValue={this.state.user.Description}
                                            onChange={ (event) => { this.onDescChanged(event) } }
                                            />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        )
    }
}

export default UserDetails;