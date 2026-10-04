using System.Net.Http.Json;

namespace FakeWebStore.Services;

public record UserSession(
    int Id, string Username, string Email, string FirstName, string LastName,
    string AccessToken, string RefreshToken);

public record ApiError(string? Message);

// Talks to DummyJSON (https://dummyjson.com/docs/auth), a free public API with no key required.
public class AuthService(HttpClient http)
{
    private const string LoginUrl = "https://dummyjson.com/auth/login";

    public UserSession? Current { get; private set; }
    public bool IsSignedIn => Current is not null;
    public event Action? Changed;

    public async Task<string?> LoginAsync(string username, string password)
    {
        try
        {
            var response = await http.PostAsJsonAsync(LoginUrl,
                new { username, password, expiresInMins = 30 });

            if (response.IsSuccessStatusCode)
            {
                Current = await response.Content.ReadFromJsonAsync<UserSession>();
                Changed?.Invoke();
                return null; // null means success
            }

            var error = await response.Content.ReadFromJsonAsync<ApiError>();
            return error?.Message ?? "Sign-in failed. Try again.";
        }
        catch (HttpRequestException)
        {
            return "Can't reach the sign-in service. Check your connection and try again.";
        }
    }

    public void Logout()
    {
        Current = null;
        Changed?.Invoke();
    }
}