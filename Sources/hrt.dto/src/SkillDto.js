

const HateosDto = require('./HateosDto')

class SkillDto extends HateosDto {

    get SkillID() { return this._skillId; }
    set SkillID(val) { this._skillId = val; }

    get Name() { return this._name; }
    set Name(val) { this._name = val; }
}

module.exports = SkillDto;