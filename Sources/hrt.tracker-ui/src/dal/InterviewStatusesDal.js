


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class InterviewStatusesDal extends DalBase {

    constructor() {
        super();
    }

    async insertInterviewStatus(newInterviewStatus) {
        let inst = this.Instance;

        try {
            let res = await inst.put(`/interviewstatuses`, newInterviewStatus);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateInterviewStatus(updatedInterviewStatus) {
        let inst = this.Instance;
        
        try {
            let res = await inst.post(`/interviewstatuses`, updatedInterviewStatus);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteInterviewStatus(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/interviewstatuses/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getInterviewStatuses()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/interviewstatuses`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getInterviewStatus(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/interviewstatuses/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = InterviewStatusesDal;