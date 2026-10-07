using GymMasterAppDemo.Models;

namespace GymMasterAppDemo.Data
{
    public interface IHealthRecordRepository
    {
        long Add(HealthRecordModel model);
        void Edit(HealthRecordModel model);
        HealthRecordModel GetById(long id);
    }
}