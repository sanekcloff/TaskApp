using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.Models
{
    public enum Priority
    {
        None, Low, Medium, High, Extra
    }
    public class Objective
    {
        public Objective()
        {
            Title = string.Empty;
            Description = null;
            DateOfEnd = DateTime.Now.AddDays(1);
            IsExecuted = false;
            Priority = Priority.None;
            Category = new Category();
        }
        public Objective(string title, string? description, DateTime dateOfEnd, bool isExecuted, Priority priority, Category category)
        {
            Title = title;
            Description = description;
            DateOfEnd = dateOfEnd;
            IsExecuted = isExecuted;
            Priority = priority;
            Category = category;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DateOfEnd { get; set; }
        public bool IsExecuted { get; set; }
        public Priority Priority { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string IsExecutedText => IsExecuted ? "Выполнено" : "Не выполнено";
        public string PriorityText => Priority switch
        {
            Priority.Low => "Низкий",
            Priority.Medium => "Средний",
            Priority.High => "Высокий",
            Priority.Extra => "Экстремальный",
            _ => "Не выбран"
        };
        public override string ToString() => $"[{Id}] {Category.Title}/{Title} - ({Description}), Дата окончания: {DateOfEnd.ToString("d")}, Приоритет: {PriorityText}, Статус - {IsExecutedText}";

    }
}
