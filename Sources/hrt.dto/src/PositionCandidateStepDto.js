

class PositionCandidateStepDto {

    get StepID() { return this._stepId; }
    set StepID(val) { this._stepId = val; }

    get Name() { return this._name; }
    set Name(val) { this._name = val; }

    get ReqDueDate() { return this._reqDueDate; }
    set ReqDueDate(val) { this._reqDueDate = val; }

    get RequiresRespInDays() { return this._requiresRespInDays; }
    set RequiresRespInDays(val) { this._requiresRespInDays = val; }
}

module.exports = PositionCandidateStepDto;