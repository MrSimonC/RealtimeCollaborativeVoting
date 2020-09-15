using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrantsCollaboration.Models
{

    public class CurrentVotes
    {
        public int approveCount { get; set; }
        public int rejectCount { get; set; }
        public List<Vote> votes { get; set; } = new List<Vote>();
    }
}
