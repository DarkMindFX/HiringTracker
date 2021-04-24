
const { it, expect } = require('@jest/globals');
const { constants } = require('../constants');

const PositionDal = require('../../src/PositionDal');
const { prepInitParams, execSetup,
    execTeardown } = require('../DALTestHelper');
const PositionEntity = require('../../src/entities/Position');
const PositionSkillEntity = require('../../src/entities/PositionSkill');

describe('Position.GetAll', function() {
    it('returns all Positions', async () => {
        let initParams = prepInitParams();
        let dal = new PositionDal();
        dal.init(initParams);

        let positions = await dal.GetAll();

        expect(positions).not.toEqual(null);
        expect(positions.length).toBeGreaterThan(0);
    })
});

describe('Position.GetDetails', function() {
    it('returns Position details', async () => {
        let initParams = prepInitParams();
        let dal = new PositionDal();
        dal.init(initParams);

        let positionId = 100001;
        let position = await dal.GetDetails(positionId);

        expect(position).not.toEqual(null);
        expect(position.PositionID).toEqual(positionId);
    })
});


describe('Position.InsertPosition', function() {
    it('inserts new Position record', async () => {

        let testName = '000.InsertPosition.Success';
        await execSetup(__dirname, testName);

        let title = '[Test] Position TYHGFVB543';
        let shortDesc = '[Test] Position Short Desc TYHGFVB543';
        let desc = '[Test] Position TYHGFVB543 Full Desc';
        let userId = constants.USER_ID_JOEB;

        let newPosition = new PositionEntity();
        newPosition.Title = title;
        newPosition.ShortDesc = shortDesc;
        newPosition.Desc = desc;
        newPosition.StatusID = constants.POSSTATUS_ID_OPEN;

        let initParams = prepInitParams();
        let dal = new PositionDal();
        dal.init(initParams);

        let result = await dal.Upsert(newPosition, userId);

        await execTeardown(__dirname, testName);

        expect(result['NewPositionID']).not.toEqual(null);
        expect(parseInt(result['NewPositionID'])).toBeGreaterThan(0);        
    })
});

describe('Position.DeletePosition', function() {
    it('deletes exisiting Position record', async () => {

        let testName = '010.DeletePosition.Success';
        await execSetup(__dirname, testName);

        let title = '[Test] Position 65TRF435G';
        let userId = constants.USER_ID_DONALDT;

        let initParams = prepInitParams();
        let dal = new PositionDal();
        dal.init(initParams);

        let positions = await dal.GetAll();

        let position = positions.find( p => p.Title == title);

        let result = await dal.Delete(position.PositionID, userId);        

        await execTeardown(__dirname, testName);

        expect(result).not.toEqual(null)
        expect(result['Removed']).not.toEqual(null)
        expect(result['Removed']).toEqual(true);
        
    })
});

describe('Position.UpdatePosition', function() {
    it('updates new Position record', async () => {

        let testName = '020.UpdatePosition.Success';
        await execSetup(__dirname, testName);

        let title = '[Test] Position 987GFGHHT';
        let newTitle = '[UPDATED][Test] Position 987GFGHHT';
        let newshortDesc = '[UPDATED][Test] Position Short Desc 987GFGHHT';
        let newdesc = '[UPDATED][Test] Position 987GFGHHT Full Desc';
        let userId = constants.USER_ID_JOEB;

        let initParams = prepInitParams();
        let dal = new PositionDal();
        dal.init(initParams);

        let positions = await dal.GetAll();

        let position = positions.find( p => p.Title == title);

        position.Title = newTitle;
        position.ShortDesc = newshortDesc;
        position.Desc = newdesc;
        position.StatusID = constants.POSSTATUS_ID_CLOSED;        

        let result = await dal.Upsert(position, userId);

        await execTeardown(__dirname, testName);

        expect(result['NewPositionID']).toEqual(null);
     
    })
});

describe('Position.GetSkills', function() {
    it('returns skills for the given position', async () => {
        
        let initParams = prepInitParams();
        let dal = new PositionDal();
        dal.init(initParams);

        let posId = 100001;
        let skills = await dal.GetSkills(posId);

        expect(skills).not.toEqual(null);
        expect(skills.length).toBeGreaterThan(0);
    })
});

describe('Position.GetSkills - Invalid Position', function() {
    it('returns skills for the given position', async () => {
        
        let initParams = prepInitParams();
        let dal = new PositionDal();
        dal.init(initParams);

        let posId = 23000000;
        let skills = await dal.GetSkills(posId);

        expect(skills).toEqual(null);
    })
});

describe('Position.SetSkills', function() {
    it('sets skills for the given position', async () => {

        const snooze = ms => new Promise(resolve => setTimeout(resolve, ms));

        let testName = '030.SetSkills.Success';
        await execSetup(__dirname, testName);
        
        let initParams = prepInitParams();
        let dal = new PositionDal();
        dal.init(initParams);

        await snooze(1000); // waiting to guarantee that record was created by setup

        const title = '[Test] Position TGSVJ654'

        let positions = await dal.GetAll();
        let position = positions.find( p => p.Title == title);
        let posId = position.PositionID;

        let skills = [];
        let skill1 = new PositionSkillEntity();
        skill1.SkillID = 3;
        skill1.ProficiencyID = 1;
        skill1.IsMandatory = true;

        let skill2 = new PositionSkillEntity();
        skill2.SkillID = 5;
        skill2.ProficiencyID = 2;
        skill2.IsMandatory = false;

        let skill3 = new PositionSkillEntity();
        skill3.SkillID = 7;
        skill3.ProficiencyID = 3;
        skill3.IsMandatory = true;

        skills.push(skill1);
        skills.push(skill2);
        skills.push(skill3);

        let posSkills = null;

        try {

            await dal.SetSkills(posId, skills);

            posSkills = await dal.GetSkills(posId);

        }
        finally {

            await execTeardown(__dirname, testName);

            expect(posSkills).not.toEqual(null);
            expect(posSkills.length).toEqual(3);
        }
    })
});