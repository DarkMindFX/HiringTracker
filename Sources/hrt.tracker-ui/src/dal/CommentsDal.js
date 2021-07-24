


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class CommentsDal extends DalBase {

    constructor() {
        super();
    }

    async insertComment(newComment) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/comments`, newComment);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateComment(updatedComment) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/comments`, updatedComment);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteComment(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/comments/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getComments()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/comments`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getComment(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/comments/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = CommentsDal;