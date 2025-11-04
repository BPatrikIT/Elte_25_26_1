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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            InitializeCustomMenu();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            int boardSize = 6; // default

            string modeText = ((ComboBox)Controls.Find("comboBoxGameMode", true)[0]).SelectedItem.ToString();

            switch (modeText)
            {
                case "6 x 6":
                    boardSize = 6;
                    break;
                case "10 x 10":
                    boardSize = 10;
                    break;
                case "16 x 16":
                    boardSize = 16;
                    break;
            }

            string player1Name = ((TextBox)Controls.Find("textBoxPlayer1", true)[0]).Text;
            string player2Name = ((TextBox)Controls.Find("textBoxPlayer2", true)[0]).Text;

            GameWindow gameWindow = new GameWindow(player1Name, player2Name, boardSize);
            gameWindow.Show();
            this.Hide();
        }

        // Visually appealing and user-friendly menu design
        private void InitializeCustomMenu()
        {
            SuspendLayout();
            // 
            // Form (Menu)
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(246, 247, 249); // modern light background
            ClientSize = new Size(800, 320);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.CenterScreen;
            Name = "Menu";
            Text = "MineSweeper - Menu";

            //
            // MenuStrip (top)
            //
            MenuStrip menuStripMain = new MenuStrip();
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Dock = DockStyle.Top;
            menuStripMain.BackColor = Color.Transparent;
            menuStripMain.RenderMode = ToolStripRenderMode.System;

            // File menu and items
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("File");
            ToolStripMenuItem saveGameItem = new ToolStripMenuItem("Save Game");
            ToolStripMenuItem loadGameItem = new ToolStripMenuItem("Load Game");
            ToolStripMenuItem exitItem = new ToolStripMenuItem("Exit");

            fileMenu.DropDownItems.AddRange(new ToolStripItem[] {
                saveGameItem,
                loadGameItem,
                new ToolStripSeparator(),
                exitItem
            });

            // Help menu
            ToolStripMenuItem helpMenu = new ToolStripMenuItem("Help");
            ToolStripMenuItem aboutItem = new ToolStripMenuItem("About");
            helpMenu.DropDownItems.Add(aboutItem);

            menuStripMain.Items.AddRange(new ToolStripItem[] {
                fileMenu,
                helpMenu
            });

            // Keep the original behaviour placeholder
            saveGameItem.Enabled = false;

            //
            // Main responsive layout panel
            //
            TableLayoutPanel layoutMain = new TableLayoutPanel();
            layoutMain.Name = "layoutMain";
            layoutMain.Dock = DockStyle.Top;
            layoutMain.AutoSize = true;
            layoutMain.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutMain.Padding = new Padding(28);
            layoutMain.BackColor = Color.Transparent;
            layoutMain.ColumnCount = 2;
            layoutMain.RowCount = 4;
            layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            layoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            layoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            layoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            layoutMain.Margin = new Padding(20, 30, 20, 20);

            //
            // Player 1
            //
            Label labelPlayer1 = new Label();
            labelPlayer1.Text = "Player 1:";
            labelPlayer1.AutoSize = true;
            labelPlayer1.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            labelPlayer1.ForeColor = Color.FromArgb(33, 33, 33);
            labelPlayer1.Anchor = AnchorStyles.Left;
            labelPlayer1.Margin = new Padding(3, 12, 12, 12);

            TextBox textBoxPlayer1 = new TextBox();
            textBoxPlayer1.Name = "textBoxPlayer1";
            textBoxPlayer1.PlaceholderText = "Enter name";
            textBoxPlayer1.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxPlayer1.BorderStyle = BorderStyle.FixedSingle;
            textBoxPlayer1.BackColor = Color.White;
            textBoxPlayer1.ForeColor = Color.FromArgb(30, 30, 30);
            textBoxPlayer1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBoxPlayer1.Margin = new Padding(3, 8, 3, 8);
            textBoxPlayer1.Width = 260;
            textBoxPlayer1.TabIndex = 0;

            //
            // Player 2
            //
            Label labelPlayer2 = new Label();
            labelPlayer2.Text = "Player 2:";
            labelPlayer2.AutoSize = true;
            labelPlayer2.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            labelPlayer2.ForeColor = Color.FromArgb(33, 33, 33);
            labelPlayer2.Anchor = AnchorStyles.Left;
            labelPlayer2.Margin = new Padding(3, 12, 12, 12);

            TextBox textBoxPlayer2 = new TextBox();
            textBoxPlayer2.Name = "textBoxPlayer2";
            textBoxPlayer2.PlaceholderText = "Enter name";
            textBoxPlayer2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxPlayer2.BorderStyle = BorderStyle.FixedSingle;
            textBoxPlayer2.BackColor = Color.White;
            textBoxPlayer2.ForeColor = Color.FromArgb(30, 30, 30);
            textBoxPlayer2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBoxPlayer2.Margin = new Padding(3, 8, 3, 8);
            textBoxPlayer2.Width = 260;
            textBoxPlayer2.TabIndex = 1;

            //
            // Game Mode
            //
            Label labelGameMode = new Label();
            labelGameMode.Text = "Game Mode:";
            labelGameMode.AutoSize = true;
            labelGameMode.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            labelGameMode.ForeColor = Color.FromArgb(33, 33, 33);
            labelGameMode.Anchor = AnchorStyles.Left;
            labelGameMode.Margin = new Padding(3, 12, 12, 12);

            ComboBox comboBoxGameMode = new ComboBox();
            comboBoxGameMode.Name = "comboBoxGameMode";
            comboBoxGameMode.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            comboBoxGameMode.Items.AddRange(new string[] { "6 x 6", "10 x 10", "16 x 16" });
            comboBoxGameMode.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxGameMode.FlatStyle = FlatStyle.Flat;
            comboBoxGameMode.BackColor = Color.White;
            comboBoxGameMode.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            comboBoxGameMode.Margin = new Padding(3, 8, 3, 8);
            comboBoxGameMode.SelectedIndex = 0;
            comboBoxGameMode.Width = 260;
            comboBoxGameMode.TabIndex = 2;

            //
            // Start Game Button
            //
            Button buttonStartGame = new Button();
            buttonStartGame.Name = "buttonStartGame";
            buttonStartGame.Text = "Start Game";
            buttonStartGame.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonStartGame.FlatStyle = FlatStyle.Flat;
            buttonStartGame.FlatAppearance.BorderSize = 0;
            buttonStartGame.BackColor = Color.FromArgb(0, 120, 215); // accent color
            buttonStartGame.ForeColor = Color.White;
            buttonStartGame.Cursor = Cursors.Hand;
            buttonStartGame.Padding = new Padding(12, 6, 12, 6);
            buttonStartGame.Anchor = AnchorStyles.Right;
            buttonStartGame.Margin = new Padding(3, 12, 3, 3);
            buttonStartGame.TabIndex = 3;
            buttonStartGame.AutoSize = true;
            // simple hover effect
            Color btnAccent = buttonStartGame.BackColor;
            Color btnAccentHover = Color.FromArgb(0, 98, 179);
            buttonStartGame.MouseEnter += (s, e) => { buttonStartGame.BackColor = btnAccentHover; };
            buttonStartGame.MouseLeave += (s, e) => { buttonStartGame.BackColor = btnAccent; };

            buttonStartGame.Click += new EventHandler(StartButton_Click);

            //
            // Add controls to layout
            //
            layoutMain.Controls.Add(labelPlayer1, 0, 0);
            layoutMain.Controls.Add(textBoxPlayer1, 1, 0);
            layoutMain.Controls.Add(labelPlayer2, 0, 1);
            layoutMain.Controls.Add(textBoxPlayer2, 1, 1);
            layoutMain.Controls.Add(labelGameMode, 0, 2);
            layoutMain.Controls.Add(comboBoxGameMode, 1, 2);

            // Place the button in the second column in the last row and align right
            layoutMain.Controls.Add(buttonStartGame, 1, 3);

            //
            // Add top-level controls to the form
            //
            Controls.Add(layoutMain);
            Controls.Add(menuStripMain);
            MainMenuStrip = menuStripMain;

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
