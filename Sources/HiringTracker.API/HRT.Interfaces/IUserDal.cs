using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.Interfaces
{
    public interface IUserDal : IDalBase<User>
    {
        User GetByLogin(string login);        
    }
}
