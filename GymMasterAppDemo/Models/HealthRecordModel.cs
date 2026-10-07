namespace GymMasterAppDemo.Models
{
    public class HealthRecordModel
    {
        public long Id { get; set; }

        public bool? HasBodyPain { get; set; }
        public string BodyPainDesc { get; set; }

        public bool? IsObest { get; set; }
        public bool? IsSmoker { get; set; }
        public bool? FamilyHeartHistory { get; set; }
        public bool? HasHypertasis { get; set; }
        public bool? HasDiabetes { get; set; }
        public bool? HasHeartIssues { get; set; }
        public bool? HasAsthma { get; set; }
        public bool? HasThyroedes { get; set; }
        public bool? HasArthritis { get; set; }
        public bool? HasOsteoporosis { get; set; }
        public bool? HasAllergies { get; set; }
        public bool? HasMyosceletic { get; set; }
        public string MyoskeleticDesc { get; set; }

        public bool? HasOther { get; set; }
        public string OtherDesc { get; set; }

        public string EmergencyContact { get; set; }
        public string EmergencyPhone { get; set; }
        public string Comment { get; set; }
    }
}