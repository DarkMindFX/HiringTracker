

const HateosDto = require('./HateosDto')

class PositionDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get departmentid() { return this.DepartmentID; }
		set departmentid(val) { this.DepartmentID = val; }

		
		get title() { return this.Title; }
		set title(val) { this.Title = val; }

		
		get shortdesc() { return this.ShortDesc; }
		set shortdesc(val) { this.ShortDesc = val; }

		
		get description() { return this.Description; }
		set description(val) { this.Description = val; }

		
		get statusid() { return this.StatusID; }
		set statusid(val) { this.StatusID = val; }

		
		get createddate() { return this.CreatedDate; }
		set createddate(val) { this.CreatedDate = val; }

		
		get createdbyid() { return this.CreatedByID; }
		set createdbyid(val) { this.CreatedByID = val; }

		
		get modifieddate() { return this.ModifiedDate; }
		set modifieddate(val) { this.ModifiedDate = val; }

		
		get modifiedbyid() { return this.ModifiedByID; }
		set modifiedbyid(val) { this.ModifiedByID = val; }

				
}

module.exports = PositionDto;