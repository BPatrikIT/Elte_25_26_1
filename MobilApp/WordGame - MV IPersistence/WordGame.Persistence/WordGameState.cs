using System;
using System.Collections.Generic;
using System.Text;

namespace WordGame.Persistence
{
    public class WordGameState
    {

        public IReadOnlyList<string>? UsedWords { get; set; }
        public string? CurrentWord { get; set; }
        public uint Score { get; set; }

    }
}
