
const { SQLDal } = require('./SQLDal');
const InterviewStatusEntity = require('./entities/InterviewStatus');
const mssql = require('mssql');

class InterviewStatusDal extends SQLDal {

    init(initParams) {
        super.initDal(initParams);        
    }

    async GetAll() {
        let statuses = null;
        await mssql.connect(this._config);

        let records = await super.execStorProcRecordset(mssql, 'p_InterviewStatus_GetAll');

        if(records) {
            statuses = [];
            for(let r in records) {
                let prof = new InterviewStatusEntity();
                prof.StatusID = parseInt(records[r].StatusID);
                prof.Name = records[r].Name;

                statuses.push(prof);
            }
        }

        return statuses;
    }
}

module.exports = InterviewStatusDal;