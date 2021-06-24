

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRT.Interfaces.Entities;

namespace HRT.Interfaces
{
    public interface IPositionSkillDal : IDalBase<PositionSkill>
    {
        PositionSkill Get(
                    System.Int64? ID
        );

        bool Delete(
                    System.Int64? ID
        );

                IList<PositionSkill> GetByPositionID(System.Int64 PositionID);
                IList<PositionSkill> GetBySkillID(System.Int64 SkillID);
                IList<PositionSkill> GetBySkillProficiencyID(System.Int64 SkillProficiencyID);
            }
}

