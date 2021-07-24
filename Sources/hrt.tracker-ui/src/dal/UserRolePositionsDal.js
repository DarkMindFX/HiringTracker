


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class UserRolePositionsDal extends DalBase {

    constructor() {
        super();
    }

    async insertUserRolePosition(newUserRolePosition) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/userrolepositions`, newUserRolePosition);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateUserRolePosition(updatedUserRolePosition) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/userrolepositions`, updatedUserRolePosition);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteUserRolePosition(positionid,userid) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/userrolepositions/${positionid}/${userid}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getUserRolePositions()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/userrolepositions`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getUserRolePosition(positionid,userid) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/userrolepositions/${positionid}/${userid}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = UserRolePositionsDal;