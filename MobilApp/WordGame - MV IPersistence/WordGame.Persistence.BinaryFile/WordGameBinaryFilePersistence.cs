using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace WordGame.Persistence.BinaryFile
{
    public class WordGameBinaryFilePersistence : IWordGamePersistence
    {
        public async Task SaveGameStateAsync(WordGameState state)
        {
            try
            {
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "save.dat");
                
                await Task.Run(() =>
                {
                    using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        bw.Write(state.CurrentWord ?? "");
                        bw.Write(state.Score);
                        if (state.UsedWords != null)
                        {
                            bw.Write(state.UsedWords.Count);
                            foreach (string usedWord in state.UsedWords)
                                bw.Write(usedWord);
                        }
                        else bw.Write(0);
                    }
                });
            }
            catch { }
        }

        public async Task<WordGameState?> LoadGameStateAsync()
        {
            try
            {
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "save.dat");
                WordGameState? state = null;
                await Task.Run(() =>
                {
                    if (File.Exists(fileName))
                    {
                        using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            state = new WordGameState();
                            state.CurrentWord = br.ReadString();
                            state.Score = br.ReadUInt32();

                            int usedWordCount = br.ReadInt32();
                            List<string> usedWords = new List<string>(usedWordCount);
                            for (int i = 0; i < usedWordCount; i++)
                                usedWords.Add(br.ReadString());
                            state.UsedWords = usedWords;
                        }
                    }
                });
                return state;
            }
            catch
            {
                return null;
            }
        }
    }
}
