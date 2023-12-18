using Microsoft.AspNetCore.Components.Authorization;

namespace PlaygroundStandAloneWasm.Services;

public class AppAuthProvider : AuthenticationStateProvider
{

    private AuthenticationState _authenticationState;


    bool hasTriedLogginIn = false;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return _authenticationState;
    }
}
