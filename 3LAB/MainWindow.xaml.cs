using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace _3LAB
{
    // Интерфейс действия для Undo/Redo
    interface IAction
    {
        /// <summary>
        /// Выполнить операцию отмены (undo). При этом метод должен положить обратное действие в стек redoStack.
        /// </summary>
        void ApplyUndo(InkCanvas canvas, Stack<IAction> redoStack);

        /// <summary>
        /// Выполнить операцию повтора (redo). При этом метод должен положить обратное действие в стек undoStack.
        /// </summary>
        void ApplyRedo(InkCanvas canvas, Stack<IAction> undoStack);
    }

    // Действие: добавлен штрих (рисунок)
    class AddStrokeAction : IAction
    {
        public Stroke Stroke { get; }

        public AddStrokeAction(Stroke s) => Stroke = s;

        public void ApplyUndo(InkCanvas canvas, Stack<IAction> redoStack)
        {
            // Удаляем штрих с холста (undo) и кладём сюда же AddStrokeAction в redo — чтобы redo восстановил его
            canvas.Strokes.Remove(Stroke);
            // Для повтора требуется заново добавить тот же штрих
            redoStack.Push(new AddStrokeAction(Stroke));
        }

        public void ApplyRedo(InkCanvas canvas, Stack<IAction> undoStack)
        {
            // Повтор — добавляем штрих и помещаем обратно undo-действие
            canvas.Strokes.Add(Stroke);
            undoStack.Push(new AddStrokeAction(Stroke));
        }
    }

    // Действие: очистка холста (удалены множество штрихов)
    class ClearAction : IAction
    {
        public List<Stroke> Strokes { get; }

        public ClearAction(IEnumerable<Stroke> strokes)
        {
            // Сохраняем список штрихов (те же объекты Stroke)
            Strokes = strokes.ToList();
        }

        public void ApplyUndo(InkCanvas canvas, Stack<IAction> redoStack)
        {
            // Восстанавливаем все штрихи на холст (в том же порядке)
            foreach (var s in Strokes)
                canvas.Strokes.Add(s);
            // Для redo нужно снова удалить эти штрихи
            redoStack.Push(new ClearAction(Strokes));
        }

        public void ApplyRedo(InkCanvas canvas, Stack<IAction> undoStack)
        {
            // Повтор очистки: удаляем те штрихи, которые есть
            foreach (var s in Strokes)
                canvas.Strokes.Remove(s);
            // Для undo — восстановление этих штрихов
            undoStack.Push(new ClearAction(Strokes));
        }
    }

    public partial class MainWindow : Window
    {
        private Brush _previousBackground = null;
        private Brush _tempBackground = null;

        // Новая модель: храним истории как стеки действий
        private readonly Stack<IAction> _undoActions = new();
        private readonly Stack<IAction> _redoActions = new();

        public MainWindow()
        {
            InitializeComponent();

            // Защиты: inkCanvas может быть null до InitializeComponent, но здесь мы уже после инициализации
            if (inkCanvas != null)
            {
                var da = inkCanvas.DefaultDrawingAttributes;
                da.Color = Colors.Black;
                da.Width = 3;
                da.Height = 3;

                inkCanvas.StrokeCollected += InkCanvas_StrokeCollected;
            }

            StatusText.Text = "Готов к работе";
        }

        // При добавлении штриха — фиксируем AddStrokeAction и очищаем redoStack
        private void InkCanvas_StrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            // e.Stroke уже добавлен в inkCanvas.Strokes к моменту события
            _undoActions.Push(new AddStrokeAction(e.Stroke));
            _redoActions.Clear();
        }

        // Undo
        private void UndoAction()
        {
            if (_undoActions.Count == 0) return;

            var action = _undoActions.Pop();
            action.ApplyUndo(inkCanvas, _redoActions);
            StatusText.Text = "Отмена (Ctrl+Z)";
        }

        // Redo
        private void RedoAction()
        {
            if (_redoActions.Count == 0) return;

            var action = _redoActions.Pop();
            action.ApplyRedo(inkCanvas, _undoActions);
            StatusText.Text = "Повтор (Ctrl+Y)";
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (e.Key == Key.Z) { UndoAction(); e.Handled = true; }
                else if (e.Key == Key.Y) { RedoAction(); e.Handled = true; }
                else if (e.Key == Key.S) { SaveInkCanvasToPng(); StatusText.Text = "Рисунок сохранён (Ctrl+S)"; e.Handled = true; }
            }
        }

        private void miUndo_Click(object sender, RoutedEventArgs e) => UndoAction();
        private void miRedo_Click(object sender, RoutedEventArgs e) => RedoAction();

        // Меню/тулбар
        private void miChangeBackground_Click(object sender, RoutedEventArgs e)
        {
            _previousBackground = this.Background;
            PaletteBorder.Visibility = Visibility.Visible;
        }

        private void miSave_Click(object sender, RoutedEventArgs e)
        {
            SaveInkCanvasToPng();
        }

        private void miExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Главное: Clear теперь создаёт ClearAction и помещает в undoStack
        private void miClear_Click(object sender, RoutedEventArgs e)
        {
            if (inkCanvas == null) return;

            if (inkCanvas.Strokes.Count == 0) return;

            // Сохраняем все штрихи
            var saved = inkCanvas.Strokes.ToArray(); // массив текущих штрихов
            // Удаляем их с холста
            inkCanvas.Strokes.Clear();

            // Создаём действие "Clear" и пушим в undo; очищаем redo (новое действие)
            var clearAction = new ClearAction(saved);
            _undoActions.Push(clearAction);
            _redoActions.Clear();

            StatusText.Text = "Холст очищен";
        }

        private void miAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Разработчик: Воронин Г.А.\nГруппа: АСОИ-221\nЛабораторная работа №3 — WPF (InkCanvas, Undo/Redo, Палитра)", "О разработчике", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Палитра
        private void PaletteColor_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button b && b.Background is SolidColorBrush scb)
            {
                _tempBackground = scb;
                this.Background = _tempBackground;
            }
        }

        private void btnPaletteOk_Click(object sender, RoutedEventArgs e)
        {
            if (_tempBackground != null)
                this.Background = _tempBackground;
            PaletteBorder.Visibility = Visibility.Collapsed;
        }

        private void btnPaletteCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Background = _previousBackground;
            PaletteBorder.Visibility = Visibility.Collapsed;
        }

        // Управление кистью
        private void cbBrushColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (inkCanvas == null) return;

            if (cbBrushColor.SelectedItem is ComboBoxItem item && item.Tag is string tag)
            {
                Color c;
                try { c = (Color)ColorConverter.ConvertFromString(tag); }
                catch { c = Colors.Black; }
                inkCanvas.DefaultDrawingAttributes.Color = c;
            }
        }

        private void sliderBrushSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (inkCanvas == null) return;

            double v = e.NewValue;
            inkCanvas.DefaultDrawingAttributes.Width = v;
            inkCanvas.DefaultDrawingAttributes.Height = v;
            tbSizeValue.Text = ((int)v).ToString();
        }

        private void rbMode_Checked(object sender, RoutedEventArgs e)
        {
            if (inkCanvas == null) return;

            if (rbInk.IsChecked == true) inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            else if (rbSelect.IsChecked == true) inkCanvas.EditingMode = InkCanvasEditingMode.Select;
            else if (rbEraseByPoint.IsChecked == true) inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
            else if (rbEraseByStroke.IsChecked == true) inkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
        }

        // Сохранение в PNG
        private void SaveInkCanvasToPng()
        {
            if (inkCanvas == null) return;

            var dlg = new SaveFileDialog() { Filter = "PNG Image|*.png", FileName = "drawing.png" };
            if (dlg.ShowDialog(this) == true)
            {
                try
                {
                    var rtb = new RenderTargetBitmap(
                        (int)Math.Max(1, inkCanvas.ActualWidth),
                        (int)Math.Max(1, inkCanvas.ActualHeight),
                        96d, 96d, PixelFormats.Default);

                    var dv = new DrawingVisual();
                    using (var dc = dv.RenderOpen())
                    {
                        var vb = new VisualBrush(inkCanvas);
                        dc.DrawRectangle(vb, null, new Rect(new Point(), new Size(inkCanvas.ActualWidth, inkCanvas.ActualHeight)));
                    }
                    rtb.Render(dv);

                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(rtb));

                    using (var fs = File.OpenWrite(dlg.FileName))
                    {
                        encoder.Save(fs);
                    }

                    MessageBox.Show("Сохранено: " + dlg.FileName, "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Подсказки в статусе
        private void UIElement_MouseEnter(object sender, MouseEventArgs e)
        {
            string hint = "";
            if (sender is MenuItem mi)
            {
                hint = mi.Header?.ToString()?.Replace("_", "") ?? "";
                if (mi == miChangeBackground) hint = "Изменить цвет фона (палитра)";
                else if (mi == miSave) hint = "Сохранить рисунок (Ctrl+S) (PNG)";
                else if (mi == miExit) hint = "Выход из приложения";
                else if (mi == miClear) hint = "Очистить рисунок";
                else if (mi == miAbout) hint = "Информация о разработчике";
                else if (mi == miUndo) hint = "Отменить последнее действие (Ctrl+Z)";
                else if (mi == miRedo) hint = "Повторить отмененное действие (Ctrl+Y)";
            }
            else if (sender is Button btn)
            {
                switch (btn.Name)
                {
                    case "tbChangeBackground": hint = "Изменить фон окна"; break;
                    case "tbSave": hint = "Сохранить рисунок (Ctrl+S)"; break;
                    case "tbClear": hint = "Очистить рисунок"; break;
                    case "tbUndo": hint = "Отменить (Ctrl+Z)"; break;
                    case "tbRedo": hint = "Повторить (Ctrl+Y)"; break;
                }
            }
            StatusText.Text = hint;
        }

        private void UIElement_MouseLeave(object sender, MouseEventArgs e)
        {
            StatusText.Text = "";
        }
    }
}
