using Microsoft.AspNetCore.Mvc;
using MortgageSystem.Controllers;
using MortgageSystem.Data.DataTransferObjects;
using MortgageSystem.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MortgageSystem.Tests.ControllerTests
{
    public class MortgageAccountsControllerTest
    {
        [Fact]
        public void GetMortgageAccountTest_Success()
        {
            //Arrange
            var controller = new MortgageAccountsController(new MockMortgageAccountService(true));

            //Act
            OkObjectResult result = (OkObjectResult)controller.GetMortgageAccount(123);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 200);
            Assert.True(result.Value?.GetType() == typeof(DTOMortgageAccount));

            DTOMortgageAccount account = (DTOMortgageAccount)result.Value;
            Assert.NotNull(account);
            Assert.True(account.AccountNumber == 123);
            Assert.True(account.InterestRate == 1.11m);
            Assert.True(account.Balance == 2.22m);
            Assert.True(account.MonthlyPayment == 3.33m);
        }

        [Fact]
        public void GetMortgageAccountTest_Failure()
        {
            //Arrange
            var controller = new MortgageAccountsController(new MockMortgageAccountService(false));

            //Act
            NotFoundResult result = (NotFoundResult)controller.GetMortgageAccount(123);

            //Assert
            Assert.True(result.StatusCode == 404);
        }

        [Fact]
        public void GetMortgageAccountsTest_Success()
        {
            //Arrange
            var controller = new MortgageAccountsController(new MockMortgageAccountService(true));

            //Act
            OkObjectResult result = (OkObjectResult)controller.GetMortgageAccounts();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 200);
            Assert.True(result.Value?.GetType() == typeof(List<DTOMortgageAccount>));

            List<DTOMortgageAccount> accounts = (List<DTOMortgageAccount>)result.Value;
            Assert.NotNull(accounts);
            Assert.True(accounts.Count == 3);
        }

        [Fact]
        public void GetMortgageAccountsTest_Failure()
        {
            //Arrange
            var controller = new MortgageAccountsController(new MockMortgageAccountService(false));

            //Act
            NotFoundResult result = (NotFoundResult)controller.GetMortgageAccounts();

            //Assert
            Assert.True(result.StatusCode == 404);
        }

        [Fact]
        public void CreateMortgageAccountTest_Success()
        {
            //Arrange
            var controller = new MortgageAccountsController(new MockMortgageAccountService(true));
            var account = new DTOMortgageAccount()
            {
                AccountNumber = 123,
                Balance = 1234.56m,
                InterestRate = 12.34m,
                MonthlyPayment = 456.78m
            };

            //Act
            OkObjectResult result = (OkObjectResult)controller.CreateMortgageAccount(account);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 200);
            Assert.True(result.Value?.GetType() == typeof(DTOMortgageAccount));

            DTOMortgageAccount newAccount = (DTOMortgageAccount)result.Value;
            Assert.NotNull(newAccount);
            Assert.True(newAccount.AccountNumber == 123);
            Assert.True(newAccount.InterestRate == 12.34m);
            Assert.True(newAccount.Balance == 1234.56m);
            Assert.True(newAccount.MonthlyPayment == 456.78m);

        }

        [Fact]
        public void CreateMortgageAccountTest_Failure()
        {
            //Arrange
            var controller = new MortgageAccountsController(new MockMortgageAccountService(false));

            //Act
            ObjectResult result = (ObjectResult)controller.CreateMortgageAccount(new DTOMortgageAccount());

            //Assert
            Assert.True(result.StatusCode == 500);
            Assert.True(result.Value == "Error creating account");
        }

        [Fact]
        public void UpdateMortgageAccountTest_Success()
        {
            //Arrange
            var controller = new MortgageAccountsController(new MockMortgageAccountService(true));
            var account = new DTOMortgageAccount()
            {
                AccountNumber = 123,
                Balance = 1234.56m,
                InterestRate = 12.34m,
                MonthlyPayment = 456.78m
            };

            //Act
            OkObjectResult result = (OkObjectResult)controller.UpdateMortgageAccount(123, account);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 200);
            Assert.True(result.Value?.GetType() == typeof(DTOMortgageAccount));

            DTOMortgageAccount newAccount = (DTOMortgageAccount)result.Value;
            Assert.NotNull(newAccount);
            Assert.True(newAccount.AccountNumber == 123);
            Assert.True(newAccount.InterestRate == 12.34m);
            Assert.True(newAccount.Balance == 1234.56m);
            Assert.True(newAccount.MonthlyPayment == 456.78m);
        }

        [Fact]
        public void UpdateMortgageAccountTest_Failure_AccountNumberMismatch()
        {
            //Arrange
            var controller = new MortgageAccountsController(new MockMortgageAccountService(true));
            var account = new DTOMortgageAccount()
            {
                AccountNumber = 123,
                Balance = 1234.56m,
                InterestRate = 12.34m,
                MonthlyPayment = 456.78m
            };

            //Act
            BadRequestResult result = (BadRequestResult)controller.UpdateMortgageAccount(456, account);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 400);
        }

        [Fact]
        public void UpdateMortgageAccountTest_Failure_AccountNotFound()
        {
            //Arrange
            var controller = new MortgageAccountsController(new MockMortgageAccountService(false));
            var account = new DTOMortgageAccount()
            {
                AccountNumber = 456
            };

            //Act
            NotFoundResult result = (NotFoundResult)controller.UpdateMortgageAccount(456, account);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 404);
        }

        [Fact]
        public void UpdateMortgageAccountTest_Failure_ExceptionThrown()
        {
            //Arrange
            var service = new MockMortgageAccountService(true);
            service.ThrowException = true;
            var controller = new MortgageAccountsController(service);
            var account = new DTOMortgageAccount()
            {
                AccountNumber = 456
            };

            //Act
            ObjectResult result = (ObjectResult)controller.UpdateMortgageAccount(456, account);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 500);
            Assert.True(result.Value == "Error updating account");
        }
    }
}
