using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.Interfaces.Entities
{
    public class TrackedEntity
    {
        public DateTime CreatedDate
        {
            get; set;
        }


        public long CreatedByID
        {
            get; set;
        }

        public DateTime? ModifiedDate
        {
            get; set;
        }

        public long? ModifiedByID
        {
            get; set;
        }
    }
}
