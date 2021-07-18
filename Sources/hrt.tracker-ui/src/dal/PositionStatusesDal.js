


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class PositionStatusesDal extends DalBase {

    constructor() {
        super();
    }

    async insertPositionStatus(newPositionStatus) {
        let inst = this.Instance;

        try {
            let res = await inst.put(`/positionstatuses`, newPositionStatus);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updatePositionStatus(updatedPositionStatus) {
        let inst = this.Instance;
        
        try {
            let res = await inst.post(`/positionstatuses`, updatedPositionStatus);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deletePositionStatus(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/positionstatuses/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getPositionStatuses()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/positionstatuses`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getPositionStatus(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/positionstatuses/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = PositionStatusesDal;