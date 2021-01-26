using Microsoft.EntityFrameworkCore;
using ServerEF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerEF
{
    /// <summary>
    /// Подключение к бд
    /// </summary>
    class Context : DbContext
    {
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Source> Sources { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=stockmarket;Username=postgres;Password=123123");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            //добавляем уникальность полей
            modelBuilder.Entity<Source>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Company>().HasIndex(x => x.Ticker).IsUnique();
            //modelBuilder.Entity<Stock>().
            //   Property(p => p.Date)
            //   .HasColumnType("datetime2")
            //   .HasPrecision(0)
            //   .IsRequired();
        }
        
    }
}
