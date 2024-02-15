using CryptoScanner.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoScanner.Data.Database
{
    public class CoinDbContext : DbContext
    {
        public CoinDbContext(DbContextOptions<CoinDbContext> options) : base(options)
        {

        }
        public DbSet<CoinModel> Coins { get; set; }
    }
}
