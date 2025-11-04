using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Minesweeper.Model;

namespace Minesweeper.View
{
    public partial class GameWindow : Form
    {
        public GameWindow(string _playerName, string _player2Name, int _boardSize)
        {
            InitializeComponent();
            InitializeCustomGameWindow();
            Game game = new Game(new string[] { _playerName, _player2Name }, new Board(_boardSize, (_boardSize * _boardSize) / 6));
            InitializeBoard(_boardSize, new string[] { _playerName, _player2Name });
        }

        private void InitializeCustomGameWindow()
        {
            SuspendLayout();
            // 
            // Form (GameWindow)
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(246, 247, 249); // modern light background
                                                       // Keep a reasonable default; InitializeBoard will resize to fit the actual game.
            ClientSize = new Size(600, 500);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.CenterScreen;
            Name = "GameWindow";
            Text = "MineSweeper - Game";
            ResumeLayout(false);
            PerformLayout();
        }

        // Add this method inside the GameWindow class

        private void CellButton_Click(object sender, EventArgs e)
        {
            // TODO: Implement cell click logic here
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                
                //Point cell = (Point)clickedButton.Tag;
                // Example: Show cell coordinates in the button text
                //clickedButton.Text = $"{cell.X},{cell.Y}";
            }
        }

        private void InitializeBoard(int _boardSize, string[] _players)
        {
            // Layout constants
            const int cellSize = 40;
            const int step = 45; // cellSize + gap (40 + 5)
            const int leftMargin = 50;
            const int rightMargin = 50;
            const int gridTop = 70; // Y coordinate where the first row starts
            const int titleTop = 20;
            const int bottomMargin = 40;

            // Calculate required client size to fit the grid and margins
            int rightmostCellX = leftMargin + (_boardSize - 1) * step + cellSize;
            int requiredWidth = rightmostCellX + rightMargin;

            int bottomCellY = gridTop + (_boardSize - 1) * step + cellSize;
            int requiredHeight = bottomCellY + bottomMargin;

            // Ensure minimal reasonable dimensions
            requiredWidth = Math.Max(requiredWidth, 400);
            requiredHeight = Math.Max(requiredHeight, 300);

            // Apply computed size so controls can be positioned correctly (title centered)
            ClientSize = new Size(requiredWidth, requiredHeight);

            // Title label
            Label titleLabel = new Label();
            titleLabel.Text = $"Minesweeper - {_players[0]} vs {_players[1]}";
            titleLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            titleLabel.AutoSize = true;
            
            Controls.Add(titleLabel);

            // Create grid buttons
            for (int r = 0; r < _boardSize; r++)
            {
                for (int c = 0; c < _boardSize; c++)
                {
                    Button cellButton = new Button();
                    cellButton.Size = new Size(cellSize, cellSize);
                    cellButton.Location = new Point(leftMargin + c * step, gridTop + r * step);
                    cellButton.Tag = new Point(r, c); // Store row and column in Tag
                    cellButton.BackColor = Color.LightGray;
                    cellButton.FlatStyle = FlatStyle.Flat;
                    cellButton.Click += CellButton_Click;
                    Controls.Add(cellButton);
                }
            }

            //Modify the label placement after the window size set
            titleLabel.Location = new Point((ClientSize.Width - titleLabel.Width) / 2, titleTop);
        }
    }
}
