

using HRT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.HiringTracker.API.Dal
{
    public interface ICandidateDal : IDalBase<Candidate>
    {
        Candidate Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            IList<Candidate> GetByCreatedByID(System.Int64 CreatedByID);
            IList<Candidate> GetByModifiedByID(System.Int64? ModifiedByID);
        }
}
