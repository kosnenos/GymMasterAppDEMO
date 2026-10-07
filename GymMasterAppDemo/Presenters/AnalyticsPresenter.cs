using System;
using GymMasterAppDemo.Data;
using GymMasterAppDemo.Views;

namespace GymMasterAppDemo.Presenters
{
    public class AnalyticsPresenter
    {
        private readonly IAnalyticsView _view;
        private readonly IAnalyticsRepository _repository;

        public AnalyticsPresenter(IAnalyticsView view, IAnalyticsRepository repository)
        {
            _view = view;
            _repository = repository;

            _view.ViewLoaded += View_ViewLoaded;
            _view.RefreshClicked += View_RefreshClicked;
            _view.ExportExcelClicked += View_ExportExcelClicked;
            _view.CloseClicked += View_CloseClicked;
        }

        private void View_ViewLoaded(object sender, EventArgs e)
        {
            try
            {
                LoadAllReports();
            }
            catch (Exception ex)
            {
                _view.ShowErrorMessage("Αποτυχία φόρτωσης αναφορών.\n\n" + ex.Message);
            }
        }

        private void View_RefreshClicked(object sender, EventArgs e)
        {
            try
            {
                RefreshCurrentTab();
            }
            catch (Exception ex)
            {
                _view.ShowErrorMessage("Αποτυχία ανανέωσης αναφοράς.\n\n" + ex.Message);
            }
        }

        private void View_ExportExcelClicked(object sender, EventArgs e)
        {
            try
            {
                _view.ExportCurrentTabToExcel();
            }
            catch (Exception ex)
            {
                _view.ShowErrorMessage("Αποτυχία εξαγωγής σε Excel.\n\n" + ex.Message);
            }
        }

        private void View_CloseClicked(object sender, EventArgs e)
        {
            _view.CloseView();
        }

        private void LoadAllReports()
        {
            LoadRecentCustomers();
            LoadExpiringToday();
            LoadUnpaidMemberships();
        }

        private void RefreshCurrentTab()
        {
            switch (_view.SelectedTabIndex)
            {
                case 0:
                    LoadRecentCustomers();
                    break;
                case 1:
                    LoadExpiringToday();
                    break;
                case 2:
                    LoadUnpaidMemberships();
                    break;
                default:
                    LoadRecentCustomers();
                    break;
            }
        }

        private void LoadRecentCustomers()
        {
            var items = _repository.GetRecentCustomers();
            _view.BindRecentCustomers(items);
            _view.SetRecentCustomersCount(items.Count);
        }

        private void LoadExpiringToday()
        {
            var items = _repository.GetExpiringTodayMemberships();
            _view.BindExpiringToday(items);
            _view.SetExpiringTodayCount(items.Count);
        }

        private void LoadUnpaidMemberships()
        {
            var items = _repository.GetUnpaidMemberships();
            _view.BindUnpaidMemberships(items);
            _view.SetUnpaidCount(items.Count);
        }
    }
}