
class CandidateSkillEntity {

    get SkillID() { return this._skillId; }
    set SkillID(val) { this._skillId = val; }

    get SkillName() { return this._skillName; }
    set SkillName(val) { this._skillName = val; }

    get CandidateID() { return this._CandidateId; }
    set CandidateID(val) { this._CandidateId = val; }

    get ProficiencyID() { return this._profId; }
    set ProficiencyID(val) { this._profId = val; }

    get ProficiencyName() { return this._proficiency; }
    set ProficiencyName(val) { this._proficiency = val; }

    get IsMandatory() { return this._isMandatory; }
    set IsMandatory(val) { this._isMandatory = val; }
}

module.exports = CandidateSkillEntity;