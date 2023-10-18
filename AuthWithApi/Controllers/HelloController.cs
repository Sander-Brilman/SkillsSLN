using AuthWithApi.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using System.Security.Claims;

namespace AuthWithApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class HelloController : ControllerBase
{
    private readonly UserManager<ApiUser> _userManager;

    private readonly SignInManager<ApiUser> _signInManager;

    public HelloController(UserManager<ApiUser> userManager, SignInManager<ApiUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet("setUser")]
    public async Task SetUser()
    {
        await _userManager.CreateAsync(new ApiUser()
        {
            UserName = "e@mail.com",
            Email = "e@mail.com",
        }, "my&nya7qp#hPBDEaYs^hifm9A%!78DK4RB6RsG8!");
    }

    [HttpGet("login")]
    public async Task<ActionResult> Login()
    {
        var result = await _signInManager.PasswordSignInAsync("e@mail.com", "my&nya7qp#hPBDEaYs^hifm9A%!78DK4RB6RsG8!", true, false);

        if (result.Succeeded is false)
        {
            return BadRequest();
        }

        return Ok();
    }

    [Authorize]
    [HttpGet("hello")]
    public async Task<ActionResult<string>> ReturnHello()
    {
        ClaimsPrincipal user = User;


        return "Hello";
    }
}
