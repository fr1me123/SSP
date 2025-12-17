using System.Windows;

namespace _7LAB
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MultiEditAnimation_Click(object sender, RoutedEventArgs e)
        {
            new MultiEditAnimation().Show();
            this.Close();
        }

        private void RunningButton_Click(object sender, RoutedEventArgs e)
        {
            new RunningButton().Show();
            this.Close();
        }

        private void Pulsar_Click(object sender, RoutedEventArgs e)
        {
            new Pulsar().Show();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}