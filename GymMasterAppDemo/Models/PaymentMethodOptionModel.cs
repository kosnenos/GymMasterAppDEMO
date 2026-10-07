namespace GymMasterAppDemo.Models
{
    /// <summary>
    /// Αναπαριστά μία εγγραφή του πίνακα PayMethodList.
    /// </summary>
    public class PaymentMethodOptionModel
    {
        public string MethodType { get; set; } = string.Empty;
        public string MethodDescription { get; set; } = string.Empty;

        public override string ToString() => MethodDescription;
    }
}