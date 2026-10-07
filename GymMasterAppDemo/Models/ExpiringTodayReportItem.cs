using System;

namespace GymMasterAppDemo.Models
{
    public class ExpiringTodayReportItem
    {
        public long MembershipId { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string MembershipType { get; set; }
        public DateTime StartDate { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }
}