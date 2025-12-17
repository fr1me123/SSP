using System.Windows;
using System.Windows.Controls;

namespace _5LAB
{
    public partial class MultiEdit : Window
    {
        public MultiEdit()
        {
            InitializeComponent();
        }

        private void BigText_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Style = (Style)Resources["BigTextStyle"];
        }

        private void SmallText_GotFocus(object sender, RoutedEventArgs e)
        {
            foreach (var ctrl in new[] { LeftBig, LeftSmall1, LeftSmall2, RightBig, RightSmall1, RightSmall2 })
                ctrl.Style = (Style)Resources["SmallTextStyle"];
            (sender as TextBox).Style = (Style)Resources["BigTextStyle"];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn.Tag?.ToString() == "Left")
            {
                LeftBig.Clear();
                LeftSmall1.Clear();
                LeftSmall2.Clear();
            }
            else if (btn.Tag?.ToString() == "Right")
            {
                RightBig.Clear();
                RightSmall1.Clear();
                RightSmall2.Clear();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
