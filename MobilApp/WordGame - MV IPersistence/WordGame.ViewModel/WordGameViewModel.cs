using WordGame.Model;

namespace WordGame.ViewModel
{
    public class WordGameViewModel : BindingSource
    {
        #region Fields
        private Model.WordGameModel _model;

        private string _currentWord = "Test Text";
        private int _score = 0;

        #endregion

        #region Properties

        public string UserWord { get; set; }

        public string CurrentWord
        {
            get { return _currentWord; }
            private set 
            {
                if (value != _currentWord)
                {
                    _currentWord = value;
                    OnPropertyChanged();
                }
            }
        }

        public DelegateCommand AcceptWordCommand { get; private set; }

        public int CurrentScore
        {
            get { return _score; }
            private set
            {
                if (value != _score)
                {
                    _score = value;
                    OnPropertyChanged();
                }
            }
        }


        #endregion

        #region Constructors

        public WordGameViewModel(Model.WordGameModel model)
        {
            _model = model;
            _model_GameStateChanged(this, EventArgs.Empty);

            _model.GameStateChanged += _model_GameStateChanged;

            AcceptWordCommand = new DelegateCommand(p => _model.SubmitWord(UserWord));
        }

        #endregion

        #region Model Event Handlers

        private void _model_GameStateChanged(object? sender, System.EventArgs e)
        {
            CurrentWord = _model.CurrentWord;
            CurrentScore = Convert.ToInt32(_model.Score);
        }

        #endregion
    }
}
