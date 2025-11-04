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
        private Game game;
        public GameWindow(string _playerName, string _player2Name, int _boardSize)
        {
            InitializeComponent();
            InitializeCustomGameWindow();
            game = new Game(
                new string[] { _playerName, _player2Name },
                new Board(_boardSize, (_boardSize * _boardSize) / 6)
            );
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

        private void CellButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                Point cell = (Point)clickedButton.Tag;

                bool valid = game.Reveal(cell.X, cell.Y);

                if (valid)
                {
                    UpdateButtonAppearance(clickedButton, cell.X, cell.Y);
                }
            }
        }

        private void UpdateButtonAppearance(Button button, int row, int col)
        {
            Board board = game.GetBoard();
            Cell cell = board.Cells[row, col];

            if (cell.IsMine)
            {
                button.BackColor = Color.Red;
                button.Text = "💣";
                MessageBox.Show("Boom! You hit a mine!");
            }
            else
            {
                button.BackColor = Color.White;
                int n = cell.NeighboringMines;
                button.Text = n > 0 ? n.ToString() : "";
            }

            button.Enabled = false;
        }


        private void InitializeBoard(int _boardSize, string[] _players)
        {
            // Layout constants
            const int cellSize = 40;
            const int cellGap = 5;
            const int step = cellSize + cellGap;
            const int minLeftRightMargin = 20;
            const int titleTop = 20;
            const int interItemSpacing = 10; // spacing between title and grid
            const int bottomMargin = 40;

            // Compute grid dimensions
            int gridWidth = _boardSize * step - cellGap;   // no trailing gap after last cell
            int gridHeight = _boardSize * step - cellGap;

            // --- Create and measure the title label BEFORE setting client size ---
            Label titleLabel = new Label
            {
                Text = $"Minesweeper - {_players[0]} vs {_players[1]}",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point),
                AutoSize = true
            };

            // Temporary measure using CreateGraphics (because Controls.Add not called yet)
            using (Graphics g = CreateGraphics())
            {
                SizeF measuredSize = g.MeasureString(titleLabel.Text, titleLabel.Font);
                titleLabel.Width = (int)Math.Ceiling(measuredSize.Width);
                titleLabel.Height = (int)Math.Ceiling(measuredSize.Height);
            }

            // --- Calculate required form size based on BOTH grid and title widths ---
            int requiredWidth = Math.Max(400, Math.Max(gridWidth + minLeftRightMargin * 2, titleLabel.Width + minLeftRightMargin * 2));
            int requiredHeight = Math.Max(300, titleTop + titleLabel.Height + interItemSpacing + gridHeight + bottomMargin);

            ClientSize = new Size(requiredWidth, requiredHeight);

            Controls.Add(titleLabel);
            titleLabel.Location = new Point((ClientSize.Width - titleLabel.Width) / 2, titleTop);

            // --- Place grid under the title (gridTop depends on actual title height) ---
            int gridTop = titleLabel.Bottom + interItemSpacing;

            // Center grid horizontally, respecting minimum margin
            int computedLeft = (ClientSize.Width - gridWidth) / 2;
            int left = Math.Max(minLeftRightMargin, computedLeft);

            // --- Create grid buttons ---
            for (int r = 0; r < _boardSize; r++)
            {
                for (int c = 0; c < _boardSize; c++)
                {
                    Button cellButton = new Button
                    {
                        Size = new Size(cellSize, cellSize),
                        Location = new Point(left + c * step, gridTop + r * step),
                        Tag = new Point(r, c),
                        BackColor = Color.LightGray,
                        FlatStyle = FlatStyle.Flat
                    };
                    cellButton.FlatAppearance.BorderSize = 1;
                    cellButton.Click += CellButton_Click;
                    Controls.Add(cellButton);
                }
            }

            // --- Ensure all controls are visible (dynamic resize if necessary) ---
            int farRight = Controls.Cast<Control>().Max(ctrl => ctrl.Right);
            int farBottom = Controls.Cast<Control>().Max(ctrl => ctrl.Bottom);

            bool resized = false;
            int newWidth = ClientSize.Width;
            int newHeight = ClientSize.Height;

            if (farRight + minLeftRightMargin > ClientSize.Width)
            {
                newWidth = farRight + minLeftRightMargin;
                resized = true;
            }
            if (farBottom + bottomMargin > ClientSize.Height)
            {
                newHeight = farBottom + bottomMargin;
                resized = true;
            }

            if (resized)
            {
                ClientSize = new Size(newWidth, newHeight);

                // Recenter title
                titleLabel.Location = new Point((ClientSize.Width - titleLabel.Width) / 2, titleTop);

                // Recenter grid
                computedLeft = (ClientSize.Width - gridWidth) / 2;
                left = Math.Max(minLeftRightMargin, computedLeft);

                foreach (Control ctrl in Controls)
                {
                    if (ctrl is Button btn && btn.Tag is Point pt)
                    {
                        int rr = pt.X;
                        int cc = pt.Y;
                        btn.Location = new Point(left + cc * step, gridTop + rr * step);
                    }
                }
            }
        }
    }
}
