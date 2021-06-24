

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRT.Interfaces.Entities;

namespace HRT.Interfaces
{
    public interface IUserRoleCandidateDal : IDalBase<UserRoleCandidate>
    {
        UserRoleCandidate Get(
                    System.Int64 CandidateID,
                    System.Int64 UserID
        );

        bool Delete(
                    System.Int64 CandidateID,
                    System.Int64 UserID
        );

                IList<UserRoleCandidate> GetByCandidateID(System.Int64 CandidateID);
                IList<UserRoleCandidate> GetByUserID(System.Int64 UserID);
                IList<UserRoleCandidate> GetByRoleID(System.Int64 RoleID);
            }
}

