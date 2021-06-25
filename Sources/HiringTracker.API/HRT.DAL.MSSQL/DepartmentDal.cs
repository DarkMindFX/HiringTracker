

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using HRT.Common;
using HRT.DAL.MSSQL;
using HRT.Interfaces;
using HRT.Interfaces.Entities;

namespace HRT.DAL.MSSQL 
{
    class DepartmentDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IDepartmentDal))]
    public class DepartmentDal: SQLDal, IDepartmentDal
    {
        public IInitParams CreateInitParams()
        {
            return new DepartmentDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public Department Get(System.Int64? ID)
        {
            Department result = default(Department);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Department_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = DepartmentFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Department_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

                public IList<Department> GetByParentID(System.Int64? ParentID)
        {
            var entitiesOut = base.GetBy<Department, System.Int64?>("p_Department_GetByParentID", ParentID, "@ParentID", SqlDbType.BigInt, 0, DepartmentFromRow);

            return entitiesOut;
        }
                public IList<Department> GetByManagerID(System.Int64 ManagerID)
        {
            var entitiesOut = base.GetBy<Department, System.Int64>("p_Department_GetByManagerID", ManagerID, "@ManagerID", SqlDbType.BigInt, 0, DepartmentFromRow);

            return entitiesOut;
        }
        
        public IList<Department> GetAll()
        {
            IList<Department> result = base.GetAll<Department>("p_Department_GetAll", DepartmentFromRow);

            return result;
        }

        public Department Insert(Department entity) 
        {
            Department entityOut = base.Upsert<Department>("p_Department_Insert", entity, AddUpsertParameters, DepartmentFromRow);

            return entityOut;
        }

        public Department Update(Department entity) 
        {
            Department entityOut = base.Upsert<Department>("p_Department_Update", entity, AddUpsertParameters, DepartmentFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, Department entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pName = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Current, (object)entity.Name != null ? (object)entity.Name : DBNull.Value);   cmd.Parameters.Add(pName); 
                SqlParameter pUUID = new SqlParameter("@UUID", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "UUID", DataRowVersion.Current, (object)entity.UUID != null ? (object)entity.UUID : DBNull.Value);   cmd.Parameters.Add(pUUID); 
                SqlParameter pParentID = new SqlParameter("@ParentID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ParentID", DataRowVersion.Current, (object)entity.ParentID != null ? (object)entity.ParentID : DBNull.Value);   cmd.Parameters.Add(pParentID); 
                SqlParameter pManagerID = new SqlParameter("@ManagerID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ManagerID", DataRowVersion.Current, (object)entity.ManagerID != null ? (object)entity.ManagerID : DBNull.Value);   cmd.Parameters.Add(pManagerID); 
        
            return cmd;
        }

        protected Department DepartmentFromRow(DataRow row)
        {
            var entity = new Department();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.Name = !DBNull.Value.Equals(row["Name"]) ? (System.String)row["Name"] : default(System.String);
                    entity.UUID = !DBNull.Value.Equals(row["UUID"]) ? (System.String)row["UUID"] : default(System.String);
                    entity.ParentID = !DBNull.Value.Equals(row["ParentID"]) ? (System.Int64?)row["ParentID"] : default(System.Int64?);
                    entity.ManagerID = !DBNull.Value.Equals(row["ManagerID"]) ? (System.Int64)row["ManagerID"] : default(System.Int64);
        
            return entity;
        }
        
    }
}
