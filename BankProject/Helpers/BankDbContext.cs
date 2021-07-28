using BankProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BankProject.Helpers
{
    public class BankDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<BaseAccount> BankAccounts { get; set; } 
        public DbSet<BaseCard> Cards { get; set; }
        public DbSet<PersonalInfo> PersonalInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=DbBank1;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(a => a.PersonalInfo)
                .WithOne(b => b.User)
                .HasForeignKey<PersonalInfo>(b => b.UserId);

            modelBuilder.Entity<User>()
                .HasMany(c => c.BankAccounts)
                .WithOne(e => e.User)
                .IsRequired();

            modelBuilder.Entity<BaseAccount>()
                .HasMany(c => c.Cards)
                .WithOne(e => e.BankAccount)
                .IsRequired();
        }
    }
}
