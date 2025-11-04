using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Model
{
    internal class Cell
    {
        private bool isMine = false;
        private bool isRevealed =false;
        private int neighboringMines = 0;

        public bool IsMine
        {
            get { return isMine; }
            set { isMine = value; }
        }

        public bool IsRevealed
        {
            get { return isRevealed; }
            set { isRevealed = value; }
        }

        public int NeighboringMines
        {
            get { return neighboringMines; }
            set { neighboringMines = value; }
        }
    }
}
