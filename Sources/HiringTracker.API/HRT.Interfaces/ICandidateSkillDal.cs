

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRT.Interfaces.Entities;

namespace HRT.Interfaces
{
    public interface ICandidateSkillDal : IDalBase<CandidateSkill>
    {
        CandidateSkill Get(System.Int64 CandidateID, System.Int64 SkillID);

        bool Delete(System.Int64 CandidateID, System.Int64 SkillID);

        void SetCandidateSkills(System.Int64 CandidateID, IList<CandidateSkill> Skills);

        IList<CandidateSkill> GetByCandidateID(System.Int64 CandidateID);
        IList<CandidateSkill> GetBySkillID(System.Int64 SkillID);
        IList<CandidateSkill> GetBySkillProficiencyID(System.Int64 SkillProficiencyID);
    }
}

