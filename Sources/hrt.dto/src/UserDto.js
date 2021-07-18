

const HateosDto = require('./HateosDto')

class UserDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get login() { return this.Login; }
		set login(val) { this.Login = val; }

		
		get firstname() { return this.FirstName; }
		set firstname(val) { this.FirstName = val; }

		
		get lastname() { return this.LastName; }
		set lastname(val) { this.LastName = val; }

		
		get email() { return this.Email; }
		set email(val) { this.Email = val; }

		
		get description() { return this.Description; }
		set description(val) { this.Description = val; }

		
		get password() { return this.Password; }
		set password(val) { this.Password = val; }
}

module.exports = UserDto;