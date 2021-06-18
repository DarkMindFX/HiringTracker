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
    class PositionDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IPositionDal))]
    public class PositionDal: SQLDal, IPositionDal
    {
        public IInitParams CreateInitParams()
        {
            return new PositionDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public bool Delete(long id)
        {
            bool removed = base.Delete<Position>("p_Position_Delete", id, "@ID");

            return removed;
        }

        public Position Get(long id)
        {
            Position entityOut = base.Get<Position>("p_Position_GetDetails", id, "@ID", PositionFromRow);

            return entityOut;
        }

        public IList<Position> GetAll()
        {
            IList<Position> result = base.GetAll<Position>("p_Position_GetAll", PositionFromRow);

            return result;
        }

        public Position Upsert(Position entity) 
        {
            Position entityOut = base.Upsert<Position>("p_Position_Upsert", entity, AddUpsertParameters, PositionFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, Position entity)
        {
            		   SqlParameter pID = new SqlParameter(@"ID",    SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 

		   SqlParameter pDepartmentID = new SqlParameter(@"DepartmentID",    SqlDbType.BigInt, 0, ParameterDirection.Input, true, 0, 0, "DepartmentID", DataRowVersion.Current, (object)entity.DepartmentID != null ? (object)entity.DepartmentID : DBNull.Value);   cmd.Parameters.Add(pDepartmentID); 

		   SqlParameter pTitle = new SqlParameter(@"Title",    SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Title", DataRowVersion.Current, (object)entity.Title != null ? (object)entity.Title : DBNull.Value);   cmd.Parameters.Add(pTitle); 

		   SqlParameter pShortDesc = new SqlParameter(@"ShortDesc",    SqlDbType.NVarChar, 250, ParameterDirection.Input, false, 0, 0, "ShortDesc", DataRowVersion.Current, (object)entity.ShortDesc != null ? (object)entity.ShortDesc : DBNull.Value);   cmd.Parameters.Add(pShortDesc); 

		   SqlParameter pDescription = new SqlParameter(@"Description",    SqlDbType.NVarChar, 4000, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Current, (object)entity.Description != null ? (object)entity.Description : DBNull.Value);   cmd.Parameters.Add(pDescription); 

		   SqlParameter pStatusID = new SqlParameter(@"StatusID",    SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "StatusID", DataRowVersion.Current, (object)entity.StatusID != null ? (object)entity.StatusID : DBNull.Value);   cmd.Parameters.Add(pStatusID); 

		   SqlParameter pCreatedDate = new SqlParameter(@"CreatedDate",    SqlDbType.DateTime2, 0, ParameterDirection.Input, false, 0, 0, "CreatedDate", DataRowVersion.Current, (object)entity.CreatedDate != null ? (object)entity.CreatedDate : DBNull.Value);   cmd.Parameters.Add(pCreatedDate); 

		   SqlParameter pCreatedByID = new SqlParameter(@"CreatedByID",    SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CreatedByID", DataRowVersion.Current, (object)entity.CreatedByID != null ? (object)entity.CreatedByID : DBNull.Value);   cmd.Parameters.Add(pCreatedByID); 

		   SqlParameter pModifiedDate = new SqlParameter(@"ModifiedDate",    SqlDbType.DateTime2, 0, ParameterDirection.Input, true, 0, 0, "ModifiedDate", DataRowVersion.Current, (object)entity.ModifiedDate != null ? (object)entity.ModifiedDate : DBNull.Value);   cmd.Parameters.Add(pModifiedDate); 

		   SqlParameter pModifiedByID = new SqlParameter(@"ModifiedByID",    SqlDbType.BigInt, 0, ParameterDirection.Input, true, 0, 0, "ModifiedByID", DataRowVersion.Current, (object)entity.ModifiedByID != null ? (object)entity.ModifiedByID : DBNull.Value);   cmd.Parameters.Add(pModifiedByID); 



            return cmd;
        }

        protected Position PositionFromRow(DataRow row)
        {
            var entity = new Position();

            		entity.ID = (System.Int64?)row["ID"];
		entity.DepartmentID = !DBNull.Value.Equals(row["DepartmentID"]) ?  (System.Int64?)row["DepartmentID"] : null;
		entity.Title = (System.String)row["Title"];
		entity.ShortDesc = (System.String)row["ShortDesc"];
		entity.Description = (System.String)row["Description"];
		entity.StatusID = (System.Int64)row["StatusID"];
		entity.CreatedDate = (System.DateTime)row["CreatedDate"];
		entity.CreatedByID = (System.Int64)row["CreatedByID"];
		entity.ModifiedDate = !DBNull.Value.Equals(row["ModifiedDate"]) ?  (System.DateTime?)row["ModifiedDate"] : null;
		entity.ModifiedByID = !DBNull.Value.Equals(row["ModifiedByID"]) ?  (System.Int64?)row["ModifiedByID"] : null;


            return entity;
        }
		
		public void SetSkills(long id, IList<PositionSkill> skills)
        {
            using (var conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_PositionSkills_Upsert", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                var tblSkills = PositionSkillsToTable(skills);

                AddParameter(cmd, "@PositionID", SqlDbType.BigInt, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, id);

                AddParameter(cmd, "@Skills", SqlDbType.Structured, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, tblSkills);

                cmd.ExecuteNonQuery();
            }
        }

        public IList<PositionSkill> GetSkills(long id)
        {
            IList<PositionSkill> result = null;

            using (var conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_PositionSkills_GetByPosition", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@PositionID", SqlDbType.BigInt, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, id);

                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Current, null);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count >= 1)
                {
                    result = new List<PositionSkill>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var p = PositionSkillFromRow(row);

                        result.Add(p);
                    }
                }
            }

            return result;
        }
		
		private PositionSkill PositionSkillFromRow(DataRow row)
        {
            var entity = new PositionSkill();
            entity.IsMandatory = (bool)row["IsMandatory"];
            entity.ProficiencyID = (long)row["SkillProficiencyID"];
            entity.ProficiencyName = (string)row["SkillProficiency"];
            entity.SkillName = (string)row["SkillName"];
            entity.SkillID = (long)row["SkillID"];
            entity.PositionID = (long)row["PositionID"];

            return entity;
        }

        private DataTable PositionSkillsToTable(IList<PositionSkill> skills)
        {
            var table = new DataTable();
            table.Columns.Add(new DataColumn("SkillID", typeof(long)));
            table.Columns.Add(new DataColumn("IsMandatory", typeof(bool)));
            table.Columns.Add(new DataColumn("ProficiencyID", typeof(long)));

            foreach(var s in skills)
            {
                var row = table.NewRow();
                row[0] = s.SkillID;
                row[1] = s.IsMandatory;
                row[2] = s.ProficiencyID;

                table.Rows.Add(row);
            }

            return table;
        }

        public long? Upsert(Position entity, long? editorID)
        {
            throw new NotImplementedException();
        }
    }
}
