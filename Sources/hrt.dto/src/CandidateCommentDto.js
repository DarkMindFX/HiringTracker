

const HateosDto = require('./HateosDto')

class CandidateCommentDto extends HateosDto {
		
		get candidateid() { return this.CandidateID; }
		set candidateid(val) { this.CandidateID = val; }

		
		get commentid() { return this.CommentID; }
		set commentid(val) { this.CommentID = val; }

				
}

module.exports = CandidateCommentDto;