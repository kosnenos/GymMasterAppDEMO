namespace GymMasterAppDemo.Models
{
    /// <summary>
    /// Αναπαριστά μία εγγραφή του πίνακα ServiceList.
    /// </summary>
    public class ServiceOptionModel
    {
        public string ServiceCode { get; set; } = string.Empty;
        public string ServiceDescription { get; set; } = string.Empty;

        public override string ToString() => ServiceDescription;
    }
}