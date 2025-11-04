using System.Reflection;
using WordGame.Model;

namespace WordGame
{
    public partial class MainPage : ContentPage
    {
        private WordGameModel _model;
        public MainPage(WordGameModel model)
        {
            InitializeComponent();
            _model = model;

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            _model.SubmitWord(_wordEntry.Text);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _model.GameStateChanged += Model_GameStateChanged;
            _model.GameOver += Model_GameOver;
            Model_GameStateChanged(this, EventArgs.Empty);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _model.GameStateChanged -= Model_GameStateChanged;
            _model.GameOver -= Model_GameOver;
        }

        private void Model_GameOver(object? sender, EventArgs e)
        {
            ;
        }

        private void Model_GameStateChanged(object? sender, EventArgs e)
        {
            _currentWordLabel.Text = $"Current Word: {_model.CurrentWord}";
            _scoreLabel.Text = $"Score: {_model.Score}";
        }
    }

}
