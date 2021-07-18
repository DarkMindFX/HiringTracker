

const HateosDto = require('./HateosDto')

class UserRolePositionDto extends HateosDto {
		
		get positionid() { return this.PositionID; }
		set positionid(val) { this.PositionID = val; }

		
		get userid() { return this.UserID; }
		set userid(val) { this.UserID = val; }

		
		get roleid() { return this.RoleID; }
		set roleid(val) { this.RoleID = val; }

				
}

module.exports = UserRolePositionDto;