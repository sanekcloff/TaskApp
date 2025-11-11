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
        // поле для записи вариантов тестов по ключу и значению в виде 2-х строк
        private Dictionary<string, string> _tests;
        // поле для сохранения рандомно выбраного текста
        private KeyValuePair<string, string> _selectedTest;
        // конструктор для инициализации словаря тестов и ответов, а так же для получения рандомного теста и ввода его условия в текстовое поле на форме
        public LoginWindow()
        {
            //метод для инициализации компонентов окна
            InitializeComponent();

            // инициализация словаря тестов
            _tests = new Dictionary<string, string>()
            {
                {"2 + 2 = ?", "4"},
                {"6 + 1 = ?", "7"},
                {"16 + (-19) = ?", "-3"},
                {"Как переводится слово Dog?", "собака"},
            };
            // выбор случайного теста
            _selectedTest = _tests.ElementAt(new Random().Next(_tests.Count));
            // вывод теста на текстовое поле на форме
            TestTextBlock.Text = _selectedTest.Key;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // сохраняем введённый ответ в переменную с приведением в нижний регистр
            var actual = KeyCodeTextBox.Text.ToLower();
            // сохраняем ответ из словаря в переменную с приведением в нижний регистр
            var result = _selectedTest.Value.ToLower();
            // проверка на равенство ответов
            if (actual == result)
            {
                // Если ответы совпали то выводим всплывающее окно и переходим на новое окно
                MessageBox.Show("Верно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                // сохраняем текущее главное окно приложения в переменную
                var tempWnd = Application.Current.MainWindow;
                // заменяем главное окно приложения на новое
                Application.Current.MainWindow = new GeneralWindow();
                // закрываем старое главное окно через переменнную
                tempWnd.Close();
                // открываем новое главное окно
                Application.Current.MainWindow.Show();
            }
            // Если ответы не совпали то выводим всплывающее окно с уведомление о неверном ответе
            else MessageBox.Show("Не верно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
