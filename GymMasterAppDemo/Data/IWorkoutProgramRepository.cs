using System.Collections.Generic;
using GymMasterAppDemo.Models;

namespace GymMasterAppDemo.Data
{
    public interface IWorkoutProgramRepository
    {
        List<WorkoutProgramModel> GetByCustomerId(long customerId);

        WorkoutProgramModel GetById(long workoutProgramId);
        List<WorkoutProgramDetailModel> GetDetailsByProgramId(long workoutProgramId);

        long Add(WorkoutProgramModel program, List<WorkoutProgramDetailModel> details);
        void Update(WorkoutProgramModel program, List<WorkoutProgramDetailModel> details);

        void Delete(long workoutProgramId);

        WorkoutProgramPrintHeaderModel GetPrintHeader(long workoutProgramId);
        List<WorkoutProgramPrintDetailModel> GetPrintDetails(long workoutProgramId);
    }
}