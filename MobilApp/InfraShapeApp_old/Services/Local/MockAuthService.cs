
using InfraShapeApp.Models;
using InfraShapeApp.Services.Interfaces;
using System.Threading.Tasks;


namespace InfraShapeApp.Services.Local;

public class MockAuthService : IAuthService
{
    private AuthState _state = new();

    public Task<AuthState> LoginAsync(string email, string password)
    {
        _state = new AuthState
        {
            IsLoggedIn = true,
            UserId = Guid.NewGuid().ToString(),
            Token = "MOCK_TOKEN"
        };
        return Task.FromResult(_state);
    }

    public Task LogoutAsync()
    {
        _state = new AuthState();
        return Task.CompletedTask;
    }

    public Task<AuthState> GetAuthStateAsync() => Task.FromResult(_state);
}
