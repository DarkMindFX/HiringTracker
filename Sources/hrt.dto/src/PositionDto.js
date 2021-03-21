
class PositionDto {

    get PositionID() { return this._positionId; }
    set PositionID(val) { this._positionId = val; }

    get Title() { return this._title; }
    set Title(val) { this._title = val; }

    get ShortDesc() { return this._shortDesc; }
    set ShortDesc(val) { this._shortDesc = val; }

    get Desc() { return this._desc; }
    set Desc(val) { this._desc = val; }

    // PositionStatusDto
    get Status() { return this._status; }
    set Status(val) { this._status = val; }

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

module.exports = PositionDto;