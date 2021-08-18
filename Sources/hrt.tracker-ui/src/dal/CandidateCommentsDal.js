


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class CandidateCommentsDal extends DalBase {

    constructor() {
        super();
    }

    async insertCandidateComment(newCandidateComment) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/candidatecomments`, newCandidateComment);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateCandidateComment(updatedCandidateComment) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/candidatecomments`, updatedCandidateComment);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteCandidateComment(candidateid,commentid) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/candidatecomments/${candidateid}/${commentid}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getCandidateComments()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/candidatecomments`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getCandidateCommentsByCandidateID(candidateId)
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/candidatecomments/bycandidate/${candidateId}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getCandidateComment(candidateid,commentid) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/candidatecomments/${candidateid}/${commentid}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = CandidateCommentsDal;