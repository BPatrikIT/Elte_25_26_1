namespace PushMe
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
            components = new System.ComponentModel.Container();
            pushButton = new Button();
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            timer = new System.Windows.Forms.Timer(components);
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // pushButton
            // 
            pushButton.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 238);
            pushButton.Location = new Point(331, 182);
            pushButton.Name = "pushButton";
            pushButton.Size = new Size(150, 50);
            pushButton.TabIndex = 0;
            pushButton.Text = "PUSH ME";
            pushButton.UseVisualStyleBackColor = true;
            pushButton.Click += pushButton_Click;
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(24, 24);
            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
            statusStrip.Location = new Point(0, 512);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(828, 32);
            statusStrip.TabIndex = 1;
            statusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(279, 25);
            statusLabel.Text = "Click the button to start the game";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(828, 544);
            Controls.Add(statusStrip);
            Controls.Add(pushButton);
            MinimumSize = new Size(850, 600);
            Name = "Form1";
            Text = "Button Hunter";
            FormClosing += GameClosing;
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button pushButton;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Timer timer;
    }
}
