using System.Windows;

namespace _4LAB
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenTask1_Click(object sender, RoutedEventArgs e)
        {
            new Task1Window().Show();
            this.Hide();
        }

        private void OpenTask2_Click(object sender, RoutedEventArgs e)
        {
            new Task2Window().Show();
            this.Hide();
        }

        private void OpenTask3_Click(object sender, RoutedEventArgs e)
        {
            new Task3Window().Show();
            this.Hide();
        }

        private void OpenTask4_Click(object sender, RoutedEventArgs e)
        {
            new Task4Window().Show();
            this.Hide();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}