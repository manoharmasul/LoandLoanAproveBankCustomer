using Dapper;
using LoandLoanAproveBankCustomer.Context;
using LoandLoanAproveBankCustomer.Model;
using LoandLoanAproveBankCustomer.Repository.Interface;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace LoandLoanAproveBankCustomer.Repository.Class
{
    
        public class BankRepository : IBankRepository
        {
            private readonly DapperContext context;
            public BankRepository(DapperContext context)
            {
                this.context = context;
            }


           private readonly CustomerRepository custrepo;

            public async Task<int> AddNewBank(Bank bank)
            {
                var query = "insert into tblBank (bName) values (@bName);select scope_identity() as int";
                using (var connetion = context.CreateConnection())
                {
                    var result = await connetion.QuerySingleAsync<int>(query, bank);
                 await AddNewCustomerr(bank.custlist,result);
                    return result;
                }
            }
        public async Task<int> AddNewCustomerr(List<Customer> custlist, int result)
        {

            //cId,bId,cName,mobileNo,emailId,accountType,accountBalance
            var query = @"insert into tblCustomer (bId,cName,mobileNo,emailId,accountType,accountBalance)
                       values(@bId,@cName,@mobileNo,@emailId,@accountType,@accountBalance)";
            using (var connection = context.CreateConnection())
            {
                foreach (var customer in custlist)
                {
                    customer.bId = result;

                    var result1 = await connection.ExecuteAsync(query, customer);

                }
                return result;
            }

        }

        public async Task<List<Bank>> GetAllBanksWithCustomers()
            {
                List<Bank> blist = new List<Bank>();
                var query = "select * from tblBank";
                using (var connection = context.CreateConnection())
                {
                    var result = await connection.QueryAsync<Bank>(query);
                    blist = result.ToList();
                    foreach (var bank in blist)
                    {
                        var custlist = await connection.QueryAsync<Customer>(@"select * from tblCustomer where bId=@bId", new { bId = bank.bId });
                        bank.custlist = custlist.ToList();

                    }
                    return blist;
                }
            }

            public async Task<List<Bankloans>> GetAllBanksWithLoans()
            {
                List<Bankloans> blist = new List<Bankloans>();
                var query = "select * from tblBank";
                using (var connection = context.CreateConnection())
                {
                    var result = await connection.QueryAsync<Bankloans>(query);
                    blist = result.ToList();
                    foreach (var bank in blist)
                    {
                        var custlist = await connection.QueryAsync<AproveLoans>(@"select * from tblloansApprove where bId=@bId", new { bId = bank.bId });
                        bank.loanlist = custlist.ToList();

                    }
                    return blist;
                }
            }

        public async Task<List<CombineModel>> GetAllBanksWithLoansWithCustomers()
        {
            //bId,bName,cId,cName,mobileNo,emailId,accountType,accountBalance,aId,lAmmount,status


            var query = @"select b.bName,c.cName,c.mobileNo,c.emailId,l.lAmmount,l.status from tblBank b inner join tblCustomer c on b.bId=c.bId
                    inner join tblloansApprove l on c.cId=l.cId";
            using(var connection = context.CreateConnection())
            {
                var result=await connection.QueryAsync<CombineModel>(query);  
                return result.ToList(); 
            }
        }

        public Task<List<AproveLoans>> GetAllAproveLoansWithCustomers()
        {
            throw new NotImplementedException();
        }

        public async Task<List<CombineModel>> GetAllAproveLoansByBankId(int? bankId)
        {
            var query = "select bName,cName,mobileNo,emailId,lAmmount,status from tblCustomer inner join tblloansApprove on tblCustomer.cId = tblloansApprove.cId inner join tblBank on tblCustomer.bId=tblBank.bId where tblCustomer.bId = @bId";
            using(var connection = context.CreateConnection())
            {
                var reslt=await connection.QueryAsync<CombineModel>(query,new {bId=bankId});
                return reslt.ToList();
            }

        }

        public async Task<List<combine>> GetAllAproveLoansByCustmerId(int id)
        {
            double total=0;    
            var query = "select c.cId, c.cName,c.emailId,la.status,l.lName,l.lAmmount from tblCustomer c inner join tblloansApprove la on c.cId=la.cId inner join tblLoans l on la.lId=l.lId where c.cId=@cId";
            using(var connection=context.CreateConnection())
            {
                var result = await connection.QueryAsync<combine>(query, new { cId = id });
               
                return result.ToList();
            }
        }
    }
}



