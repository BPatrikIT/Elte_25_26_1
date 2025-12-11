using GyorsHir.Model;
using GyorsHir.Persistence;
using GyorsHir.Persistence.TextJSON;
using GyorsHir.ViewModel;

namespace GyorsHir
{
    public partial class App : Application
    {

        #region Fields

        private MainFlyoutPage _rootPage;

        private IGyorsHirPersistence _persistence;
        private GyorsHirModel _model;
        private GyorsHirViewModel _viewModel;
        private GyorsHir.ViewModel.IShare _iShare;

        #endregion

        #region Constructors

        public App()
        {
            InitializeComponent();

            _persistence = new GyorsHirTextJSONPersistence();

            _model = new GyorsHirModel(_persistence);

            _iShare = new MAUIShare();

            _viewModel = new GyorsHirViewModel(_model, _iShare);
            _viewModel.NewsLoaded += _viewModel_NewsLoaded;
            _viewModel.NewsItemSelected += _viewModel_NewsItemSelected;

            _rootPage = new MainFlyoutPage();
            _rootPage.BindingContext = _viewModel;
            MainPage = _rootPage;
        }

        #endregion

        #region App Lifecycle Methods

        protected override Window CreateWindow(IActivationState activationState)
        {
            Window window = base.CreateWindow(activationState);

            window.Created += Window_Created;

            return window;
        }

        private async void Window_Created(object sender, EventArgs e)
        {
            await _model.InitializeAsync();
        }

        #endregion

        #region ViewModel Event Handlers

        private async void _viewModel_NewsLoaded(object sender, EventArgs e)
        {
            if (!(_rootPage.NavigationPage.CurrentPage is PortalNewsPage))
                await _rootPage.NavigationPage.PushAsync(new PortalNewsPage());
        }
        private async void _viewModel_NewsItemSelected(object sender, EventArgs e)
        {
            if (!(_rootPage.NavigationPage.CurrentPage is NewsDetailsPage))
                await _rootPage.NavigationPage.PushAsync(new NewsDetailsPage());
        }

        #endregion

    }
}