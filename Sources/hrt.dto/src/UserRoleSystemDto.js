

const HateosDto = require('./HateosDto')

class UserRoleSystemDto extends HateosDto {
		
		get userid() { return this.UserID; }
		set userid(val) { this.UserID = val; }

		
		get roleid() { return this.RoleID; }
		set roleid(val) { this.RoleID = val; }

				
}

module.exports = UserRoleSystemDto;