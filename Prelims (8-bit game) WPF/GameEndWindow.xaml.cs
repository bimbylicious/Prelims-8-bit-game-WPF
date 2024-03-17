using System.ComponentModel;
using System.Windows;

namespace BinaryConverterGame
{
    public partial class GameEndWindow : Window, INotifyPropertyChanged
    {
        public int Score { get; set; }
        public int ElapsedTime { get; set; }

        public GameEndWindow(int score)
        {
            InitializeComponent();
            Score = score;
            ElapsedTime = 0; // Initialize ElapsedTime
        }

        private void EndGameButton_Click(object sender, RoutedEventArgs e)
        {
            Close(); // Close the game end window
        }

        private void LeaderboardButton_Click(object sender, RoutedEventArgs e)
        {
            // Show the leaderboard window
            LeaderboardWindow leaderboardWindow = new LeaderboardWindow();
            leaderboardWindow.ShowDialog();
        }
        public void ShowLeaderboardButtonVisible(bool isVisible)
        {
            LeaderboardButton.Visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
