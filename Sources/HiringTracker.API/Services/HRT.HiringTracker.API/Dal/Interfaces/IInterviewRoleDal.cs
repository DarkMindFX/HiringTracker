

using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.HiringTracker.API.Dal
{
    public interface IInterviewRoleDal : IDalBase<InterviewRole>
    {
        InterviewRole Get(System.Int64 InterviewID,System.Int64 UserID);

        bool Delete(System.Int64 InterviewID,System.Int64 UserID);

            IList<InterviewRole> GetByInterviewID(System.Int64 InterviewID);
            IList<InterviewRole> GetByUserID(System.Int64 UserID);
            IList<InterviewRole> GetByRoleID(System.Int64 RoleID);
        }
}
