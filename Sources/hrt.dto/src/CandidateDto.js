

const HateosDto = require('./HateosDto')

class CandidateDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get firstname() { return this.FirstName; }
		set firstname(val) { this.FirstName = val; }

		
		get middlename() { return this.MiddleName; }
		set middlename(val) { this.MiddleName = val; }

		
		get lastname() { return this.LastName; }
		set lastname(val) { this.LastName = val; }

		
		get email() { return this.Email; }
		set email(val) { this.Email = val; }

		
		get phone() { return this.Phone; }
		set phone(val) { this.Phone = val; }

		
		get cvlink() { return this.CVLink; }
		set cvlink(val) { this.CVLink = val; }

		
		get createdbyid() { return this.CreatedByID; }
		set createdbyid(val) { this.CreatedByID = val; }

		
		get createddate() { return this.CreatedDate; }
		set createddate(val) { this.CreatedDate = val; }

		
		get modifiedbyid() { return this.ModifiedByID; }
		set modifiedbyid(val) { this.ModifiedByID = val; }

		
		get modifieddate() { return this.ModifiedDate; }
		set modifieddate(val) { this.ModifiedDate = val; }

				
}

module.exports = CandidateDto;