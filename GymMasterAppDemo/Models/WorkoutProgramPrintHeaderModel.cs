using System;

namespace GymMasterAppDemo.Models
{
    public class WorkoutProgramPrintHeaderModel
    {
        public long ProgramId { get; set; }
        public long CustomerId { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }

        public string Goal { get; set; }
        public int Frequency { get; set; }
        public int Duration { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Comments { get; set; }
    }
}