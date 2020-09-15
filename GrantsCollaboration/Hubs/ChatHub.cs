using Azure;
using GrantsCollaboration.Helpers;
using GrantsCollaboration.Models;
using GrantsEntities;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        HttpClient httpClient;
        EntityHelper eh;

        public ChatHub(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient();
            eh = new EntityHelper();
        }

        public async Task SendMessage(string message)
        {
            string? sender = Context.User?.Identity?.Name ?? "anonymous";
            await Clients.All.SendAsync("ReceiveMessage", sender, message);
        }

        public async Task UpdateCurrentVotes(string grantId)
        {
            Grant? grant = await eh.GetCurrentGrantStatus(grantId, httpClient);
            int approveCount = 0;
            int rejectCount = 0;
            var approveNames = new List<string>();
            var rejectNames = new List<string>();

            if (grant != null)
            {
                approveCount = grant.ApproveCount;
                rejectCount = grant.RejectCount;
                GetVoteNames(grant, out approveNames, out rejectNames);
            }
            await Clients.All.SendAsync("UpdateCount", grantId, approveCount, rejectCount, string.Join(", ", approveNames), string.Join(", ", rejectNames));
        }

        private static void GetVoteNames(Grant grant, out List<string> approveNames, out List<string> rejectNames)
        {
            approveNames = grant.Votes.Where(v => v.ApproveVoteCount == 1).Select(v => v.UserName.Split('.').First()).ToList();
            rejectNames = grant.Votes.Where(v => v.RejectVoteCount == 1).Select(v => v.UserName.Split('.').First()).ToList();
        }

        public async Task VoteApprove(string grantId)
        {
            Grant grant = await eh.GetCurrentGrantStatus(grantId, httpClient);

            string url = "https://myAzureWebsiteAzureFunctions.azurewebsites.net/api/VoteApprove";
#if DEBUG
            url = "http://localhost:7071/api/VoteApprove";
#endif
            string username = Context.User?.Identity?.Name ?? "anonymous";
            HttpResponseMessage? response = await httpClient.PostAsJsonAsync(url, new NewVote { GrantId = grantId, Username = username });
            if (response?.IsSuccessStatusCode ?? false)
            {
                GetVoteNames(grant, out List<string> approveNames, out List<string> rejectNames);
                await Clients.All.SendAsync("UpdateCount",
                                            grantId,
                                            ++grant.ApproveCount,
                                            grant.RejectCount,
                                            string.Join(", ", approveNames.Concat(new string[] { username.Split('.').First() })),
                                            rejectNames);
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task VoteReject(string grantId)
        {
            Grant grant = await eh.GetCurrentGrantStatus(grantId, httpClient);

            string url = "https://myAzureWebsiteAzureFunctions.azurewebsites.net/api/VoteReject";
#if DEBUG
            url = "http://localhost:7071/api/VoteReject";
#endif
            string username = Context.User?.Identity?.Name ?? "anonymous";
            HttpResponseMessage? response = await httpClient.PostAsJsonAsync(url, new NewVote { GrantId = grantId, Username = username });
            if (response?.IsSuccessStatusCode ?? false)
            {
                GetVoteNames(grant, out List<string> approveNames, out List<string> rejectNames);
                await Clients.All.SendAsync("UpdateCount",
                                            grantId,
                                            grant.ApproveCount,
                                            ++grant.RejectCount,
                                            approveNames,
                                            string.Join(", ", rejectNames.Concat(new string[] { username.Split('.').First() })));
            }
            else
            {
                throw new Exception();
            }
        }
    }
}