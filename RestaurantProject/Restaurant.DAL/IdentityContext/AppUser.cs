using Microsoft.AspNetCore.Identity;

namespace Restaurant.DAL.Data
{
    public class AppUser : IdentityUser
    {
        public string? CustomerName { get; set; }
        public string? CustomerSurname { get; set; }

    }
}
