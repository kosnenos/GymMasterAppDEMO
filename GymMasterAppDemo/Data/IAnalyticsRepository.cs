using System.Collections.Generic;
using GymMasterAppDemo.Models;

namespace GymMasterAppDemo.Data
{
    public interface IAnalyticsRepository
    {
        List<RecentCustomerReportItem> GetRecentCustomers();
        List<ExpiringTodayReportItem> GetExpiringTodayMemberships();
        List<UnpaidMembershipReportItem> GetUnpaidMemberships();
    }
}