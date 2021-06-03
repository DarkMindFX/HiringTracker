using HRT.Common;
using HRT.Interfaces;
using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HRT.DAL.MSSQL
{
    class InterviewRoleDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IInterviewRoleDal))]
    public class InterviewRoleDal : SQLDal, IInterviewRoleDal
    {
        public IInitParams CreateInitParams()
        {
            return new InterviewRoleDalInitParams();
        }

        public IList<InterviewRole> GetByInterview(long id)
        {
            throw new NotImplementedException();
        }

        public IList<InterviewRole> GetByRole(long id)
        {
            throw new NotImplementedException();
        }

        public IList<InterviewRole> GetByUser(long id)
        {
            throw new NotImplementedException();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public bool RemoveUserRole(long interviewId, long userId)
        {
            bool result = false;

            using (var conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_InterviewRole_RemoveUserRole", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@InterviewID", SqlDbType.BigInt, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, interviewId);

                AddParameter(cmd, "@UserId", SqlDbType.BigInt, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, userId);

                cmd.ExecuteNonQuery();
            }

            return result;
        }

        public bool UpsertUserRole(long interviewId, long userId, long roleId)
        {
            throw new NotImplementedException();
        }
    }
}
