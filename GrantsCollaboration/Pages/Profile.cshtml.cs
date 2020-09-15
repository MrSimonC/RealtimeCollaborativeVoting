using GrantsCollaboration.MSGraph;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GrantsCollaboration.Pages
{
    [AuthorizeForScopes(Scopes = new[] { "user.read", "calendars.read" })]
    public class ProfileModel : PageModel
    {
        private readonly ITokenAcquisition tokenAcquisition;
        private readonly GraphSettings graphSettings;
        public ProfileModel(ITokenAcquisition tokenAcquisition, IOptions<GraphSettings> graphSettingsValue)
        {
            this.tokenAcquisition = tokenAcquisition;
            graphSettings = graphSettingsValue.Value;
        }
        public async Task OnGetAsync()
        {
            GraphServiceClient graphClient = GetGraphServiceClient(new[] { "user.read", "calendars.read" });

            User? me = await graphClient.Me.Request().GetAsync();
            ViewData["Me"] = me;

            ICalendarEventsCollectionPage? cal = await graphClient.Me.Calendar.Events.Request().GetAsync();
            foreach (Event? evnt in cal)
            {
                Console.WriteLine(evnt.Subject);
            }

            try
            {
                // Get user photo
                using Stream? photoStream = await graphClient.Me.Photo.Content.Request().GetAsync();
                byte[] photoByte = ((MemoryStream)photoStream).ToArray();
                ViewData["Photo"] = Convert.ToBase64String(photoByte);
            }
            catch (Exception)
            {
                ViewData["Photo"] = null;
            }
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
}