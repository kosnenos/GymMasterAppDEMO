using System;

namespace GymMasterAppDemo.Models
{
    /// <summary>
    /// Αναπαριστά μία κίνηση πληρωμής συνδρομής.
    /// Αντιστοιχεί στον πίνακα Payments.
    /// </summary>
    public class PaymentModel
    {
        /// <summary>
        /// Πρωτεύον κλειδί πληρωμής.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Ξένο κλειδί προς τη συνδρομή.
        /// </summary>
        public long MembershipId { get; set; }

        /// <summary>
        /// Ημερομηνία πληρωμής.
        /// </summary>
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// Ώρα πληρωμής.
        /// </summary>
        public TimeSpan PaymentTime { get; set; }

        /// <summary>
        /// Κωδικός τρόπου πληρωμής
        /// (π.χ. 00, 10, 20, 99).
        /// </summary>
        public string MethodType { get; set; } = string.Empty;

        /// <summary>
        /// Ποσό πληρωμής.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Χρόνος δημιουργίας εγγραφής.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Σχόλια για την κίνηση πληρωμής.
        /// </summary>
        public string Comment { get; set; } = string.Empty;

        /// <summary>
        /// Περιγραφή τρόπου πληρωμής από τον πίνακα PayMethodList.
        /// Χρησιμοποιείται για εμφάνιση στο grid.
        /// </summary>
        public string MethodDescriptionText { get; set; } = string.Empty;

        /// <summary>
        /// Επιστρέφει την ώρα πληρωμής σε μορφή HH:mm.
        /// Χρήσιμο για εμφάνιση στο grid.
        /// </summary>
        public string PaymentTimeDisplay
        {
            get
            {
                return PaymentTime.ToString(@"hh\:mm");
            }
        }

        /// <summary>
        /// Φιλική περιγραφή τρόπου πληρωμής.
        /// Μπορεί αργότερα να αντικατασταθεί από lookup από πίνακα λίστας.
        /// </summary>
        public string MethodDescription
        {
            get
            {
                switch (MethodType)
                {
                    case "00":
                        return "Μετρητά";
                    case "10":
                        return "Κάρτα/Prepaid";
                    case "20":
                        return "Κατάθεση WebBank";
                    case "99":
                        return "Άλλος τρόπος";
                    default:
                        return string.Empty;
                }
            }
        }
    }
}