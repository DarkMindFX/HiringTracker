using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.Interfaces
{
    public interface ICandidateDal : IDalBase<Candidate>
    {
        IList<Skill> GetSkills(long id);

        void SetSkills(long id, IList<Skill> skills);
    }
}
