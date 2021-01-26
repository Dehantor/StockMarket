
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace StockMarket.Models
{
    public class Context:DbContext
    {
        public DbSet<Stock> Stocks { get; set; }
        public Context(DbContextOptions<Context> options):base(options)
        {

        }
        public DbSet<Source> Source { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}
