using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.Interfaces
{
    public interface IInterviewSearchParams
    {
        long? TypeID { get; set; }

        long? StatusID { get; set; }

        DateTime? StartTime { get; set; }

        DateTime? EndTime { get; set; }

        long? CandidateID { get; set; }

        long? InterviewerID { get; set; }

        long? PositionID { get; set; }
    }

    public interface IInterviewDal : IDalBase<Interview>
    {
        IList<Interview> Find(IInterviewSearchParams searchParams);
    }
}
