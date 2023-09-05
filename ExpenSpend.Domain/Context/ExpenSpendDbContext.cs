using ExpenSpend.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpenSpend.Domain.Context
{
    public class ExpenSpendDbContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }
        public ExpenSpendDbContext(DbContextOptions<ExpenSpendDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() {Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin"},
                new IdentityRole() {Name = "User", ConcurrencyStamp = "2", NormalizedName = "User"}
            );
        }

        private static void SeedUsers(ModelBuilder builder)
        {
            var hasher = new PasswordHasher<IdentityUser>();
            builder.Entity<User>().HasData(
                new User() { 
                    Email = "admin@asp.net", 
                    FirstName = "Admin", 
                    LastName = "User", 
                    UserName = "admin", 
                    ConcurrencyStamp ="1",
                    PasswordHash = hasher.HashPassword(null,"1q2w3E*")
                },
                new User() { 
                    Email = "user@asp.net", 
                    FirstName = "User", 
                    LastName = "User", 
                    UserName = "user", 
                    ConcurrencyStamp ="2",
                    PasswordHash = hasher.HashPassword(null, "1q2w3E*")
                });
        }
    }
}
