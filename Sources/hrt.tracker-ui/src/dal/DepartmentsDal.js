


const axios = require('axios');
const constants = require('../constants');

const DalBase = require('./DalBase');


class DepartmentsDal extends DalBase {

    constructor() {
        super();
    }

    async insertDepartment(newDepartment) {
        let inst = this.Instance;

        try {
            let res = await inst.post(`/departments`, newDepartment);

            return res;
        }
        catch(error) {
            console.log(error.response);
            return error.response;
        }
    }

    async updateDepartment(updatedDepartment) {
        let inst = this.Instance;
        
        try {
            let res = await inst.put(`/departments`, updatedDepartment);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async deleteDepartment(id) {
        let inst = this.Instance;

        try {
            let res = await inst.delete(`/departments/${id}`);

            return res;        
        }
        catch(error) {
            return error.response;
        }
    }

    async getDepartments()
    {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/departments`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }

    async getDepartment(id) {
        let inst = this.Instance;

        try {
            let res = await inst.get(`/departments/${id}`);

            return res;
        }
        catch(error) {
            return error.response;
        }
    }
}

module.exports = DepartmentsDal;