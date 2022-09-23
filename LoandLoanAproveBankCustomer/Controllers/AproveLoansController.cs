using LoandLoanAproveBankCustomer.Model;
using LoandLoanAproveBankCustomer.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace LoandLoanAproveBankCustomer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AproveLoansController : ControllerBase
    {
        private readonly IAproveLoansRepository _aproveRepository;
        public AproveLoansController(IAproveLoansRepository aproveRepository)
        {
            _aproveRepository = aproveRepository;
        }
        [HttpPost]
        public async Task<IActionResult> ApplyforLoans(AproveLoans aproveLoans)
        {
            var result=await  _aproveRepository.ApplyForLoan(aproveLoans);
            if (result >= 1)
            {
                return StatusCode(200, "aplication send successsfully");
            }
            else if(result==-1)
            {
                return StatusCode(500, "bank Id or customer Id is wrong");
            }
            else if(result==-2)
            {
                return StatusCode(500, "please enter right loan id");
            }
            else if(result==-3)
            {
                return StatusCode(500, "Sorry Your Cibil Score dont satisfy our condition");
            }
            else
            {
                return StatusCode(400, "Someting is wrong");
            }
        }
        [HttpPut("/Pay loans")]
        public async Task<IActionResult> PayLoan(int lId,int cId)
        {
    
                var result = await _aproveRepository.PayLoans(lId, cId);

                if (result == -1)
                {
                    return StatusCode(400, "please check your account balance is not enough");
                }
                return StatusCode(200, "loan paid successfully with loan id   :" + lId + "  and customer id   :" + cId);
          
            
        }
       
    }
}
                                                                           