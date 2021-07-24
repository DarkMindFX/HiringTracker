


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class PositionsDal extends DalBase {

    constructor() {
        super();
    }

    async insertPosition(newPosition) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/positions`, newPosition);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updatePosition(updatedPosition) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/positions`, updatedPosition);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deletePosition(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/positions/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getPositions()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/positions`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getPosition(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/positions/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = PositionsDal;