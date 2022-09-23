using LoandLoanAproveBankCustomer.Model;
using LoandLoanAproveBankCustomer.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoandLoanAproveBankCustomer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBankRepository _bankRepo;
        public BankController(IBankRepository bankRepo)
        {
            _bankRepo = bankRepo;
        }
        [HttpPost]
        public async Task<IActionResult> AddNewBank(Bank bank)
        {
            var result=await _bankRepo.AddNewBank(bank);
            if (result != 0)
            {
                return StatusCode(200, "Bank Added Successfully ");
            }
            else
                return StatusCode(400, "Something is wrong");
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBanksWithCustomerss()
        {
            var result = await _bankRepo.GetAllBanksWithCustomers();
           
            return Ok(result);  
        }
        [HttpGet("/GetBanksWithLoans")]
        public async Task<IActionResult> GetAllBanksWithLoans()
        {
            var result=await _bankRepo.GetAllBanksWithLoans();
            return Ok(result);
        }
        [HttpGet("/Get All Loans")]
        public async Task<IActionResult> GetAllLoanswith()
        {
            var result = await _bankRepo.GetAllBanksWithLoansWithCustomers();
            return Ok(result);
        }
        [HttpGet("/GetAllAproveLoansByBankId")]
        public async Task<IActionResult> GetAllAproveLoansById(int? bId)
        {
            var result = await _bankRepo.GetAllAproveLoansByBankId(bId);

            return Ok(result);
        }
        [HttpGet("GetAllAproveLoansByCustId")]
        public async Task<IActionResult> GetAllAproveLoansByCustId(int id)
        {
            var result=await _bankRepo.GetAllAproveLoansByCustmerId(id);
            return Ok(result);
        }
    }
}
