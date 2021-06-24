

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRT.Interfaces.Entities;

namespace HRT.Interfaces
{
    public interface ICandidateDal : IDalBase<Candidate>
    {
        Candidate Get(
                    System.Int64? ID
        );

        bool Delete(
                    System.Int64? ID
        );

                IList<Candidate> GetByCreatedByID(System.Int64 CreatedByID);
                IList<Candidate> GetByModifiedByID(System.Int64? ModifiedByID);
            }
}

