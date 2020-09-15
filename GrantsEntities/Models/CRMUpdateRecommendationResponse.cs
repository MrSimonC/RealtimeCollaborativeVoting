using System;
using System.Collections.Generic;
using System.Text;

namespace GrantsEntities.Models
{
    public class CRMUpdateRecommendationResponse
    {
        public bool isFailed { get; set; }
        public bool isSuccess { get; set; }
        public Reason[] reasons { get; set; }
        public Error[] errors { get; set; }
        public Success[] successes { get; set; }
    }

    public class Reason
    {
        public string message { get; set; }
        public Metadata metadata { get; set; }
    }

    public class Metadata
    {
    }

    public class Error
    {
        public string message { get; set; }
        public Metadata1 metadata { get; set; }
        public object[] reasons { get; set; }
    }

    public class Metadata1
    {
    }

    public class Success
    {
        public string message { get; set; }
        public Metadata2 metadata { get; set; }
    }

    public class Metadata2
    {
    }

}
