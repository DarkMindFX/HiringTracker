
class PositionEntity {

    get PositionID() { return this._positionId; }
    set PositionID(val) { this._positionId = val; }

    get Title() { return this._title; }
    set Title(val) { this._title = val; }

    get ShortDesc() { return this._shortDesc; }
    set ShortDesc(val) { this._shortDesc = val; }

    get Desc() { return this._desc; }
    set Desc(val) { this._desc = val; }

    get StatusID() { return this._statusId; }
    set StatusID(val) { this._statusId = val; }

    get CreatedDate() { return this._createdDate; }
    set CreatedDate(val) { this._createdDate = val; }

    get CreatedByID() { return this._createdById; }
    set CreatedByID(val) { this._createdById = val; }

    get ModifiedDate() { return this._modifiedDate; }
    set ModifiedDate(val) { this._modifiedDate = val; }

    get ModifiedByID() { return this._modifiedById; }
    set ModifiedByID(val) { this._modifiedById = val; }


}

module.exports = PositionEntity;