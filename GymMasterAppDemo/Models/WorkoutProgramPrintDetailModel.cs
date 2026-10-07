namespace GymMasterAppDemo.Models
{
    public class WorkoutProgramPrintDetailModel
    {
        public string DayOfWeek { get; set; }
        public string MuscleGroup { get; set; }
        public string ExerciseName { get; set; }

        public int Sets { get; set; }
        public int Reps { get; set; }
        public string RestTime { get; set; }
    }
}