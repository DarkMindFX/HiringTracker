
const { SQLDal } = require('./SQLDal');
const SkillEntity = require('./entities/Skill');
const mssql = require('mssql');

class SkillDal extends SQLDal {

    init(initParams) {
        super.initDal(initParams);        
    }

    async Upsert(skills) {

        let tvp = new mssql.Table();
        tvp.columns.add('SkillID', mssql.BigInt);
        tvp.columns.add('Name', mssql.NVarChar(50));

        for(let s in skills) {
            tvp.rows.add( skills[s].SkillID, skills[s].Name );
        }

        await mssql.connect(this._config);
        let inParams = [];
        inParams['Skills']  = { type: mssql.TVP, value: tvp }; 
        
        super.execStorProcValue(mssql, 'p_Skill_Upsert', inParams);
    }

    async GetAll() {
        let skills = null;        

        await mssql.connect(this._config);

        let records = await super.execStorProcRecordset(mssql, 'p_Skill_GetAll');

        if(records) {
            skills = [];
            for(let r in records) {
                let skill = new SkillEntity();
                skill.SkillID = parseInt(records[r].SkillID);
                skill.Name = records[r].Name;

                skills.push(skill);
            }
        }

        return skills;
    }
}

module.exports = SkillDal;