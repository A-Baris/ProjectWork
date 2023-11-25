using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Restaurant.MVC.Models.Context
{
    public class UserRoleIdentityContext : IdentityDbContext<AppUser, AppRole, string>
    {


        public UserRoleIdentityContext(DbContextOptions<UserRoleIdentityContext> options) : base(options)
        {
        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("server=DESKTOP-KUQ9PNH;database=RestaurantUserDB;uid=sa;pwd=1234;TrustServerCertificate=True");
        //    }
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}

