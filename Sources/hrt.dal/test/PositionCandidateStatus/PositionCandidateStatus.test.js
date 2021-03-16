
const { it, expect } = require('@jest/globals');
const { constants } = require('../constants');

const PositionCandidateStatusDal = require('../../src/PositionCandidateStatusDal');
const { prepInitParams } = require('../DALTestHelper')

describe('PositionCandidateStatus.GetAll', function() {
    it('returns all types of proficiency', async () => {
        let initParams = prepInitParams();
        let dal = new PositionCandidateStatusDal();
        dal.init(initParams);

        let statuses = await dal.GetAll();

        expect(statuses).not.toEqual(null);
        expect(statuses.length).toBeGreaterThanOrEqual(4);
    })
});
