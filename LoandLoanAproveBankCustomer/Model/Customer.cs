namespace LoandLoanAproveBankCustomer.Model
{
    public class Customer
    {
        //cId,bId,cName,mobileNo,emailId,accountType,accountBalance  
        public int cId { get; set; }
        public int bId { get; set; }
        public string cName { get; set; }
        public string mobileNo { get; set; }
        public string emailId { get; set; }
        public string accountType { get; set; }
        public double accountBalance { get; set; }
        public int cibilScore { get; set; }
    }
}
