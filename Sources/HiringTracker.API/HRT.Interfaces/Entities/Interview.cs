using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.Interfaces.Entities
{
    public class Interview : TrackedEntity
    {
        public long InterviewID
        {
            get; set;
        }

        public long ProposalID
        {
            get; set;
        }

        public long InterviewTypeID
        {
            get; set;
        }

        public DateTime StartTime
        {
            get; set;
        }

        public DateTime EndTime
        {
            get; set;
        }

        public long InterviewStatusID
        {
            get; set;
        }
    }
}
