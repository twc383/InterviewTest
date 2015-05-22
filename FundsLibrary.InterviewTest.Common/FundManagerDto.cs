using System;

namespace FundsLibrary.InterviewTest.Common
{
    public class FundManagerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime ManagedSince { get; set; }
        public string Biography { get; set; }
        public Location Location { get; set; }
    }
}
