using Microsoft.AspNetCore.Mvc;
using MortgageSystem.Data.DataTransferObjects;
using MortgageSystem.Data.Models;
using MortgageSystem.DataConversion;
using MortgageSystem.Services;

namespace MortgageSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MortgageAccountsController : ControllerBase
    {
        #region Private fields
        private readonly IAccountService<MortgageAccount> _mortgageAccountService;
        #endregion

        #region Constructors
        public MortgageAccountsController(IAccountService<MortgageAccount> mortgageAccountService)
        {
            _mortgageAccountService = mortgageAccountService;
        }
        #endregion

        #region Controller Actions

        [Route("/api/MortgageAccounts")]
        [HttpGet]
        public IActionResult GetMortgageAccounts()
        {
            var accounts = _mortgageAccountService.GetAllAccounts() ?? new List<MortgageAccount>();
            var dtoAccounts = new List<DTOMortgageAccount>();

            foreach (var account in accounts)
            {
                dtoAccounts.Add(MortgageAccountConversion.ToDTOMortgageAccount(account));
            }

            return dtoAccounts.Count != 0 ? Ok(dtoAccounts) : NotFound();
        }

        [Route("/api/MortgageAccounts/{accountNumber}")]
        [HttpGet]
        public IActionResult GetMortgageAccount(int accountNumber)
        {
            var account = _mortgageAccountService.GetAccount(accountNumber);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(MortgageAccountConversion.ToDTOMortgageAccount(account));
        }

        [HttpPost]
        public IActionResult CreateMortgageAccount(DTOMortgageAccount account)
        {
            //Convert Data Transfer Object to Data Model Object so it can be created on database
            var newMortgageAccount = MortgageAccountConversion.ToMortgageAccount(account);

            //Use exception handling to return relevant response depending if account was created successfully
            try
            {
                var createdMortgageAccount = _mortgageAccountService.CreateAccount(newMortgageAccount);

                //Convert back to DTO and return
                return Ok(MortgageAccountConversion.ToDTOMortgageAccount(createdMortgageAccount));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating account");
            }
        }

        [Route("/api/MortgageAccounts/{accountNumber}")]
        [HttpPut]
        public IActionResult UpdateMortgageAccount(int accountNumber, DTOMortgageAccount account)
        {
            //Check the accountNumber from the route matches the accountNumber of the account details to be updated
            if (accountNumber != account.AccountNumber)
            {
                return BadRequest();
            }

            //Look up the account to update
            if (_mortgageAccountService.GetAccount(account.AccountNumber) == null)
            {
                return NotFound();
            }

            //Use exception handling to return relevant response depending if account was updated successfully
            try
            {
                var updatedMortgageAccount = _mortgageAccountService.UpdateAccount(MortgageAccountConversion.ToMortgageAccount(account));

                return Ok(MortgageAccountConversion.ToDTOMortgageAccount(updatedMortgageAccount));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating account");
            }
        }

        [Route("/api/MortgageAccounts/{accountNumber}")]
        [HttpDelete]
        public IActionResult DeleteMortgageAccount(int accountNumber)
        {
            //Look up the account to delete
            if (_mortgageAccountService.GetAccount(accountNumber) == null)
            {
                return NotFound();
            }

            //Use exception handling to return relevant response depending if account was deleted successfully
            try
            {
                _mortgageAccountService.DeleteAccount(accountNumber);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting account");
            }
        }

        [Route("/api/MortgageAccounts/Populate")]
        [HttpPost]
        public IActionResult Populate()
        {
            MortgageAccount mortgageAccount = new(1, 1.23m, 250000m, 505.25m);
            _mortgageAccountService.CreateAccount(mortgageAccount);

            MortgageAccount mortgageAccount1 = new(1, 3.32m, 505500.25m, 1252.32m);
            _mortgageAccountService.CreateAccount(mortgageAccount1);

            MortgageAccount mortgageAccount2 = new(1, 4.75m, 101101.01m, 350.97m);
            _mortgageAccountService.CreateAccount(mortgageAccount2);

            MortgageAccount mortgageAccount3 = new(1, 3.99m, 1000000.22m, 258.98m);
            _mortgageAccountService.CreateAccount(mortgageAccount3);

            MortgageAccount mortgageAccount4 = new(1, 6.65m, 110000m, 90.25m);
            _mortgageAccountService.CreateAccount(mortgageAccount4);

            MortgageAccount mortgageAccount5 = new(1, 4.56m, 81000m, 400.99m);
            _mortgageAccountService.CreateAccount(mortgageAccount5);

            MortgageAccount mortgageAccount6 = new(1, 7.77m, 125250.33m, 70.25m);
            _mortgageAccountService.CreateAccount(mortgageAccount6);

            MortgageAccount mortgageAccount7 = new(1, 6.32m, 808808m, 777.77m);
            _mortgageAccountService.CreateAccount(mortgageAccount7);

            MortgageAccount mortgageAccount8 = new(1, 3.12m, 123456m, 897.25m);
            _mortgageAccountService.CreateAccount(mortgageAccount8);

            MortgageAccount mortgageAccount9 = new(1, 4.11m, 97789m, 97.25m);
            _mortgageAccountService.CreateAccount(mortgageAccount9);

            return Ok();
        }

        [Route("/api/MortgageAccounts/Purge")]
        [HttpDelete]
        public IActionResult Purge()
        {
            var accounts = _mortgageAccountService.GetAllAccounts() ?? new List<MortgageAccount>();

            foreach (var account in accounts)
            {
                _mortgageAccountService.DeleteAccount(account.AccountNumber);
            }

            return Ok();
        }

        #endregion
    }
}
