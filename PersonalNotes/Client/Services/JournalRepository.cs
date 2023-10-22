using PersonalNotes.Shared;
using System.Net.Http.Json;

namespace PersonalNotes.Client.Services;

public class JournalRepository
{
    private readonly HttpClient _httpClient;

    public JournalRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Gets all journal previews for current logged in user
    /// </summary>
    /// <returns>
    ///     a list of all the journal previews for current logged in user
    /// </returns>
    /// <exception cref="HttpRequestException">If the call to the api failed</exception>
    /// <exception cref="InvalidOperationException">If the json response could not be parsed </exception>
    public async Task<List<JournalPreviewDTO>> GetAllJournalsPreviewsAsync()
    {
        var response = await _httpClient.GetAsync("/api/journal/");

        if (response.IsSuccessStatusCode is false)
        {
            throw new HttpRequestException("Failed to get journal previews", null, response.StatusCode);
        }

        return await response.Content.ReadFromJsonAsync<List<JournalPreviewDTO>>()
            ?? throw new InvalidOperationException("failed to parse api response into a journal preview list");
    }

    /// <summary>
    /// Gets the full journal for the specified id
    /// </summary>
    /// <param name="id">the id of the journal</param>
    /// <returns>the full journal</returns>
    /// <exception cref="HttpRequestException">If the call to the api failed</exception>
    /// <exception cref="InvalidOperationException">If the json response could not be parsed</exception>
    public async Task<JournalDTO> GetSingleJournal(int id)
    {
        var response = await _httpClient.GetAsync($"/api/journal/{id}");

        if (response.IsSuccessStatusCode == false)
        {
            throw new HttpRequestException("failed to get single journal with id " + id, null, response.StatusCode);
        }

        return await response.Content.ReadFromJsonAsync<JournalDTO>()
            ?? throw new InvalidOperationException("failed to parse api response into a journal object");
    }

    /// <summary>
    /// Creates and returns a new journal
    /// </summary>
    /// <param name="journal">The information for the new journal. the id propperty will be ignored</param>
    /// <returns>the newly created journal</returns>
    /// <exception cref="HttpRequestException">If the call to the api failed</exception>
    /// <exception cref="InvalidOperationException">If the json response could not be parsed</exception>
    public async Task<JournalDTO> CreateNewJournalAsync(JournalDTO journal)
    {
        journal.Id = 0;

        var response = await _httpClient.PostAsJsonAsync("/api/journal/", journal);

        if (response.IsSuccessStatusCode == false)
        {
            throw new HttpRequestException("failed to create new journal", null, response.StatusCode);
        }

        return await response.Content.ReadFromJsonAsync<JournalDTO>()
            ?? throw new InvalidOperationException("api did not return the newly created journal");
    }

    /// <summary>
    /// Updates all fields of a journal
    /// </summary>
    /// <param name="journal">The journal object containing all the new information</param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException">if the call to the api failed</exception>
    public async Task UpdateJournalAsync(JournalDTO journal)
    {
        var response = await _httpClient.PutAsJsonAsync("/api/journal/" + journal.Id, journal);

        if (response.IsSuccessStatusCode == false)
        {
            throw new HttpRequestException("failed to update journal", null, response.StatusCode);
        }
    }

    /// <summary>
    /// Deletes a specified journal
    /// </summary>
    /// <param name="id">the id of the journal to be deleted</param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException">if the call to the api failed</exception>
    public async Task DeleteJournalAsync(int id)
    {
        var response = await _httpClient.DeleteAsync("/api/journal/" + id);

        if (response.IsSuccessStatusCode == false)
        {
            throw new HttpRequestException("failed to delete journal", null, response.StatusCode);
        }
    }
}
