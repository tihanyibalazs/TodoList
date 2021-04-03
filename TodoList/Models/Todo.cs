using System;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public DateTime TodoDate { get; set; }
        [Required]
        public bool Done { get; set; }
        [Required]
        public User User { get; set; }
        public TodoItem() { }
        public TodoItem(string name, DateTime creationDate, DateTime todoDate, bool done, User user)
        {
            Name = name;
            CreationDate = creationDate;
            TodoDate = todoDate;
            Done = done;
            User = user;
        }

    }
}
