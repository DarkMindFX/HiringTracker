

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRT.Interfaces.Entities;

namespace HRT.Interfaces
{
    public interface IDepartmentDal : IDalBase<Department>
    {
        Department Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

        IList<Department> GetByParentID(System.Int64? ParentID);
        IList<Department> GetByManagerID(System.Int64 ManagerID);
            }
}

