using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.Models
{
    public class Category
    {
        public Category() 
        { 
            Title = string.Empty;
            Description = null;
        }
        public Category(string title, string? description)
        {
            Title = title;
            Description = description;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        public override string ToString() => $"[{Id}] {Title} - {Description}";
    }
}
