

const HateosDto = require('./HateosDto')

class CandidatePropertyDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get name() { return this.Name; }
		set name(val) { this.Name = val; }

		
		get value() { return this.Value; }
		set value(val) { this.Value = val; }

		
		get candidateid() { return this.CandidateID; }
		set candidateid(val) { this.CandidateID = val; }

				
}

module.exports = CandidatePropertyDto;