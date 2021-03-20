
const { it, expect } = require('@jest/globals');
const { constants } = require('../constants');

const SkillDal = require('../../src/SkillDal');
const { prepInitParams, execSetup,
    execTeardown } = require('../DALTestHelper');
const SkillEntity = require('../../src/entities/Skill');

describe('Skill.GetAll', function() {
    it('returns all skills', async () => {
        let initParams = prepInitParams();
        let dal = new SkillDal();
        dal.init(initParams);

        let skills = await dal.GetAll();

        expect(skills).not.toEqual(null);
        expect(skills.length).toBeGreaterThan(0);
    })
});

describe('Skill.InsertSkill', function() {
    it('inserts new skill record', async () => {

        let testName = '000.InsertSkill.Success';
        await execSetup(__dirname, testName);

        let newSkillId = 300001;
        let newSkillName = '[Test RTYHGFVBN] Inserted Skill';
        let newSkill = new SkillEntity()
        newSkill.SkillID = newSkillId;
        newSkill.Name = newSkillName;
        

        let initParams = prepInitParams();
        let dal = new SkillDal();
        dal.init(initParams);

        let skills = await dal.GetAll();
        skills.push(newSkill);

        await dal.Upsert(skills);

        await execTeardown(__dirname, testName);
        
    })
});

describe('Skill.DeleteSkill', function() {
    it('deletes exisiting skill record', async () => {

        let testName = '010.DeleteSkill.Success';
        await execSetup(__dirname, testName);

        let delSkillName = '[Test QAZXCVFR] Delete Skill';      

        let initParams = prepInitParams();
        let dal = new SkillDal();
        dal.init(initParams);

        let skills = await dal.GetAll();
        let skill = skills.find( s => s.Name == delSkillName);

        let result = await dal.Delete(skill.SkillID);

        expect(result['Removed']).toEqual(true);

        await execTeardown(__dirname, testName);
        
    })
});