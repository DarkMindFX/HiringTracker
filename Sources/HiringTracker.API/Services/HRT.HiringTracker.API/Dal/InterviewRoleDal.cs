

using HRT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(IInterviewRoleDal))]
    public class InterviewRoleDal : DalBaseImpl<InterviewRole, Interfaces.IInterviewRoleDal>, IInterviewRoleDal
    {

        public InterviewRoleDal(Interfaces.IInterviewRoleDal dalImpl) : base(dalImpl)
        {
        }

        public InterviewRole Get(System.Int64 InterviewID,System.Int64 UserID)
        {
            return _dalImpl.Get(            InterviewID,            UserID);
        }

        public bool Delete(System.Int64 InterviewID,System.Int64 UserID)
        {
            return _dalImpl.Delete(            InterviewID,            UserID);
        }

        public IList<InterviewRole> GetByInterviewID(System.Int64 InterviewID)
        {
            return _dalImpl.GetByInterviewID(InterviewID);
        }
        public IList<InterviewRole> GetByUserID(System.Int64 UserID)
        {
            return _dalImpl.GetByUserID(UserID);
        }
        public IList<InterviewRole> GetByRoleID(System.Int64 RoleID)
        {
            return _dalImpl.GetByRoleID(RoleID);
        }
            }
}
