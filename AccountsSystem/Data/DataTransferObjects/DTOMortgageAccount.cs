namespace MortgageSystem.Data.DataTransferObjects
{
    public class DTOMortgageAccount : DTOAccount
    {
        public decimal InterestRate { get; set; }
        public decimal Balance { get; set; }
        public decimal MonthlyPayment { get; set; }
    }
}
