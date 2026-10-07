using System;

namespace GymMasterAppDemo.Models
{
    /// <summary>
    /// Βοηθητική κλάση με υπολογισμούς που αφορούν τη συνδρομή.
    /// Την κρατάμε ξεχωριστά ώστε να μη φορτώνουμε τις φόρμες
    /// με business logic.
    /// </summary>
    public static class MembershipCalculationHelper
    {
        /// <summary>
        /// Επιστρέφει τη διάρκεια της συνδρομής σε ημέρες
        /// βάσει του MembershipType.
        /// </summary>
        public static int GetDurationDays(string membershipType)
        {
            switch (membershipType)
            {
                case "HM01":
                    return 1;   // Ημερήσια
                case "MH01":
                    return 30;  // Μηνιαία
                case "MH03":
                    return 90;  // Τρίμηνη
                case "MH12":
                    return 365; // Ετήσια
                case "ΜΗ01":
                    return 30;
                case "ΜΗ03":
                    return 90;
                case "ΜΗ12":
                    return 365;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Υπολογίζει την ημερομηνία λήξης βάσει ημερομηνίας έναρξης
        /// και τύπου συνδρομής.
        /// </summary>
        public static DateTime CalculateEndDate(DateTime startDate, string membershipType)
        {
            int duration = GetDurationDays(membershipType);

            if (duration <= 0)
                return startDate;

            return startDate.AddDays(duration);
        }

        /// <summary>
        /// Υπολογίζει την ημερομηνία λήξης βάσει διάρκειας.
        /// </summary>
        public static DateTime CalculateEndDate(DateTime startDate, int durationDays)
        {
            if (durationDays <= 0)
                return startDate;

            return startDate.AddDays(durationDays);
        }

        /// <summary>
        /// Υπολογίζει το συνολικό υπόλοιπο της συνδρομής.
        /// Δεν επιστρέφει αρνητική τιμή.
        /// </summary>
        public static decimal CalculateBalance(decimal price, decimal totalPaid)
        {
            decimal balance = price - totalPaid;
            return balance < 0 ? 0 : balance;
        }

        /// <summary>
        /// Επιστρέφει το status που πρέπει να πάρει μια ΝΕΑ συνδρομή,
        /// βάσει πρώτης πληρωμής και συνολικού κόστους.
        /// </summary>
        public static string GetStatusForNewMembership(decimal price, decimal firstPaymentAmount)
        {
            if (firstPaymentAmount >= price)
                return "1"; // Εξοφλημένη

            return "0";     // Ανεξόφλητη
        }

        /// <summary>
        /// Επιστρέφει το status μετά από νέα πληρωμή στη φόρμα διαχείρισης.
        /// </summary>
        public static string GetStatusAfterPayment(decimal price, decimal totalPaid)
        {
            decimal balance = CalculateBalance(price, totalPaid);

            if (balance <= 0)
                return "1"; // Εξοφλημένη

            return "0";     // Ανεξόφλητη
        }

        /// <summary>
        /// Ελέγχει αν δύο χρονικά διαστήματα συνδρομών επικαλύπτονται.
        /// </summary>
        public static bool HasDateOverlap(
            DateTime startDate1,
            DateTime endDate1,
            DateTime startDate2,
            DateTime endDate2)
        {
            return startDate1 < endDate2 && startDate2 < endDate1;
        }
    }
}