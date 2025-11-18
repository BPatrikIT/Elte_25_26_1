using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WordGame.Persistence.TextJSON
{
    public class WordGameJSONFilePersistence : IWordGamePersistence
    {

        public async Task SaveGameStateAsync(WordGameState state)
        {
            try
            {
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "save.dat");
                string json = JsonConvert.SerializeObject(state);
                await File.WriteAllTextAsync(fileName, json);
            }
            catch { }
        }

        public async Task<WordGameState?> LoadGameStateAsync()
        {
            try
            {
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "save.dat");
                WordGameState? state = null;
                if (File.Exists(fileName))
                {
                    string json = await File.ReadAllTextAsync(fileName);
                    state = JsonConvert.DeserializeObject<WordGameState>(json);
                }
                return state;
            }
            catch
            {
                return null;
            }
        }

    }
}
