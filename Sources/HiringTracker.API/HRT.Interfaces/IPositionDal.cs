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
        IList<PositionSkill> GetSkills(long id);

        void SetSkills(long id, IList<PositionSkill> skills);
    }
}
