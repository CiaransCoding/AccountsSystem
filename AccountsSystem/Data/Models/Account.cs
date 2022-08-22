using System.ComponentModel.DataAnnotations;

namespace MortgageSystem.Data.Models
{
    public class Account
    {
        #region Public Properties
        [Key]
        public int AccountNumber { get; set; }

        //Implement later
        //public List<Decimal> TransactionHistory { get; set; }
        #endregion

        #region Constructors
        public Account(int accountNumber)
        {
            AccountNumber = accountNumber;
        }
        #endregion
    }
}
