using GrantsEntities.Models;
using GrantsEntities.Models.DocketCRMRequestResponse;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrantsEntities.MockData
{
    public static class MockDataHelper
    {
        public static List<DocketCRMResponse> MockDataCRMResponse()
        {
            return new List<DocketCRMResponse> {
                new DocketCRMResponse()
                {
                    msnfp_Name = "My lovely review",
                    msnfp_ReviewId = "CR1000",
                    msnfp_RequestId = new Msnfp_Requestid{ id = "CR1000" }
                },
                new DocketCRMResponse()
                {
                    msnfp_Name = "My lovely review 2",
                    msnfp_ReviewId = "CR2000",
                    msnfp_RequestId = new Msnfp_Requestid{ id = "CR2000" }
                }
            };
        }

        public static DocketCRMRequestResponse GetDocketCRMRequestResponseMockData()
        {
            return new DocketCRMRequestResponse
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
        }
    }
}
