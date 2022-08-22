using Microsoft.EntityFrameworkCore;
using MortgageSystem.Data.Models;

namespace MortgageSystem.Data
{
    public class DataContext : DbContext
    {
        #region Constructors
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        #endregion

        #region Data
        public DbSet<MortgageAccount> MortgageAccounts { get; set; }

        #endregion
    }
}
