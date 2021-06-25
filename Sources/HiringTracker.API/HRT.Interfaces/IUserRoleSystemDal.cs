

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRT.Interfaces.Entities;

namespace HRT.Interfaces
{
    public interface IUserRoleSystemDal : IDalBase<UserRoleSystem>
    {
        UserRoleSystem Get(System.Int64 UserID,System.Int64 RoleID);

        bool Delete(System.Int64 UserID,System.Int64 RoleID);

        IList<UserRoleSystem> GetByUserID(System.Int64 UserID);
        IList<UserRoleSystem> GetByRoleID(System.Int64 RoleID);
            }
}

