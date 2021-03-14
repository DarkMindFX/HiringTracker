
const { SQLDal } = require('./SQLDal');
const SkillProficiencyEntity = require('./entities/SkillProficiency');
const mssql = require('mssql');

class SkillProficiencyDal extends SQLDal {

    init(initParams) {
        super.initDal(initParams);        
    }

    async GetAll() {
        let skillProfs = null;
        await mssql.connect(this._config);

        let records = await super.execStorProcRecordset(mssql, 'p_SkillProficiency_GetAll');

        if(records) {
            skillProfs = [];
            for(let r in records) {
                let prof = new SkillProficiencyEntity();
                prof.ProficiencyID = parseInt(records[r].ProficiencyID);
                prof.Name = records[r].Name;

                skillProfs.push(prof);
            }
        }

        return skillProfs;
    }
}

module.exports = SkillProficiencyDal;