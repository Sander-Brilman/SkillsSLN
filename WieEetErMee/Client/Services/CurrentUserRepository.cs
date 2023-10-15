using Blazored.LocalStorage;

namespace WieEetErMee.Client.Services;

public class CurrentUserRepository
{
    private const string currentUserKey = "currentUser";
    private readonly ILocalStorageService _localStorage;

    public CurrentUserRepository(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    bool isLoaded = false;

    string? currentUser;


    /// <summary>
    /// Ensures that the current user is loaded into memory.
    /// </summary>
    private async Task EnsureLoaded()
    {
        if (!isLoaded)
        {
            currentUser = await _localStorage.GetItemAsStringAsync(currentUserKey);
            isLoaded = true;
        }
    }

    /// <summary>
    /// Sets the current user.
    /// </summary>
    /// <param name="user">The user.</param>
    public async Task SetCurrentUser(string? user) 
    { 
        await EnsureLoaded();

        currentUser = user;

        if (user is null)
        {
            await _localStorage.RemoveItemAsync(currentUserKey);    
        }

        await _localStorage.SetItemAsStringAsync(currentUserKey, currentUser);
    }


    /// <summary>
    /// Gets the current user.
    /// </summary>
    /// <returns></returns>
    public async Task<string?> GetCurrentUser()
    {
        await EnsureLoaded();

        return currentUser;
    }
}


