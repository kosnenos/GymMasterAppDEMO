namespace GymMasterAppDemo.Models
{
    /// <summary>
    /// Αναπαριστά μία εγγραφή του πίνακα StatusList.
    /// </summary>
    public class StatusOptionModel
    {
        public string StatusCode { get; set; } = string.Empty;
        public string StatusDesc { get; set; } = string.Empty;

        public override string ToString() => StatusDesc;
    }
}