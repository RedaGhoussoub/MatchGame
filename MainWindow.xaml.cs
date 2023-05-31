using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        TextBlock lastTextBlockClicked;
        bool findingMatch = false;
        int counter = 10;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void SetUpGame()
        {
            List<string> animalEmojis = new List<string>()
            {
                "🦓","🦓",
                "🐕","🐕",
                "🐒","🐒",
                "🦛","🦛",
                "🐘","🐘",
                "🐁","🐁",
                "🦎","🦎",
                "🐢","🐢",
            };
            
            Random random = new Random();

            foreach(TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timerTextBlock")
                {
                    int index = random.Next(animalEmojis.Count);
                    string emoji = animalEmojis[index];
                    textBlock.Text = emoji;
                    textBlock.Visibility = Visibility.Visible;
                    animalEmojis.RemoveAt(index);
                }
            }

            counter = 10;
            matchesFound = 0;
            timerTextBlock.Text = counter.ToString("0s");
            timer.Start();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;

            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
                matchesFound++;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            counter--;

            timerTextBlock.Text = counter.ToString("0s");

            if (matchesFound == 8)
            {
                timer.Stop();
                timerTextBlock.Text += " - Play again?";
            }

            if (counter == 0)
            {
                timer.Stop();
                timerTextBlock.Text = "You lost sorry - Click to play again?";
            }
        }

        private void TimerTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8 || counter == 0)
            {
                SetUpGame();
            }
        }
    }
}
