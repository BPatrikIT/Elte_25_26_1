using InfraShapeApp.ViewModels;
namespace InfraShapeApp.Views;
public partial class MachinesPage:ContentPage
{ public MachinesPage(MachinesViewModel vm){InitializeComponent();BindingContext=vm;} }
