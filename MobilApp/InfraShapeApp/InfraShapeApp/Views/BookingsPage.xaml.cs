using InfraShapeApp.ViewModels;
namespace InfraShapeApp.Views;
public partial class BookingsPage:ContentPage
{ public BookingsPage(BookingsViewModel vm){InitializeComponent();BindingContext=vm;} }
