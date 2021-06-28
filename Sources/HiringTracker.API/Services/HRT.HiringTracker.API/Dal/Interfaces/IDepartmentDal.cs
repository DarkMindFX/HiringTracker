

using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.HiringTracker.API.Dal
{
    public interface IDepartmentDal : IDalBase<Department>
    {
        Department Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            IList<Department> GetByParentID(System.Int64? ParentID);
            IList<Department> GetByManagerID(System.Int64 ManagerID);
        }
}
