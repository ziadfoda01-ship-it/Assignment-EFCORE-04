using Assignment_EFCORE_04.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_EFCORE_04
{
    internal class AbbDbContext : DbContext
    {
        public DbSet <Branch> Branches { get; set; }
        public DbSet <Manager> Managers { get; set; }
        public DbSet <Account> Accounts { get; set; }
        public DbSet <Customer> Customers { get; set; }
        public DbSet <customerAccount> CustomerAccounts { get; set; }
        public DbSet <Transaction> Transactions { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ZIAD\\ZIAD;Database=NationalBankDb;Trusted_Connection=True;TrustServerCertificate=True;");
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure Composite Primry Key For CustomerAccount (Many To Many )
            modelBuilder.Entity<customerAccount>()
            .HasKey(ca => new { ca.CustomerId, ca.AccountNumber });
            modelBuilder.Entity<customerAccount>()
                .HasOne(CA => CA.customer)
                .WithMany(ca => ca.customerAccounts)
                .HasForeignKey(ca => ca.CustomerId);
            modelBuilder.Entity<customerAccount>()
                .HasOne(ca => ca.Account)
                .WithMany(a => a.CustomerAccounts)
                .HasForeignKey(a => a.AccountNumber);
            modelBuilder.Entity<Account>()
                .Property(a => a.CurrentBalance)
                .HasColumnType("decimal(10,2)");
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasColumnType("decimal(10,2)");
            
        }
    }
}
