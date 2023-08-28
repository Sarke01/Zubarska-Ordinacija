using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZubarskaOrdinacija.Infrastructure.Identity.Entiteti;

namespace ZubarskaOrdinacija.Infrastructure.Identity
{
    public class IdentityDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationRole>()
                   .HasData(new ApplicationRole(Guid.Parse("5310feb4-a1e1-4439-b511-fd2293f33af0"), IdentityUloge.ADMIN));

            builder.Entity<ApplicationRole>()
                   .HasData(new ApplicationRole(Guid.Parse("673994b0-2d2f-41e6-bae5-24dfd940140d"), IdentityUloge.PACIJENT));
            builder.Entity<ApplicationRole>()
                   .HasData(new ApplicationRole(Guid.Parse("0dc9890e-02f1-476a-8c53-cfcef272dcf8"), IdentityUloge.ZUBAR));
        }
    }
}
