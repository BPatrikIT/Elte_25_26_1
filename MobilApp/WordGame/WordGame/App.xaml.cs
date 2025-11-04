using WordGame.Model;

namespace WordGame
{
    public partial class App : Application
    {

        private WordGameModel _model;

        public App()
        {
            InitializeComponent();

            _model = new WordGameModel();

            MainPage = new MainPage(_model);
        }
    }
}
