using System.Windows;

namespace BinaryConverterGame
{
    public partial class EnterNameWindow : Window
    {
        public string PlayerName { get; private set; }

        public EnterNameWindow(int score)
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            PlayerName = NameTextBox.Text;
            Close();
        }
    }
}
