using System;

namespace GymMasterAppDemo.Models
{
    public class CustomerListModel
    {
        public long Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string OccupationDescription { get; set; }
        public long? HealthId { get; set; }
        public string HealthRecordInfo => HealthId.HasValue ? "Ναι" : "Όχι";
        public string Address { get; set; }
        public string City { get; set; }
        public string Mobile { get; set; }
        public string Home { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public string Comments { get; set; }
    }
}