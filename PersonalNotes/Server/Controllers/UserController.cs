using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalNotes.Server.Data;
using PersonalNotes.Server.Data.Models;
using PersonalNotes.Shared;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalNotes.Server.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly UserManager<ApplicationUser> _userManager;

    private readonly ApplicationDbContext _context;


    public UserController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _context = context;
    }

    [AllowAnonymous]
    [HttpGet("tryGetUsername")]
    public TryGetUsernameResultDTO TryGetUsername()
    {
        return User.Identity!.IsAuthenticated
            ? new TryGetUsernameResultDTO()
            {
                IsLoggedIn = true,
                Username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)!.Value
            }
            : new TryGetUsernameResultDTO()
            {
                IsLoggedIn = false,
                Username = ""
            };
    }

    [HttpPost("logout")]
    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }

    // POST api/<UserController>
    [HttpPost("login")]
    public async Task<ActionResult<LoginResultDTO>> Login(LoginDTO loginDTO)
    {
        var result = await _signInManager.PasswordSignInAsync(loginDTO.UserName, loginDTO.Password, loginDTO.IsPersistant, false);


        return new LoginResultDTO()
        {
            Success = result.Succeeded,
            Message = result.ToString(),
        };
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegisterResultDTO>> Register(RegisterDTO registerDTO)
    {
        ApplicationUser applicationUser = new()
        {
            UserName = registerDTO.UserName,
            Email = registerDTO.Email,
        };


        var result = await _userManager.CreateAsync(applicationUser, registerDTO.Password);

        if (result.Succeeded) 
        {
            Journal journal = new()
            {
                Title = "Your first journal!",
                User = applicationUser,
                Content = "How are you feeling today? edit this journal and tell me all about it"
            };

            _context.Journal.Add(journal);
            await _context.SaveChangesAsync();
        }

        return new RegisterResultDTO()
        {
            Success = result.Succeeded,
            Message = result.Errors.Select(e => e.Description).ToArray()
        };
    }
}
