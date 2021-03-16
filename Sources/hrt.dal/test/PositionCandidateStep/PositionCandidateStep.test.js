
const { it, expect } = require('@jest/globals');
const { constants } = require('../constants');

const PositionCandidateStepDal = require('../../src/PositionCandidateStepDal');
const { prepInitParams } = require('../DALTestHelper')

describe('PositionCandidateStep.GetAll', function() {
    it('returns all types of proficiency', async () => {
        let initParams = prepInitParams();
        let dal = new PositionCandidateStepDal();
        dal.init(initParams);

        let statuses = await dal.GetAll();

        expect(statuses).not.toEqual(null);
        expect(statuses.length).toBeGreaterThanOrEqual(10);
    })
});
