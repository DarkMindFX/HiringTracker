

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRT.Interfaces.Entities;

namespace HRT.Interfaces
{
    public interface IInterviewFeedbackDal : IDalBase<InterviewFeedback>
    {
                IList<InterviewFeedback> GetByInterviewID(System.Int64 InterviewID);
                IList<InterviewFeedback> GetByInterviewerID(System.Int64 InterviewerID);
                IList<InterviewFeedback> GetByCreatedByID(System.Int64 CreatedByID);
                IList<InterviewFeedback> GetByModifiedByID(System.Int64? ModifiedByID);
            }
}

