using Dapper;
using LoandLoanAproveBankCustomer.Context;
using LoandLoanAproveBankCustomer.Model;
using LoandLoanAproveBankCustomer.Repository.Interface;
using System.Security.Cryptography;

namespace LoandLoanAproveBankCustomer.Repository.Class
{
    public class AproveLoansRepository: IAproveLoansRepository
    {
        private readonly DapperContext _context;
        public AproveLoansRepository(DapperContext conext)
        {
            _context = conext;  
        }

        public async Task<int> ApplyForLoan(AproveLoans aproveLoans)
        {
            var loanAmmount=(double)0;
            //aId,bId,cId,lAmmount,status
            var query = "insert into tblloansApprove (bId,cId,lAmmount,status,lId) " +
                        "values(@bId,@cId,@lAmmount,@status,@lId)";
            using(var connection=_context.CreateConnection())
            {
                
              
                try
                {
                    var bankId = await connection.QuerySingleAsync<int>
                   (@"select bId from tblCustomer where cId=@cId", new { cId = aproveLoans.cId });
                     aproveLoans.bId = bankId;
                }
                catch(Exception ex) 
                {
                    return -1;
                }
                var cibilCheck = await connection.QuerySingleAsync<int>
                  (@"select cibilScore from tblCustomer where cId=@cId", new { cId = aproveLoans.cId });
                if (cibilCheck <= 500)
                {
                    return -3;
                }
                try
                {
                    loanAmmount = await connection.QuerySingleAsync<double>
                  (@"select lAmmount from tblLoans where lId=@lId", new { lId = aproveLoans.lId });
                    aproveLoans.lAmmount = loanAmmount;
                }
                catch (Exception)
                {
                    return -2;
                }
                var balance = await connection.QuerySingleAsync<double>
                   (@"select accountBalance from tblCustomer 
                    where cId=@cId", new { cId =aproveLoans.cId });
                balance += loanAmmount;
                var updateBalance = await connection.ExecuteAsync(@"update tblCustomer set accountBalance=@accountBalance
                                                 where cId=@cId", new { cId = aproveLoans.cId, accountBalance = balance });

                aproveLoans.status = "Pending";
                     var result = await connection.ExecuteAsync(query, aproveLoans);

                    return result;
              
            }
        }

        public async Task<int> PayLoans(int lId,int cId )
        {
            using(var connection=_context.CreateConnection())
            {
                var loanammount=await connection.QuerySingleAsync<double>
                (@"select lAmmount from tblLoans where lId=@lId", new {lId=lId});

                var balance = await connection.QuerySingleAsync<double>
                (@"select accountBalance from tblCustomer where cId=@cId", new {cId=cId});

                if (loanammount > balance)
                {
                    return -1;
                }
                else
                {
                    balance -= loanammount;
                    var updatedBalance = await connection.ExecuteAsync(@"update tblCustomer set accountBalance=@accountBalance
                    where cId=@cId", new { cId = cId, accountBalance = balance });

                    var result = await connection.ExecuteAsync(@"update tblloansApprove set status='clear' 
                    where lId=@lId and cId=@cId", new { lId = lId, cId = cId });
                    return result;
                }

            }

        }
    }
}
