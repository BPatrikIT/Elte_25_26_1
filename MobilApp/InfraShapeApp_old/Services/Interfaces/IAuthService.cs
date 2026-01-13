
using InfraShapeApp.Models;
using System.Threading.Tasks;


namespace InfraShapeApp.Services.Interfaces;

public interface IAuthService
{
    Task<AuthState> LoginAsync(string email, string password);
    Task LogoutAsync();
    Task<AuthState> GetAuthStateAsync();
}
