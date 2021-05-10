

class CandidateEntity {

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

    get CreatedByID() { return this._createdById; }
    set CreatedByID(val) { this._createdById = val; }

    get ModifiedDate() { return this._modifiedDate; }
    set ModifiedDate(val) { this._modifiedDate = val; }

    get ModifiedByID() { return this._modifiedById; }
    set ModifiedByID(val) { this._modifiedById = val; }
}

module.exports = CandidateEntity;