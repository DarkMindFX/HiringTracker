


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class ProposalCommentsDal extends DalBase {

    constructor() {
        super();
    }

    async insertProposalComment(newProposalComment) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/proposalcomments`, newProposalComment);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateProposalComment(updatedProposalComment) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/proposalcomments`, updatedProposalComment);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteProposalComment(proposalid,commentid) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/proposalcomments/${proposalid}/${commentid}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getProposalComments()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/proposalcomments`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getProposalComment(proposalid,commentid) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/proposalcomments/${proposalid}/${commentid}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = ProposalCommentsDal;