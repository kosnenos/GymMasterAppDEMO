using System;

namespace GymMasterAppDemo.Models
{
    public class CustomerMembershipPrintRowModel
    {
        public long MembershipId { get; set; }
        public string MembershipTypeDescription { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ServiceDescription { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string StatusDescriptionText { get; set; } = string.Empty;
    }
}