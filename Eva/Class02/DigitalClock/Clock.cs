using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalClock
{
    public partial class Clock : UserControl
    {
        public Clock()
        {
            InitializeComponent();
            timer.Interval = 1000;
            timer.Tick += RefreshTime;
            timer.Start();
        }

        private int timeZone;

        public string City
        {
            get { return cityLabel.Text; }
            set { cityLabel.Text = value; }
        }

        public int TimeZone
        {
            get { return timeZone; }
            set 
            {
                timeZone = value;
                RefreshTime(this, EventArgs.Empty);
            }
        }

        private void RefreshTime(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            timeLabel.Text = time
                .AddHours(timeZone)
                .ToString(time.Second % 2 == 0 ? "HH:mm" : "HH mm");
        }

        private void Clock_Load(object sender, EventArgs e)
        {

        }
    }
}
