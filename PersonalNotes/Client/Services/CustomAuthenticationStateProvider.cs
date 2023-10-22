using Microsoft.AspNetCore.Components.Authorization;
using PersonalNotes.Shared;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;

namespace PersonalNotes.Client.Services;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;

    public CustomAuthenticationStateProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    private bool isLoaded = false;

    private ClaimsPrincipal _user = new(new ClaimsIdentity());


    private void SetUserToLoggedIn(string username)
    {
        Claim[] claims =
        {
            new Claim(ClaimTypes.Name, username)
        };

        ClaimsIdentity identity = new(claims, "Application");
        _user = new ClaimsPrincipal(identity);
    }


    public async Task<LoginResultDTO> LoginAsync(LoginDTO loginDTO)
    {
        var result = await _httpClient.PostAsJsonAsync("/api/user/login", loginDTO);

        if (result.IsSuccessStatusCode is false)
        {
            throw new HttpRequestException("login endpoint failed", null, result.StatusCode);
        }

        LoginResultDTO loginResult = await result.Content.ReadFromJsonAsync<LoginResultDTO>()
            ?? throw new InvalidOperationException("login api endpoint did not return a complete LoginResultDTO");


        // authenticate user
        if (loginResult.Success is true)
        {
            SetUserToLoggedIn(loginDTO.UserName);



            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }


        return loginResult;
    }

    /// <summary>
    /// Logs the current user out and sets it back to anomynous
    /// </summary>
    /// <returns></returns>
    /// <exception cref="HttpRequestException">If the logging out process failed</exception>
    public async Task Logout()
    {
        var response = await _httpClient.PostAsync("/api/user/logout", null);

        if (response.IsSuccessStatusCode == false)
        {
            throw new HttpRequestException("Failed to log out, api returned an error");
        }

        _user = new(new ClaimsIdentity());

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    /// <summary>
    /// Gets the current authentication state.
    /// </summary>
    /// <returns>the current AuthenticationState</returns>
    /// <exception cref="HttpRequestException">when the api request failed</exception>
    /// <exception cref="InvalidOperationException">when the json that was returned from the api could not be parsed</exception>
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (isLoaded is false)
        {
            var response = await _httpClient.GetAsync("/api/user/tryGetUsername");

            if (response.IsSuccessStatusCode is false)
            {
                throw new HttpRequestException("failed to check if the user is logged in", null, response.StatusCode);
            }

            TryGetUsernameResultDTO result = await response.Content.ReadFromJsonAsync<TryGetUsernameResultDTO>()
                ?? throw new InvalidOperationException("failed to parse api result into TryGetUsernameResultDTO");

            if (result.IsLoggedIn)
            {
                SetUserToLoggedIn(result.Username);
            }

            isLoaded = true;
        }

        return new AuthenticationState(_user);
    }
}
