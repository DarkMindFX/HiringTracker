

const HateosDto = require('./HateosDto')

class RoleDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get name() { return this.Name; }
		set name(val) { this.Name = val; }

				
}

module.exports = RoleDto;