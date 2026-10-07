using System.Collections.Generic;
using GymMasterAppDemo.Models;

namespace GymMasterAppDemo.Data
{
    public interface IOccupationRepository
    {
        List<OccupationItemModel> GetAll();
    }
}