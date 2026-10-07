using System.Collections.Generic;
using GymMasterAppDemo.Models;

namespace GymMasterAppDemo.Data
{
    public interface ICustomerRepository
    {
        List<CustomerListModel> GetActiveCustomers();
        List<CustomerListModel> SearchActiveCustomers(string searchValue);
        CustomerModel GetById(long id);
        bool CustomerIdExists(long id);
        void Add(CustomerModel customer);
        void Edit(CustomerModel customer);
        void UpdateHealthId(long customerId, long healthId);
    }
}