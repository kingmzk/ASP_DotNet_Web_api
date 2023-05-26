using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASP_DotNet_Web_api.Data
{
    public class MZWalksAuthDbContext : IdentityDbContext                        //IdentityDbContext. This class is responsible for managing the database context for the authentication and authorization system in your ASP.NET Web API.
    {
        public MZWalksAuthDbContext(DbContextOptions<MZWalksAuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "c099979b-5785-4a21-aed1-b40c5ff5c1e3";
            var writerRoleId = "acfee914-511b-44fa-bc8f-3dd0b5b47ea2";

            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                },

                new IdentityRole()
                {
                    Id = writerRoleId,
                    ConcurrencyStamp= writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
