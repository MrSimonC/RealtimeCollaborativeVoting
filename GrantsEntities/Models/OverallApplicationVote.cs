using System;
using System.Collections.Generic;
using System.Text;

namespace GrantsEntities.Models
{
    public class OverallApplicationVote
    {
        public string ReviewId { get; set; } = "";
        public bool Approve { get; set; }
    }
}
