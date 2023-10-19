using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlaygroundHostedWasm.Server.Data.Models;

namespace PlaygroundHostedWasm.Server.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<ApiUser> _userManager;

    private readonly SignInManager<ApiUser> _signInManager;

    public UserController(UserManager<ApiUser> userManager, SignInManager<ApiUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet("setUser")]
    public async Task SetUser()
    {
        await _userManager.CreateAsync(new ApiUser()
        {
            FirstName = "Sander",
            Email = "e@mail.nl",
            UserName = "e@mail.nl",
        }, "&g4vxfZh$gfCwy6WQeZBUaow*^@bm!zpc5%wcu5nQ");
    }

    [HttpGet("login")]
    public async Task<ActionResult> Login()
    {
        var result = await _signInManager.PasswordSignInAsync("e@mail.nl", "&g4vxfZh$gfCwy6WQeZBUaow*^@bm!zpc5%wcu5nQ", false, false);

        if (result.Succeeded is false)
        {
            return BadRequest();
        }

        return Ok();
    }

    [Authorize]
    [HttpGet("protectedString")]
    public async Task<string> GetProtectedString()
    {
        return "Oh Hi Mark!";
    }

}
