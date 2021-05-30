using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.Interfaces.Entities
{
    public class PositionCandidate : TrackedEntity
    {
        public long? ProposalID
        {
            get; set;
        }

        public long CandidateID
        {
            get; set;
        }

        public long PositionID
        {
            get; set;
        }

        public long StatusID
        {
            get; set;
        }

        public string StatusName
        {
            get; set;
        }

        public long CurrentStepID
        {
            get; set;
        }

        public string CurrentStepName
        {
            get; set;
        }

        public long NextStepID
        {
            get; set;
        }

        public string NextStepName
        {
            get; set;
        }

        public DateTime Proposed
        {
            get; set;
        }

        public DateTime? DueDate
        {
            get; set;
        }

    }
}
