

const axios = require('axios');
const { LoginRequest } = require('hrt.dto');
const constants = require('../constants');

const DalBase = require('./DalBase');

class UsersDal extends DalBase {

    constructor() {
        super();
    }

    async registerUser(newUser) {
        const url = this.ApiUrl;

        console.log('PUT /users', newUser);
       
        let inst = axios.create({
            baseURL: url
        })

        try {
            let res = await inst.put(`/users`, newUser);

            console.log('PUT /users', res);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateUser(updatedUser) {
        const url = this.ApiUrl;
       
        let inst = axios.create({
            baseURL: url
        })
        
        try {
            let res = await inst.post(`/users`, updatedUser);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteUser(id) {
        const url = this.ApiUrl;
       
        let inst = axios.create({
            baseURL: url
        })

        try {
            let res = await inst.delete(`/users/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getUser(id) {
        const url = this.ApiUrl;
       
        let inst = axios.create({
            baseURL: url
        })

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

        const url = this.ApiUrl;

        let inst = axios.create({
            baseURL: url
        })
       
        try {
            let res = await inst.post(`/login`, loginDto);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

}

module.exports = UsersDal;