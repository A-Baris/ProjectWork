using Microsoft.AspNetCore.Identity;
using Restaurant.Entity.Enums;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Models
{
    public class AppUser : IdentityUser
    {
        [MaxLength(100)]
        public string? CustomerName{get;set;}
        [MaxLength(100)]
        public string? CustomerSurname{get;set;}
        public bool? UserRight { get;set;}
    }
}
