using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace _10Lab
{
    public partial class Task2Window : Window
    {
        public Task2Window()
        {
            InitializeComponent();
        }

        private void Switch_MouseEnter(object sender, MouseEventArgs e)
        {
            Debug.WriteLine("Mouse Enter: " + ((FrameworkElement)sender).Name);

            if (sender is Grid switchContainer)
            {
                // Находим ScaleTransform в TransformGroup
                TransformGroup transformGroup = switchContainer.RenderTransform as TransformGroup;
                if (transformGroup != null)
                {
                    ScaleTransform scaleTransform = transformGroup.Children[0] as ScaleTransform;
                    if (scaleTransform != null)
                    {
                        // Анимация увеличения
                        DoubleAnimation animation = new DoubleAnimation
                        {
                            To = 1.2,
                            Duration = TimeSpan.FromSeconds(0.3),
                            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
                        };

                        scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
                        scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
                    }
                }
            }
        }

        private void Switch_MouseLeave(object sender, MouseEventArgs e)
        {
            Debug.WriteLine("Mouse Leave: " + ((FrameworkElement)sender).Name);

            if (sender is Grid switchContainer)
            {
                // Находим ScaleTransform в TransformGroup
                TransformGroup transformGroup = switchContainer.RenderTransform as TransformGroup;
                if (transformGroup != null)
                {
                    ScaleTransform scaleTransform = transformGroup.Children[0] as ScaleTransform;
                    if (scaleTransform != null)
                    {
                        // Анимация возврата к нормальному размеру
                        DoubleAnimation animation = new DoubleAnimation
                        {
                            To = 1.0,
                            Duration = TimeSpan.FromSeconds(0.3),
                            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
                        };

                        scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
                        scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
                    }
                }
            }
        }

        private void Switch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("Mouse Down: " + ((FrameworkElement)sender).Name);

            if (sender is Grid switchContainer)
            {
                // Находим RotateTransform в TransformGroup
                TransformGroup transformGroup = switchContainer.RenderTransform as TransformGroup;
                if (transformGroup != null)
                {
                    RotateTransform rotateTransform = transformGroup.Children[1] as RotateTransform;
                    if (rotateTransform != null)
                    {
                        Debug.WriteLine($"Rotating {switchContainer.Name} from {rotateTransform.Angle} to {rotateTransform.Angle + 20}");

                        // Плавный поворот всей области на 20 градусов по часовой стрелке
                        DoubleAnimation animation = new DoubleAnimation
                        {
                            To = rotateTransform.Angle + 20,
                            Duration = TimeSpan.FromSeconds(0.3),
                            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
                        };

                        rotateTransform.BeginAnimation(RotateTransform.AngleProperty, animation);
                    }
                }
            }

            e.Handled = true;
        }

        // Метод для тестирования - поворачивает все переключатели
        private void TestRotateAll_Click(object sender, RoutedEventArgs e)
        {
            RotateSwitch(RotateTransform1);
            RotateSwitch(RotateTransform2);
            RotateSwitch(RotateTransform3);
        }

        private void RotateSwitch(RotateTransform rotateTransform)
        {
            if (rotateTransform != null)
            {
                DoubleAnimation animation = new DoubleAnimation
                {
                    To = rotateTransform.Angle + 20,
                    Duration = TimeSpan.FromSeconds(0.3),
                    EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
                };

                rotateTransform.BeginAnimation(RotateTransform.AngleProperty, animation);
            }
        }
    }
}