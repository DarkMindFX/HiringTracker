


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class RolesDal extends DalBase {

    constructor() {
        super();
    }

    async insertRole(newRole) {
        let inst = this.Instance;

        try {
            let res = await inst.put(`/roles`, newRole);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateRole(updatedRole) {
        let inst = this.Instance;
        
        try {
            let res = await inst.post(`/roles`, updatedRole);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteRole(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/roles/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getRoles()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/roles`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getRole(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/roles/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = RolesDal;