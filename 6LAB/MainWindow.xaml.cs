using System.Windows;

namespace _6LAB
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NavigateToTask1(object sender, RoutedEventArgs e)
        {
            Task1Window task1Window = new Task1Window();
            task1Window.Show();
            this.Close();
        }

        private void NavigateToTask2(object sender, RoutedEventArgs e)
        {
            Task2Window task2Window = new Task2Window();
            task2Window.Show();
            this.Close();
        }

        private void ExitApplication(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}