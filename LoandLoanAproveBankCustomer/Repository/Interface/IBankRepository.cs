using LoandLoanAproveBankCustomer.Model;

namespace LoandLoanAproveBankCustomer.Repository.Interface
{
    public interface IBankRepository
    {
        Task<int> AddNewBank(Bank bank);
        Task<List<Bank>> GetAllBanksWithCustomers();
        Task<List<Bankloans>> GetAllBanksWithLoans();
        Task<List<CombineModel>> GetAllBanksWithLoansWithCustomers();       
        Task<List<AproveLoans>> GetAllAproveLoansWithCustomers();   
        Task<List<CombineModel>> GetAllAproveLoansByBankId(int? bankId);
        Task<List<combine>> GetAllAproveLoansByCustmerId(int id);
       // public  Task<int> AddNewCustomerr(List<Customer> custlist, int result, Customer customerr);


    }
}
