

const axios = require('axios')
const constants = require('../constants');
const DalBase = require('./DalBase');

class PositionsDal extends DalBase {

    constructor() {
        super();        
    }

    async getPositions() {

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

    async getPositionSkills(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/positions/${id}/skills`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async addPosition(positionUpsertDto) {
        let inst = this.Instance;

        try {
            let res = await inst.put(`/positions`, positionUpsertDto);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async updatePosition(positionUpsertDto) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/positions`, positionUpsertDto);

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
}

module.exports = PositionsDal;