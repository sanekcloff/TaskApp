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

namespace TaskApp.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private Dictionary<string, string> _tests;
        private KeyValuePair<string, string> _selectedTest;
        public LoginWindow()
        {
            InitializeComponent();
            _tests = new Dictionary<string, string>()
            {
                {"2 + 2 = ?", "4"},
                {"6 + 1 = ?", "7"},
                {"16 + (-19) = ?", "-3"},
                {"Как переводится слово Dog?", "Собака"},
            };
            _selectedTest = _tests.ElementAt(new Random().Next(0, _tests.Count));
            TestTextBlock.Text = _selectedTest.Key;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (KeyCodeTextBox.Text == _selectedTest.Value)
            {
                MessageBox.Show("Верно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                var tempWnd = Application.Current.MainWindow;
                Application.Current.MainWindow = new GeneralWindow();
                tempWnd.Close();
                Application.Current.MainWindow.Show();
            }
            else MessageBox.Show("Не верно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
