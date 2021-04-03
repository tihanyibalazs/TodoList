using System;

namespace TodoList.ViewModel
{
    public class TodoViewModel
    {
        public TodoViewModel(string name, DateTime todoDate, bool done, int id)
        {
            Name = name;
            TodoDate = todoDate;
            Done = done;
            Id = id;
        }

        public string Name { get; set; }
        public DateTime TodoDate { get; set; }
        public bool Done { get; set; }
        public int Id { get; set; }
    }
}
