using Dapper;
using LoandLoanAproveBankCustomer.Context;
using LoandLoanAproveBankCustomer.Model;
using LoandLoanAproveBankCustomer.Repository.Interface;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace LoandLoanAproveBankCustomer.Repository.Class
{
    public class CustomerRepository: ICustomerRepository
    {
        private readonly DapperContext _dapperContext;
        public CustomerRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<int> AddNewCustomer(Customer customer)
        {
          var query= @"insert into tblCustomer (bId,cName,mobileNo,emailId,accountType,accountBalance,cibilScore)
                         values(@bId,@cName,@mobileNo,@emailId,@accountType,@accountBalance,@cibilScore)";
            using(var connection=_dapperContext.CreateConnection())
            {
                var result=await connection.ExecuteAsync(query,customer);   
                return result;  
            }
        }

        /*  public async Task<int> AddNewCustomer(List<Customer> custlist,int result)
          {

              //cId,bId,cName,mobileNo,emailId,accountType,accountBalance
              var query = @"insert into tblCustomer (bId,cName,mobileNo,emailId,accountType,accountBalance)
                         values(@bId,@cName,@mobileNo,@emailId,@accountType,@accountBalance)";
              using(var connection=_dapperContext.CreateConnection())
              {
                  foreach (var customer in custlist)
                  {
                      customer.bId = result;

                      var result1 = await connection.ExecuteAsync(query,customer);

                  }
                  return result;
              }

          }*/


        public async Task<int> Trnsaction(int cId, double ammount,string transactionType)
        {
           
            var query = "update tblCustomer set accountBalance=@accountBalance where cId=@cId";
            using(var connection=_dapperContext.CreateConnection())
            {
                if (transactionType == "Credit")
                {
                    var addammount=await connection.QuerySingleAsync<double>(@"select accountBalance from tblCustomer where cId=@cId",new {cId=cId});
                    addammount +=ammount;   
                    var result = await connection.ExecuteAsync(query, new { cId = cId, accountBalance = addammount });
                    return result;
                }
                else
                { 
                    var addammount = await connection.QuerySingleAsync<double>(@"select accountBalance from tblCustomer where cId=@cId", new { cId = cId });
                    addammount -=ammount;
                    var result = await connection.ExecuteAsync(query, new { cId = cId, accountBalance = addammount });
                    return result;

                }
            }
        }
    }
}
