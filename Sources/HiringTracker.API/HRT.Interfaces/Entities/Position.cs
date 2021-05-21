using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.Interfaces.Entities
{
    public class Position : TrackedEntity
    {
        public long PositionID
        {
            get; set;
        }

         public string Title
        {
            get; set;
        }

        public string ShortDesc
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        
        public long StatusID
        {
            get; set;
        }        
    }
}
