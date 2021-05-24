using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.Interfaces.Entities
{
    public class PositionCandidateStep
    {
        public long StepID
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public bool ReqDueDate
        {
            get; set;
        }

        public int RequiresRespInDays
        {
            get; set;
        }
    }
}
