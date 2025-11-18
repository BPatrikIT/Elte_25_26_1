using WordGame.Model;
using WordGame.Persistence;
using WordGame.ViewModel;

namespace WordGame
{
    public partial class App : Application
    {

        #region Fields

        private IWordGamePersistence _persistence;
        private WordGameModel _model;
        private WordGameViewModel _viewModel;

        #endregion

        #region Constructors

        public App()
        {
            InitializeComponent();

            //_persistence = new Persistence.TextJSON.WordGameJSONFilePersistence();
            _persistence = new Persistence.BinaryFile.WordGameBinaryFilePersistence();
            _model = new WordGameModel(_persistence);

            _viewModel = new WordGameViewModel(_model);

            MainPage = new MainPage();
            MainPage.BindingContext = _viewModel;
        }

        #endregion

        #region App Lifecycle Methods

        protected override Window CreateWindow(IActivationState activationState)
        {
            Window window = base.CreateWindow(activationState);

            window.Created += Window_Created;
            window.Deactivated += Window_Deactivated;
            window.Destroying += Window_Destroying;

            return window;
        }

        private async void Window_Created(object sender, EventArgs e) => await _model.LoadGameAsync();
        private async void Window_Deactivated(object sender, EventArgs e) => await _model.SaveGameAsync();
        private async void Window_Destroying(object sender, EventArgs e) => await _model.SaveGameAsync();

        #endregion

    }
}