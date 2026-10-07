using System.Collections.Generic;
using GymMasterAppDemo.Models;

namespace GymMasterAppDemo.Data
{
    public interface ILookupRepository
    {
        List<ServiceOptionModel> GetServices();
        List<MembershipTypeOptionModel> GetMembershipTypes();
        List<PaymentMethodOptionModel> GetPaymentMethods();
        List<StatusOptionModel> GetStatuses();

        List<WorkoutGoalOptionModel> GetWorkoutGoals();
        List<string> GetMuscleGroups();
        List<ExerciseOptionModel> GetExercisesByMuscleGroup(string muscleGroup);
    }
}