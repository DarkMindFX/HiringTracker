
const { it, expect } = require('@jest/globals');
const { constants } = require('../constants');

const UserDal = require('../../src/UserDal');
const { prepInitParams, execSetup,
    execTeardown } = require('../DALTestHelper');
const UserEntity = require('../../src/entities/User');

describe('User.GetAll', function() {
    it('returns all Users', async () => {
        let initParams = prepInitParams();
        let dal = new UserDal();
        dal.init(initParams);

        let Users = await dal.GetAll();

        expect(Users).not.toEqual(null);
        expect(Users.length).toBeGreaterThan(0);
    })
});

describe('User.GetDetails', function() {
    it('returns User details', async () => {
        let initParams = prepInitParams();
        let dal = new UserDal();
        dal.init(initParams);

        let userId = 100001;
        let user = await dal.GetDetails(userId);

        expect(user).not.toEqual(null);
        expect(user.UserID).toEqual(userId);
    })
});


describe('User.InsertUser', function() {
    it('inserts new User record', async () => {

        let testName = '000.InsertUser.Success';
        await execSetup(__dirname, testName);

        let newUser = new UserEntity()
        newUser.Login = '[Test RTYHGFVBN] Inserted User';
        newUser.FirstName = '[Test RTYHGFVBN] FirstName User';
        newUser.LastName = '[Test RTYHGFVBN] LastName User';
        newUser.Description = '[Test RTYHGFVBN] Desc User';
        newUser.Email = 'testuser@email.com';
        newUser.PwdHash = 'Hash1234';
        newUser.Salt = 'Salt12345';        

        let initParams = prepInitParams();
        let dal = new UserDal();
        dal.init(initParams);

        let result = await dal.Upsert(newUser);

        expect(result).not.toEqual(null);
        expect(parseInt(result['NewUserID'])).toBeGreaterThan(0);

        await execTeardown(__dirname, testName);
        
    })
});


describe('User.DeleteUser', function() {
    it('deletes exisiting User record', async () => {

        let testName = '010.DeleteUser.Success';
        await execSetup(__dirname, testName);

        let delUserLogin = '[Test QAZXCVFR34] Delete User';      

        let initParams = prepInitParams();
        let dal = new UserDal();
        dal.init(initParams);

        let Users = await dal.GetAll();
        let User = Users.find( s => s.Login == delUserLogin);

        let result = await dal.Delete(User.UserID);

        expect(result['Removed']).toEqual(true);

        await execTeardown(__dirname, testName);
        
    })
});
