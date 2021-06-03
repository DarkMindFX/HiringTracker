using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using HRT.Common;
using HRT.DAL.MSSQL;
using HRT.Interfaces;
using HRT.Interfaces.Entities;

namespace HRT.Interfaces
{
    class DepartmentDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IDepartmentDal))]
    public class DepartmentDal : SQLDal, IDepartmentDal
    {
        public IInitParams CreateInitParams()
        {
            return new DepartmentDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public bool Delete(long id)
        {
            bool removed = base.Delete<Department>("p_Department_Delete", id, "@ID");

            return removed;
        }

        public Department Get(long id)
        {
            Department entityOut = base.Get<Department>("p_Department_GetDetails", id, "@ID", DepartmentFromRow);

            return entityOut;
        }

        public IList<Department> GetAll()
        {
            IList<Department> result = base.GetAll<Department>("p_Department_GetAll", DepartmentFromRow);

            return result;
        }

        public Department Upsert(Department entity)
        {
            Department entityOut = base.Upsert<Department>("p_Department_Upsert", entity, AddUpsertParameters, DepartmentFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, Department entity)
        {
            SqlParameter pID = new SqlParameter(@"ID", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value); cmd.Parameters.Add(pID);

            SqlParameter pName = new SqlParameter(@"Name", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Current, (object)entity.Name != null ? (object)entity.Name : DBNull.Value); cmd.Parameters.Add(pName);

            SqlParameter pUUID = new SqlParameter(@"UUID", SqlDbType.NVarChar, 50, ParameterDirection.Input, true, 0, 0, "UUID", DataRowVersion.Current, (object)entity.UUID != null ? (object)entity.UUID : DBNull.Value); cmd.Parameters.Add(pUUID);

            SqlParameter pParentID = new SqlParameter(@"ParentID", SqlDbType.BigInt, 0, ParameterDirection.Input, true, 0, 0, "ParentID", DataRowVersion.Current, (object)entity.ParentID != null ? (object)entity.ParentID : DBNull.Value); cmd.Parameters.Add(pParentID);



            return cmd;
        }

        protected Department DepartmentFromRow(DataRow row)
        {
            var entity = new Department();

            entity.ID = (System.Int64?)row["ID"];
            entity.Name = (System.String)row["Name"];
            entity.UUID = !DBNull.Value.Equals(row["UUID"]) ? (System.String)row["UUID"] : null;
            entity.ParentID = !DBNull.Value.Equals(row["ParentID"]) ? (System.Int64?)row["ParentID"] : null;


            return entity;
        }

        public long? Upsert(Department entity, long? editorID)
        {
            throw new NotImplementedException();
        }
    }
}
