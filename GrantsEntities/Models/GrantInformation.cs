using System;

namespace GrantsEntities.Models
{
    public class GrantInformation
    {
        public string Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Url { get; set; } = "";
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}
