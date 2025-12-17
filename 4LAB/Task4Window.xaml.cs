using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;

namespace _4LAB
{
    public partial class Task4Window : Window
    {
        public Task4Window()
        {
            InitializeComponent();
            inkCanvas.DefaultDrawingAttributes = new DrawingAttributes
            {
            };
            inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
