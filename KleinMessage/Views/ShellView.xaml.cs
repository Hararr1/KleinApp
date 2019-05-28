using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KleinMessage.Views
{
    /// <summary>
    /// Logika interakcji dla klasy ShellView.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ChatButton_Click(object sender, RoutedEventArgs e)
        {
            chatLabel.Background = new SolidColorBrush(Color.FromRgb(41, 216, 144));
            searchLabel.Background = new SolidColorBrush(Colors.Black);
            settingsLabel.Background = new SolidColorBrush(Colors.Black);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            settingsLabel.Background = new SolidColorBrush(Color.FromRgb(41, 216, 144));
            searchLabel.Background = new SolidColorBrush(Colors.Black);
            chatLabel.Background = new SolidColorBrush(Colors.Black);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            searchLabel.Background = new SolidColorBrush(Color.FromRgb(41, 216, 144));
            chatLabel.Background = new SolidColorBrush(Colors.Black);
            settingsLabel.Background = new SolidColorBrush(Colors.Black);
        }
    }
}
