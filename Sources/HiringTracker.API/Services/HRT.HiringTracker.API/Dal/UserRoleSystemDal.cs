

using HRT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(IUserRoleSystemDal))]
    public class UserRoleSystemDal : DalBaseImpl<UserRoleSystem, Interfaces.IUserRoleSystemDal>, IUserRoleSystemDal
    {

        public UserRoleSystemDal(Interfaces.IUserRoleSystemDal dalImpl) : base(dalImpl)
        {
        }

        public UserRoleSystem Get(System.Int64 UserID,System.Int64 RoleID)
        {
            return _dalImpl.Get(            UserID,            RoleID);
        }

        public bool Delete(System.Int64 UserID,System.Int64 RoleID)
        {
            return _dalImpl.Delete(            UserID,            RoleID);
        }

        public IList<UserRoleSystem> GetByUserID(System.Int64 UserID)
        {
            return _dalImpl.GetByUserID(UserID);
        }
        public IList<UserRoleSystem> GetByRoleID(System.Int64 RoleID)
        {
            return _dalImpl.GetByRoleID(RoleID);
        }
            }
}
