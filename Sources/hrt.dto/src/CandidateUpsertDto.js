
class CandidateUpsertDto {

    get Candidate() { return this._candidate; }
    set Candidate(val) { this._candidate = val; }

    get Skills() { return this._skills; }
    set Skills(val) { this._skills = val; }

}

class CandidateUpsertResponseDto {

    get CandidateID() { return this._candidateId; }
    set CandidateID(val) { this._candidateId = val; } 
}


module.exports = { CandidateUpsertDto, CandidateUpsertResponseDto };