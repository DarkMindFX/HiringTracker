

const HateosDto = require('./HateosDto')

class DepartmentDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get name() { return this.Name; }
		set name(val) { this.Name = val; }

		
		get uuid() { return this.UUID; }
		set uuid(val) { this.UUID = val; }

		
		get parentid() { return this.ParentID; }
		set parentid(val) { this.ParentID = val; }

		
		get managerid() { return this.ManagerID; }
		set managerid(val) { this.ManagerID = val; }

				
}

module.exports = DepartmentDto;