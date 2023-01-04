using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Mythos.Common.Users;

namespace Mythos.Common;

public class MythosDbContext : IdentityDbContext<MythosUser>
{
    public MythosDbContext(DbContextOptions<MythosDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //builder.Entity<MythosUser>();

        base.OnModelCreating(builder);
    }
}
