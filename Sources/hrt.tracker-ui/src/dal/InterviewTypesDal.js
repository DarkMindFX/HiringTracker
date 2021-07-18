


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class InterviewTypesDal extends DalBase {

    constructor() {
        super();
    }

    async insertInterviewType(newInterviewType) {
        let inst = this.Instance;

        try {
            let res = await inst.put(`/interviewtypes`, newInterviewType);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateInterviewType(updatedInterviewType) {
        let inst = this.Instance;
        
        try {
            let res = await inst.post(`/interviewtypes`, updatedInterviewType);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteInterviewType(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/interviewtypes/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getInterviewTypes()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/interviewtypes`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getInterviewType(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/interviewtypes/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = InterviewTypesDal;