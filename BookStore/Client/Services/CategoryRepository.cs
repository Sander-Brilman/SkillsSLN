using System.Net.Http.Json;

namespace BookStore.Client.Services;

public class CategoryRepository
{
    private readonly HttpClient _httpClient;

    public CategoryRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Gets all the categories from the api
    /// </summary>
    /// <returns>A list of all the categories</returns>
    public async Task<List<CategoryDTO>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<CategoryDTO>>("/api/category");
    }

    /// <summary>
    /// Gets a single category based on its ID
    /// </summary>
    /// <param name="id">the ID of the category</param>
    /// <returns>The category with the matching id</returns>
    public async Task<CategoryDTO> GetSingleAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<CategoryDTO>("/api/category/" + id);
    }

    /// <summary>
    /// Creates a new category and returns the newly created category.
    /// 
    /// the ID propperty will be ignored.
    /// </summary>
    /// <param name="newCategory">the new category to be created</param>
    /// <returns>the newly created category with its appropirate ID set</returns>
    public async Task<CategoryDTO> CreateNewAsync(CategoryDTO newCategory)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/category/", newCategory);

        return await response.Content.ReadFromJsonAsync<CategoryDTO>();
    }

    /// <summary>
    /// updates the category
    /// </summary>
    /// <param name="category">the category to be updated</param>
    /// <returns></returns>
    public async Task UpdateAsync(CategoryDTO category)
    {
        await _httpClient.PutAsJsonAsync("/api/category/" + category.Id, category);
    }

    /// <summary>
    /// Deletes the specified category
    /// </summary>
    /// <param name="category">the category that is to be deleted</param>
    /// <returns></returns>
    public async Task DeleteAsync(CategoryDTO category)
    {
        await _httpClient.DeleteAsync("/api/category/" + category.Id);
    }
}
