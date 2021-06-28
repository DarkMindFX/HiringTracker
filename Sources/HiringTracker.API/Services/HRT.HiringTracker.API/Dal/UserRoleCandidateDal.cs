

using HRT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(IUserRoleCandidateDal))]
    public class UserRoleCandidateDal : DalBaseImpl<UserRoleCandidate, Interfaces.IUserRoleCandidateDal>, IUserRoleCandidateDal
    {

        public UserRoleCandidateDal(Interfaces.IUserRoleCandidateDal dalImpl) : base(dalImpl)
        {
        }

        public UserRoleCandidate Get(System.Int64 CandidateID,System.Int64 UserID)
        {
            return _dalImpl.Get(            CandidateID,            UserID);
        }

        public bool Delete(System.Int64 CandidateID,System.Int64 UserID)
        {
            return _dalImpl.Delete(            CandidateID,            UserID);
        }

        public IList<UserRoleCandidate> GetByCandidateID(System.Int64 CandidateID)
        {
            return _dalImpl.GetByCandidateID(CandidateID);
        }
        public IList<UserRoleCandidate> GetByUserID(System.Int64 UserID)
        {
            return _dalImpl.GetByUserID(UserID);
        }
        public IList<UserRoleCandidate> GetByRoleID(System.Int64 RoleID)
        {
            return _dalImpl.GetByRoleID(RoleID);
        }
            }
}
