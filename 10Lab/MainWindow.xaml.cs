using System.Windows;

namespace _10Lab
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Task1_Click(object sender, RoutedEventArgs e)
        {
            Task1Window window = new Task1Window();
            window.Show();
        }

        private void Task2_Click(object sender, RoutedEventArgs e)
        {
            Task2Window window = new Task2Window();
            window.Show();
        }

        private void TaskExtra_Click(object sender, RoutedEventArgs e)
        {
            TaskExtraWindow window = new TaskExtraWindow();
            window.Show();
        }
    }
}