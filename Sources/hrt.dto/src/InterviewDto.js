

const HateosDto = require('./HateosDto')

class InterviewDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get proposalid() { return this.ProposalID; }
		set proposalid(val) { this.ProposalID = val; }

		
		get interviewtypeid() { return this.InterviewTypeID; }
		set interviewtypeid(val) { this.InterviewTypeID = val; }

		
		get starttime() { return this.StartTime; }
		set starttime(val) { this.StartTime = val; }

		
		get endtime() { return this.EndTime; }
		set endtime(val) { this.EndTime = val; }

		
		get interviewstatusid() { return this.InterviewStatusID; }
		set interviewstatusid(val) { this.InterviewStatusID = val; }

		
		get createdbyid() { return this.CreatedByID; }
		set createdbyid(val) { this.CreatedByID = val; }

		
		get creteddate() { return this.CretedDate; }
		set creteddate(val) { this.CretedDate = val; }

		
		get modifiedbyid() { return this.ModifiedByID; }
		set modifiedbyid(val) { this.ModifiedByID = val; }

		
		get modifieddate() { return this.ModifiedDate; }
		set modifieddate(val) { this.ModifiedDate = val; }

				
}

module.exports = InterviewDto;