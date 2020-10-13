using GrantsCollaboration.Models;
using GrantsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GrantsCollaboration.Helpers
{
    public class EntityHelper
    {
        public async Task<Grant> GetCurrentGrantStatus(string grantId, HttpClient httpClient)
        {
            //e.g. string url = "https://myAzureWebsiteAzureFunctions.azurewebsites.net/api/GetVotes";
            string url = "https://grantsentitiesexample.azurewebsites.net/api/GetVotes?code=wXQPG39aJZlZEQkxBN4C46NJQiefbdx7O1oiVQ39Cn9Tm0jmR4QTvA==";
#if DEBUG
            url = "http://localhost:7071/api/GetVotes";
#endif
            HttpResponseMessage? response = await httpClient.PostAsJsonAsync(url, new NewVote { GrantId = grantId });

            var result = new Grant();
            if (response != null && response.Content.Headers.ContentLength != 0)
            {
                result = await response.Content.ReadFromJsonAsync<Grant>() ?? new Grant();
            }
            return result;
        }
    }
}
