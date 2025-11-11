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
using TaskApp.Context;
using TaskApp.Models;

namespace TaskApp.Views
{
    /// <summary>
    /// Логика взаимодействия для ObjectiveManageWindow.xaml
    /// </summary>
    public partial class ObjectiveManageWindow : Window
    {
        private bool _isAdd;
        private List<string> _priorities;
        private List<Category> _categories;
        private Objective _objective;
        private AppDbContext _ctx;

        // инициализациия значений в переменных и определение контекста для окна исходя из значения присланого в параметр objective
        public ObjectiveManageWindow(Objective? objective, AppDbContext ctx)
        {
            InitializeComponent();
            _ctx = ctx;
            _categories = ctx.Categories.ToList();
            _priorities = new List<string>()
            {
                "Не выбран","Низкий","Средний","Высокий","Экстримальный"
            };

            CategoryComboBox.ItemsSource = _categories;
            PriorityComboBox.ItemsSource = _priorities;
            if (objective == null)
            {
                _isAdd = true;
                _objective = new Objective();
                ActionButton.Content = "Добавить";
                Title = "Добавление задачи";
            }
            else
            {
                _isAdd = false;
                _objective = objective;
                ActionButton.Content = "Редактировать";
                Title = "Редактирование задачи";

                TitleTextBox.Text = objective.Title;
                DescriptionTextBox.Text = objective.Description;
                DateOfEndDatePicker.SelectedDate = objective.DateOfEnd;
                IsExecutedCheckBox.IsChecked = objective.IsExecuted;
                CategoryComboBox.SelectedValue = objective.Category;
                switch (objective.Priority)
                {
                    case Priority.None:
                        PriorityComboBox.SelectedIndex = 0;
                        break;
                    case Priority.Low:
                        PriorityComboBox.SelectedIndex = 1;
                        break;
                    case Priority.Medium:
                        PriorityComboBox.SelectedIndex = 2;
                        break;
                    case Priority.High:
                        PriorityComboBox.SelectedIndex = 3;
                        break;
                    case Priority.Extra:
                        PriorityComboBox.SelectedIndex = 4;
                        break;
                }
            }
        }
        // метод для добавления\редактирования задачи в бд в зависимости от контекста использования окна
        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isAdd)
            { 
                _objective.Title = TitleTextBox.Text;
                _objective.Description = DescriptionTextBox.Text;
                _objective.IsExecuted = IsExecutedCheckBox.IsChecked!.Value;
                _objective.DateOfEnd = DateOfEndDatePicker.SelectedDate!.Value;
                _objective.CategoryId = ((Category)CategoryComboBox.SelectedValue).Id;
                _objective.Priority = (Priority)PriorityComboBox.SelectedIndex;

                // так как создавался новый экземпляр то вызываем метод Add
                _ctx.Objectives.Add(_objective);
                _ctx.SaveChanges();
                this.Close();
            }
            else
            {
                _objective.Title = TitleTextBox.Text;
                _objective.Description = DescriptionTextBox.Text;
                _objective.IsExecuted = IsExecutedCheckBox.IsChecked!.Value;
                _objective.DateOfEnd = DateOfEndDatePicker.SelectedDate!.Value;
                _objective.Category = (Category)CategoryComboBox.SelectedValue;
                _objective.Priority = (Priority)PriorityComboBox.SelectedIndex;

                // просто вызываем сохранение изменений так как в objectivew находится экземпляр изз бд
                _ctx.SaveChanges();
                this.Close();
            }
        }
    }
}
