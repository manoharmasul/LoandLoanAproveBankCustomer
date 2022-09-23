namespace LoandLoanAproveBankCustomer.Model
{
    public class Bank
    {
        public int bId { get; set; }
        public string bName { get; set; }
        public List<Customer> custlist { get; set; }
    }
    public class Bankloans
    {
        public int bId { get; set; }
        public string bName { get; set; }
        public List<AproveLoans> loanlist { get; set; }
    }


}
