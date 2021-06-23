

using HRT.DAL.MSSQL;
using HRT.Interfaces;
using HRT.Interfaces.Entities;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Test.HRT.DAL.MSSQL
{
    public class TestPositionDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IPositionDal dal = new PositionDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void Position_GetAll_Success()
        {
            var dal = PreparePositionDal("DALInitParams");

            IList<Position> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Position\\000.GetDetails.Success")]
        public void Position_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            Position entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(3, entity.DepartmentID);
                            Assert.AreEqual("Title 8a00311e1ac1447988ad276cde5293a9", entity.Title);
                            Assert.AreEqual("ShortDesc 8a00311e1ac1447988ad276cde5293a9", entity.ShortDesc);
                            Assert.AreEqual("Description 8a00311e1ac1447988ad276cde5293a9", entity.Description);
                            Assert.AreEqual(5, entity.StatusID);
                            Assert.AreEqual(DateTime.Parse("5/7/2020 5:53:11 AM"), entity.CreatedDate);
                            Assert.AreEqual(100002, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("11/2/2020 11:40:11 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100002, entity.ModifiedByID);
                      }

        [Test]
        public void Position_GetDetails_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PreparePositionDal("DALInitParams");

            Position entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("Position\\010.Delete.Success")]
        public void Position_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Position_Delete_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PreparePositionDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("Position\\020.Insert.Success")]
        public void Position_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PreparePositionDal("DALInitParams");

            var entity = new Position();
                          entity.DepartmentID = 2;
                            entity.Title = "Title dc11d2c4a22a4293b9c96115491d25bb";
                            entity.ShortDesc = "ShortDesc dc11d2c4a22a4293b9c96115491d25bb";
                            entity.Description = "Description dc11d2c4a22a4293b9c96115491d25bb";
                            entity.StatusID = 3;
                            entity.CreatedDate = DateTime.Parse("3/9/2020 11:16:11 AM");
                            entity.CreatedByID = 100002;
                            entity.ModifiedDate = DateTime.Parse("7/16/2019 10:52:11 AM");
                            entity.ModifiedByID = 33000067;
                          
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(2, entity.DepartmentID);
                            Assert.AreEqual("Title dc11d2c4a22a4293b9c96115491d25bb", entity.Title);
                            Assert.AreEqual("ShortDesc dc11d2c4a22a4293b9c96115491d25bb", entity.ShortDesc);
                            Assert.AreEqual("Description dc11d2c4a22a4293b9c96115491d25bb", entity.Description);
                            Assert.AreEqual(3, entity.StatusID);
                            Assert.AreEqual(DateTime.Parse("3/9/2020 11:16:11 AM"), entity.CreatedDate);
                            Assert.AreEqual(100002, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("7/16/2019 10:52:11 AM"), entity.ModifiedDate);
                            Assert.AreEqual(33000067, entity.ModifiedByID);
              
        }

        [TestCase("Position\\030.Update.Success")]
        public void Position_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
                          entity.DepartmentID = 3;
                            entity.Title = "Title 3c6b40a98c7c4381975a3cef4c630c20";
                            entity.ShortDesc = "ShortDesc 3c6b40a98c7c4381975a3cef4c630c20";
                            entity.Description = "Description 3c6b40a98c7c4381975a3cef4c630c20";
                            entity.StatusID = 1;
                            entity.CreatedDate = DateTime.Parse("11/26/2022 3:05:11 PM");
                            entity.CreatedByID = 33000067;
                            entity.ModifiedDate = DateTime.Parse("11/26/2022 3:05:11 PM");
                            entity.ModifiedByID = 33000067;
              
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(3, entity.DepartmentID);
                            Assert.AreEqual("Title 3c6b40a98c7c4381975a3cef4c630c20", entity.Title);
                            Assert.AreEqual("ShortDesc 3c6b40a98c7c4381975a3cef4c630c20", entity.ShortDesc);
                            Assert.AreEqual("Description 3c6b40a98c7c4381975a3cef4c630c20", entity.Description);
                            Assert.AreEqual(1, entity.StatusID);
                            Assert.AreEqual(DateTime.Parse("11/26/2022 3:05:11 PM"), entity.CreatedDate);
                            Assert.AreEqual(33000067, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("11/26/2022 3:05:11 PM"), entity.ModifiedDate);
                            Assert.AreEqual(33000067, entity.ModifiedByID);
              
        }

        [Test]
        public void Position_Update_InvalidId()
        {
            var dal = PreparePositionDal("DALInitParams");

            var entity = new Position();
            entity.ID = Int64.MaxValue - 1;
                          entity.DepartmentID = 3;
                            entity.Title = "Title 3c6b40a98c7c4381975a3cef4c630c20";
                            entity.ShortDesc = "ShortDesc 3c6b40a98c7c4381975a3cef4c630c20";
                            entity.Description = "Description 3c6b40a98c7c4381975a3cef4c630c20";
                            entity.StatusID = 1;
                            entity.CreatedDate = DateTime.Parse("11/26/2022 3:05:11 PM");
                            entity.CreatedByID = 33000067;
                            entity.ModifiedDate = DateTime.Parse("11/26/2022 3:05:11 PM");
                            entity.ModifiedByID = 33000067;
              
            try
            {
                entity = dal.Upsert(entity);

                Assert.Fail("Fail - exception was expected, but wasn't thrown.");
            }
            catch (Exception ex)
            {
                Assert.Pass("Success - exception thrown as expected");
            }
        }

        protected IPositionDal PreparePositionDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IPositionDal dal = new PositionDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
