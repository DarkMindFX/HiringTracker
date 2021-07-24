


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class InterviewFeedbacksDal extends DalBase {

    constructor() {
        super();
    }

    async insertInterviewFeedback(newInterviewFeedback) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/interviewfeedbacks`, newInterviewFeedback);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateInterviewFeedback(updatedInterviewFeedback) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/interviewfeedbacks`, updatedInterviewFeedback);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteInterviewFeedback(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/interviewfeedbacks/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getInterviewFeedbacks()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/interviewfeedbacks`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getInterviewFeedback(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/interviewfeedbacks/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = InterviewFeedbacksDal;