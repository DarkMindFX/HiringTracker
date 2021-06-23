

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
                IList<UserRoleSystem> GetByUserID(System.Int64 UserID);
                IList<UserRoleSystem> GetByRoleID(System.Int64 RoleID);
            }
}

