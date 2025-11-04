using Microsoft.Maui.Controls;
using System;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        Random random = new Random();

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"You hit me {count} time. DONT HIT ME!!!";
            else
                CounterBtn.Text = $"You hit me {count} times. DONT HIT ME!!!";

            // Move button to a random position
            if (CounterBtn.Parent is Layout parentLayout)
            {
                double maxX = parentLayout.Width - CounterBtn.Width;
                double maxY = parentLayout.Height - CounterBtn.Height;

                if (maxX > 0 && maxY > 0)
                {
                    double newX = random.NextDouble() * maxX;
                    double newY = random.NextDouble() * maxY;

                    AbsoluteLayout.SetLayoutBounds(CounterBtn, new Rect(newX, newY, CounterBtn.Width, CounterBtn.Height));
                }
            }

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}
