

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
    class RoleDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IRoleDal))]
    public class RoleDal: SQLDal, IRoleDal
    {
        public IInitParams CreateInitParams()
        {
            return new RoleDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public bool Delete(long id)
        {
            bool removed = base.Delete<Role>("p_Role_Delete", id, "@ID");

            return removed;
        }

        public Role Get(long id)
        {
            Role entityOut = base.Get<Role>("p_Role_GetDetails", id, "@ID", RoleFromRow);

            return entityOut;
        }

        
        public IList<Role> GetAll()
        {
            IList<Role> result = base.GetAll<Role>("p_Role_GetAll", RoleFromRow);

            return result;
        }

        public Role Upsert(Role entity) 
        {
            Role entityOut = base.Upsert<Role>("p_Role_Upsert", entity, AddUpsertParameters, RoleFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, Role entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pName = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Current, (object)entity.Name != null ? (object)entity.Name : DBNull.Value);   cmd.Parameters.Add(pName); 
        
            return cmd;
        }

        protected Role RoleFromRow(DataRow row)
        {
            var entity = new Role();

                    entity.ID = (System.Int64?)row["ID"];
                    entity.Name = (System.String)row["Name"];
        
            return entity;
        }
        
    }
}
