const { SQLDal } = require('./SQLDal');
const CandidateEntity = require('./entities/Candidate');
const CandidateSkillEntity = require('./entities/CandidateSkill');
const mssql = require('mssql');

class CandidateDal extends SQLDal {

    init(initParams) {
        super.initDal(initParams);        
    }

    async GetDetails(candidateId) {
        let candidate = null;

        await mssql.connect(this._config);

        let inParams = {};
        inParams['CandidateID'] = { value: candidateId, type: mssql.BigInt };

        let outParams = {};
        outParams['Found'] = { value: null, type: mssql.Bit };

        let records = await super.execStorProcRecordset(mssql, 'p_Candidate_GetDetails', inParams, outParams);

        if(records && records.length > 0) {
            let record = records[0];

            candidate = this._recordToEntity(record);
        }

        return candidate;
    }

    async GetAll() {
        let candidates = null;

        await mssql.connect(this._config);

        let records = await super.execStorProcRecordset(mssql, 'p_Candidate_GetAll');

        if(records) {
            candidates = [];

            for(let r in records) {
                let record = records[r];

                let candidate = this._recordToEntity(record);

                candidates.push(candidate);
            }
        }

        return candidates;
    }

    async Delete(candidateId, editorId) {

        await mssql.connect(this._config);

        let inParams = {};
        inParams['CandidateID'] = { value: candidateId, type: mssql.BigInt };
        
        let outParams = {};
        outParams['Removed'] = { value: null, type: mssql.Bit };

        let removed = await super.execStorProcValue(mssql, 'p_Candidate_Delete', inParams, outParams);

        return removed;
    }

    async GetSkills(candidateId) {

        let skills = null;

        await mssql.connect(this._config);

        let inParams = {};
        inParams['CandidateID'] = { value: candidateId, type: mssql.BigInt };

        let outParams = {};
        outParams['Found'] = { value: null, type: mssql.Bit };

        let records = await super.execStorProcRecordset(mssql, 'p_CandidateSkills_GetByCandidate', inParams, outParams);

        if(records) {
            skills = [];

            for(let r in records) {

                let record = records[r];

                let ps = new CandidateSkillEntity();
                ps.SkillID = parseInt(record['SkillID']);
                ps.ProficiencyID = parseInt(record['SkillProficiencyID']);
                ps.IsMandatory = record['IsMandatory'];
                ps.SkillName = record['SkillName'];
                ps.ProficiencyName = record['SkillName'];

                skills.push(ps);

            }
        }

        return skills;
    }

    async Upsert(candidate, editorId) {

        await mssql.connect(this._config);

        let inParams = {};
        inParams['CandidateID'] = { value: candidate.CandidateID, type: mssql.BigInt };
        inParams['FirstName'] = { value: candidate.FirstName, type: mssql.NVarChar(50) };
        inParams['MiddleName'] = { value: candidate.MiddleName, type: mssql.NVarChar(50) };
        inParams['LastName'] = { value: candidate.LastName, type: mssql.NVarChar(50) };
        inParams['Email'] = { value: candidate.Email, type: mssql.NVarChar(50) };
        inParams['Phone'] = { value: candidate.Phone, type: mssql.NVarChar(50) };
        inParams['CVLink'] = { value: candidate.CVLink, type: mssql.NVarChar(1000) };
        inParams['ChangedByUserID'] = { value: editorId, type: mssql.BigInt };

        let outParams = {};
        outParams['NewCandidateID'] = { value: null, type: mssql.BigInt };

        let candidateId = await super.execStorProcValue(mssql, 'p_Candidate_Upsert', inParams, outParams);

        return candidateId;
    }

    async SetSkills(candidateId, skills) {

        const tvpSkills = new mssql.Table();
        tvpSkills.columns.add('SkillID', mssql.BigInt);
        tvpSkills.columns.add('IsMandatory', mssql.Bit);
        tvpSkills.columns.add('ProficiencyID', mssql.BigInt);
        
        skills.forEach(s => {            
            tvpSkills.rows.add(s.SkillID, s.IsMandatory, s.ProficiencyID);
        });

        await mssql.connect(this._config);

        let inParams = {};
        inParams['CandidateID'] = { value: candidateId, type: mssql.BigInt };
        inParams['Skills'] = { value: tvpSkills, type: mssql.TVP };

        super.execStorProcValue(mssql, 'p_CandidateSkills_Upsert', inParams);
    }

    _recordToEntity(record) {
        let entity = new CandidateEntity();
        entity.CandidateID = parseInt(record['CandidateID']);
        entity.FirstName = record['FirstName'];
        entity.MiddleName = record['MiddleName'];
        entity.LastName = record['LastName'];
        entity.Email = record['Email'];
        entity.Phone = record['Phone'];
        entity.CVLink = record['CVLink'];
        entity.CreatedByID = parseInt(record['CreatedByID']);
        entity.CreatedDate = new Date(Date.parse(record['CreatedDate']));
        entity.ModifiedDate = record['ModifiedDate'] ? new Date(Date.parse(record['ModifiedDate'])) : null
        entity.ModifiedByID = record['ModifiedByID'] ? parseInt(record['ModifiedByID']) : null

        return entity;
    }



}

module.exports = CandidateDal;