using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace BinaryConverterGame
{
    public class LeaderboardSystem
    {
        private const string leaderboardFileName = "leaderboard.csv";
        private string leaderboardDirectory = "C:\\Users\\PC\\OneDrive\\Desktop\\LEADERBOARDSS";


        private string leaderboardFilePath => Path.Combine(leaderboardDirectory, leaderboardFileName);

        public void UpdateLeaderboard(string playerName, int score, int elapsedTime)
        {
            List<LeaderboardEntry> leaderboard = LoadLeaderboard();
            leaderboard.Add(new LeaderboardEntry(playerName, score, elapsedTime));
            leaderboard.Sort((x, y) => y.Score.CompareTo(x.Score)); // Sort the leaderboard
            SaveLeaderboard(leaderboard); // Save the updated leaderboard
        }


        public List<LeaderboardEntry> LoadLeaderboard()
        {
            List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();

            try
            {
                if (File.Exists(leaderboardFilePath))
                {
                    string[] lines = File.ReadAllLines(leaderboardFilePath);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 3 && int.TryParse(parts[1], out int score) && int.TryParse(parts[2], out int elapsedTime))
                        {
                            leaderboard.Add(new LeaderboardEntry(parts[0], score, elapsedTime));
                        }
                        else
                        {
                            MessageBox.Show($"Invalid entry found in the leaderboard file: {line}", "Leaderboard Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Leaderboard file does not exist.", "Leaderboard Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading leaderboard: {ex.Message}", "Leaderboard Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return leaderboard;
        }



        private void SaveLeaderboard(List<LeaderboardEntry> leaderboard)
        {
            try
            {
                Directory.CreateDirectory(leaderboardDirectory);

                using (StreamWriter writer = new StreamWriter(leaderboardFilePath))
                {
                    foreach (LeaderboardEntry entry in leaderboard)
                    {
                        writer.WriteLine($"{entry.PlayerName},{entry.Score},{entry.ElapsedTime}");
                    }
                }
                MessageBox.Show("Leaderboard saved successfully.", "Leaderboard Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving leaderboard: {ex.Message}", "Leaderboard Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }

    public class LeaderboardEntry
    {
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public int ElapsedTime { get; set; }

        public LeaderboardEntry(string playerName, int score, int elapsedTime)
        {
            PlayerName = playerName;
            Score = score;
            ElapsedTime = elapsedTime;
        }
    }

}
