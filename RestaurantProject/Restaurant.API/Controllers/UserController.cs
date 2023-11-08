using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.DAL.Data;
using Restaurant.MVC.Models.ViewModels;
using System.Security.Claims;

namespace Restaurant.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<AppUser> userManager,IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetUser()
        //{
        //    var userdata = User.FindFirst("username").Value;
        //    var username = User.FindFirstValue(userdata);
        //    var user = await _userManager.FindByNameAsync(username);

        //    if (user == null)
        //    {
        //        return NotFound(); // Return a 404 Not Found response if the user is not found.
        //    }

        //    var userViewModel = _mapper.Map<ProfileVM>(user);
        //    return Ok(userViewModel);
        //}
    }
}
