using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using W_Machine.Models;

namespace W_Machine.Concrete
{
    public class EFDbContext : DbContext
    {
        public EFDbContext(DbContextOptions<EFDbContext> options)
           : base(options)
        {
            var sqlServerOptionsExtension =
                  options.FindExtension<SqlServerOptionsExtension>();
            if (sqlServerOptionsExtension != null)
            {
                string connectionString = sqlServerOptionsExtension.ConnectionString;
            }
        }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Coin> Coins { get; set; }
    }
}
