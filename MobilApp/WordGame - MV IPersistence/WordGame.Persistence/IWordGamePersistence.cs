using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WordGame.Persistence
{
    public interface IWordGamePersistence
    {
        Task SaveGameStateAsync(WordGameState state);
        Task<WordGameState?> LoadGameStateAsync();
    }
}
