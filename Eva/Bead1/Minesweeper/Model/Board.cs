using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Model
{
    internal class Board
    {
        private Cell[,] cells;
        private int size;
        private int mineCount;
        private int countOfUnRevealedCells;
        public Board(int size, int mineCount)
        {
            this.size = size;
            this.mineCount = mineCount;
            cells = new Cell[size, size];
            countOfUnRevealedCells = size * size;
            InitializeBoard();
        }

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        //getes setters

        public Cell[,] Cells
        {
            get { return cells; }
            set { cells = value; }
        }

        public int CounterOfUnRevealedCells
        {
            get { return countOfUnRevealedCells; }
            set { countOfUnRevealedCells = value; }
        }

        public int MineCount
        {
            get { return mineCount; }
            set { mineCount = value; }
        }

        private void InitializeBoard()
        {
            // Initialize cells
            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    cells[r, c] = new Cell();
                }
            }
            // Place mines
            PlaceMines();
        }
        private void PlaceMines()
        {
            for (int i = 0; i < mineCount; i++)
            {
                Random rand = new Random();
                int r, c;
                do
                {
                    r = rand.Next(size);
                    c = rand.Next(size);
                } while (cells[r, c].IsMine);
                cells[r, c].IsMine = true;
                // After placing a mine, update neighboring mine counts
                CalculateNeighboringMines(r, c);
            }
        }
        private void CalculateNeighboringMines(int r, int c)
        {
            for (int rOffset = -1; rOffset <= 1; rOffset++)
            {
                for (int cOffset = -1; cOffset <= 1; cOffset++)
                {
                    int newRow = r + rOffset;
                    int newCol = c + cOffset;
                    if (newRow >= 0 && newRow < size && newCol >= 0 && newCol < size && !(rOffset == 0 && cOffset == 0))
                    {
                        cells[newRow, newCol].NeighboringMines++;
                    }
                }
            }
        }
    }
}
