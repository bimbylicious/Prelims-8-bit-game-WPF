using System.Collections.Generic;
using System.Windows;

namespace BinaryConverterGame
{
    public partial class LeaderboardWindow : Window
    {
        public LeaderboardWindow()
        {
            InitializeComponent();
            LoadLeaderboard();
        }

        private void LoadLeaderboard()
        {
            LeaderboardSystem leaderboardSystem = new LeaderboardSystem();
            List<LeaderboardEntry> leaderboardEntries = leaderboardSystem.LoadLeaderboard();

            LeaderboardListBox.ItemsSource = leaderboardEntries;
        }
    }
}
