

using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.HiringTracker.API.Dal
{
    public interface ICandidatePropertyDal : IDalBase<CandidateProperty>
    {
        CandidateProperty Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            IList<CandidateProperty> GetByCandidateID(System.Int64 CandidateID);
        }
}
