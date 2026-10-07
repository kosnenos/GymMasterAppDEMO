namespace GymMasterAppDemo.Models
{
    public class WorkoutProgramDetailModel
    {
        public long Id { get; set; }
        public long ProgramId { get; set; }

        public string DayOfWeek { get; set; }

        public string MuscleGroup { get; set; }

        public string ExerciseCode { get; set; }
        public string ExerciseName { get; set; }

        public int Sets { get; set; }
        public int Reps { get; set; }

        // Στο spec δίνεται ως textbox με default τιμή 1.
        // Το κρατάμε string ώστε να έχουμε ελευθερία ("1", "60 sec", κτλ.)
        public string RestTime { get; set; }
    }
}