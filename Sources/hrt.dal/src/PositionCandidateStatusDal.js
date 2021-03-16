
const { SQLDal } = require('./SQLDal');
const PositionCandidateStatusEntity = require('./entities/PositionCandidateStatus');
const mssql = require('mssql');

class PositionCandidateStatusDal extends SQLDal {

    init(initParams) {
        super.initDal(initParams);        
    }

    async GetAll() {
        let statuses = null;
        await mssql.connect(this._config);

        let records = await super.execStorProcRecordset(mssql, 'p_PositionCandidateStatus_GetAll');

        if(records) {
            statuses = [];
            for(let r in records) {
                let prof = new PositionCandidateStatusEntity();
                prof.StatusID = parseInt(records[r].StatusID);
                prof.Name = records[r].Name;

                statuses.push(prof);
            }
        }

        return statuses;
    }
}

module.exports = PositionCandidateStatusDal;