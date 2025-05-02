using Mriguel.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mriguel.Infrastructure.Identity
{
    /// <summary>
    /// Identity database context
    /// </summary>
    public class IdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // Customize the ASP.NET Identity model
            builder.Entity<ApplicationUser>().ToTable("Users", "identity");
            
            // Add any additional identity-specific configuration here
        }
    }
}
