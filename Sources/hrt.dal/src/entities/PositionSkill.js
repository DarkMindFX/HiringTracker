
class PositionSkillEntity {

    get SkillID() { return this._skillId; }
    set SkillID(val) { this._skillId = val; }

    get SkillName() { return this._skillName; }
    set SkillName(val) { this._skillName = val; }

    get PositionID() { return this._positionId; }
    set PositionID(val) { this._positionId = val; }

    get ProficiencyID() { return this._profId; }
    set ProficiencyID(val) { this._profId = val; }

    get ProficiencyName() { return this._proficiency; }
    set ProficiencyName(val) { this._proficiency = val; }

    get IsMandatory() { return this._isMandatory; }
    set IsMandatory(val) { this._isMandatory = val; }
}

module.exports = PositionSkillEntity;