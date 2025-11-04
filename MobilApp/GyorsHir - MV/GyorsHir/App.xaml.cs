using GyorsHir.Model;

namespace GyorsHir
{
    public partial class App : Application
    {

        #region Fields

        private NavigationPage _rootPage;

        private GyorsHirModel _model;

        #endregion

        #region Constructors

        public App()
        {
            InitializeComponent();

            _model = new GyorsHirModel();
            _model.NewsChannelLoaded += _model_NewsChannelLoaded;

            _rootPage = new NavigationPage(new MainPage(_model));
            MainPage = _rootPage;
        }

        #endregion

        #region Model Event Handlers

        private async void _model_NewsChannelLoaded(object sender, EventArgs e)
            => await _rootPage.Navigation.PushAsync(new PortalNewsPage(_model));

        #endregion

    }
}