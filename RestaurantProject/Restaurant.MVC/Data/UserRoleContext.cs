using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.MVC.Data
{
    public class UserRoleContext:IdentityDbContext<AppUser,AppRole,string>
    {
        public UserRoleContext(DbContextOptions<UserRoleContext> options):base(options) 
        { 
        }
       
    }


      
    }

