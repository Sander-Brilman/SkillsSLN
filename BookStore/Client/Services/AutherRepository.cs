global using BookStore.Shared;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BookStore.Client.Services;

public class AutherRepository
{
    private readonly HttpClient _httpClient;

    public AutherRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Gets all the authors from the api
    /// </summary>
    /// <returns>a list of all the authors</returns>
    /// <exception cref="InvalidProgramException">api did not return all the authors</exception>
    public async Task<List<AuthorDTO>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<AuthorDTO>>("/api/author")
            ?? throw new InvalidProgramException("api did not return all the authors");
    }

    /// <summary>
    /// gets a single author based on its ID
    /// </summary>
    /// <param name="id">the id of the author to be fetched</param>
    /// <returns>the author</returns>
    /// <exception cref="InvalidProgramException">api did not the requested author</exception>
    public async Task<AuthorDTO> GetSingleAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<AuthorDTO>("/api/author/" + id)
            ?? throw new InvalidProgramException("api did not the requested author");

    }

    /// <summary>
    /// Creates a new author and returns the newly created author.
    /// 
    /// the ID propperty will be ignored.
    /// </summary>
    /// <param name="author">the new author to be created</param>
    /// <returns>the newly created author with its appropirate ID set</returns>
    public async Task<AuthorDTO> CreateAuthorAsync(AuthorDTO author)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/author", author);

        return await response.Content.ReadFromJsonAsync<AuthorDTO>()
            ?? throw new InvalidProgramException("api did not the newly created author");
    }

    /// <summary>
    /// Updates all the author's propperties
    /// </summary>
    /// <param name="author">the author to be updates</param>
    /// <returns></returns>
    public async Task UpdateAsync(AuthorDTO author)
    {
        await _httpClient.PutAsJsonAsync("/api/author/" + author.Id, author);
    }



    /// <summary>
    /// Deletes the specified author
    /// </summary>
    /// <param name="author">the author to be deleted</param>
    /// <returns></returns>
    public async Task DeleteAsync(AuthorDTO author)
    {
        await _httpClient.DeleteAsync("/api/author/" +  author.Id);
    }


}
