using System;

namespace GrantsCollaboration.Models
{
    public class Vote
    {
        public string UserName { get; set; } = "";
        public int ApproveVoteCount { get; set; } = 0;
        public int RejectVoteCount { get; set; } = 0;
        public DateTime VoteTime { get; set; }
    }
}
