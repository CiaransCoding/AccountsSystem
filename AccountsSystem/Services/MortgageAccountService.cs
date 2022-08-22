using Microsoft.EntityFrameworkCore;
using MortgageSystem.Data;
using MortgageSystem.Data.Models;

namespace MortgageSystem.Services
{
    public class MortgageAccountService : IAccountService<MortgageAccount>
    {
        #region Private Fields
        private readonly DataContext _context;
        #endregion

        #region Constructors
        public MortgageAccountService(DataContext context)
        {
            this._context = context;
        }
        #endregion

        #region IAccountService Methods
        public MortgageAccount CreateAccount(MortgageAccount account)
        {
            //The ExecuteSqlInterpolated method takes an interpolated string as a parameter and sanitises the 'accountNumber' parameter to prevent SQL injection threats
            _context.Database.ExecuteSqlInterpolated($"INSERT INTO dbo.MortgageAccounts(Balance, InterestRate, MonthlyPayment) VALUES({account.Balance}, {account.InterestRate}, {account.MonthlyPayment})");

            //Return the account that has just been created
            return this.GetAllAccounts().LastOrDefault();
        }

        public MortgageAccount GetAccount(int accountNumber)
        {
            //The FromSqlInterpolated method takes an interpolated string as a parameter and sanitises the 'accountNumber' parameter to prevent SQL injection threats
            var account = _context.MortgageAccounts
                            .FromSqlInterpolated($"SELECT * FROM dbo.MortgageAccounts WHERE AccountNumber = {accountNumber}")
                            .ToList()
                            .FirstOrDefault();

            return account;
        }

        public List<MortgageAccount> GetAllAccounts()
        {
            var query = "SELECT * FROM dbo.MortgageAccounts";
            var mortgageAccounts = _context.MortgageAccounts
                                            .FromSqlRaw(query)
                                            .ToList();

            return mortgageAccounts;
        }

        public MortgageAccount UpdateAccount(MortgageAccount account)
        {
            _context.Database.ExecuteSqlInterpolated(@$"
                UPDATE dbo.MortgageAccounts
                SET Balance = {account.Balance}, InterestRate = {account.InterestRate}, MonthlyPayment = {account.MonthlyPayment}
                WHERE AccountNumber = {account.AccountNumber}
            ");

            //Return the account that has just been updated
            return this.GetAccount(account.AccountNumber);
        }

        public void DeleteAccount(int accountNumber)
        {
            _context.Database.ExecuteSqlInterpolated(@$"
                DELETE FROM dbo.MortgageAccounts
                WHERE AccountNumber = {accountNumber}
            ");
        }

        #endregion
    }
}
