using System.Data;
using System.Net.Http.Json;
using WieEetErMee.Shared;

namespace WieEetErMee.Client.Services;

public class DayPresenceRepository
{
    private readonly HttpClient _httpClient;

    public DayPresenceRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    /// <summary>
    /// Gets the total presence for a specified date.
    /// </summary>
    /// <param name="date">The date.</param>
    /// <returns></returns>
    /// <exception cref="System.Exception">Failed get DayPresense from Date {date}</exception>
    public async Task<DayPresenceDTO> GetPresenceForDate(DateOnly date)
    {
        HttpResponseMessage response = await _httpClient.GetAsync("/api/daypresence/" + date.ToLongDateString());

        if (response.IsSuccessStatusCode is false)
        {
            throw new Exception($"Failed get DayPresense from Date {date}");
        }

        return await response.Content.ReadFromJsonAsync<DayPresenceDTO>() 
            ?? throw new Exception($"Failed get DayPresense from Date {date}");
    }
}
