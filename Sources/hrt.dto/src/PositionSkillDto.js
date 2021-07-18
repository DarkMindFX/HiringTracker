

const HateosDto = require('./HateosDto')

class PositionSkillDto extends HateosDto {
		
		get positionid() { return this.PositionID; }
		set positionid(val) { this.PositionID = val; }

		
		get skillid() { return this.SkillID; }
		set skillid(val) { this.SkillID = val; }

		
		get ismandatory() { return this.IsMandatory; }
		set ismandatory(val) { this.IsMandatory = val; }

		
		get skillproficiencyid() { return this.SkillProficiencyID; }
		set skillproficiencyid(val) { this.SkillProficiencyID = val; }

				
}

module.exports = PositionSkillDto;