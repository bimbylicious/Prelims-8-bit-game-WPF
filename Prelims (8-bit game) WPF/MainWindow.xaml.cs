using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace BinaryConverterGame
{
    public partial class MainWindow : Window
    {
        private int decimalNumber;
        private int initialTimerDuration = 30;
        private int score = 0;
        private int currentRound = 1;
        private DispatcherTimer countdownTimer;
        private int elapsedTimeForCountdown = 0; // Separate variable for countdown timer
        private int elapsedTimeOverall = 0; // Variable for overall elapsed time
        private LeaderboardSystem leaderboardSystem;
        private GameEndWindow gameEndWindow;

        public MainWindow()
        {
            InitializeComponent();
            DecimalTextBlock.Visibility = Visibility.Hidden;
            GenerateRandomNumber();
            leaderboardSystem = new LeaderboardSystem();
            elapsedTimeOverall = 0; // Initialize overall elapsed time
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = false;
            DecimalTextBlock.Visibility = Visibility.Visible; // Set visibility to Visible
            StartCountdownTimer();
        }

        private void StartCountdownTimer()
        {
            if (countdownTimer != null)
            {
                countdownTimer.Stop();
                countdownTimer = null;
            }

            if (currentRound >= 11)
            {
                initialTimerDuration = 10;
            }
            else
            {
                initialTimerDuration = 30 - (currentRound - 1) * 2;
            }

            TimerTextBlock.Text = $"Time Remaining: {initialTimerDuration} s";

            countdownTimer = new DispatcherTimer();
            countdownTimer.Interval = TimeSpan.FromSeconds(1);
            countdownTimer.Tick += CountdownTimer_Tick;
            countdownTimer.Start();
        }

        private void CountdownTimer_Tick(object? sender, EventArgs e)
        {
            int timeRemaining = initialTimerDuration - elapsedTimeForCountdown;
            TimerTextBlock.Text = $"Time Remaining: {timeRemaining} s";

            if (timeRemaining <= 0)
            {
                countdownTimer.Stop();
                SubmitButton.IsEnabled = false;
                ShowGameEndWindow();
            }

            elapsedTimeForCountdown++; // Increment countdown timer elapsed time
            elapsedTimeOverall++; // Increment overall elapsed time
            OverallTimeTextBlock.Text = $"Overall Time: {elapsedTimeOverall} s";
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAnswer())
            {
                score += CalculateScore();
                ScoreTextBlock.Text = $"Score: {score}";

                if (initialTimerDuration > 10)
                {
                    initialTimerDuration -= 2;
                }

                ResetCountdownTimer();

                GenerateRandomNumber();
                ResetButtons();

                currentRound++;
                RoundTextBlock.Text = $"Round: {currentRound}";
            }
            else
            {
                MessageBox.Show("Incorrect answer. Please try again.");
            }
        }

        private void ResetCountdownTimer()
        {
            countdownTimer.Stop();
            elapsedTimeForCountdown = 0; // Reset countdown timer elapsed time
            TimerTextBlock.Text = $"Time Remaining: {initialTimerDuration} s";
            countdownTimer.Start();
        }

        private bool CheckAnswer()
        {
            string binaryInput = CalculateBinaryInput();
            string binaryDecimal = CalculateBinary(decimalNumber);
            return binaryInput.Equals(binaryDecimal);
        }

        private string CalculateBinaryInput()
        {
            return Bit0.Content.ToString() + Bit1.Content.ToString() + Bit2.Content.ToString() +
                   Bit3.Content.ToString() + Bit4.Content.ToString() + Bit5.Content.ToString() +
                   Bit6.Content.ToString() + Bit7.Content.ToString();
        }

        private string CalculateBinary(int decimalNumber)
        {
            return Convert.ToString(decimalNumber, 2).PadLeft(8, '0');
        }

        private int CalculateScore()
        {
            int points = 0;

            if (currentRound >= 1 && currentRound <= 5)
            {
                points = 1;
            }
            else if (currentRound >= 6 && currentRound <= 10)
            {
                points = 3;
            }
            else if (currentRound >= 11)
            {
                points = 5;
            }

            return points;
        }

        private void BitButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton.Content.ToString() == "0")
            {
                clickedButton.Content = "1";
            }
            else
            {
                clickedButton.Content = "0";
            }
        }

        private void GenerateRandomNumber()
        {
            Random rand = new Random();
            decimalNumber = rand.Next(1, 256);
            DecimalTextBlock.Text = decimalNumber.ToString();
            DecimalTextBlock.Visibility = Visibility.Visible;
        }

        private void ResetButtons()
        {
            foreach (var child in BitStackPanel.Children)
            {
                if (child is StackPanel panel)
                {
                    foreach (var innerChild in panel.Children)
                    {
                        if (innerChild is Button button)
                        {
                            button.Content = "0";
                        }
                    }
                }
            }
        }

        private void ShowGameEndWindow()
        {
            if (IsTop10(score))
            {
                // If the score is top 10, prompt for player name
                EnterNameWindow enterNameWindow = new EnterNameWindow(score, elapsedTimeOverall);
                if (enterNameWindow.ShowDialog() == true)
                {
                    leaderboardSystem.UpdateLeaderboard(enterNameWindow.PlayerName, score, elapsedTimeOverall);
                }
            }

            // Show the end game window
            gameEndWindow = new GameEndWindow(elapsedTimeOverall);
            gameEndWindow.ShowLeaderboardButtonVisible(true);
            gameEndWindow.ShowDialog();
        }

        private bool IsTop10(int score)
        {
            List<LeaderboardEntry> leaderboard = leaderboardSystem.LoadLeaderboard();
            return leaderboard.Count < 10 || score > leaderboard.Last().Score;
        }
        private void LeaderboardButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the leaderboard window
            LeaderboardWindow leaderboardWindow = new LeaderboardWindow();
            leaderboardWindow.ShowDialog();
        }

    }
}
