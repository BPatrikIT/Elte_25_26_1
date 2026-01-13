
namespace InfraShapeApp.Models;

public class UserProfile
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string ProfileImagePath { get; set; } = string.Empty;
}
