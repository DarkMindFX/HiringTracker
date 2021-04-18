
const { SQLDal } = require('./SQLDal');
const PositionEntity = require('./entities/Position');
const PositionSkillEntity = require('./entities/PositionSkill');
const mssql = require('mssql');

class PositionDal extends SQLDal {

    init(initParams) {
        super.initDal(initParams);        
    }

    async GetDetails(positionId) {
        let position = null;

        await mssql.connect(this._config);

        let inParams = {};
        inParams['PositionID'] = { value: positionId, type: mssql.BigInt };

        let outParams = {};
        outParams['Found'] = { value: null, type: mssql.Bit };

        let records = await super.execStorProcRecordset(mssql, 'p_Position_GetDetails', inParams, outParams);

        if(records && records.length > 0) {

            let record = records[0];

            position = this._recordToEntity(record);
        }

        return position;
    }

    async GetAll() {
        let positions = null;

        await mssql.connect(this._config);

        let records = await super.execStorProcRecordset(mssql, 'p_Position_GetAll');

        if(records) {
            positions = [];

            for(let r in records) {
                let record = records[r];

                let position = this._recordToEntity(record);

                positions.push(position);
            }
        }

        return positions;
    }

    async Upsert(position, editorId) {

        await mssql.connect(this._config);

        let inParams = {};
        inParams['PositionID'] = { value: position.PositionID, type: mssql.BigInt };
        inParams['DepartmentID'] = { value: position.DepartmentID, type: mssql.BigInt };
        inParams['Title'] = { value: position.Title, type: mssql.NVarChar(50) };
        inParams['ShortDesc'] = { value: position.ShortDesc, type: mssql.NVarChar(250) };
        inParams['Description'] = { value: position.Desc, type: mssql.NVarChar(mssql.MAX) };
        inParams['StatusID'] = { value: position.StatusID, type: mssql.BigInt };
        inParams['ChangedByUserID'] = { value: editorId, type: mssql.BigInt };

        let outParams = {};
        outParams['NewPositionID'] = { value: null, type: mssql.BigInt };

        let positionId = await super.execStorProcValue(mssql, 'p_Position_Upsert', inParams, outParams);

        return positionId;
    }

    async Delete(positionId, editorId) {

        await mssql.connect(this._config);

        let inParams = {};
        inParams['PositionID'] = { value: positionId, type: mssql.BigInt };
        
        let outParams = {};
        outParams['Removed'] = { value: null, type: mssql.Bit };

        let removed = await super.execStorProcValue(mssql, 'p_Position_Delete', inParams, outParams);

        return removed;
    }

    async GetSkills(positionId) {

        let posSkills = null;

        await mssql.connect(this._config);

        let inParams = {};
        inParams['PositionID'] = { value: positionId, type: mssql.BigInt };

        let outParams = {};
        outParams['Found'] = { value: null, type: mssql.Bit };

        let records = await super.execStorProcRecordset(mssql, 'p_PositionSkills_GetByPosition', inParams, outParams);

        if(records) {
            posSkills = [];

            for(let r in records) {

                let record = records[r];

                let ps = new PositionSkillEntity();
                ps.SkillID = parseInt(record['SkillID']);
                ps.ProficiencyID = parseInt(record['SkillProficiencyID']);
                ps.IsMandatory = record['IsMandatory'];
                ps.SkillName = record['SkillName'];
                ps.ProficiencyName = record['SkillName'];

                posSkills.push(ps);

            }
        }

        return posSkills;
    }

    async SetSkills(positionId, skills) {

        const tvpSkills = new mssql.Table();
        tvpSkills.columns.add('SkillID', mssql.BigInt);
        tvpSkills.columns.add('IsMandatory', mssql.Bit);
        tvpSkills.columns.add('ProficiencyID', mssql.BigInt);
        
        skills.forEach(s => {            
            tvpSkills.rows.add(s.SkillID, s.IsMandatory, s.ProficiencyID);
        });

        await mssql.connect(this._config);

        let inParams = {};
        inParams['PositionID'] = { value: positionId, type: mssql.BigInt };
        inParams['Skills'] = { value: tvpSkills, type: mssql.TVP };

        super.execStorProcValue(mssql, 'p_PositionSkills_Upsert', inParams);
    }

    _recordToEntity(record) {
        let position = new PositionEntity();
        position.PositionID = parseInt(record['PositionID'])
        position.DepartmentID = record['DepartmentID'] ? parseInt(record['DepartmentID']) : null;
        position.Title = record['Title'];
        position.ShortDesc = record['ShortDesc'];
        position.Desc = record['Description'];
        position.StatusID = parseInt(record['StatusID'])
        position.CreatedDate = new Date(Date.parse(record['CreatedDate']))
        position.CreatedByID = parseInt(record['CreatedByID'])
        position.ModifiedDate = record['ModifiedDate'] ? new Date(Date.parse(record['ModifiedDate'])) : null
        position.ModifiedByID = record['ModifiedByID'] ? parseInt(record['ModifiedByID']) : null

        return position;
    }

}

module.exports = PositionDal;