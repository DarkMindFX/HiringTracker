

const HateosDto = require('./HateosDto')

class UserRoleCandidateDto extends HateosDto {
		
		get candidateid() { return this.CandidateID; }
		set candidateid(val) { this.CandidateID = val; }

		
		get userid() { return this.UserID; }
		set userid(val) { this.UserID = val; }

		
		get roleid() { return this.RoleID; }
		set roleid(val) { this.RoleID = val; }

				
}

module.exports = UserRoleCandidateDto;