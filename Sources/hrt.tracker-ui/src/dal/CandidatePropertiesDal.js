


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class CandidatePropertiesDal extends DalBase {

    constructor() {
        super();
    }

    async insertCandidateProperty(newCandidateProperty) {
        let inst = this.Instance;

        try {
            let res = await inst.put(`/candidateproperties`, newCandidateProperty);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateCandidateProperty(updatedCandidateProperty) {
        let inst = this.Instance;
        
        try {
            let res = await inst.post(`/candidateproperties`, updatedCandidateProperty);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteCandidateProperty(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/candidateproperties/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getCandidateProperties()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/candidateproperties`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getCandidateProperty(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/candidateproperties/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = CandidatePropertiesDal;