

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRT.Interfaces.Entities;

namespace HRT.Interfaces
{
    public interface IInterviewStatusDal : IDalBase<InterviewStatus>
    {
        InterviewStatus Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            }
}

