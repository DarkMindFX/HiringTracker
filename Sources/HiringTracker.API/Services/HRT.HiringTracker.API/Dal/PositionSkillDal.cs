

using HRT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(IPositionSkillDal))]
    public class PositionSkillDal : DalBaseImpl<PositionSkill, Interfaces.IPositionSkillDal>, IPositionSkillDal
    {

        public PositionSkillDal(Interfaces.IPositionSkillDal dalImpl) : base(dalImpl)
        {
        }

        public PositionSkill Get(System.Int64 PositionID, System.Int64 SkillID)
        {
            return _dalImpl.Get(PositionID, SkillID);
        }

        public bool Delete(System.Int64 PositionID, System.Int64 SkillID)
        {
            return _dalImpl.Delete(PositionID, SkillID);
        }

        public IList<PositionSkill> GetByPositionID(System.Int64 PositionID)
        {
            return _dalImpl.GetByPositionID(PositionID);
        }
        public IList<PositionSkill> GetBySkillID(System.Int64 SkillID)
        {
            return _dalImpl.GetBySkillID(SkillID);
        }
        public IList<PositionSkill> GetBySkillProficiencyID(System.Int64 SkillProficiencyID)
        {
            return _dalImpl.GetBySkillProficiencyID(SkillProficiencyID);
        }

        public void SetPositionSkills(long PositionID, IList<PositionSkill> Skills)
        {
            _dalImpl.SetPositionSkills(PositionID, Skills);
        }
    }
}
