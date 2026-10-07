using System;

namespace GymMasterAppDemo.Models
{
    public class WorkoutProgramModel
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }

        public string GoalCode { get; set; }
        public string GoalDescription { get; set; }

        public int Duration { get; set; }
        public int Frequency { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Comments { get; set; }
    }
}