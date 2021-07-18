

const HateosDto = require('./HateosDto')

class PositionCommentDto extends HateosDto {
		
		get positionid() { return this.PositionID; }
		set positionid(val) { this.PositionID = val; }

		
		get commentid() { return this.CommentID; }
		set commentid(val) { this.CommentID = val; }

				
}

module.exports = PositionCommentDto;