using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.Interfaces.Entities
{
    public class Candidate : TrackedEntity
    {
        public long CandidateID
        {
            get; set;
        }

        public string FirstName
        {
            get; set;
        }

        public string MiddleName
        {
            get; set;
        }

        public string LastName
        {
            get; set;
        }

        public string Email
        {
            get; set;
        }

        public string Phone
        {
            get; set;
        }

        public string CVLink
        {
            get; set;
        }
    }
}
