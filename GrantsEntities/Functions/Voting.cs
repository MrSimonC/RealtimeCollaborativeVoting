using GrantsCollaboration.Models;
using GrantsEntities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace GrantsEntities
{
    public class Voting
    {
        private readonly HttpClient httpClient;

        public Voting(IHttpClientFactory httpClientFactory) => httpClient = httpClientFactory.CreateClient();


        /// <summary>
        /// Approve/Reject whole application in CRM
        /// </summary>
        /// <param name="req">Not used</param>
        /// <returns>List of strings: all existing entity Ids</returns>
        [FunctionName("VoteOverallApplication")]
        public async Task<IActionResult> RunVoteOverallApplication(
           [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
           ILogger log,
           [DurableClient] IDurableEntityClient client)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            OverallApplicationVote? vote = JsonConvert.DeserializeObject<OverallApplicationVote>(requestBody);

            if (vote == null)
            {
                return new BadRequestErrorMessageResult("Can't determine vote");
            }

            // get reviewId
            int crmApproveOrReject;
            if (vote.Approve)
            {
                crmApproveOrReject = 247960000; // 247960000 - Can Be Awarded
            }
            else
            {
                crmApproveOrReject = 247960001; // 247960001 - Do Not Award
            }

            // tell CRM the outcome
            string url = $"http://myOtherAzureWebsiteForCRM.azurewebsites.net/api/Review/UpdateRecommendation?reviewId={vote.ReviewId}&recommendation={crmApproveOrReject}";
            log.LogInformation($"VoteOverallApplication: Url is: {url}");
            HttpResponseMessage? crmResponse = await httpClient.PutAsync(url, null);
            if (crmResponse != null && crmResponse.IsSuccessStatusCode)
            {
                return new OkObjectResult(HttpStatusCode.Accepted);
            }
            return new BadRequestErrorMessageResult("Couldn't update");
        }

        /// <summary>
        /// Returns a list of strings: all existing entity Ids
        /// </summary>
        /// <param name="req">Not used</param>
        /// <returns>List of strings: all existing entity Ids</returns>
        [FunctionName("GetAllApplications")]
        public async Task<IActionResult> RunGetAllApplications(
           [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
           ILogger log,
           [DurableClient] IDurableEntityClient client)
        {
            using var source = new CancellationTokenSource();
            CancellationToken token = source.Token;

            var listOfEntityIds = new List<string>();

            var query = new EntityQuery
            {
                PageSize = 100, // default
                FetchState = true
            };

            do
            {
                EntityQueryResult? entityList = await client.ListEntitiesAsync(query, token);
                IEnumerable<DurableEntityStatus>? entities = entityList.Entities;
                listOfEntityIds.AddRange(entities.Select(e => e.EntityId.EntityKey));
            }
            while (query.ContinuationToken != null);
            return new OkObjectResult(listOfEntityIds);
        }

        /// <summary>
        /// With a DocketId (from DocketRest) Return name, description and Id of applications
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("GetAllCRMApplications")]
        public async Task<IActionResult> RunGetAllCRMApplications(
           [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
           ILogger log)
        {
            // get docket.id
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            DocketRequest? docket = JsonConvert.DeserializeObject<DocketRequest>(requestBody);

            // get list of applications
            string url = $"http://myOtherAzureWebsiteForCRM.azurewebsites.net/api/Review/GetReviewsByDocketCode?docketCode={docket.Id}";
            List<DocketCRMResponse>? crmResponse = await httpClient.GetFromJsonAsync<List<DocketCRMResponse>>(url);
            var applicationList = new List<DocketResponse>();
            if (crmResponse?.Any() ?? false)
            {
                applicationList.AddRange(crmResponse.Select(d
                    => new DocketResponse
                    {
                        Name = d.msnfp_Name,
                        ReviewId = d.msnfp_ReviewId,
                        RequestId = d.msnfp_RequestId.id
                    }).ToList());
            }

            // fill in the detail
            foreach (DocketResponse? application in applicationList)
            {
                string detailUrl = $"http://myOtherAzureWebsiteForCRM.azurewebsites.net/api/Request/GetById?requestId={application.RequestId}";

                //var response = await httpClient.GetAsync(url);
                //var json = await response.Content.ReadAsStringAsync();

                Models.DocketCRMRequestResponse.DocketCRMRequestResponse? crmReqResponse = await httpClient.GetFromJsonAsync<Models.DocketCRMRequestResponse.DocketCRMRequestResponse>(detailUrl);
                if (crmReqResponse != null)
                {
                    application.DeliveryFramework = crmReqResponse.fsdyn_DeliveryFramework.name;
                    application.Recipient = crmReqResponse.msnfp_RecipientId.name;
                    application.SubmittedBy = crmReqResponse.msnfp_SubmittedById.name;
                    application.SubmittedDate = crmReqResponse.msnfp_SubmittedDate;
                    application.Purpose = crmReqResponse.msnfp_Purpose;
                    application.Amount = crmReqResponse.msnfp_AmountRequested.value;
                    application.Duration = crmReqResponse.msnfp_RequestedDuration;
                    application.StartDate = crmReqResponse.msnfp_RequestedStartDate;
                    application.StrengtheningCommunities = crmReqResponse.fsdyn_StrengthingCommunitiesApp.id;
                    application.UrlSharepoint = $"https://myCompanySharepointURL.sharepoint.com/sites/Demo-CRM-Integrations/fsdyn_strengtheningcommunitiesapp/{crmReqResponse.msnfp_name}_{crmReqResponse.fsdyn_StrengthingCommunitiesApp.id.Replace("-", "")}";
                    application.UrlCRM = $"https://myCompany.dynamics.com/main.aspx?appid=myAppId&pagetype=entityrecord&etn=msnfp_request&id={application.RequestId}";

                }
            }

            return new OkObjectResult(applicationList);
        }



        [FunctionName("GetVotes")]
        public async Task<IActionResult> RunGetVotes(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log,
            [DurableClient] IDurableEntityClient client)
        {
            log.LogInformation("GetVotes triggered.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            NewVote vote = JsonConvert.DeserializeObject<NewVote>(requestBody);

            var entityId = new EntityId(nameof(Grant), vote.GrantId.ToString());
            EntityStateResponse<Grant> stateResponse = await client.ReadEntityStateAsync<Grant>(entityId);
            return new OkObjectResult(stateResponse.EntityState);
        }

        [FunctionName("VoteApprove")]
        public async Task<IActionResult> RunVoteApprove(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log,
            [DurableClient] IDurableEntityClient client)
        {
            log.LogInformation("VoteApprove triggered");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            NewVote vote = JsonConvert.DeserializeObject<NewVote>(requestBody);

            var entityId = new EntityId(nameof(Grant), vote.GrantId.ToString());
            await client.SignalEntityAsync<IGrant>(entityId, proxy => proxy.AddApproveVote(vote.Username));
            return new OkObjectResult(HttpStatusCode.Accepted);
        }

        [FunctionName("VoteReject")]
        public async Task<IActionResult> RunVoteReject(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log,
            [DurableClient] IDurableEntityClient client)
        {
            log.LogInformation("VoteReject triggered.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            NewVote vote = JsonConvert.DeserializeObject<NewVote>(requestBody);

            var entityId = new EntityId(nameof(Grant), vote.GrantId.ToString());
            await client.SignalEntityAsync<IGrant>(entityId, proxy => proxy.AddRejectVote(vote.Username));
            return new OkObjectResult(HttpStatusCode.Accepted);
        }

        [FunctionName("SetHasStarted")]
        public async Task<IActionResult> RunSetHasStarted(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log,
            [DurableClient] IDurableEntityClient client)
        {
            log.LogInformation("SetInformation triggered.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            GrantInformation? grantInformation = JsonConvert.DeserializeObject<GrantInformation>(requestBody);

            var entityId = new EntityId(nameof(Grant), grantInformation.Id);
            await client.SignalEntityAsync<IGrant>(entityId, proxy => proxy.SetHasStarted());
            return new OkObjectResult(HttpStatusCode.Accepted);
        }

        [FunctionName("SetInformation")]
        public async Task<IActionResult> RunSetInformation(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log,
            [DurableClient] IDurableEntityClient client)
        {
            log.LogInformation("SetInformation triggered.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            GrantInformation? grantInformation = JsonConvert.DeserializeObject<GrantInformation>(requestBody);

            var entityId = new EntityId(nameof(Grant), grantInformation.Id);
            await client.SignalEntityAsync<IGrant>(entityId, proxy => proxy.SetName(grantInformation.Name));
            await client.SignalEntityAsync<IGrant>(entityId, proxy => proxy.SetDescription(grantInformation.Description));
            await client.SignalEntityAsync<IGrant>(entityId, proxy => proxy.SetUrl(grantInformation.Url));
            await client.SignalEntityAsync<IGrant>(entityId, proxy => proxy.SetDate(grantInformation.Date));
            await client.SignalEntityAsync<IGrant>(entityId, proxy => proxy.SetAmount(grantInformation.Amount));
            return new OkObjectResult(HttpStatusCode.Accepted);
        }
    }
}
