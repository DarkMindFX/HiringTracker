

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRT.Interfaces.Entities;

namespace HRT.Interfaces
{
    public interface IPositionDal : IDalBase<Position>
    {
                IList<Position> GetByDepartmentID(System.Int64? DepartmentID);
                IList<Position> GetByStatusID(System.Int64 StatusID);
                IList<Position> GetByCreatedByID(System.Int64 CreatedByID);
                IList<Position> GetByModifiedByID(System.Int64? ModifiedByID);
            }
}

