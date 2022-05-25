using BankDataWithMinimalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankDataWithMinimalAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<BankAccount> BankAccount => Set<BankAccount>();
       
    }
}
