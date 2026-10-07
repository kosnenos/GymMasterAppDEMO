using System;

namespace GymMasterAppDemo.Models
{
    public class UnpaidMembershipReportItem
    {
        public long MembershipId { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string MembershipType { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public decimal Balance { get; set; }
        public string Status { get; set; }
    }
}