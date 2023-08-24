using ExpenSpend.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpenSpend.Domain.Context
{
    public class ExpenSpendDbContext : IdentityDbContext<User>
    {
        public ExpenSpendDbContext(DbContextOptions<ExpenSpendDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
