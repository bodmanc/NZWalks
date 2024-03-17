using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "72785587-6fb8-4ebb-9a6a-738a66c67bed";
            var writerRoleId = "57f4e8a5-f18b-435e-97fc-b1cef2ce651a";

            var roles = new List<IdentityRole>
            {
                 new IdentityRole
                {
                   Id = readerRoleId,
                   ConcurrencyStamp = readerRoleId,
                   Name = "Reader",
                   NormalizedName = "Reader".ToUpper(),
                },
                 new IdentityRole
                 {
                     Id = writerRoleId,
                     ConcurrencyStamp = writerRoleId,
                     Name = "Writer",
                     NormalizedName = "Writer".ToUpper()
                 }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
