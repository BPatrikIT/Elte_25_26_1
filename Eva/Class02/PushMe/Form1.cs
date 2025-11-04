namespace PushMe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer.Interval = 1000;
            timer.Tick += UpdateStatusBar;

        }
        private int points = 0;
        private Random generator = new Random();
        private DateTime startTime;

        private void pushButton_Click(object sender, EventArgs e)
        {
            int x = ClientSize.Width - pushButton.Width;
            int y = ClientSize.Height - pushButton.Height - statusStrip.Height;
            pushButton.Location = new Point(generator.Next(x), generator.Next(y));

            if (!timer.Enabled)
            {
                startTime = DateTime.Now;
                timer.Start();
            }
            else
            {
                ++points;
            }
            UpdateStatusBar(sender, e);
        }

        private void UpdateStatusBar(object? sender, EventArgs e)
        {
            double elapsedSeconds = (DateTime.Now - startTime).TotalSeconds;
            statusLabel.Text = $"Points: {points}, Time: {elapsedSeconds:F0} s, Rate: {points / elapsedSeconds:F2} points/s";
        }

        private void GameClosing(object? sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && timer.Enabled)
            {
                double elapsedSeconds = (DateTime.Now - startTime).TotalSeconds;
                double pushPerSeconds = points / elapsedSeconds;
                MessageBox.Show($"Pushes per seconds: {pushPerSeconds:F2}", "Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
