using System.Windows;
using static System.Formats.Asn1.AsnWriter;

namespace BinaryConverterGame
{
    public partial class EnterNameWindow : Window
    {
        public string PlayerName { get; private set; }
        private readonly int score;
        private readonly int elapsedTimeOverall;

        public EnterNameWindow(int score, int elapsedTimeOverall)
        {
            InitializeComponent();
            this.score = score;
            this.elapsedTimeOverall = elapsedTimeOverall;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            PlayerName = NameTextBox.Text;
            // Call UpdateLeaderboard from LeaderboardSystem
            LeaderboardSystem leaderboardSystem = new LeaderboardSystem();
            leaderboardSystem.UpdateLeaderboard(PlayerName, score, elapsedTimeOverall);
            Close();
        }

    }
}
