using System.Net.Http.Json;

namespace BookStore.Client.Services;

public class BookRespository
{
    private readonly HttpClient _httpClient;

    public BookRespository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Gets all the books from the API
    /// </summary>
    /// <returns>a list of all the books</returns>
    public async Task<List<BookDTO>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<BookDTO>>("/api/book/");
    }

    /// <summary>
    /// Gets a single book based on its id
    /// </summary>
    /// <param name="id">the ID of the book</param>
    /// <returns>the book with the specified id</returns>
    public async Task<BookDTO> GetSingleAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<BookDTO>("/api/book/" + id);
    }

    /// <summary>
    /// Created a new book and returns the result
    /// 
    /// during the creating of the book the ID propperty will be ignored
    /// </summary>
    /// <param name="book">the book to be created</param>
    /// <returns>the new book with the ID propperty set to its accurate value</returns>
    public async Task<BookDTO> CreateAsync(BookDTO book)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/book/" + book.Id, book);

        return await response.Content.ReadFromJsonAsync<BookDTO>();
    }

    /// <summary>
    /// Deletes a book
    /// </summary>
    /// <param name="book">The book to be deleted</param>
    /// <returns></returns>
    public async Task DeleteAsync(BookDTO book)
    {
        await _httpClient.DeleteAsync("/api/book/" + book.Id);
    }

    /// <summary>
    /// Updates a book 
    /// </summary>
    /// <param name="book">the book to be updated</param>
    /// <returns></returns>
    public async Task UpdateAsync(BookDTO book)
    {
        await _httpClient.PutAsJsonAsync("/api/book/" + book.Id, book);  
    }

}
