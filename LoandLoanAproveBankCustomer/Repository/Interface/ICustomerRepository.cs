using LoandLoanAproveBankCustomer.Model;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace LoandLoanAproveBankCustomer.Repository.Interface
{
    public interface ICustomerRepository
    {
        Task<int> AddNewCustomer(Customer customer);
        Task<int> Trnsaction(int cId,double ammount,string transactionType);
        
        
    }
}
