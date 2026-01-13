using InfraShapeApp.ViewModels;
namespace InfraShapeApp.Views;
public partial class DataEntryPage:ContentPage
{ public DataEntryPage(DataEntryViewModel vm){InitializeComponent();BindingContext=vm;} }
