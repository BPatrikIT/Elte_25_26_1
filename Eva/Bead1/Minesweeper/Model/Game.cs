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

        private void RevealCell(int r, int c)
        {
            board.Cells[r, c].IsRevealed = true;
            board.CounterOfUnRevealedCells--;
            if (board.Cells[r, c].IsMine)
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
            if (CheckGameOver())
            {
                return;
            }
            //If the revealed cell has 0 neighboring mines, reveal its neighbors
            if (board.Cells[r, c].NeighboringMines == 0)
            {
                for (int rOffset = -1; rOffset <= 1; rOffset++)
                {
                    for (int cOffset = -1; cOffset <= 1; cOffset++)
                    {
                        int newRow = r + rOffset;
                        int newCol = c + cOffset;
                        if (newRow >= 0 && newRow < board.Size && newCol >= 0 && newCol < board.Size && !(rOffset == 0 && cOffset == 0))
                        {
                            //Recursively reveal neighboring cells
                            RevealCell(newRow, newCol);
                        }
                    }
                }
            }
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
