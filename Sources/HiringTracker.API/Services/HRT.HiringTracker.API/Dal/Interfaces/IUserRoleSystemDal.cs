

using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.HiringTracker.API.Dal
{
    public interface IUserRoleSystemDal : IDalBase<UserRoleSystem>
    {
        UserRoleSystem Get(System.Int64 UserID,System.Int64 RoleID);

        bool Delete(System.Int64 UserID,System.Int64 RoleID);

            IList<UserRoleSystem> GetByUserID(System.Int64 UserID);
            IList<UserRoleSystem> GetByRoleID(System.Int64 RoleID);
        }
}
