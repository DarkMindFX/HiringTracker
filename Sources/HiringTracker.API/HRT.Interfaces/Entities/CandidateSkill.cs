using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.Interfaces.Entities
{
    public class CandidateSkill
    {
        public long SkillID
        {
            get; set;
        }

        public string SkillName
        {
            get; set;
        }

        public long ProficiencyID
        {
            get; set;
        }

        public string ProficiencyName
        {
            get; set;
        }
    }
}
