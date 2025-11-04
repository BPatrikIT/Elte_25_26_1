using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Model
{
    internal class Game
    {
        private Player[] players = new Player[2];
        private Board board;
        private int currentPlayerIndex = 0;
        private enum GameState
        {
            Ongoing,
            Player1_Won,
            Player2_Won,
            Draw
        }
        private GameState state;

        public Game(String[] players, Board board)
        {
            Player player1 = new Player();
            player1.UserName = players[0];
            Player player2 = new Player();
            player2.UserName = players[1];
            this.players[0] = player1;
            this.players[1] = player2;

            state = GameState.Ongoing;

            this.board = board;
        }

        public Board GetBoard()
        {
            return board;
        }

        private void RevealCell(int r, int c)
        {
            // Prevent out-of-bounds or duplicate reveals
            if (r < 0 || r >= board.Size || c < 0 || c >= board.Size)
                return;

            var cell = board.Cells[r, c];
            if (cell.IsRevealed)
                return;

            cell.IsRevealed = true;
            board.CounterOfUnRevealedCells--;

            if (cell.IsMine)
            {
                switch (currentPlayerIndex)
                {
                    case 0:
                        state = GameState.Player2_Won;
                        break;
                    case 1:
                        state = GameState.Player1_Won;
                        break;
                }
                return;
            }

            // Check for win condition
            if (CheckGameOver())
                return;

            // If no neighboring mines, reveal neighbors
            if (cell.NeighboringMines == 0)
            {
                for (int rOffset = -1; rOffset <= 1; rOffset++)
                {
                    for (int cOffset = -1; cOffset <= 1; cOffset++)
                    {
                        if (rOffset == 0 && cOffset == 0)
                            continue; // Skip the current cell

                        int newRow = r + rOffset;
                        int newCol = c + cOffset;

                        // Recursive reveal, safe-checked at the top
                        RevealCell(newRow, newCol);
                    }
                }
            }
        }

        public bool Reveal(int row, int col)
        {
            if (state != GameState.Ongoing || board.Cells[row, col].IsRevealed)
                return false;

            RevealCell(row, col);
            NextTurn();
            return true;
        }

        private void NextTurn()
        {
            //Switch to the next player
            currentPlayerIndex = (currentPlayerIndex + 1) % 2;
        }

        private bool CheckGameOver()
        {
            if (board.CounterOfUnRevealedCells <= board.MineCount)
            {
                switch (currentPlayerIndex)
                {
                    case 0:
                        state = GameState.Player1_Won;
                        break;
                    case 1:
                        state = GameState.Player2_Won;
                        break;
                }
                return true;
            }
            return false;
        }
        private void Save()
        {

        }

        private void Load()
        {

        }
    }
}
