using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Input;
using TaskApp.Context;
using TaskApp.Models;


namespace TaskApp.Views
{
    /// <summary>
    /// Логика взаимодействия для GeneralWindow.xaml
    /// </summary>
    public partial class GeneralWindow : Window
    {
        private readonly AppDbContext _ctx;
        public GeneralWindow()
        {
            InitializeComponent();
            _ctx = new AppDbContext();
            ReloadList();
        }
        //мето для присовения листу на форме значений из БД
        private void ReloadList()
        {
            ObjectivesListView.ItemsSource = _ctx.Objectives.Include(obj => obj.Category).ToList();
        }
        // метод для открытия окна добвления
        private void AddObjectiveButton_Click(object sender, RoutedEventArgs e)
        {
            new ObjectiveManageWindow(null, _ctx).ShowDialog();
            ReloadList();
        }

        // метод для открытия окна редактирования
        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new ObjectiveManageWindow((Objective)ObjectivesListView.SelectedItem, _ctx).ShowDialog();
            ReloadList();
        }

        private void ListViewItem_KeyDown(object sender, KeyEventArgs e)
        {
            var selectedItem = (Objective)ObjectivesListView.SelectedItem;
            if (e.Key == Key.Delete && selectedItem != null)
            {
                var result = MessageBox.Show("Действительно удалить?", "Подтвердить", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _ctx.Objectives.Remove(selectedItem);
                    _ctx.SaveChanges();
                    ReloadList();
                }
            }
        }
    }
}
