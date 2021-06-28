

using HRT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(IDepartmentDal))]
    public class DepartmentDal : DalBaseImpl<Department, Interfaces.IDepartmentDal>, IDepartmentDal
    {

        public DepartmentDal(Interfaces.IDepartmentDal dalImpl) : base(dalImpl)
        {
        }

        public Department Get(System.Int64? ID)
        {
            return _dalImpl.Get(ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(ID);
        }

        public IList<Department> GetByParentID(System.Int64? ParentID)
        {
            return _dalImpl.GetByParentID(ParentID);
        }
        public IList<Department> GetByManagerID(System.Int64 ManagerID)
        {
            return _dalImpl.GetByManagerID(ManagerID);
        }
    }
}
