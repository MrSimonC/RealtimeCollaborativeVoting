using GrantsEntities.Models;
using GrantsEntities.Models.DocketCRMRequestResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace GrantsEntities.Functions_MockData
{
    public static class MockData
    {
        [FunctionName("GetById")]
        public static IActionResult GetByIdRun(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            DocketCRMRequestResponse? result = new DocketCRMRequestResponse
            {
                fsdyn_DeliveryFramework = new Fsdyn_Deliveryframework { name = "My Framework Name" },
                msnfp_RecipientId = new Msnfp_Recipientid { name = "Recipient Id" },
                msnfp_SubmittedById = new Msnfp_Submittedbyid { name = "Simon" },
                msnfp_SubmittedDate = new DateTime(2020, 10, 9),
                msnfp_Purpose = "To repair the school roof",
                msnfp_AmountRequested = new Msnfp_Amountrequested { value = 100.00f },
                msnfp_RequestedDuration = 3,
                msnfp_RequestedStartDate = new DateTime(2020, 10, 20),
                fsdyn_StrengthingCommunitiesApp = new Fsdyn_Strengthingcommunitiesapp { id = "12345" }
            };

            return new OkObjectResult(result);
        }



        [FunctionName("GetReviewsByDocketCode")]
        public static IActionResult GetReviewsByDocketCodeRun(
                [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
                ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            List<DocketCRMResponse>? result = new List<DocketCRMResponse> {
                new DocketCRMResponse()
                {
                    msnfp_Name = "My lovely review",
                    msnfp_ReviewId = "CR1000",
                    id = "CR1000"
                },
                new DocketCRMResponse()
                {
                    msnfp_Name = "My lovely review 2",
                    msnfp_ReviewId = "CR2000",
                    id = "CR2000"
                }
            };

            return new OkObjectResult(result);
        }

        [FunctionName("UpdateRecommendation")]
        public static IActionResult UpdateRecommendationRun(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            return new OkObjectResult("OK");
        }

    }
}
