using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace _6LAB
{
    public partial class Task2Window : Window
    {
        public Task2Window()
        {
            InitializeComponent();
            InitializeComboBoxes();
            UpdateCloseButtonState();
            UpdateStatus("Приложение готово к работе");
        }

        private void InitializeComboBoxes()
        {
            FontStyleComboBox.SelectedIndex = 0;
            ColorComboBox.SelectedIndex = 0;
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox1.Text = @"Это пример текста для первого поля.

Триггеры в WPF позволяют автоматически изменять свойства элементов при выполнении определенных условий.

Виды триггеров:
• Property Trigger - срабатывает при изменении свойства
• Data Trigger - срабатывает при изменении связанных данных
• Event Trigger - срабатывает при возникновении события
• Multi Trigger - срабатывает при выполнении нескольких условий";

            TextBox2.Text = @"Второе текстовое поле.

Здесь демонстрируются различные типы триггеров:

1. Простые триггеры (кнопка 'Открыть')
2. MultiTrigger (кнопка 'Очистить') 
3. DataTrigger и MultiDataTrigger (кнопка 'Закрыть')
4. EventTrigger с анимацией (текстовые поля)

Кнопка 'Закрыть' доступна только когда оба поля пусты!";

            UpdateCloseButtonState();
            UpdateStatus("Текст загружен в оба поля");
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox1.Clear();
            TextBox2.Clear();
            UpdateCloseButtonState();
            UpdateStatus("Оба текстовых поля очищены");
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Возврат в главное меню
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCloseButtonState();

            int text1Length = TextBox1.Text.Length;
            int text2Length = TextBox2.Text.Length;

            UpdateStatus($"Текст 1: {text1Length} символов | Текст 2: {text2Length} символов");
        }

        private void UpdateCloseButtonState()
        {
            // DataTrigger будет автоматически менять стиль кнопки
            CloseButton.IsEnabled = string.IsNullOrEmpty(TextBox1.Text) && string.IsNullOrEmpty(TextBox2.Text);
        }

        private void FontStyleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontStyleComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string styleTag = selectedItem.Tag.ToString();
                ApplyFontStyle(styleTag);
            }
        }

        private void ColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ColorComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string colorTag = selectedItem.Tag.ToString();
                ApplyTextColor(colorTag);
            }
        }

        private void ApplyFontStyle(string styleTag)
        {
            switch (styleTag)
            {
                case "Normal":
                    ApplyTextSettings("Arial", 12, FontWeights.Normal);
                    UpdateStatus("Применен обычный стиль шрифта");
                    break;
                case "Header":
                    ApplyTextSettings("Times New Roman", 16, FontWeights.Bold);
                    UpdateStatus("Применен стиль заголовка");
                    break;
                case "Code":
                    ApplyTextSettings("Consolas", 11, FontWeights.Normal);
                    UpdateStatus("Применен стиль кода");
                    break;
                case "Highlight":
                    ApplyTextSettings("Verdana", 14, FontWeights.SemiBold);
                    UpdateStatus("Применен выделенный стиль");
                    break;
            }
        }

        private void ApplyTextColor(string colorTag)
        {
            Color color = Colors.Black;

            switch (colorTag)
            {
                case "Black": color = Colors.Black; break;
                case "Blue": color = Colors.Blue; break;
                case "Red": color = Colors.Red; break;
                case "Green": color = Colors.Green; break;
                case "Purple": color = Colors.Purple; break;
            }

            TextBox1.Foreground = new SolidColorBrush(color);
            TextBox2.Foreground = new SolidColorBrush(color);

            UpdateStatus($"Применен цвет текста: {colorTag}");
        }

        private void ApplyTextSettings(string fontFamily, double fontSize, FontWeight fontWeight)
        {
            TextBox1.FontFamily = new FontFamily(fontFamily);
            TextBox2.FontFamily = new FontFamily(fontFamily);

            TextBox1.FontSize = fontSize;
            TextBox2.FontSize = fontSize;

            TextBox1.FontWeight = fontWeight;
            TextBox2.FontWeight = fontWeight;
        }

        private void UpdateStatus(string message)
        {
            StatusText.Text = $"Статус: {message}";
        }

        private void ExitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}