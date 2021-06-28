

using HRT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(IPositionDal))]
    public class PositionDal : DalBaseImpl<Position, Interfaces.IPositionDal>, IPositionDal
    {

        public PositionDal(Interfaces.IPositionDal dalImpl) : base(dalImpl)
        {
        }

        public Position Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }

        public IList<Position> GetByDepartmentID(System.Int64? DepartmentID)
        {
            return _dalImpl.GetByDepartmentID(DepartmentID);
        }
        public IList<Position> GetByStatusID(System.Int64 StatusID)
        {
            return _dalImpl.GetByStatusID(StatusID);
        }
        public IList<Position> GetByCreatedByID(System.Int64 CreatedByID)
        {
            return _dalImpl.GetByCreatedByID(CreatedByID);
        }
        public IList<Position> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            return _dalImpl.GetByModifiedByID(ModifiedByID);
        }
            }
}
