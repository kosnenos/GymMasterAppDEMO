using System;
using System.Collections.Generic;
using GymMasterAppDemo.Models;

namespace GymMasterAppDemo.Views
{
    public interface IAnalyticsView
    {
        event EventHandler ViewLoaded;
        event EventHandler RefreshClicked;
        event EventHandler ExportExcelClicked;
        event EventHandler CloseClicked;

        int SelectedTabIndex { get; }

        void BindRecentCustomers(List<RecentCustomerReportItem> items);
        void BindExpiringToday(List<ExpiringTodayReportItem> items);
        void BindUnpaidMemberships(List<UnpaidMembershipReportItem> items);

        void SetRecentCustomersCount(int count);
        void SetExpiringTodayCount(int count);
        void SetUnpaidCount(int count);

        void ExportCurrentTabToExcel();

        void ShowInfoMessage(string message);
        void ShowErrorMessage(string message);

        void CloseView();
    }
}