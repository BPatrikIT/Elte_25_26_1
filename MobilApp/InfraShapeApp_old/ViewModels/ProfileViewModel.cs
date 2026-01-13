
using InfraShapeApp.Models;
using InfraShapeApp.Services.Interfaces;
using Microsoft.Maui.Controls;


namespace InfraShapeApp.ViewModels;

public class ProfileViewModel : BaseViewModel
{
    private readonly IAuthService _authService;

    public UserProfile Profile { get; set; } = new();

    public Command LoginCommand { get; }

    public ProfileViewModel(IAuthService authService)
    {
        _authService = authService;
        LoginCommand = new Command(async () =>
        {
            await _authService.LoginAsync(Profile.Email, "password");
        });
    }
}
