using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace PowerOffTimer.Views
{
    public class MainWindow : Window
    {
        public const int MinimumValue = 0;

        public const int MaximumValue = 99;

        public TextBox HoursTextBox { get; set; }

        public TextBox MinutesTextBox { get; set; }

        public TextBox SecondsTextBox { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            HoursTextBox = this.FindControl<TextBox>("Hours");
            MinutesTextBox = this.FindControl<TextBox>("Minutes");
            SecondsTextBox = this.FindControl<TextBox>("Seconds");

            HoursTextBox.PointerWheelChanged += HoursTextBox_PointerWheelChanged;
            MinutesTextBox.PointerWheelChanged += MinutesTextBox_PointerWheelChanged;
            SecondsTextBox.PointerWheelChanged += SecondsTextBox_PointerWheelChanged;

#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void SecondsTextBox_PointerWheelChanged(object sender, PointerWheelEventArgs e)
        {
            UpdateValue(e, SecondsTextBox);
        }

        private void MinutesTextBox_PointerWheelChanged(object sender, PointerWheelEventArgs e)
        {
            UpdateValue(e, MinutesTextBox);
        }

        private void HoursTextBox_PointerWheelChanged(object sender, PointerWheelEventArgs e)
        {
            UpdateValue(e, HoursTextBox);
        }

        private void UpdateValue(PointerWheelEventArgs e, TextBox textProperty)
        {
            if (e.Delta.Y > 0)
            {
                var newValue = int.Parse(textProperty.Text) + 1;

                textProperty.Text = newValue >= MaximumValue
                    ? MaximumValue.ToString()
                    : newValue.ToString();
            }

            if (e.Delta.Y < 0)
            {
                var newValue = int.Parse(textProperty.Text) - 1;

                textProperty.Text = newValue >= MinimumValue
                    ? newValue.ToString()
                    : MinimumValue.ToString();
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
