
const { it, expect } = require('@jest/globals');
const { constants } = require('../constants');

const InterviewStatusDal = require('../../src/InterviewStatusDal');
const { prepInitParams } = require('../DALTestHelper')

describe('InterviewStatus.GetAll', function() {
    it('returns all types of proficiency', async () => {
        let initParams = prepInitParams();
        let dal = new InterviewStatusDal();
        dal.init(initParams);

        let statuses = await dal.GetAll();

        expect(statuses).not.toEqual(null);
        expect(statuses.length).toBeGreaterThanOrEqual(4);
    })
});
