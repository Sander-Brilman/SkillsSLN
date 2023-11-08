using Microsoft.AspNetCore.Components.Authorization;

namespace PlaygroundStandAloneWasm.Services;

public class CustomAuthProvider : AuthenticationStateProvider
{


    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
    }
}
