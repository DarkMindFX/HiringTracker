

using HRT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace HRT.HiringTracker.API.Dal
{
    [Export(typeof(ICandidateSkillDal))]
    public class CandidateSkillDal : DalBaseImpl<CandidateSkill, Interfaces.ICandidateSkillDal>, ICandidateSkillDal
    {

        public CandidateSkillDal(Interfaces.ICandidateSkillDal dalImpl) : base(dalImpl)
        {
        }

        public CandidateSkill Get(System.Int64 CandidateID, System.Int64 SkillID)
        {
            return _dalImpl.Get(CandidateID, SkillID);
        }

        public bool Delete(System.Int64 CandidateID, System.Int64 SkillID)
        {
            return _dalImpl.Delete(CandidateID, SkillID);
        }

        public IList<CandidateSkill> GetByCandidateID(System.Int64 CandidateID)
        {
            return _dalImpl.GetByCandidateID(CandidateID);
        }
        public IList<CandidateSkill> GetBySkillID(System.Int64 SkillID)
        {
            return _dalImpl.GetBySkillID(SkillID);
        }
        public IList<CandidateSkill> GetBySkillProficiencyID(System.Int64 SkillProficiencyID)
        {
            return _dalImpl.GetBySkillProficiencyID(SkillProficiencyID);
        }

        public void SetCandidateSkills(long CandidateID, IList<CandidateSkill> Skills)
        {
            _dalImpl.SetCandidateSkills(CandidateID, Skills);
        }
    }
}
