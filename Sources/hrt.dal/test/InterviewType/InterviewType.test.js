
const { it, expect } = require('@jest/globals');
const { constants } = require('../constants');

const InterviewTypeDal = require('../../src/InterviewTypeDal');
const { prepInitParams } = require('../DALTestHelper')

describe('InterviewType.GetAll', function() {
    it('returns all types of proficiency', async () => {
        let initParams = prepInitParams();
        let dal = new InterviewTypeDal();
        dal.init(initParams);

        let statuses = await dal.GetAll();

        expect(statuses).not.toEqual(null);
        expect(statuses.length).toBeGreaterThanOrEqual(5);
    })
});
