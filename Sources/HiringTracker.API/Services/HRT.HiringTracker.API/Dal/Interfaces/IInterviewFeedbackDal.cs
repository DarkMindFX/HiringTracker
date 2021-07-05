

using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.HiringTracker.API.Dal
{
    public interface IInterviewFeedbackDal : IDalBase<InterviewFeedback>
    {
        InterviewFeedback Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            IList<InterviewFeedback> GetByInterviewID(System.Int64 InterviewID);
            IList<InterviewFeedback> GetByInterviewerID(System.Int64 InterviewerID);
            IList<InterviewFeedback> GetByCreatedByID(System.Int64 CreatedByID);
            IList<InterviewFeedback> GetByModifiedByID(System.Int64? ModifiedByID);
        }
}
