
class PositionUpsertDto {

    get Position() { return this._position; }
    set Position(val) { this._position = val; }

    get Skills() { return this._skills; }
    set Skills(val) { this._skills = val; }

}

class PositionUpsertResponseDto {

    get PositionID() { return this._positionId; }
    set PositionID(val) { this._positionId = val; } 
}


module.exports = { PositionUpsertDto, PositionUpsertResponseDto };