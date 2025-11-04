using System.Reflection;
using System.Text.Json.Serialization;

namespace WordGame.Model
{
    public class WordGameModel
    {
        private List<string> _words = new List<string>();
        private List<string> _usedWords = new List<string>();

        public string CurrentWord
        {
            get; private set;
        }

        public int Score
        {
            get; private set;
        }

        public event EventHandler? GameStateChanged;

        public event EventHandler? GameOver;

        public WordGameModel()
        {
            Assembly assembly = IntrospectionExtensions.GetTypeInfo(typeof(WordGameModel)).Assembly;
            using Stream? stream = assembly.GetManifestResourceStream("WordGame.Model.Data.wordshu.txt");
            using StreamReader reader = new StreamReader(stream);
            while (!reader.EndOfStream)
                _words.Add(reader.ReadLine());

            NewGame();
        }

        private void NewGame()
        {
            Random rnd = new Random();
            string startWord = _words[rnd.Next(_words.Count)];

            CurrentWord = startWord;
            Score = 0;

            GameStateChanged?.Invoke(this, EventArgs.Empty);

            _usedWords.Clear();
            _usedWords.Add(startWord);
        }

        public void SubmitWord(string word)
        {
            if (!string.IsNullOrWhiteSpace(word))
            {
                word = word.Trim().ToLower();

                if (word.First() == word.Last() &&
                    _words.Contains(word))
                {
                    if (_usedWords.Contains(word))
                    {
                        GameOver?.Invoke(this, EventArgs.Empty);
                        //await DisplayAlert("Game Over!", $"Score: {_usedWords.Count - 1}", "Yea");
                        NewGame(); //RIP
                    }
                    else
                    {
                        _usedWords.Add(word);
                        CurrentWord = word;
                        Score = _usedWords.Count - 1;
                        GameStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

    }
}
