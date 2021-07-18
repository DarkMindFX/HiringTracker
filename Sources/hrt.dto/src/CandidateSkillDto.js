

const HateosDto = require('./HateosDto')

class CandidateSkillDto extends HateosDto {
		
		get candidateid() { return this.CandidateID; }
		set candidateid(val) { this.CandidateID = val; }

		
		get skillid() { return this.SkillID; }
		set skillid(val) { this.SkillID = val; }

		
		get skillproficiencyid() { return this.SkillProficiencyID; }
		set skillproficiencyid(val) { this.SkillProficiencyID = val; }

				
}

module.exports = CandidateSkillDto;