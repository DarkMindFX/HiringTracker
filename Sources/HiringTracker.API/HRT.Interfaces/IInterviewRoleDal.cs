using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.Interfaces
{
    public interface IInterviewRoleDal : IInitializable
    {

        bool UpsertUserRole(long interviewId, long userId, long roleId);

        bool RemoveUserRole(long interviewId, long userId);

        IList<InterviewRole> GetByInterview(long id);

        IList<InterviewRole> GetByUser(long id);

        IList<InterviewRole> GetByRole(long id);
    }
}
