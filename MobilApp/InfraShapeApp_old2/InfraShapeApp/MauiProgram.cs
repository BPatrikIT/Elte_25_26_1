using InfraShapeApp.Services;
using InfraShapeApp.ViewModels;
using InfraShapeApp.Views;

namespace InfraShapeApp;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();

        builder.Services.AddSingleton<IUserService, MockUserService>();
        builder.Services.AddSingleton<IPassService, MockPassService>();
        builder.Services.AddSingleton<IProgressService, MockProgressService>();
        builder.Services.AddSingleton<IBookingService, MockBookingService>();

        builder.Services.AddSingleton<ProfileViewModel>();
        builder.Services.AddSingleton<PassesViewModel>();
        builder.Services.AddSingleton<ProgressViewModel>();
        builder.Services.AddSingleton<DataEntryViewModel>();
        builder.Services.AddSingleton<BookingsViewModel>();
        builder.Services.AddSingleton<MachinesViewModel>();

        builder.Services.AddSingleton<ProfilePage>();
        builder.Services.AddSingleton<PassesPage>();
        builder.Services.AddSingleton<ProgressPage>();
        builder.Services.AddSingleton<DataEntryPage>();
        builder.Services.AddSingleton<BookingsPage>();
        builder.Services.AddSingleton<MachinesPage>();

        builder.Services.AddSingleton<AppShell>();
        return builder.Build();
    }
}
