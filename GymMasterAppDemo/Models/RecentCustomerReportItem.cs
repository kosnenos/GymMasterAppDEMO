using System;

namespace GymMasterAppDemo.Models
{
    public class RecentCustomerReportItem
    {
        public long CustomerId { get; set; }
        public string FullName { get; set; }
        public string MobilePhone { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}