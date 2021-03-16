
const { SQLDal } = require('./SQLDal');
const PositionStatusEntity = require('./entities/PositionStatus');
const mssql = require('mssql');

class PositionStatusDal extends SQLDal {

    init(initParams) {
        super.initDal(initParams);        
    }

    async GetAll() {
        let statuses = null;
        await mssql.connect(this._config);

        let records = await super.execStorProcRecordset(mssql, 'p_PositionStatus_GetAll');

        if(records) {
            statuses = [];
            for(let r in records) {
                let prof = new PositionStatusEntity();
                prof.StatusID = parseInt(records[r].StatusID);
                prof.Name = records[r].Name;

                statuses.push(prof);
            }
        }

        return statuses;
    }
}

module.exports = PositionStatusDal;