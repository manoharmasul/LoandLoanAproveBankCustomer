using LoandLoanAproveBankCustomer.Model;

namespace LoandLoanAproveBankCustomer.Repository.Interface
{
    public interface IAproveLoansRepository
    {
        Task<int> ApplyForLoan(AproveLoans aproveLoans);    
        Task<int> PayLoans(int lId,int cId);
        
    }
}
