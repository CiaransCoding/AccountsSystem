using System.ComponentModel.DataAnnotations;

namespace MortgageSystem.Data.Models
{
    public class MortgageAccount : Account
    {
        #region Constructors
        public MortgageAccount(int accountNumber, decimal interestRate, decimal balance, decimal monthlyPayment) : base(accountNumber)
        {
            InterestRate = interestRate;
            Balance = balance;
            MonthlyPayment = monthlyPayment;
        }
        #endregion

        #region Public Properties
        public decimal InterestRate { get; set; }
        public decimal Balance { get; set; }
        public decimal MonthlyPayment { get; set; }
        #endregion
    }
}
