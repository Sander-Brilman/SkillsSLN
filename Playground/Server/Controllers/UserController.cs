using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Playground.Shared;

namespace Playground.Server.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager;

    private readonly UserManager<IdentityUser> _userManager;

    public UserController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }


    [HttpGet("setUser")]
    public async Task SetUser()
    {
        await _userManager.CreateAsync(new IdentityUser()
        {
            Email = "test@test.nl",
            UserName = "test@test.nl",
        }, "ue3md6uhgsqCLAuCrTndRwzPvm4v7PFzLDxkXQ%&");
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginDTO loginDTO)
    {
        var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, true, false);

        if (result.Succeeded)
        {
            return Ok();
        }

        return BadRequest();
    }

    [Authorize]
    [HttpGet("text")]
    public async Task<string> GetText()
    {
        return "Hello There!";
    }
}
