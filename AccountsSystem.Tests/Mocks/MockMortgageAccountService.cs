using MortgageSystem.Data.Models;
using MortgageSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageSystem.Tests.Mocks
{
    public class MockMortgageAccountService : IAccountService<MortgageAccount>
    {
        public MockMortgageAccountService(bool returnAccount)
        {
            ReturnAccount = returnAccount;
        }

        public bool ReturnAccount { get; set; }
        public bool ThrowException { get; set; }

        public MortgageAccount CreateAccount(MortgageAccount account)
        {
            if (ReturnAccount)
            {
                return account;
            }

            throw new InvalidOperationException();
        }

        public void DeleteAccount(int accountNumber)
        {
            return;
        }

        public MortgageAccount? GetAccount(int accountNumber)
        {
            if (ReturnAccount)
            {
                return new MortgageAccount(accountNumber, 1.11m, 2.22m, 3.33m);
            }

            return null;
        }

        public List<MortgageAccount>? GetAllAccounts()
        {
            if (ReturnAccount)
            {
                return new List<MortgageAccount>()
                {
                    new MortgageAccount(1, 1.11m, 2.22m, 3.33m),
                    new MortgageAccount(2, 2.22m, 3.33m, 4.44m),
                    new MortgageAccount(3, 3.33m, 4.44m, 5.55m),
                };
            }

            return null;
        }

        public MortgageAccount? UpdateAccount(MortgageAccount account)
        {
            if (ThrowException)
            {
                throw new InvalidOperationException();
            }

            if (ReturnAccount)
            {
                return account;
            }

            return null;
        }
    }
}
