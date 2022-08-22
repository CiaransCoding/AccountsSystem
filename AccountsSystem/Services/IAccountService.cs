using MortgageSystem.Data;

namespace MortgageSystem.Services
{
    public interface IAccountService<T>
    {
        List<T> GetAllAccounts();
        T GetAccount(int accountNumber);
        T CreateAccount(T account);
        T UpdateAccount(T account);
        void DeleteAccount(int accountNumber);
    }
}
