using System.Windows;
using System.Windows.Controls;

namespace _8LAB
{
    public partial class TextEditor : Window
    {
        public TextEditor()
        {
            InitializeComponent();
        }

        // Назад — возвращаемся в главное окно
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        // Универсальная функция: ищет TextBox внутри того же StackPanel
        private TextBox GetParentTextBox(object sender)
        {
            if (sender is Button button)
            {
                var parentPanel = button.Parent as Panel;
                if (parentPanel != null && parentPanel.Parent is Panel outerPanel)
                {
                    foreach (var child in outerPanel.Children)
                    {
                        if (child is TextBox tb)
                            return tb;
                    }
                }
            }
            return null;
        }

        // Копировать
        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            TextBox tb = GetParentTextBox(sender);
            if (tb != null && !string.IsNullOrEmpty(tb.SelectedText))
            {
                Clipboard.SetText(tb.SelectedText);
            }
            else if (tb != null && !string.IsNullOrEmpty(tb.Text))
            {
                Clipboard.SetText(tb.Text);
            }
        }

        // Вырезать
        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            TextBox tb = GetParentTextBox(sender);
            if (tb != null && !string.IsNullOrEmpty(tb.SelectedText))
            {
                Clipboard.SetText(tb.SelectedText);
                tb.SelectedText = string.Empty;
            }
            else if (tb != null && !string.IsNullOrEmpty(tb.Text))
            {
                Clipboard.SetText(tb.Text);
                tb.Clear();
            }
        }

        // Вставить
        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            TextBox tb = GetParentTextBox(sender);
            if (tb != null && Clipboard.ContainsText())
            {
                int caretIndex = tb.CaretIndex;
                string pasteText = Clipboard.GetText();
                tb.Text = tb.Text.Insert(caretIndex, pasteText);
                tb.CaretIndex = caretIndex + pasteText.Length;
            }
        }
    }
}
