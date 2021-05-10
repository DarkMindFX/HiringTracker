
const HateosDto = require('./HateosDto')

class CandidateDto extends HateosDto {

    get CandidateID() { return this._candidateId; }
    set CandidateID(val) { this._candidateId = val; }

    get FirstName() { return this._fname; }
    set FirstName(val) { this._fname = val; }

    get MiddleName() { return this._mname; }
    set MiddleName(val) { this._mname = val; }

    get LastName() { return this._lname; }
    set LastName(val) { this._lname = val; }

    get Email() { return this._email; }
    set Email(val) { this._email = val; }

    get Phone() { return this._phone; }
    set Phone(val) { this._phone = val; }

    get CVLink() { return this._cvlink; }
    set CVLink(val) { this._cvlink = val; }

    get CreatedDate() { return this._createdDate; }
    set CreatedDate(val) { this._createdDate = val; }

    // UserDto
    get CreatedBy() { return this._createdBy; }
    set CreatedBy(val) { this._createdBy = val; }

    get ModifiedDate() { return this._modifiedDate; }
    set ModifiedDate(val) { this._modifiedDate = val; }

    // UserDto
    get ModifiedBy() { return this._modifiedBy; }
    set ModifiedBy(val) { this._modifiedBy = val; }
}

module.exports = CandidateDto;