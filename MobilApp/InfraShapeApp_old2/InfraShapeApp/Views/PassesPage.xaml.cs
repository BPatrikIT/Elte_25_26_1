using InfraShapeApp.ViewModels;
namespace InfraShapeApp.Views;
public partial class PassesPage:ContentPage
{ public PassesPage(PassesViewModel vm){InitializeComponent();BindingContext=vm;} }
