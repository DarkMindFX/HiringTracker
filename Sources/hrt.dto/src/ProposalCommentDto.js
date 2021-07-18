

const HateosDto = require('./HateosDto')

class ProposalCommentDto extends HateosDto {
		
		get proposalid() { return this.ProposalID; }
		set proposalid(val) { this.ProposalID = val; }

		
		get commentid() { return this.CommentID; }
		set commentid(val) { this.CommentID = val; }

				
}

module.exports = ProposalCommentDto;