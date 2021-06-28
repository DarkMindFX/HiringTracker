

using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.HiringTracker.API.Dal
{
    public interface ICandidateSkillDal : IDalBase<CandidateSkill>
    {
        CandidateSkill Get(System.Int64 CandidateID,System.Int64 SkillID);

        bool Delete(System.Int64 CandidateID,System.Int64 SkillID);

            IList<CandidateSkill> GetByCandidateID(System.Int64 CandidateID);
            IList<CandidateSkill> GetBySkillID(System.Int64 SkillID);
            IList<CandidateSkill> GetBySkillProficiencyID(System.Int64 SkillProficiencyID);
        }
}
