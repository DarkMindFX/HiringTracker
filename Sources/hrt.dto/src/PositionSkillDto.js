


const HateosDto = require('./HateosDto')

class PositionSkillDto extends HateosDto {

    get Skill() { return this._skill; }
    set Skill(val) { this._skill = val; }

    get Proficiency() { return this._proficiency; }
    set Proficiency(val) { this._proficiency = val; }

    get IsMandatory() { return this._isMandatory; }
    set IsMandatory(val) { this._isMandatory = val; }
}

module.exports = PositionSkillDto;