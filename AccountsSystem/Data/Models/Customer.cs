using System.ComponentModel.DataAnnotations;

namespace MortgageSystem.Data.Models
{
    public class Customer
    {
        #region Public properties
        [Key]
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        //public List<Account>? Accounts { get; set; }
        #endregion

        #region Constructors
        public Customer(int id, string firstName, string surname)
        {
            CustomerID = id;
            Surname = surname;
            FirstName = firstName;
        }
        #endregion
    }
}
