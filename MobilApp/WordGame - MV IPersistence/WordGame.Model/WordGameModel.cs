using System.Reflection;
using WordGame.Persistence;

namespace WordGame.Model
{
    public class WordGameModel
    {

        #region Fields

        private IWordGamePersistence? _persistence;

        private List<string> _words = new List<string>();
        private List<string> _usedWords = new List<string>();

        #endregion

        #region Properties

        public string? CurrentWord { get; private set; }
        public uint Score { get; private set; }

        #endregion

        #region Events

        public event EventHandler? IncorrectWord;
        public event EventHandler? GameStateChanged;
        public event EventHandler? GameOver;

        #endregion

        #region Constructors

        public WordGameModel(IWordGamePersistence? persistence)
        {
            _persistence = persistence;

            Assembly assembly = IntrospectionExtensions.GetTypeInfo(typeof(WordGameModel)).Assembly;
            using (Stream? stream = assembly.GetManifestResourceStream("WordGame.Model.Data.wordshu.txt"))
            using (StreamReader sr = new StreamReader(stream))
            {
                while (!sr.EndOfStream)
                    _words.Add(sr.ReadLine());
            }

            NewGame();
        }

        #endregion

        #region Private Methods

        private void OnIncorrectWord()
            => IncorrectWord?.Invoke(this, EventArgs.Empty);
        private void OnGameStateChanged()
            => GameStateChanged?.Invoke(this, EventArgs.Empty);
        private void OnGameOver()
            => GameOver?.Invoke(this, EventArgs.Empty);

        #endregion

        #region Public Methods

        public void NewGame()
        {
            Random rnd = new Random();
            string startWord = _words[rnd.Next(_words.Count)];
            CurrentWord = startWord;

            _usedWords.Clear();
            _usedWords.Add(startWord);

            Score = 0;

            OnGameStateChanged();
        }

        public void SubmitWord(string word)
        {
            if (!string.IsNullOrWhiteSpace(word) && !string.IsNullOrWhiteSpace(CurrentWord))
            {
                word = word.Trim().ToLower();
                if (CurrentWord.Last() == word.First() && _words.Contains(word))
                {
                    if (_usedWords.Contains(word))
                    {
                        //Game Over
                        OnGameOver();
                        NewGame();
                    }
                    else
                    {
                        CurrentWord = word;
                        _usedWords.Add(word);
                        ++Score;
                        OnGameStateChanged();
                    }
                }
                else OnIncorrectWord();
            }
        }

        public async Task SaveGameAsync()
        {
            if (_persistence != null)
                await _persistence.SaveGameStateAsync(new WordGameState()
                {
                    UsedWords = _usedWords,
                    CurrentWord = CurrentWord,
                    Score = Score
                });
        }

        public async Task LoadGameAsync()
        {
            if (_persistence != null)
            {
                WordGameState? state = await _persistence.LoadGameStateAsync();
                if (state != null)
                {
                    if (state.UsedWords != null)
                        _usedWords = new List<string>(state.UsedWords);
                    CurrentWord = state.CurrentWord;
                    Score = state.Score;
                    OnGameStateChanged();
                }
            }
        }

        #endregion

    }
}