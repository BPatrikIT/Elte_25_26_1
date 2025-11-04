namespace DigitalClock
{
    partial class Clock
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            cityLabel = new Label();
            timeLabel = new Label();
            timer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // cityLabel
            // 
            cityLabel.AutoSize = true;
            cityLabel.Location = new Point(36, 33);
            cityLabel.Name = "cityLabel";
            cityLabel.Size = new Size(94, 25);
            cityLabel.TabIndex = 0;
            cityLabel.Text = "City Name";
            // 
            // timeLabel
            // 
            timeLabel.AutoSize = true;
            timeLabel.BorderStyle = BorderStyle.Fixed3D;
            timeLabel.Font = new Font("Consolas", 16F, FontStyle.Regular, GraphicsUnit.Point, 238);
            timeLabel.Location = new Point(36, 75);
            timeLabel.Name = "timeLabel";
            timeLabel.Size = new Size(109, 39);
            timeLabel.TabIndex = 1;
            timeLabel.Text = "HH:MM";
            // 
            // Clock
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(timeLabel);
            Controls.Add(cityLabel);
            Name = "Clock";
            Size = new Size(176, 161);
            Load += Clock_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label cityLabel;
        private Label timeLabel;
        private System.Windows.Forms.Timer timer;
    }
}
