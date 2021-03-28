

const axios = require('axios')
const constants = require('../constants');

const DalBase = require('./DalBase');

class SkillsDal extends DalBase {

    constructor() {
        super();
        this._skills = null;  // list of skills available
        this._profs = null;   // list of skill proficiencies   
    }

    async loadSkills() {

        const url = this.ApiUrl;
 
        let inst = axios.create({
            baseURL: url
        })

        let res = await inst.get(`/skills`);

        this._skills = {};
        res.data.forEach(r => {
            this._skills[r._skillId] = {
                SkillID: r._skillId,
                Name: r._name
            }
        });
    }

    async loadProficiences() {

        const url = `${constants.HRT_API_HOST}/api/${constants.HRT_API_VERSION}`;
        
        let inst = axios.create({
            baseURL: url
        })

        let res = await inst.get(`/skillproficiencies`);

        this._profs = {};
        res.data.forEach(r => {
            this._profs[r._id] = {
                ProficiencyID : r._id,
                Name: r._name
            }
        });
    }

    async getSkills() {

        if(!this._skills) {
            await this.loadSkills();
        }

        return Object.values(this._skills);
    }

    async getSkill(id) {

        if(!this._skills) {
            await this.loadSkills();
        }

        return this._skills[id];
    }

    async getSkillByName(name) {

        if(!this._skills) {
            await this.loadSkills();
        }

        let result = Object.values(this._skills).find( s => s.Name == name)

        return result;
    }

    async getProficiencyByName(name) {
        
        if(!this._profs) {
            await this.loadProficiences();
        }

        let result = Object.values(this._profs).find( s => s.Name == name)

        return result;
    }

    async getProficiencies()
    {
        if(!this._profs) {
            await this.loadProficiences();
        }

        return Object.values(this._profs);
    }

    async getProficiency(id)
    {
        if(!this._profs) {
            await this.loadProficiences();
        }

        return this._profs[id];
    }
}

module.exports = SkillsDal;