using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.Interfaces.Entities
{
    public class PositionSkill
    {
        public long PositionID
        {
            get; set;
        }

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

        public bool IsMandatory
        {
            get; set;
        }
    }
}
