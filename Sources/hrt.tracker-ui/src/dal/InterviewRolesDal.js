


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class InterviewRolesDal extends DalBase {

    constructor() {
        super();
    }

    async insertInterviewRole(newInterviewRole) {
        let inst = this.Instance;

        try {
            let res = await inst.put(`/interviewroles`, newInterviewRole);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateInterviewRole(updatedInterviewRole) {
        let inst = this.Instance;
        
        try {
            let res = await inst.post(`/interviewroles`, updatedInterviewRole);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteInterviewRole(interviewid,userid) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/interviewroles/${interviewid}/${userid}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getInterviewRoles()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/interviewroles`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getInterviewRole(interviewid,userid) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/interviewroles/${interviewid}/${userid}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = InterviewRolesDal;