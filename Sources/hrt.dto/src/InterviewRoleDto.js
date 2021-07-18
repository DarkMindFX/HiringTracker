

const HateosDto = require('./HateosDto')

class InterviewRoleDto extends HateosDto {
		
		get interviewid() { return this.InterviewID; }
		set interviewid(val) { this.InterviewID = val; }

		
		get userid() { return this.UserID; }
		set userid(val) { this.UserID = val; }

		
		get roleid() { return this.RoleID; }
		set roleid(val) { this.RoleID = val; }

				
}

module.exports = InterviewRoleDto;