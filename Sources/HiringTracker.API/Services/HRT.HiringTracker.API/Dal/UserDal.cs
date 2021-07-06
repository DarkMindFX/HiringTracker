

using HRT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(IUserDal))]
    public class UserDal : DalBaseImpl<User, Interfaces.IUserDal>, IUserDal
    {

        public UserDal(Interfaces.IUserDal dalImpl) : base(dalImpl)
        {
        }

        public User Get(System.Int64? ID)
        {
            return _dalImpl.Get(ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(ID);
        }

        public User GetByLogin(string Login)
        {
            return _dalImpl.GetByLogin(Login);
        }
    }
}
