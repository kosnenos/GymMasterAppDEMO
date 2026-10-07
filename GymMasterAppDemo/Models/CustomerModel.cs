using System;

namespace GymMasterAppDemo.Models
{
    public class CustomerModel
    {
        public long Id { get; set; }                  // Δίνεται από τον χρήστη
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Gender { get; set; }            // "Α" ή "Γ"
        public string OccupationId { get; set; }      
        public long? HealthId { get; set; }
        public string Address { get; set; }
        public string City { get; set; } = "Θεσσαλονίκη";
        public string Mobile { get; set; }
        public string Home { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; } = true;      // 1 = ενεργός
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string Comments { get; set; }
    }
}