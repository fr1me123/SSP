using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace _7LAB
{
    public partial class RunningButton : Window
    {
        private Random random = new Random();

        public RunningButton()
        {
            InitializeComponent();
            RunAwayButton.MouseEnter += RunAwayButton_MouseEnter;

            // Центрируем кнопку при загрузке окна
            this.Loaded += (s, e) => CenterRunAwayButton();
        }

        private void CenterRunAwayButton()
        {
            // Центрируем кнопку
            double centerX = (MainCanvas.ActualWidth - RunAwayButton.ActualWidth) / 2;
            double centerY = (MainCanvas.ActualHeight - RunAwayButton.ActualHeight) / 2;

            Canvas.SetLeft(RunAwayButton, centerX);
            Canvas.SetTop(RunAwayButton, centerY);
        }

        private void RunAwayButton_MouseEnter(object sender, MouseEventArgs e)
        {
            // Вычисляем безопасную зону (отступ от краев)
            double margin = 20;
            double maxX = MainCanvas.ActualWidth - RunAwayButton.ActualWidth - margin;
            double maxY = MainCanvas.ActualHeight - RunAwayButton.ActualHeight - margin;

            // Новая случайная позиция в пределах безопасной зоны
            double newX = random.Next((int)margin, (int)Math.Max(margin, maxX));
            double newY = random.Next((int)margin, (int)Math.Max(margin, maxY));

            // Анимация перемещения
            DoubleAnimation xAnimation = new DoubleAnimation(newX, TimeSpan.FromSeconds(0.3));
            DoubleAnimation yAnimation = new DoubleAnimation(newY, TimeSpan.FromSeconds(0.3));

            xAnimation.EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut };
            yAnimation.EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut };

            // Используем прямую установку позиции вместо трансформации для более точного позиционирования
            RunAwayButton.BeginAnimation(Canvas.LeftProperty, xAnimation);
            RunAwayButton.BeginAnimation(Canvas.TopProperty, yAnimation);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}