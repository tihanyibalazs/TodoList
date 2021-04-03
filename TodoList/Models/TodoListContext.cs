using Microsoft.EntityFrameworkCore;

namespace TodoList.Models
{
    public class TodoListContext : DbContext
    {
        public TodoListContext(DbContextOptions<TodoListContext> options)
        : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<TodoItem> Todos { get; set; }
    }
}
