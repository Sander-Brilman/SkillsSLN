using PersonalNotes.Shared;
using System.Net.Http.Json;
using System.Runtime.InteropServices;

namespace PersonalNotes.Client.Services;

public class RegisterUserService
{
    private readonly HttpClient _httpClient;

    public RegisterUserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Registers a user
    /// </summary>
    /// <param name="registerDTO">the object containing all the information for the new user</param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException">if the api request failed</exception>
    /// <exception cref="InvalidOperationException">if the api response could not be parsed</exception>
    public async Task<RegisterResultDTO> RegisterUserAsync(RegisterDTO registerDTO)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/user/register", registerDTO);

        if (response.IsSuccessStatusCode is false)
        {
            throw new HttpRequestException("Api request to register user failed horribly", null, response.StatusCode);
        }

        return await response.Content.ReadFromJsonAsync<RegisterResultDTO>()
            ?? throw new InvalidOperationException("Json response from api could not be parsed to RegisterResultDTO");
    }


}
