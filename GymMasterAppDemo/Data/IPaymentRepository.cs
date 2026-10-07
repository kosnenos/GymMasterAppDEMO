using System.Collections.Generic;
using GymMasterAppDemo.Models;

namespace GymMasterAppDemo.Data
{
    public interface IPaymentRepository
    {
        List<PaymentModel> GetByMembershipId(long membershipId);
        PaymentModel GetById(long paymentId);
        long Insert(PaymentModel payment);
        decimal GetTotalPaidByMembershipId(long membershipId);
        void Update(PaymentModel payment);
        void Delete(long paymentId);
        void DeleteByMembershipId(long membershipId);
    }
}