using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrantsCollaboration.Models
{
    public class OverallApplicationVoteRequest
    {
        public string ReviewId { get; set; } = "";
        public bool Approve { get; set; }
    }
}
