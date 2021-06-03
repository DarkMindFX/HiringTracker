using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.Interfaces.Entities
{
    public class InterviewFeedback : TrackedEntity
    {
        public long FeedbackID
        {
            get; set;
        }

        public string Comment
        {
            get; set;
        }

        public int Rating
        {
            get; set;
        }

        public long InterviewID
        {
            get; set;
        }

        public long InterviewerID
        {
            get; set;
        }
    }
}
