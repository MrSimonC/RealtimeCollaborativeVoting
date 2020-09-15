using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;

namespace GrantsCollaboration.Models
{
    [JsonObject(MemberSerialization.OptIn)]

    public class Grant : IGrant
    {
        [JsonProperty("Name")]
        public string Name { get; set; } = "";

        [JsonProperty("description")]
        public string Description { get; set; } = "";

        [JsonProperty("sharepointUrl")]
        public string SharepointUrl { get; set; } = "";

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("approveCount")]
        public int ApproveCount { get; set; }

        [JsonProperty("rejectCount")]
        public int RejectCount { get; set; }

        [JsonProperty("votes")]
        public List<Vote> Votes { get; set; } = new List<Vote>();

        [JsonProperty("hasStarted")]
        public bool HasStarted { get; set; }

        public void AddApproveVote(string username)
        {
            ApproveCount++;
            Votes.Add(new Vote
            {
                UserName = username,
                ApproveVoteCount = 1,
                VoteTime = DateTime.Now
            });
        }

        public void AddRejectVote(string username)
        {
            RejectCount++;
            Votes.Add(new Vote
            {
                UserName = username,
                RejectVoteCount = 1,
                VoteTime = DateTime.Now
            });
        }

        public void SetName(string name) => Name = name;

        public void SetDescription(string description) => Description = description;
        public void SetUrl(string url) => SharepointUrl = url;
        public void SetDate(DateTime date) => Date = date;
        public void SetAmount(decimal amount) => Amount = amount;
        public void SetHasStarted() => HasStarted = true;

        [FunctionName(nameof(Grant))]
        public static Task Run([EntityTrigger] IDurableEntityContext ctx)
        => ctx.DispatchAsync<Grant>();
    }
}
