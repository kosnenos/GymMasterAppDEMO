using System;
using System.Collections.Generic;
using GymMasterAppDemo.Models;

namespace GymMasterAppDemo.Data
{
    public interface IMembershipRepository
    {
        List<MembershipModel> GetByCustomerId(long customerId);
        List<MembershipModel> SearchByCustomer(string searchValue);
        MembershipModel GetById(long membershipId);

        bool HasOverlappingMembership(long customerId, DateTime startDate, DateTime endDate);
        bool HasOverlappingMembership(long customerId, DateTime startDate, DateTime endDate, long excludeMembershipId);

        long Insert(MembershipModel membership);
        void Update(MembershipModel membership);
        void UpdateStatus(long membershipId, string statusCode);
        void Delete(long membershipId);
    }
}