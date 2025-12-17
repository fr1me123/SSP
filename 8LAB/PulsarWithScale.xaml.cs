using System.Windows;
using System.Windows.Controls;

namespace _8LAB
{
    public partial class PulsarWithScale : Window
    {
        public PulsarWithScale()
        {
            InitializeComponent();
        }

        private void ScaleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!IsLoaded || ScaleValueText == null)
                return;

            double scale = ScaleSlider.Value;

            // Обновляем текст масштаба
            ScaleValueText.Text = $"{scale:F1}x";

            // Применяем масштаб к пульсару
            PulsarScaleTransform.ScaleX = scale;
            PulsarScaleTransform.ScaleY = scale;

            // Применяем масштаб к кнопке
            ButtonScaleTransform.ScaleX = scale;
            ButtonScaleTransform.ScaleY = scale;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}