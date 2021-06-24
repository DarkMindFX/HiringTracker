

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRT.Interfaces.Entities;

namespace HRT.Interfaces
{
    public interface IUserRolePositionDal : IDalBase<UserRolePosition>
    {
        UserRolePosition Get(
                    System.Int64 PositionID,
                    System.Int64 UserID
        );

        bool Delete(
                    System.Int64 PositionID,
                    System.Int64 UserID
        );

                IList<UserRolePosition> GetByPositionID(System.Int64 PositionID);
                IList<UserRolePosition> GetByUserID(System.Int64 UserID);
                IList<UserRolePosition> GetByRoleID(System.Int64 RoleID);
            }
}

