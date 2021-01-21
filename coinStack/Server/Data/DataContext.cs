using coinStack.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coinStack.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Coin> Coins { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<UserPortfolio> UserPortfolios { get; set; }
        public DbSet<PortfolioTransaction> PortfolioTransactions { get; set; }
        public DbSet<PortfolioCoin> PortfolioCoins { get; set; }
    }
}
