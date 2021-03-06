const { it, expect } = require('@jest/globals');
const { constants } = require('../constants');

const CandidateDal = require('../../src/CandidateDal');
const { prepInitParams, execSetup,
    execTeardown } = require('../DALTestHelper');
const CandidateEntity = require('../../src/entities/Candidate');
const CandidateSkillEntity = require('../../src/entities/CandidateSkill');

describe('Candidate.GetAll', function() {
    it('returns all Candidates', async () => {
        let initParams = prepInitParams();
        let dal = new CandidateDal();
        dal.init(initParams);

        let candidates = await dal.GetAll();

        expect(candidates).not.toEqual(null);
        expect(candidates.length).toBeGreaterThan(0);
    })
});

describe('Candidate.GetDetails', function() {
    it('returns Candidate details', async () => {
        let initParams = prepInitParams();
        let dal = new CandidateDal();
        dal.init(initParams);

        let candidateId = 100001;
        let candidate = await dal.GetDetails(candidateId);

        expect(candidate).not.toEqual(null);
        expect(candidate.CandidateID).toEqual(candidateId);
        expect(candidate.FirstName).toEqual('George');
        expect(candidate.LastName).toEqual('Washington');
    })
});

describe('Candidate.GetSkills', function() {
    it('returns skills for the given candidate', async () => {
        
        let initParams = prepInitParams();
        let dal = new CandidateDal();
        dal.init(initParams);

        let posId = 100001;
        let skills = await dal.GetSkills(posId);

        expect(skills).not.toEqual(null);
        expect(skills.length).toBeGreaterThan(0);
    })
});

describe('Candidate.GetSkills - Invalid Candidate', function() {
    it('returns skills for the given candidate', async () => {
        
        let initParams = prepInitParams();
        let dal = new CandidateDal();
        dal.init(initParams);

        let posId = 23000000;
        let skills = await dal.GetSkills(posId);

        expect(skills).toEqual(null);
    })
});

describe('Candidate.InsertCandidate', function() {
    it('inserts new Candidate record', async () => {

        let testName = '000.InsertCandidate.Success';
        await execSetup(__dirname, testName);

        let firstName = '[Test] First 65TRF435G';
        let lastName = '[Test] Last 65TRF435G';
        let email = 'test_65TRF435G@gmail.com';
        let userId = constants.USER_ID_JOEB;
        let cvlink = 'http://dropbox.com/somelinktocv';

        let newCandidate = new CandidateEntity();
        newCandidate.FirstName = firstName;
        newCandidate.LastName = lastName;
        newCandidate.Email = email;
        newCandidate.CVLink = cvlink;        

        let initParams = prepInitParams();
        let dal = new CandidateDal();
        dal.init(initParams);

        let result = await dal.Upsert(newCandidate, userId);

        await execTeardown(__dirname, testName);

        expect(result['NewCandidateID']).not.toEqual(null);
        expect(parseInt(result['NewCandidateID'])).toBeGreaterThan(0);        
    })
});

describe('Candidate.DeleteCandidate', function() {
    it('deletes exisiting Candidate record', async () => {

        let testName = '010.DeleteCandidate.Success';
        await execSetup(__dirname, testName);

        let firstName = '[Test] First 45645645GF';
        let userId = constants.USER_ID_DONALDT;

        let initParams = prepInitParams();
        let dal = new CandidateDal();
        dal.init(initParams);

        let candidates = await dal.GetAll();

        let candidate = candidates.find( p => p.FirstName == firstName);

        let result = await dal.Delete(candidate.CandidateID, userId);        

        await execTeardown(__dirname, testName);

        expect(result).not.toEqual(null)
        expect(result['Removed']).not.toEqual(null)
        expect(result['Removed']).toEqual(true);
        
    })
});

describe('Candidate.UpdateCandidate', function() {
    it('updates exisiting Candidate record', async () => {

        let testName = '020.UpdateCandidate.Success';
        await execSetup(__dirname, testName);

        let email = 'Last_MNY67DF4@gmail.com';
        let newFirst = '[Test] First MNY67DF4 UPDARTED';
        let newLast = '[Test] Last MNY67DF4 UPDATED';
        let userId = constants.USER_ID_DONALDT;

        let initParams = prepInitParams();
        let dal = new CandidateDal();
        dal.init(initParams);

        let candidates = await dal.GetAll();

        let candidate = candidates.find( p => p.Email == email);

        if(!candidate) {
            expect(candidate).not.toEqual(null)
        }

        candidate.FirstName = newFirst;
        candidate.LastName = newLast;

        let result = await dal.Upsert(candidate, userId);        

        await execTeardown(__dirname, testName);
        
    })
});

describe('Candidate.SetSkills', function() {
    it('sets skills for exisiting Candidate record', async () => {

        let testName = '030.SetSkills.Success';
        await execSetup(__dirname, testName);

        let email = 'Last_89757WEG67@gmail.com';
        let userId = constants.USER_ID_DONALDT;

        let initParams = prepInitParams();
        let dal = new CandidateDal();
        dal.init(initParams);

        let candidates = await dal.GetAll();

        let candidate = candidates.find( p => p.Email == email);

        if(!candidate) {
            expect(candidate).not.toEqual(null)
        }

        let candidateId = candidate.CandidateID;

        let skills = [];
        let skill1 = new CandidateSkillEntity();
        skill1.SkillID = 3;
        skill1.ProficiencyID = 1;
        skill1.IsMandatory = true;

        let skill2 = new CandidateSkillEntity();
        skill2.SkillID = 5;
        skill2.ProficiencyID = 2;
        skill2.IsMandatory = false;

        let skill3 = new CandidateSkillEntity();
        skill3.SkillID = 7;
        skill3.ProficiencyID = 3;
        skill3.IsMandatory = true;

        skills.push(skill1);
        skills.push(skill2);
        skills.push(skill3);

        dal.SetSkills(candidateId, skills).then( async () => {
            try {
                dal.GetSkills(candidateId).then( (candSkills) => {
                    expect(candSkills).not.toEqual(null);
                    expect(candSkills.length).toEqual(3);
                });            
            }
            finally {        
                execTeardown(__dirname, testName);
            }
        }); 
    })
});
