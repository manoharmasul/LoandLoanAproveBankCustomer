using LoandLoanAproveBankCustomer.Model;
using LoandLoanAproveBankCustomer.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoandLoanAproveBankCustomer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;  
        private readonly IBankRepository _bankRepository;   
        public CustomerController(ICustomerRepository customerRepository,IBankRepository bankRepository)
        {
            _customerRepository = customerRepository;
            _bankRepository = bankRepository;   
        }
        
        [HttpPut("/Transaction ")]
        public async Task<IActionResult> Tansaction(int cId, double balance,string transactionType)
        {
            var result=await _customerRepository.Trnsaction(cId, balance,transactionType);  
            return Ok(result);

        }
        [HttpPost]
        public async Task<IActionResult> AddNewCustomer(Customer customer)
        {
            var result = await _customerRepository.AddNewCustomer(customer);
            if(result==null)
            {
                return StatusCode(400, "Someting is wrong");

            }
            else 
            {
                return StatusCode(200, "Account is opening is done !...");
            }
        }
       
    }
}
