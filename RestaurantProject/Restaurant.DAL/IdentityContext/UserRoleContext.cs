using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.DAL.Data
{
    public class UserRoleContext : IdentityDbContext<AppUser, AppRole, string>
    {

        public UserRoleContext(DbContextOptions<UserRoleContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=DESKTOP-KUQ9PNH;database=RestaurantUserDB;uid=sa;pwd=1234;TrustServerCertificate=True");
            }
            base.OnConfiguring(optionsBuilder);
        }


    }



}

