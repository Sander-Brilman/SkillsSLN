using System.Globalization;
using System.Net.Http.Json;
using System.Xml.Linq;
using WieEetErMee.Shared;

namespace WieEetErMee.Client.Services;

public class UserRepository
{
    private readonly HttpClient _httpClient;

    private readonly CurrentUserRepository _currentUserRepository;

    public UserRepository(HttpClient httpClient, CurrentUserRepository currentUserRepository)
    {
        _httpClient = httpClient;
        _currentUserRepository = currentUserRepository;
    }

    /// <summary>
    /// Gets all the user names.
    /// </summary>
    /// <returns>all the user names</returns>
    /// <exception cref="System.Exception">cannot fetch all users</exception>
    public async Task<List<string>> GetAllUsersNames()
    {
        return await _httpClient.GetFromJsonAsync<List<string>>("/api/user/allUserNames")
            ?? throw new Exception("cannot fetch all users");
    }


    /// <summary>
    /// Gets the user + settings for the specified username
    /// </summary>
    /// <param name="username">The username.</param>
    /// <returns>user default settings</returns>
    /// <exception cref="System.Exception">Cannot fetch settings for user</exception>
    public async Task<UserSettingsDTO> GetUser(string username)
    {
        return await _httpClient.GetFromJsonAsync<UserSettingsDTO>("/api/user/" + username)
            ?? throw new Exception("Cannot fetch settings for user " + username);
    }

    public async Task<UserSettingsDTO> GetCurrentUser()
    {
        string currentUser = await _currentUserRepository.GetCurrentUser()
            ?? throw new Exception("Cannot get the current user because the current user is not set");

        return await GetUser(currentUser);
    }



    /// <summary>
    /// Deletes the user.
    /// </summary>
    /// <param name="name">The user name.</param>
    /// <exception cref="System.Net.Http.HttpRequestException">Failed to delete user</exception>
    public async Task DeleteUser(string name)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync("/api/user/" + name);

        if (response.IsSuccessStatusCode is false)
        {
            throw new HttpRequestException("Failed to delete user" + name, null, response.StatusCode);
        }

        string? currentUser = await _currentUserRepository.GetCurrentUser();

        if (currentUser == name)
        {
            await _currentUserRepository.SetCurrentUser(null);
        }
    }

    /// <summary>
    /// Updates the users username.
    /// </summary>
    /// <param name="oldName">The old name.</param>
    /// <param name="newName">The new name.</param>
    /// <exception cref="System.Exception">Failed to update username for {oldName}</exception>
    public async Task UpdateUser(string username, UserSettingsDTO settings)
    {
        HttpResponseMessage response = await _httpClient.PutAsJsonAsync<UserSettingsDTO>("/api/user/" + username, settings);

        if (response.IsSuccessStatusCode is false)
        {
            throw new HttpRequestException($"{response.StatusCode}: Failed to update username for {username}", null, response.StatusCode);
        }

        string? currentUser = await _currentUserRepository.GetCurrentUser();

        if (currentUser == settings.Name)
        {
            await _currentUserRepository.SetCurrentUser(settings.Name);
        }
    }

    public async Task UpdateCurrentUser(UserSettingsDTO settings)
    {
        string currentUser = await _currentUserRepository.GetCurrentUser()
            ?? throw new Exception("Cannot update current user, current user is not set");

        await UpdateUser(currentUser, settings);
    }


    /// <summary>
    /// Creates a new user 
    /// </summary>
    /// <param name="settings">The settings containing the default presence settings </param>
    /// <param name="setAsCurrentUser">if set to <c>true</c> it will set the newly created user as the current user</param>
    /// <exception cref="System.Net.Http.HttpRequestException">Failed to create user</exception>
    public async Task CreateUser(UserSettingsDTO settings, bool setAsCurrentUser = false)
    {
        HttpResponseMessage httpResponse = await _httpClient.PostAsJsonAsync("/api/user/", settings);

        if (httpResponse.IsSuccessStatusCode == false)
        {
            throw new HttpRequestException($"{httpResponse.StatusCode}: Failed to create user", null, httpResponse.StatusCode);
        }

        if (setAsCurrentUser)
        {
            await _currentUserRepository.SetCurrentUser(settings.Name);
        }
    }
}
