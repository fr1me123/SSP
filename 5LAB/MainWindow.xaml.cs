using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _5LAB
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
            new MultiEdit().Show();
            this.Hide();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}