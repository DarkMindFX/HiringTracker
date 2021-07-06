

using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.HiringTracker.API.Dal
{
    public interface IUserDal : IDalBase<User>
    {
        User Get(System.Int64? ID);

        User GetByLogin(string Login);

        bool Delete(System.Int64? ID);
    }
}
