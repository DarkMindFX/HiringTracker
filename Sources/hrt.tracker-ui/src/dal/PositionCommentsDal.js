


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class PositionCommentsDal extends DalBase {

    constructor() {
        super();
    }

    async insertPositionComment(newPositionComment) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/positioncomments`, newPositionComment);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updatePositionComment(updatedPositionComment) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/positioncomments`, updatedPositionComment);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deletePositionComment(positionid,commentid) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/positioncomments/${positionid}/${commentid}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getPositionComments()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/positioncomments`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getPositionComment(positionid,commentid) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/positioncomments/${positionid}/${commentid}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = PositionCommentsDal;