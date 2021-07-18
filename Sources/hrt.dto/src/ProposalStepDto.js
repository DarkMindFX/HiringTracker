

const HateosDto = require('./HateosDto')

class ProposalStepDto extends HateosDto {
		
		get id() { return this.ID; }
		set id(val) { this.ID = val; }

		
		get name() { return this.Name; }
		set name(val) { this.Name = val; }

		
		get reqduedate() { return this.ReqDueDate; }
		set reqduedate(val) { this.ReqDueDate = val; }

		
		get requiresrespindays() { return this.RequiresRespInDays; }
		set requiresrespindays(val) { this.RequiresRespInDays = val; }

				
}

module.exports = ProposalStepDto;