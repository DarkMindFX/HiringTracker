

const HateosDto = require('./HateosDto')

class SkillProficiencyDto extends HateosDto {

    get ProficiencyID() { return this._id; }
    set ProficiencyID(val) { this._id = val; }

    get Name() { return this._name; }
    set Name(val) { this._name = val; }
}

module.exports = SkillProficiencyDto;