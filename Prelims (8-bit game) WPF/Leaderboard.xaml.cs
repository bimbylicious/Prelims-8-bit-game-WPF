using System.Collections.Generic;
using System.Windows;

namespace BinaryConverterGame
{
    public partial class LeaderboardWindow : Window
    {
        private LeaderboardSystem leaderboardSystem;

        public LeaderboardWindow()
        {
            InitializeComponent();
            leaderboardSystem = new LeaderboardSystem();
            LoadLeaderboard();
        }

        private void LoadLeaderboard()
        {
            List<LeaderboardEntry> leaderboardEntries = leaderboardSystem.LoadLeaderboard();
            LeaderboardListBox.ItemsSource = leaderboardEntries;
        }
    }
}
