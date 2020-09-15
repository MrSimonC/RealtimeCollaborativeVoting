using System;
using System.Collections.Generic;
using System.Text;

namespace GrantsEntities.Models
{
    public class DocketResponse
    {
        public string Name { get; set; } = "";
        public string ReviewId { get; set; } = "";
        public string RequestId { get; set; } = "";
        public string DeliveryFramework { get; set; } = "";
        public string Recipient { get; set; } = "";
        public string SubmittedBy { get; set; } = "";
        public DateTime SubmittedDate { get; set; }
        public string? Purpose { get; set; }
        public float? Amount { get; set; }
        public int? Duration { get; set; }
        public DateTime? StartDate { get; set; }
        public string StrengtheningCommunities { get; set; } = "";// id for sharepoint
        public string UrlSharepoint { get; set; } = "";
        public string UrlCRM { get; set; } = "";
    }
}
