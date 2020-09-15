using System;
using System.Threading.Tasks;

namespace GrantsCollaboration.Models
{
    public interface IGrant
    {
        void AddApproveVote(string username);
        void AddRejectVote(string username);
        void SetName(string name);
        void SetDescription(string description);
        void SetUrl(string url);
        void SetDate(DateTime date);
        void SetAmount(decimal amount);
        void SetHasStarted();
    }
}