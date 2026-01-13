
namespace InfraShapeApp.Models;

public class AuthState
{
    public bool IsLoggedIn { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}
