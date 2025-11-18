using GyorsHir.Model;
using GyorsHir.ViewModel;

namespace GyorsHir
{
    public partial class App : Application
    {

        #region Fields

        private NavigationPage _rootPage;

        private GyorsHirModel _model;
        private GyorsHirViewModel _viewModel;

        #endregion

        #region Constructors

        public App()
        {
            InitializeComponent();

            _model = new GyorsHirModel();

            _viewModel = new GyorsHirViewModel(_model);
            _viewModel.NewsLoaded += _viewModel_NewsLoaded;
            _viewModel.NewsItemSelected += _viewModel_NewsItemSelected;

            _rootPage = new NavigationPage(new MainPage());
            _rootPage.BindingContext = _viewModel;
            MainPage = _rootPage;
        }

        #endregion

        #region ViewModel Event Handlers

        private async void _viewModel_NewsLoaded(object sender, EventArgs e)
        {
            if (!(_rootPage.CurrentPage is PortalNewsPage))
                await _rootPage.Navigation.PushAsync(new PortalNewsPage());
        }
        private async void _viewModel_NewsItemSelected(object sender, EventArgs e)
        {
            if (!(_rootPage.CurrentPage is NewsDetailsPage))
                await _rootPage.Navigation.PushAsync(new NewsDetailsPage());
        }

        #endregion

    }
}