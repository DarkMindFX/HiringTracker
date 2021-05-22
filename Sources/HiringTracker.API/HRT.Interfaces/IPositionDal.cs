using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.Interfaces
{
    public interface IPositionDal : IDalBase<Position>
    {
        IList<PositionSkill> GetSkills(long id);

        void SetSkills(long id, IList<PositionSkill> skills);
    }
}
