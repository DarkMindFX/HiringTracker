

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRT.Interfaces.Entities;

namespace HRT.Interfaces
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

