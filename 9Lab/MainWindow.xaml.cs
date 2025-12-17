using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Controls;

namespace _9Lab
{
    public partial class MainWindow : Window
    {
        private bool isAnimating = false;
        private PointAnimation animation;

        public MainWindow()
        {
            InitializeComponent();
            InitializeAnimation();
        }

        private void InitializeAnimation()
        {
            animation = new PointAnimation
            {
                From = new Point(0.3, 0.3),
                To = new Point(0.7, 0.7),
                Duration = TimeSpan.FromSeconds(2),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };
        }

        private void BtnAnimate_Click(object sender, RoutedEventArgs e)
        {
            if (!isAnimating)
            {
                StartAnimation();
                BtnAnimate.Content = "Остановить анимацию";
            }
            else
            {
                StopAnimation();
                BtnAnimate.Content = "Запустить анимацию";
            }
            isAnimating = !isAnimating;
        }

        private void StartAnimation()
        {
            // Обновляем скорость перед запуском
            animation.Duration = TimeSpan.FromSeconds(SpeedSlider.Value);
            BallGradient.BeginAnimation(RadialGradientBrush.GradientOriginProperty, animation);
        }

        private void StopAnimation()
        {
            BallGradient.BeginAnimation(RadialGradientBrush.GradientOriginProperty, null);
        }

        private void SpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SpeedValueText != null)
            {
                SpeedValueText.Text = $"Текущая скорость: {SpeedSlider.Value:F1} сек";
            }

            // Если анимация запущена, сразу применяем новую скорость
            if (isAnimating)
            {
                StopAnimation();
                StartAnimation();
            }
        }

        private void ColorPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ColorPicker.SelectedItem is ComboBoxItem item && item.Tag != null)
            {
                try
                {
                    var color = (Color)ColorConverter.ConvertFromString(item.Tag.ToString());
                    BallGradient.GradientStops[1].Color = color;
                }
                catch (FormatException)
                {
                    MessageBox.Show("Ошибка в формате цвета", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}