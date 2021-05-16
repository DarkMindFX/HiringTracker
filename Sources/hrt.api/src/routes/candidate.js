
const { Error, CandidateUpsertResponseDto } = require('hrt.dto');
const { CandidateDal } = require('hrt.dal')
const { DalHelper } = require('../dalHelper')
const { Converter } = require('../coverters');
const { authUserOnly } = require('../middleware');

const constants = require('../constants');

function routeCandidates(router) {
    router.get("/api/v1/candidates", authUserOnly(), (req, res) => { getCandidates(req, res) });
    router.get("/api/v1/candidates/:id", authUserOnly(), (req, res) => { getCandidateByID(req, res) });
    router.get("/api/v1/candidates/:id/skills", authUserOnly(), (req, res) => { getCandidateSkills(req, res) });
    router.delete("/api/v1/candidates/:id", authUserOnly(), (req, res) => { deleteCandidateByID(req, res) });
    router.put('/api/v1/candidates', authUserOnly(), (req, res) => { addCandidate(req, res); })
    router.post('/api/v1/candidates', authUserOnly(), (req, res) => { updateCandidate(req, res); })
}

async function addCandidate(req, res) {
    try {
        let dal = _getCandidateDal();

        const newCandidate = Converter.candidateDto2Entity(req.body._candidate);
        const newCandSkills = [];
        if(req.body._skills) {
            req.body._skills.forEach(s => {
                let skillProfEntity = Converter.candidateSkillDto2Entity(null, s);
                newCandSkills.push(skillProfEntity)
            });
        }

        newCandidate.CandidateID = null; // setting to null - to notify that we are creating new candidate
        const result = await dal.Upsert(newCandidate, req.middleware.user.UserID);
        const candidateID = result["NewCandidateID"];
        newCandSkills.forEach( s => s.CandidateID = candidateID);

        await dal.SetSkills(candidateID, newCandSkills)

        let respDto = new CandidateUpsertResponseDto();
        respDto.CandidateID = candidateID

        res.status(constants.HTTP_Created);
        res.send(respDto);  
    }
    catch(error) {
        let msg = `Error processing ADD candidate request: ${error.message}`;
        console.error(msg);
        res.status(constants.HTTP_IntServerError);
        let errBody = new Error();
        errBody.message = msg;
        errBody.code = constants.HTTP_IntServerError;
        res.send(errBody);        
    }
}

async function updateCandidate(req, res) {
    try {
        let dal = _getCandidateDal();

        const cand = Converter.candidateDto2Entity(req.body._candidate);
        const newCandSkills = [];
        if(req.body._skills) {
            req.body._skills.forEach(s => {
                let skillProfEntity = Converter.candidateSkillDto2Entity(null, s);
                newCandSkills.push(skillProfEntity)
            });
        }

        const result = await dal.Upsert(cand, req.middleware.user.UserID);
        const candidateId = cand.CandidateID;
        newCandSkills.forEach( s => s.CandidateID = candidateId);

        await dal.SetSkills(candidateID, newCandSkills);

        res.status(constants.HTTP_OK);
        res.send();   
    }
    catch(error) {
        let msg = `Error processing ADD candidate request: ${error.message}`;
        console.error(msg);
        res.status(constants.HTTP_IntServerError);
        let errBody = new Error();
        errBody.message = msg;
        errBody.code = constants.HTTP_IntServerError;
        res.send(errBody);        
    }
}

async function deleteCandidateByID(req, res) {
    try {
        let dal = _getCandidateDal();

        if(req.params.id) {
            let cand = await dal.GetDetails(parseInt(req.params.id));

            if(cand) {
                dal.Delete(cand.CandidateID);

                res.status(constants.HTTP_OK);
                res.send();
            }
            else {
                res.status(constants.HTTP_NotFound);
                let errBody = new Error();
                errBody.message = `Candidate [id: ${req.params.id}] not found`;
                errBody.code = constants.HTTP_NotFound;
                res.send(errBody);
            }
        }
    }
    catch(error) {
        let msg = `Error processing DELETE candidate request: ${error.message}`;
        console.error(msg);
        res.status(constants.HTTP_IntServerError);
        let errBody = new Error();
        errBody.message = msg;
        errBody.code = constants.HTTP_IntServerError;
        res.send(errBody);
    }
}

async function getCandidates(req, res) {
    
    try {
        let objs = await DalHelper.getCandidatesDto();

        let dtos = Object.values( objs );
        
        res.status(constants.HTTP_OK);
        res.send(JSON.stringify(dtos, null, '\t'));
    }
    catch(error) {
        let msg = `Error processing GET candidates request: ${error.message}`;
        console.error(msg);
        res.status(constants.HTTP_IntServerError);
        let errBody = new Error();
        errBody.message = msg;
        errBody.code = constants.HTTP_IntServerError;
        res.send(errBody);
    }
}

async function getCandidateByID(req, res) {

    try {

        let dal = _getCandidateDal();

        if(req.params.id) {
            let cand = await dal.GetDetails(parseInt(req.params.id));

            if(cand) {
                let dictUsers = await DalHelper.getUsersDto();
                let dicStatuses = await DalHelper.getPositionStatusesDto();

                let dto = Converter.candidateEntity2Dto(cand, dictUsers);        

                res.status(constants.HTTP_OK);
                res.send(JSON.stringify(cand, null, 4));
            }
            else {
                res.status(constants.HTTP_NotFound);
                let errBody = new Error();
                errBody.message = `Candidate [id: ${req.params.id}] not found`;
                errBody.code = constants.HTTP_NotFound;
                res.send(errBody);
            }
        }
        else {
            res.status(constants.HTTP_BadRequest);
            let errBody = new Error();
            errBody.message = `Candidate ID was not provided`;
            errBody.code = constants.HTTP_BadRequest;
            res.send(errBody);            
        }
    }
    catch(error) {
        let msg = `Error processing GET candidate request: ${error.message}`;
        console.error(msg);
        res.status(constants.HTTP_IntServerError);
        let errBody = new Error();
        errBody.message = msg;
        errBody.code = constants.HTTP_IntServerError;
        res.send(errBody);
    }
}

async function getCandidateSkills(req, res) {

    try {

        const dal = _getCandidateDal();

        if(req.params.id) {
            let cand = await dal.GetDetails(parseInt(req.params.id));

            if(cand) {
                let skills = await dal.GetSkills(req.params.id);

                let dictSkills = await DalHelper.getSkillsDto();
                let dictProfs = await DalHelper.getSkillProficiencyDto();

                let dtos = [];   
                skills.forEach(s => dtos.push(Converter.candidateSkillEntity2Dto(s, dictSkills, dictProfs)));     

                res.status(constants.HTTP_OK);
                res.send(JSON.stringify(dtos, null, 4));
            }
            else {
                res.status(constants.HTTP_NotFound);
                let errBody = new Error();
                errBody.message = `Candidate [id: ${req.params.id}] not found`;
                errBody.code = constants.HTTP_NotFound;
                res.send(errBody);
            }
        }
        else {
            res.status(constants.HTTP_BadRequest);
            let errBody = new Error();
            errBody.message = `Candidate ID was not provided`;
            errBody.code = constants.HTTP_BadRequest;
            res.send(errBody);            
        }
    }
    catch(error) {
        let msg = `Error processing GET Candidate skills request: ${error.message}`;
        console.error(msg);
        res.status(constants.HTTP_IntServerError);
        let errBody = new Error();
        errBody.message = msg;
        errBody.code = constants.HTTP_IntServerError;
        res.send(errBody);
    }
}

function _getCandidateDal() 
{
    let initParams = DalHelper.prepInitParams();
    let dal = new CandidateDal();
    dal.init(initParams);

    return dal;
}

module.exports = routeCandidates;