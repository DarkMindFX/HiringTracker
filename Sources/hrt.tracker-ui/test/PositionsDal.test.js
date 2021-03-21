const PositionsDal = require('../src/dal/PositionsDal')

describe('PositionsDal.getPositions', function() {
    it('returns list of positions', async () => {
        let dal = new PositionsDal();
        let result = await dal.getPositions();

        expect(result).not.toEqual(null);
        expect(result.length).toBeGreaterThanOrEqual(9);
    })
});