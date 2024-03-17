using System;
using System.Collections.Generic;
using System.IO;

namespace BinaryConverterGame
{
    public class LeaderboardSystem
    {
        private const string leaderboardFileName = "leaderboard.csv";
        private const string leaderboardDirectory = "C:\\Users\\PC\\source\\repos\\Prelims (8-bit game) WPF\\Prelims (8-bit game) WPF\\bin\\Debug";

        private string leaderboardFilePath => Path.Combine(leaderboardDirectory, leaderboardFileName);


        public void UpdateLeaderboard(string playerName, int score, int elapsedTime)
        {
            List<LeaderboardEntry> leaderboard = LoadLeaderboard();
            leaderboard.Add(new LeaderboardEntry(playerName, score, elapsedTime));
            leaderboard.Sort((x, y) => y.Score.CompareTo(x.Score)); // Sort the leaderboard
            SaveLeaderboard(leaderboard);
        }

        public List<LeaderboardEntry> LoadLeaderboard()
        {
            List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();

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
                }
            }

            return leaderboard;
        }

        private void SaveLeaderboard(List<LeaderboardEntry> leaderboard)
        {
            try
            {
                // Ensure directory exists
                Directory.CreateDirectory(leaderboardDirectory);

                // Write to file
                using (StreamWriter writer = new StreamWriter(leaderboardFilePath))
                {
                    foreach (LeaderboardEntry entry in leaderboard)
                    {
                        writer.WriteLine($"{entry.PlayerName},{entry.Score},{entry.ElapsedTime}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving leaderboard: {ex.Message}");
            }
        }

    }

    public class LeaderboardEntry
    {
        public string PlayerName { get; }
        public int Score { get; }
        public int ElapsedTime { get; }

        public LeaderboardEntry(string playerName, int score, int elapsedTime)
        {
            PlayerName = playerName;
            Score = score;
            ElapsedTime = elapsedTime;
        }
    }
}
