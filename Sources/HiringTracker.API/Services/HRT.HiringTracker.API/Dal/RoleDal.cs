

using HRT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(IRoleDal))]
    public class RoleDal : DalBaseImpl<Role, Interfaces.IRoleDal>, IRoleDal
    {

        public RoleDal(Interfaces.IRoleDal dalImpl) : base(dalImpl)
        {
        }

        public Role Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }

            }
}
