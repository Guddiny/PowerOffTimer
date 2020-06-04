using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PowerOffTimer.Views
{
    public class TimeItem : UserControl
    {
        public TimeItem()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
