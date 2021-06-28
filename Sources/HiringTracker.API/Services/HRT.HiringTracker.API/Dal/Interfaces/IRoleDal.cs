

using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.HiringTracker.API.Dal
{
    public interface IRoleDal : IDalBase<Role>
    {
        Role Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

        }
}
