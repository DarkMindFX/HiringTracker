

using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.HiringTracker.API.Dal
{
    public interface IUserRolePositionDal : IDalBase<UserRolePosition>
    {
        UserRolePosition Get(System.Int64 PositionID,System.Int64 UserID);

        bool Delete(System.Int64 PositionID,System.Int64 UserID);

            IList<UserRolePosition> GetByPositionID(System.Int64 PositionID);
            IList<UserRolePosition> GetByUserID(System.Int64 UserID);
            IList<UserRolePosition> GetByRoleID(System.Int64 RoleID);
        }
}
