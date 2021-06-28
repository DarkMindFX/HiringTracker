

using HRT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(IUserRolePositionDal))]
    public class UserRolePositionDal : DalBaseImpl<UserRolePosition, Interfaces.IUserRolePositionDal>, IUserRolePositionDal
    {

        public UserRolePositionDal(Interfaces.IUserRolePositionDal dalImpl) : base(dalImpl)
        {
        }

        public UserRolePosition Get(System.Int64 PositionID,System.Int64 UserID)
        {
            return _dalImpl.Get(            PositionID,            UserID);
        }

        public bool Delete(System.Int64 PositionID,System.Int64 UserID)
        {
            return _dalImpl.Delete(            PositionID,            UserID);
        }

        public IList<UserRolePosition> GetByPositionID(System.Int64 PositionID)
        {
            return _dalImpl.GetByPositionID(PositionID);
        }
        public IList<UserRolePosition> GetByUserID(System.Int64 UserID)
        {
            return _dalImpl.GetByUserID(UserID);
        }
        public IList<UserRolePosition> GetByRoleID(System.Int64 RoleID)
        {
            return _dalImpl.GetByRoleID(RoleID);
        }
            }
}
