using Microsoft.EntityFrameworkCore;
using System.Windows;
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
        private void ReloadList()
        {
            ObjectivesListView.ItemsSource = _ctx.Objectives.Include(obj => obj.Category).ToList();
        }

        private void AddObjectiveButton_Click(object sender, RoutedEventArgs e)
        {
            new ObjectiveManageWindow(null,_ctx).ShowDialog();
            ReloadList();
        }

        private void ListViewItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            new ObjectiveManageWindow((Objective)ObjectivesListView.SelectedItem,_ctx).ShowDialog();
            ReloadList();
        }
    }
}
