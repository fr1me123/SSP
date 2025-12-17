using System;
using System.Windows;

namespace _8LAB
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Task1Button_Click(object sender, RoutedEventArgs e)
        {
            PulsarWithScale pulsarWindow = new PulsarWithScale();
            pulsarWindow.Show();
            this.Close();
        }

        private void Task2Button_Click(object sender, RoutedEventArgs e)
        {
            TextEditor textEditorWindow = new TextEditor();
            textEditorWindow.Show();
            this.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}