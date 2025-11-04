namespace DocuStatView
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip = new MenuStrip();
            fileMenu = new ToolStripMenuItem();
            openFileDialogMenu = new ToolStripMenuItem();
            countWordsMenuItem = new ToolStripMenuItem();
            textBox = new TextBox();
            listBoxCounter = new ListBox();
            labelCharacters = new Label();
            labelNonWhitespaceCharacters = new Label();
            labelSentences = new Label();
            labelFleschReadingEase = new Label();
            labelColemanLieuIndex = new Label();
            labelProperNouns = new Label();
            labelIgnoredWords = new Label();
            labelMinimumWordOccurrence = new Label();
            labelMinimumWordLength = new Label();
            spinBoxMinLength = new NumericUpDown();
            spinBoxMinOccurrence = new NumericUpDown();
            textBoxIgnoredWords = new TextBox();
            menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)spinBoxMinLength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)spinBoxMinOccurrence).BeginInit();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(24, 24);
            menuStrip.Items.AddRange(new ToolStripItem[] { fileMenu });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(932, 33);
            menuStrip.TabIndex = 1;
            menuStrip.Text = "menuStrip1";
            // 
            // fileMenu
            // 
            fileMenu.DropDownItems.AddRange(new ToolStripItem[] { openFileDialogMenu, countWordsMenuItem });
            fileMenu.Name = "fileMenu";
            fileMenu.Size = new Size(54, 29);
            fileMenu.Text = "File";
            // 
            // openFileDialogMenu
            // 
            openFileDialogMenu.Name = "openFileDialogMenu";
            openFileDialogMenu.Size = new Size(270, 34);
            openFileDialogMenu.Text = "Open file dialog";
            openFileDialogMenu.Click += OpenDialog;
            // 
            // countWordsMenuItem
            // 
            countWordsMenuItem.Name = "countWordsMenuItem";
            countWordsMenuItem.Size = new Size(270, 34);
            countWordsMenuItem.Text = "Count Words";
            // 
            // textBox
            // 
            textBox.Location = new Point(42, 47);
            textBox.Multiline = true;
            textBox.Name = "textBox";
            textBox.ReadOnly = true;
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.Size = new Size(348, 391);
            textBox.TabIndex = 2;
            // 
            // listBoxCounter
            // 
            listBoxCounter.FormattingEnabled = true;
            listBoxCounter.ItemHeight = 25;
            listBoxCounter.Location = new Point(488, 47);
            listBoxCounter.Name = "listBoxCounter";
            listBoxCounter.Size = new Size(408, 379);
            listBoxCounter.TabIndex = 3;
            // 
            // labelCharacters
            // 
            labelCharacters.AutoSize = true;
            labelCharacters.Location = new Point(12, 479);
            labelCharacters.Name = "labelCharacters";
            labelCharacters.Size = new Size(140, 25);
            labelCharacters.TabIndex = 4;
            labelCharacters.Text = "Character count:";
            // 
            // labelNonWhitespaceCharacters
            // 
            labelNonWhitespaceCharacters.AutoSize = true;
            labelNonWhitespaceCharacters.Location = new Point(12, 539);
            labelNonWhitespaceCharacters.Name = "labelNonWhitespaceCharacters";
            labelNonWhitespaceCharacters.Size = new Size(228, 25);
            labelNonWhitespaceCharacters.TabIndex = 5;
            labelNonWhitespaceCharacters.Text = "Non-whitespace characters:";
            // 
            // labelSentences
            // 
            labelSentences.AutoSize = true;
            labelSentences.Location = new Point(12, 604);
            labelSentences.Name = "labelSentences";
            labelSentences.Size = new Size(137, 25);
            labelSentences.TabIndex = 6;
            labelSentences.Text = "Sentence count:";
            // 
            // labelFleschReadingEase
            // 
            labelFleschReadingEase.AutoSize = true;
            labelFleschReadingEase.Location = new Point(274, 604);
            labelFleschReadingEase.Name = "labelFleschReadingEase";
            labelFleschReadingEase.Size = new Size(173, 25);
            labelFleschReadingEase.TabIndex = 9;
            labelFleschReadingEase.Text = "Flesch Reading Ease:";
            // 
            // labelColemanLieuIndex
            // 
            labelColemanLieuIndex.AutoSize = true;
            labelColemanLieuIndex.Location = new Point(274, 539);
            labelColemanLieuIndex.Name = "labelColemanLieuIndex";
            labelColemanLieuIndex.Size = new Size(170, 25);
            labelColemanLieuIndex.TabIndex = 8;
            labelColemanLieuIndex.Text = "Coleman Lieu Index:";
            // 
            // labelProperNouns
            // 
            labelProperNouns.AutoSize = true;
            labelProperNouns.Location = new Point(274, 479);
            labelProperNouns.Name = "labelProperNouns";
            labelProperNouns.Size = new Size(165, 25);
            labelProperNouns.TabIndex = 7;
            labelProperNouns.Text = "Proper noun count:";
            // 
            // labelIgnoredWords
            // 
            labelIgnoredWords.AutoSize = true;
            labelIgnoredWords.Location = new Point(499, 604);
            labelIgnoredWords.Name = "labelIgnoredWords";
            labelIgnoredWords.Size = new Size(133, 25);
            labelIgnoredWords.TabIndex = 12;
            labelIgnoredWords.Text = "Ignored words:";
            // 
            // labelMinimumWordOccurrence
            // 
            labelMinimumWordOccurrence.AutoSize = true;
            labelMinimumWordOccurrence.Location = new Point(499, 539);
            labelMinimumWordOccurrence.Name = "labelMinimumWordOccurrence";
            labelMinimumWordOccurrence.Size = new Size(222, 25);
            labelMinimumWordOccurrence.TabIndex = 11;
            labelMinimumWordOccurrence.Text = "Minimum word occurence:";
            // 
            // labelMinimumWordLength
            // 
            labelMinimumWordLength.AutoSize = true;
            labelMinimumWordLength.Location = new Point(499, 479);
            labelMinimumWordLength.Name = "labelMinimumWordLength";
            labelMinimumWordLength.Size = new Size(193, 25);
            labelMinimumWordLength.TabIndex = 10;
            labelMinimumWordLength.Text = "Minimum word length:";
            // 
            // spinBoxMinLength
            // 
            spinBoxMinLength.Location = new Point(742, 477);
            spinBoxMinLength.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            spinBoxMinLength.Name = "spinBoxMinLength";
            spinBoxMinLength.Size = new Size(154, 31);
            spinBoxMinLength.TabIndex = 13;
            spinBoxMinLength.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // spinBoxMinOccurrence
            // 
            spinBoxMinOccurrence.Location = new Point(742, 537);
            spinBoxMinOccurrence.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            spinBoxMinOccurrence.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            spinBoxMinOccurrence.Name = "spinBoxMinOccurrence";
            spinBoxMinOccurrence.Size = new Size(154, 31);
            spinBoxMinOccurrence.TabIndex = 14;
            spinBoxMinOccurrence.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // textBoxIgnoredWords
            // 
            textBoxIgnoredWords.Location = new Point(638, 604);
            textBoxIgnoredWords.Name = "textBoxIgnoredWords";
            textBoxIgnoredWords.Size = new Size(258, 31);
            textBoxIgnoredWords.TabIndex = 15;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(932, 654);
            Controls.Add(textBoxIgnoredWords);
            Controls.Add(spinBoxMinOccurrence);
            Controls.Add(spinBoxMinLength);
            Controls.Add(labelIgnoredWords);
            Controls.Add(labelMinimumWordOccurrence);
            Controls.Add(labelMinimumWordLength);
            Controls.Add(labelFleschReadingEase);
            Controls.Add(labelColemanLieuIndex);
            Controls.Add(labelProperNouns);
            Controls.Add(labelSentences);
            Controls.Add(labelNonWhitespaceCharacters);
            Controls.Add(labelCharacters);
            Controls.Add(listBoxCounter);
            Controls.Add(textBox);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Name = "Form1";
            Text = "Form1";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)spinBoxMinLength).EndInit();
            ((System.ComponentModel.ISupportInitialize)spinBoxMinOccurrence).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem fileMenu;
        private ToolStripMenuItem openFileDialogMenu;
        private ToolStripMenuItem countWordsMenuItem;
        private TextBox textBox;
        private ListBox listBoxCounter;
        private Label labelCharacters;
        private Label labelNonWhitespaceCharacters;
        private Label labelSentences;
        private Label labelFleschReadingEase;
        private Label labelColemanLieuIndex;
        private Label labelProperNouns;
        private Label labelIgnoredWords;
        private Label labelMinimumWordOccurrence;
        private Label labelMinimumWordLength;
        private NumericUpDown spinBoxMinLength;
        private NumericUpDown spinBoxMinOccurrence;
        private TextBox textBoxIgnoredWords;
    }
}
