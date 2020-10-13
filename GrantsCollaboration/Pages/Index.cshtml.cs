using GrantsCollaboration.Helpers;
using GrantsCollaboration.Models;
using GrantsCollaboration.MSGraph;
using GrantsEntities;
using GrantsEntities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GrantsCollaboration.Pages
{
    [AuthorizeForScopes(Scopes = new[] { "user.read", "calendars.read" })]
    public class IndexModel : PageModel
    {
        HttpClient httpClient;
        EntityHelper eh = new EntityHelper();
        public List<CommitteeReviewEvent> committeeReviewEvents { get; set; } = new List<CommitteeReviewEvent>();
        [BindProperty]
        public string CommitteeReviewId { get; set; } = "";
        private readonly ITokenAcquisition tokenAcquisition;
        private readonly GraphSettings graphSettings;
        
        public IndexModel(ITokenAcquisition tokenAcquisition, IOptions<GraphSettings> graphSettingsValue, IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient();
            this.tokenAcquisition = tokenAcquisition;
            graphSettings = graphSettingsValue.Value;
        }
        public async Task OnGetAsync()
        {
            GraphServiceClient graphClient = GetGraphServiceClient(new[] { "user.read", "calendars.read" });

            // get committee review events from user's calenear
            ICalendarEventsCollectionPage? cal = null;
            try
            {
                cal = await graphClient.Me.Calendar.Events.Request().GetAsync();
            }
            catch (Exception)
            {
                Console.WriteLine("Couldn't read events");
            }

            // filter out what we need
            if (cal != null)
            {
                foreach (Event? evnt in cal)
                {
                    if (evnt.Subject.Contains("committee review", StringComparison.CurrentCultureIgnoreCase))
                    {
                        string digitsPattern = @"CR-\d+";
                        var reg = new Regex(digitsPattern);

                        if (evnt.IsCancelled ?? false)
                        {
                            continue;
                        }

                        string id = reg.Match(evnt.Subject).Value;
                        committeeReviewEvents.Add(new CommitteeReviewEvent
                        {
                            Description = evnt.Subject,
                            CommitteeReviewId = id,
                            HasStarted = await GetHasStartedAsync(id)
                        });
                        //committeeReviewEvents = committeeReviewEvents.OrderBy(e => e.CommitteeReviewId).ToList();
                    }
                    Console.WriteLine(evnt.Subject);
                }
            }
        }

        private async Task<bool> GetHasStartedAsync(string meetingId)
        {
            Grant? state = await eh.GetCurrentGrantStatus(meetingId, httpClient);
            if (state?.HasStarted ?? false)
            {
                return true;
            }
            return false;
        }

        public async Task<IActionResult> OnPostAsync(string committeeReviewId)
        {
            if (ModelState.IsValid)
            {
                // set has started
                //e.g. string url = "https://myAzureWebsiteAzureFunctions.azurewebsites.net/api/SetHasStarted";
                string url = "https://grantsentitiesexample.azurewebsites.net/api/SetHasStarted?code=wXQPG39aJZlZEQkxBN4C46NJQiefbdx7O1oiVQ39Cn9Tm0jmR4QTvA==";
#if DEBUG
                url = "http://localhost:7071/api/SetHasStarted";
#endif
                await httpClient.PostAsJsonAsync(url, new GrantInformation { Id = committeeReviewId });

                return RedirectToPage($"Voting", new { MeetingId = committeeReviewId });
            }
            return Page();
        }

        private GraphServiceClient GetGraphServiceClient(string[] scopes)
        {
            return GraphServiceClientFactory.GetAuthenticatedGraphClient(async () =>
            {
                string result = await tokenAcquisition.GetAccessTokenForUserAsync(scopes);
                return result;
            }, graphSettings.GraphApiUrl);
        }
    }
    public class CommitteeReviewEvent
    {
        public string Description { get; set; } = "";
        public string CommitteeReviewId { get; set; } = "";
        public bool HasStarted { get; set; }
    }

}