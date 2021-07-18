


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class UserRoleCandidatesDal extends DalBase {

    constructor() {
        super();
    }

    async insertUserRoleCandidate(newUserRoleCandidate) {
        let inst = this.Instance;

        try {
            let res = await inst.put(`/userrolecandidates`, newUserRoleCandidate);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateUserRoleCandidate(updatedUserRoleCandidate) {
        let inst = this.Instance;
        
        try {
            let res = await inst.post(`/userrolecandidates`, updatedUserRoleCandidate);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteUserRoleCandidate(candidateid,userid) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/userrolecandidates/${candidateid}/${userid}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getUserRoleCandidates()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/userrolecandidates`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getUserRoleCandidate(candidateid,userid) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/userrolecandidates/${candidateid}/${userid}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = UserRoleCandidatesDal;