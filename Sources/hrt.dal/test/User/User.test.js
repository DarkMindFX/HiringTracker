
const { it, expect } = require('@jest/globals');
const { constants } = require('../constants');

const UserDal = require('../../src/UserDal');
const { prepInitParams, execSetup, execTeardown } = require('../DALTestHelper');
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

        try {
            
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
        }
        finally {
            await execTeardown(__dirname, testName);
        }
        
    })
});

describe('User.InsertUSer', function() {
    it('tries insert duplicate logic', async () => {

        let testName = '001.InsertUser.LoginAlreadyExists';

        try {
            await execSetup(__dirname, testName);

            let insUserLogin = '[Test OPRFGHUB] Inserted User';      

            let initParams = prepInitParams();
            let dal = new UserDal();
            dal.init(initParams);

            let newUser = new UserEntity()
            newUser.Login = insUserLogin;
            newUser.FirstName = '[Test OPRFGHUB] FirstName User';
            newUser.LastName = '[Test OPRFGHUB] LastName User';
            newUser.Description = '[Test OPRFGHUB] Desc User';
            newUser.Email = 'duplicated_login@email.com';
            newUser.PwdHash = 'Hash1234';
            newUser.Salt = 'Salt12345';        

            let result = await dal.Upsert(newUser);
        }
        catch(error) {
            if(error.code == 'EREQUEST' && error.originalError == "Error: User with given login already exists")
            {
                console.log("SUCCESS - exception throw as expected")
            }
            else {
                throw error;
            }
        }
        finally {
            await execTeardown(__dirname, testName);
        }
        
    })
});


describe('User.DeleteUser', function() {
    it('deletes exisiting User record', async () => {

        let testName = '010.DeleteUser.Success';

        const snooze = ms => new Promise(resolve => setTimeout(resolve, ms));

        try {
            await execSetup(__dirname, testName);

            let delUserLogin = '[Test QAZXCVFR34] Delete User';      

            let initParams = prepInitParams();
            let dal = new UserDal();
            dal.init(initParams);

            await snooze(1000); // waiting to guarantee that record was created by setup

            let Users = await dal.GetAll();
            let User = Users.find( s => s.Login == delUserLogin);

            let result = await dal.Delete(User.UserID);

            expect(result['Removed']).toEqual(true);
        }
        finally {
            await execTeardown(__dirname, testName);
        }
        
    })
});
