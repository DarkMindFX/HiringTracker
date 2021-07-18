

const HateosDto = require('./HateosDto')

class InterviewFeedbackDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get comment() { return this.Comment; }
		set comment(val) { this.Comment = val; }

		
		get rating() { return this.Rating; }
		set rating(val) { this.Rating = val; }

		
		get interviewid() { return this.InterviewID; }
		set interviewid(val) { this.InterviewID = val; }

		
		get interviewerid() { return this.InterviewerID; }
		set interviewerid(val) { this.InterviewerID = val; }

		
		get createdbyid() { return this.CreatedByID; }
		set createdbyid(val) { this.CreatedByID = val; }

		
		get createddate() { return this.CreatedDate; }
		set createddate(val) { this.CreatedDate = val; }

		
		get modifiedbyid() { return this.ModifiedByID; }
		set modifiedbyid(val) { this.ModifiedByID = val; }

		
		get modifieddate() { return this.ModifiedDate; }
		set modifieddate(val) { this.ModifiedDate = val; }

				
}

module.exports = InterviewFeedbackDto;