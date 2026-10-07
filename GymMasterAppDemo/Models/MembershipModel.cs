using System;

namespace GymMasterAppDemo.Models
{
    /// <summary>
    /// Αναπαριστά μία συνδρομή πελάτη.
    /// Αντιστοιχεί στον πίνακα Membership.
    /// </summary>
    public class MembershipModel
    {
        /// <summary>
        /// Πρωτεύον κλειδί της συνδρομής.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Ξένο κλειδί προς τον πελάτη.
        /// </summary>
        public long CustomerId { get; set; }

        /// <summary>
        /// Κωδικός υπηρεσίας (π.χ. 100 = Γενική Χρήση).
        /// </summary>
        public string ServiceCode { get; set; } = string.Empty;

        /// <summary>
        /// Κωδικός τύπου συνδρομής (π.χ. HM01, MH01, MH03, MH12).
        /// </summary>
        public string MembershipType { get; set; } = string.Empty;

        /// <summary>
        /// Διάρκεια συνδρομής σε ημέρες.
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Ημερομηνία έναρξης συνδρομής.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Ημερομηνία λήξης συνδρομής.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Κόστος συνδρομής.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Κατάσταση συνδρομής:
        /// 0 = Ανεξόφλητη
        /// 1 = Εξοφλημένη
        /// 2 = Σε εκκρεμότητα
        /// </summary>
        public string StatusCode { get; set; } = string.Empty;

        /// <summary>
        /// Ημερομηνία και ώρα δημιουργίας της εγγραφής.
        /// Συμπληρώνεται αυτόματα από τη ΒΔ.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Σχόλια για τη συνδρομή.
        /// </summary>
        public string Comment { get; set; } = string.Empty;

        /// <summary>
        /// Περιγραφή τύπου συνδρομής από τον βοηθητικό πίνακα MembershipTypeList.
        /// Χρησιμοποιείται για εμφάνιση στο grid.
        /// </summary>
        public string MembershipTypeDescription { get; set; }

        /// <summary>
        /// Περιγραφή υπηρεσίας/προγράμματος από τον βοηθητικό πίνακα ServiceList.
        /// Χρησιμοποιείται για εμφάνιση στο grid.
        /// </summary>
        public string ServiceDescription { get; set; }

        /// <summary>
        /// Περιγραφή κατάστασης από τον βοηθητικό πίνακα StatusList.
        /// Χρησιμοποιείται για εμφάνιση στο grid.
        /// </summary>
        public string StatusDescriptionText { get; set; }


        // =========================
        // Βοηθητικές/Υπολογιζόμενες ιδιότητες
        // Δεν είναι απαραίτητο να αποθηκεύονται στη ΒΔ.
        // =========================

        /// <summary>
        /// Συνολικό ποσό που έχει πληρωθεί για τη συνδρομή.
        /// Θα γεμίζει συνήθως από query ή από business logic.
        /// </summary>
        public decimal TotalPaid { get; set; }

        /// <summary>
        /// Υπόλοιπο συνδρομής.
        /// Δεν αποθηκεύεται στον πίνακα. Υπολογίζεται δυναμικά.
        /// </summary>
        public decimal Balance => Price - TotalPaid;

        /// <summary>
        /// Φιλική εμφάνιση της κατάστασης για χρήση σε φόρμες/grid.
        /// </summary>
        public string StatusDescription
        {
            get
            {
                switch (StatusCode)
                {
                    case "0":
                        return "Ανεξόφλητη";
                    case "1":
                        return "Εξοφλημένη";
                    case "2":
                        return "Σε εκκρεμότητα";
                    default:
                        return string.Empty;
                }
            }
        }

        /// <summary>
        /// Επιστρέφει true αν η συνδρομή θεωρείται εξοφλημένη.
        /// </summary>
        public bool IsFullyPaid => StatusCode == "1";

        /// <summary>
        /// Επιστρέφει true αν επιτρέπεται νέα πληρωμή.
        /// </summary>
        public bool CanAcceptPayment => StatusCode == "0" || StatusCode == "2";
    }
}