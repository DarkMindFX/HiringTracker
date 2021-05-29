using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;

namespace HRT.HiringTracker.API.Dal
{

    [Export(typeof(IUserDal))]
    public class UserDal : DalBaseImpl<User, Interfaces.IUserDal>, Dal.IUserDal
    {

        public UserDal(Interfaces.IUserDal dalImpl) : base(dalImpl)
        {
        }

        public new IDictionary<long, User> GetAllAsDictionary()
        {
            var statuses = _dalImpl.GetAll();

            IDictionary<long, User> result = statuses.ToDictionary(s => (long)s.UserID);

            return result;
        }

        public User GetByLogin(string login)
        {
            return _dalImpl.GetByLogin(login);
        }
    }
}
