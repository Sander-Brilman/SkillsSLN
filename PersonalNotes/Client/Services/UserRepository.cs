namespace PersonalNotes.Client.Services;

public class UserRepository
{
    private readonly HttpClient _httpClient;

    public UserRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task 

    public async Task CreateUser(string email, string password)
    {

    }

    public async Task<bool> LogUserIn(string email, string password)
    {


    }
}
