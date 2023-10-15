using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Web;
using WieEetErMee.Client.Pages;
using WieEetErMee.Shared;

namespace WieEetErMee.Client.Services;

public class UserPresenceRepository
{
    private readonly HttpClient _httpClient;
    private readonly CurrentUserRepository _currentUserRepository;

    public UserPresenceRepository(HttpClient httpClient, CurrentUserRepository currentUserRepository)
    {
        _httpClient = httpClient;
        _currentUserRepository = currentUserRepository;
    }


    /// <summary>
    /// Gets the presence for user for a specific date
    /// </summary>
    /// <param name="date">The date.</param>
    /// <param name="user">The username.</param>
    /// <returns></returns>
    /// <exception cref="System.Exception">
    /// Failed get the DayPresense from Date for user 
    /// or
    /// Presense for user not found
    /// </exception>
    public async Task<UserPresenceDTO> GetPresenceForUser(DateOnly date, string user)
    {
        HttpResponseMessage httpResponse = await _httpClient.GetAsync($"/api/userpresence/{user}?date=" + HttpUtility.UrlEncode(date.ToLongDateString())); 

        if (httpResponse.IsSuccessStatusCode is false) {
            throw new Exception($"Failed get the DayPresense from Date {date} for user {user}");
        }

        return await httpResponse.Content.ReadFromJsonAsync<UserPresenceDTO>()
            ?? throw new Exception($"Presense for user {user} on date {date} not found");
    }


    /// <summary>
    /// Gets the presence for current user for a specific date
    /// </summary>
    /// <param name="date">The date.</param>
    /// <returns></returns>
    /// <exception cref="System.InvalidOperationException">Cannot get user presence for current user because current user is not set</exception>
    public async Task<UserPresenceDTO> GetPresenceForCurrentUser(DateOnly date)
    {
        string currentUser = await _currentUserRepository.GetCurrentUser()
            ?? throw new InvalidOperationException("Cannot get user presence for current user because current user is not set");

        return await GetPresenceForUser(date, currentUser);
    }



    /// <summary>
    /// Sets the presence for the specified user.
    /// </summary>
    /// <param name="date">The date.</param>
    /// <param name="userPresence">The user presence.</param>
    /// <exception cref="System.Exception">Failed set DayPresense from Date for the user</exception>
    public async Task SetPresenceForUser(DateOnly date, UserPresenceDTO userPresence)
    {
        HttpResponseMessage response = await _httpClient.PutAsJsonAsync("/api/userpresence/" + userPresence.Name, userPresence);

        if (response.IsSuccessStatusCode == false)
        {
            throw new Exception($"Failed set DayPresense from Date {date} for user {userPresence.Name}");
        }
    }
}
