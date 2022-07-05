using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Animal_Match
{
    public partial class MainPage : ContentPage
    {
        int tenthsOfSecondsElapsed;
        int mathesFound;
        bool alive = true;

        public MainPage()
        {
            InitializeComponent();

            SetUpGame();
        }

        private bool OnTimerTick()
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10f).ToString("Your time " + "0.0s");
            if (mathesFound == 8)
            {
                timeTextBlock.Text += "\nPlay again?";
                return false;
            }
            return true;
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "🐅","🐅",
                "🐀","🐀",
                "🐇","🐇",
                "🦘","🦘",
                "🐿","🐿",
                "🦢","🦢",
                "🐓","🐓",
                "🐌","🐌"
            };

            Random random = new Random();
            foreach (Button textBlock in mainGrid.Children.OfType<Button>())
            {
                textBlock.IsVisible = true;
                int index = random.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                textBlock.Text = nextEmoji;
                animalEmoji.RemoveAt(index);
            }

            tenthsOfSecondsElapsed = 0;
            mathesFound = 0;
        }

        Button lastClickedButton;
        bool findingMatch = false;

        private void Clicked(object sender, EventArgs e)
        {
            if (alive)
            {
                Device.StartTimer(TimeSpan.FromSeconds(.1), OnTimerTick);
                alive = false;
            }

            Button textBlock = sender as Button;

            if (findingMatch == false)
            {
                textBlock.IsVisible = false;
                lastClickedButton = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastClickedButton.Text)
            {
                mathesFound++;
                textBlock.IsVisible = false;
                findingMatch = false;
            }
            else
            {
                lastClickedButton.IsVisible = true;
                findingMatch = false;
            }
        }

        private void TimeTextLabel_Clicked(object sender, EventArgs e)
        {
            if (mathesFound == 8)
            {
                alive = true;
                timeTextBlock.Text = "Find a pairs!";
                SetUpGame();
            }
        }
    }
}
