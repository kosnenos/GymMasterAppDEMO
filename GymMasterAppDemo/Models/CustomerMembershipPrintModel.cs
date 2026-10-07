using System;
using System.Collections.Generic;

namespace GymMasterAppDemo.Models
{
    public class CustomerMembershipPrintModel
    {
        public long CustomerId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public string Mobile { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public List<CustomerMembershipPrintRowModel> Memberships { get; set; }
            = new List<CustomerMembershipPrintRowModel>();
    }
}