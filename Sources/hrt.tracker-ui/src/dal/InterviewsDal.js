


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class InterviewsDal extends DalBase {

    constructor() {
        super();
    }

    async insertInterview(newInterview) {
        let inst = this.Instance;

        try {
            let res = await inst.put(`/interviews`, newInterview);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateInterview(updatedInterview) {
        let inst = this.Instance;
        
        try {
            let res = await inst.post(`/interviews`, updatedInterview);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteInterview(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/interviews/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getInterviews()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/interviews`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getInterview(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/interviews/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = InterviewsDal;