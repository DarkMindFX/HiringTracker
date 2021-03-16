
const { SQLDal } = require('./SQLDal');
const PositionCandidateStepEntity = require('./entities/PositionCandidateStep');
const mssql = require('mssql');

class PositionCandidateStepDal extends SQLDal {

    init(initParams) {
        super.initDal(initParams);        
    }

    async GetAll() {
        let statuses = null;
        await mssql.connect(this._config);

        let records = await super.execStorProcRecordset(mssql, 'p_PositionCandidateStep_GetAll');

        if(records) {
            statuses = [];
            for(let r in records) {
                let step = new PositionCandidateStepEntity();
                step.StepID = parseInt(records[r].StepID);
                step.Name = records[r].Name;
                step.ReqDueDate = records[r].ReqDueDate;
                step.RequiresRespInDays = records[r].RequiresRespInDays;

                statuses.push(step);
            }
        }

        return statuses;
    }
}

module.exports = PositionCandidateStepDal;