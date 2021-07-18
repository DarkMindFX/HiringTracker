

const HateosDto = require('./HateosDto')

class ProposalDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get positionid() { return this.PositionID; }
		set positionid(val) { this.PositionID = val; }

		
		get candidateid() { return this.CandidateID; }
		set candidateid(val) { this.CandidateID = val; }

		
		get proposed() { return this.Proposed; }
		set proposed(val) { this.Proposed = val; }

		
		get currentstepid() { return this.CurrentStepID; }
		set currentstepid(val) { this.CurrentStepID = val; }

		
		get stepsetdate() { return this.StepSetDate; }
		set stepsetdate(val) { this.StepSetDate = val; }

		
		get nextstepid() { return this.NextStepID; }
		set nextstepid(val) { this.NextStepID = val; }

		
		get duedate() { return this.DueDate; }
		set duedate(val) { this.DueDate = val; }

		
		get statusid() { return this.StatusID; }
		set statusid(val) { this.StatusID = val; }

		
		get createdbyid() { return this.CreatedByID; }
		set createdbyid(val) { this.CreatedByID = val; }

		
		get createddate() { return this.CreatedDate; }
		set createddate(val) { this.CreatedDate = val; }

		
		get modifiedbyid() { return this.ModifiedByID; }
		set modifiedbyid(val) { this.ModifiedByID = val; }

		
		get modifieddate() { return this.ModifiedDate; }
		set modifieddate(val) { this.ModifiedDate = val; }

				
}

module.exports = ProposalDto;