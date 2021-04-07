

const axios = require('axios');
const { LoginRequest } = require('hrt.dto');
const constants = require('../constants');

const DalBase = require('./DalBase');

class UsersDal extends DalBase {

    constructor() {
        super();
    }

    async registerUser(newUser) {
        let inst = this.Instance;

        try {
            let res = await inst.put(`/users`, newUser);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateUser(updatedUser) {
        let inst = this.Instance;
        
        try {
            let res = await inst.post(`/users`, updatedUser);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteUser(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/users/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getUser(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/users/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async login(login, password) {
        const loginDto = new LoginRequest();
        loginDto.Login = login;
        loginDto.Password = password;

        let inst = this.Instance;
       
        try {
            let res = await inst.post(`/login`, loginDto);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

}

module.exports = UsersDal;