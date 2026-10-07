namespace GymMasterAppDemo.Models
{
    public class MembershipTypeOptionModel
    {
        public string MembershipType { get; set; }
        public string Description { get; set; }
        public int DurationDays { get; set; }

        public override string ToString()
        {
            return Description;
        }
    }
}