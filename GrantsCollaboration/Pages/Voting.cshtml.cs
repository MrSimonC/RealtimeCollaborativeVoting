using Azure.Core.Pipeline;
using GrantsCollaboration.Helpers;
using GrantsCollaboration.Models;
using GrantsEntities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GrantsCollaboration.Pages
{
    public class VotingModel : PageModel
    {
        readonly HttpClient httpClient;
        readonly EntityHelper eh;
        private readonly ILogger<VotingModel> _logger;
        public Session ActiveSession { get; set; } = new Session();
        public bool Saved { get; set; } = false;

        [BindProperty]
        public List<DocketResponse> GrantApplications { get; set; } = new List<DocketResponse>();
        [BindProperty]
        public string CurrentReviewId { get; set; } = "";

        public VotingModel(ILogger<VotingModel> logger,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            httpClient = httpClientFactory.CreateClient();
            eh = new EntityHelper();
        }

        public async Task OnGetAsync(string meetingId, string currentReviewId = "")
        {
            await RefreshGrantApplicationsAsync(meetingId);
            if (currentReviewId == "")
            {
                currentReviewId = GrantApplications.FirstOrDefault()?.ReviewId ?? "";
            }
            CurrentReviewId = currentReviewId;
            ActiveSession.Id = meetingId;
        }

        private async Task RefreshGrantApplicationsAsync(string Id)
        {
            // get all applications for this committeeReviewId (Id)
            //e.g. string url = "https://myAzureWebsiteAzureFunctions.azurewebsites.net/api/GetAllCRMApplications";
            string url = "https://grantsentitiesexample.azurewebsites.net/api/GetAllCRMApplications?code=wXQPG39aJZlZEQkxBN4C46NJQiefbdx7O1oiVQ39Cn9Tm0jmR4QTvA==";
#if DEBUG
            url = "http://localhost:7071/api/GetAllCRMApplications";
#endif
            var request = new GetDocketRequest { Id = Id };
            HttpResponseMessage? response = await httpClient.PostAsJsonAsync(url, request);
            var listOfApplications = new List<DocketResponse>();
            if (response != null && response.Content.Headers.ContentLength != 0)
            {
                listOfApplications = await response.Content.ReadFromJsonAsync<List<DocketResponse>>() ?? new List<DocketResponse>();
            }
            GrantApplications.AddRange(listOfApplications);
        }

        public async Task<IActionResult> OnPostAsync(string approveOrReject, string reviewId, string sessionId)
        {
            if (ModelState.IsValid)
            {
                var request = new OverallApplicationVoteRequest
                {
                    Approve = approveOrReject == "approve" ? true : false,
                    ReviewId = reviewId
                };

                //e.g. string url = "https://myAzureWebsiteAzureFunctions.azurewebsites.net/api/VoteOverallApplication";
                string url = "https://grantsentitiesexample.azurewebsites.net/api/VoteOverallApplication?code=wXQPG39aJZlZEQkxBN4C46NJQiefbdx7O1oiVQ39Cn9Tm0jmR4QTvA==";
#if DEBUG
                url = "http://localhost:7071/api/VoteOverallApplication";
#endif
                HttpResponseMessage? response = await httpClient.PostAsJsonAsync(url, request);
                response.EnsureSuccessStatusCode();
                Saved = true;
                
                await RefreshGrantApplicationsAsync(sessionId);
                ActiveSession.Id = sessionId;
                CurrentReviewId = reviewId;
            }
            return Page();
        }
    }

    public class Session
    {
        public string Id { get; set; }
    }
}
