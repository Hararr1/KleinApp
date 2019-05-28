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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KleinMessage.Views
{
    /// <summary>
    /// Logika interakcji dla klasy LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }
        private void ClickEvent(object sender, RoutedEventArgs e)
        {

            TextBox textBox = sender as TextBox;
            textBox.Text = string.Empty;
            UsernameLabel.Background = new SolidColorBrush(Color.FromRgb(41, 216, 144));
            PasswordLabel.Background = new SolidColorBrush(Colors.Black);
        }

        private void ClickEventPassword(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            passwordBox.Clear();
            PasswordLabel.Background = new SolidColorBrush(Color.FromRgb(41, 216, 144));
            UsernameLabel.Background = new SolidColorBrush(Colors.Black);
        }

     

       
    }
}
