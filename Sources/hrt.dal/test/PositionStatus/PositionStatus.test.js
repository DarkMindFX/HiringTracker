
const { it, expect } = require('@jest/globals');
const { constants } = require('../constants');

const PositionStatusDal = require('../../src/PositionStatusDal');
const { prepInitParams } = require('../DALTestHelper')

describe('PositionStatus.GetAll', function() {
    it('returns all types of proficiency', async () => {
        let initParams = prepInitParams();
        let dal = new PositionStatusDal();
        dal.init(initParams);

        let statuses = await dal.GetAll();

        expect(statuses).not.toEqual(null);
        expect(statuses.length).toBeGreaterThanOrEqual(5);
    })
});
