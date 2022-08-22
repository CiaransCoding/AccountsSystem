using MortgageSystem.Data.DataTransferObjects;
using MortgageSystem.Data.Models;

namespace MortgageSystem.DataConversion
{
    public static class MortgageAccountConversion
    {
        public static DTOMortgageAccount ToDTOMortgageAccount(MortgageAccount mortgageAccount)
        {
            return new DTOMortgageAccount()
            {
                AccountNumber = mortgageAccount.AccountNumber,
                Balance = mortgageAccount.Balance,
                InterestRate = mortgageAccount.InterestRate,
                MonthlyPayment = mortgageAccount.MonthlyPayment
            };
        }

        public static MortgageAccount ToMortgageAccount(DTOMortgageAccount dtoMortgageAccount)
        {
            return new MortgageAccount(
                dtoMortgageAccount.AccountNumber,
                dtoMortgageAccount.InterestRate,
                dtoMortgageAccount.Balance,
                dtoMortgageAccount.MonthlyPayment);
        }
    }
}
