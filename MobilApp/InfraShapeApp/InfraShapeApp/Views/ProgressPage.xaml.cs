using InfraShapeApp.ViewModels;
namespace InfraShapeApp.Views;
public partial class ProgressPage:ContentPage
{ public ProgressPage(ProgressViewModel vm){InitializeComponent();BindingContext=vm;} }
