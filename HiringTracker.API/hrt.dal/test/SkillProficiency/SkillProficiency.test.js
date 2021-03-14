
const { it, expect } = require('@jest/globals');
const { constants } = require('../constants');

const SkillProficiencyDal = require('../../src/SkillProficiencyDal');
const { prepInitParams } = require('../DALTestHelper')

describe('SkillProficiency.GetAll', function() {
    it('returns all types of proficiency', async () => {
        let initParams = prepInitParams();
        let dal = new SkillProficiencyDal();
        dal.init(initParams);

        let skillProfs = await dal.GetAll();

        expect(skillProfs).not.toEqual(null);
        expect(skillProfs.length).toEqual(4);
    })
});
