


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class UserRoleSystemsDal extends DalBase {

    constructor() {
        super();
    }

    async insertUserRoleSystem(newUserRoleSystem) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/userrolesystems`, newUserRoleSystem);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateUserRoleSystem(updatedUserRoleSystem) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/userrolesystems`, updatedUserRoleSystem);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteUserRoleSystem(userid,roleid) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/userrolesystems/${userid}/${roleid}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getUserRoleSystems()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/userrolesystems`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getUserRoleSystem(userid,roleid) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/userrolesystems/${userid}/${roleid}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = UserRoleSystemsDal;