using System.Windows;
using System.Windows.Controls;

namespace _7LAB
{
    public partial class MultiEditAnimation : Window
    {
        public MultiEditAnimation()
        {
            InitializeComponent();
        }

        private void ClearLeft_Click(object sender, RoutedEventArgs e)
        {
            LeftTextBox.Clear();
        }

        private void ClearRight_Click(object sender, RoutedEventArgs e)
        {
            RightTextBox.Clear();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

    }
}