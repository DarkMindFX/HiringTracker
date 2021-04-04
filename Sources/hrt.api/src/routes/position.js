
const { Error, PositionDto } = require('hrt.dto');
const { PositionDal, PositionEntity, UserDal, UserEntity } = require('hrt.dal')
const { prepInitParams, getPositionsDto, getUsersDto, getPositionStatusesDto, getSkillsDto,
    getSkillProficiencyDto } = require('../dalHelper')
const { Converter } = require('../coverters');

const constants = require('../constants');

function routePositions(route) {
    route.get('/api/v1/positions', (req, res) => { getPositions(req, res); })
    route.get('/api/v1/positions/:id', (req, res) => { getPositionById(req, res); })
    route.get('/api/v1/positions/:id/skills', (req, res) => { getPositionSkills(req, res); })
    route.put('/api/v1/positions', (req, res) => { addPosition(req, res); })
    route.post('/api/v1/positions', (req, res) => { updatePosition(req, res); })
    route.delete('/api/v1/positions/:id', (req, res) => { deletePositionById(req, res); })
}

async function addPosition(req, res) {

}

async function updatePosition(req, res) {

}

async function deletePositionById(req, res) {
    try {

        let initParams = prepInitParams();
        let dal = new PositionDal();
        dal.init(initParams);

        if(req.params.id) {
            let pos = await dal.GetDetails(parseInt(req.params.id));

            if(pos) {
                
                dal.Delete(entity.PositionID);     

                res.status(constants.HTTP_OK);
                
            }
            else {
                res.status(constants.HTTP_NotFound);
                let errBody = new Error();
                errBody.message = `Position [id: ${req.query.id}] not found`;
                errBody.code = constants.HTTP_NotFound;
                res.send(erroBody);
            }
        }
        else {
            res.status(constants.HTTP_BadRequest);
            let errBody = new Error();
            errBody.message = `Position ID was not provided`;
            errBody.code = constants.HTTP_BadRequest;
            res.send(erroBody);            
        }
    }
    catch(error) {
        console.error('Error processing DELETE positions request', error.message);
        res.status(constants.HTTP_IntServerError);
        let errBody = new Error();
        errBody.message = `Error processing DELETE positions request: ${error.message}`;
        errBody.code = constants.HTTP_IntServerError;
        res.send(errBody);
    }

}

async function getPositions(req, res) {
    try {

        let dtos = Object.values( await getPositionsDto() );        

        res.status(constants.HTTP_OK);
        res.send(JSON.stringify(dtos, null, 4));
    }
    catch(error) {
        console.error('Error processing GET positions request', error.message);
        res.status(constants.HTTP_IntServerError);
        let errBody = new Error();
        errBody.message = `Error processing GET positions request: ${error.message}`;
        errBody.code = constants.HTTP_IntServerError;
        res.send(errBody);
    }
}

async function getPositionById(req, res) {
    try {

        let initParams = prepInitParams();
        let dal = new PositionDal();
        dal.init(initParams);

        if(req.params.id) {
            let pos = await dal.GetDetails(parseInt(req.params.id));

            if(pos) {
                let dictUsers = await getUsersDto();
                let dicStatuses = await getPositionStatusesDto();

                let dto = Converter.positionEntity2Dto(pos, dictUsers, dicStatuses);        

                res.status(constants.HTTP_OK);
                res.send(JSON.stringify(dto, null, 4));
            }
            else {
                res.status(constants.HTTP_NotFound);
                let errBody = new Error();
                errBody.message = `Position [id: ${req.params.id}] not found`;
                errBody.code = constants.HTTP_NotFound;
                res.send(erroBody);
            }
        }
        else {
            res.status(constants.HTTP_BadRequest);
            let errBody = new Error();
            errBody.message = `Position ID was not provided`;
            errBody.code = constants.HTTP_BadRequest;
            res.send(erroBody);            
        }
    }
    catch(error) {
        console.error('Error processing GET positions request', error.message);
        res.status(constants.HTTP_IntServerError);
        let errBody = new Error();
        errBody.message = `Error processing GET positions request: ${error.message}`;
        errBody.code = constants.HTTP_IntServerError;
        res.send(errBody);
    }
}

async function getPositionSkills(req, res) {
    try {

        let initParams = prepInitParams();
        let dal = new PositionDal();
        dal.init(initParams);

        if(req.params.id) {
            let pos = await dal.GetDetails(parseInt(req.params.id));

            if(pos) {
                let skills = await dal.GetSkills(req.params.id);

                let dictSkills = await getSkillsDto();
                let dictProfs = await getSkillProficiencyDto();

                let dtos = [];   
                skills.forEach(s => dtos.push(Converter.positionSkillEntity2Dto(s, dictSkills, dictProfs)));     

                res.status(constants.HTTP_OK);
                res.send(JSON.stringify(dtos, null, 4));
            }
            else {
                res.status(constants.HTTP_NotFound);
                let errBody = new Error();
                errBody.message = `Position [id: ${req.params.id}] not found`;
                errBody.code = constants.HTTP_NotFound;
                res.send(erroBody);
            }
        }
        else {
            res.status(constants.HTTP_BadRequest);
            let errBody = new Error();
            errBody.message = `Position ID was not provided`;
            errBody.code = constants.HTTP_BadRequest;
            res.send(erroBody);            
        }
    }
    catch(error) {
        console.error('Error processing GET position skills request', error.message);
        res.status(constants.HTTP_IntServerError);
        let errBody = new Error();
        errBody.message = `Error processing GET position skills request: ${error.message}`;
        errBody.code = constants.HTTP_IntServerError;
        res.send(errBody);
    }
}

module.exports = routePositions;