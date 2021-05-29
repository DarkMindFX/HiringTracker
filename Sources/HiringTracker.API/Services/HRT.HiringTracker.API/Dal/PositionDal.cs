using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(IPositionDal))]
    public class PositionDal : DalBaseImpl<Position, Interfaces.IPositionDal>, Dal.IPositionDal
    {

        public PositionDal(Interfaces.IPositionDal dalImpl) : base(dalImpl)
        {
        }

        public new IDictionary<long, Position> GetAllAsDictionary()
        {
            var statuses = _dalImpl.GetAll();

            IDictionary<long, Position> result = statuses.ToDictionary(s => s.PositionID ?? 0);

            return result;
        }

        public IList<PositionSkill> GetSkills(long id)
        {
            return _dalImpl.GetSkills(id);
        }

        public void SetSkills(long id, IList<PositionSkill> skills)
        {
            _dalImpl.SetSkills(id, skills);
        }
    }
}
