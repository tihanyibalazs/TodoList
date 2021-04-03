using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TodoList.Models
{
    public class DbInitialize
    {
        private static TodoListContext _context;
        private static IConfiguration _configuration;
        public static void Initialize(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = serviceProvider.GetRequiredService<TodoListContext>();

            _context.Database.Migrate();

            if (_context.Users.Any())
            {
                return;
            }

            SeedDatabase();
        }

        public static void SeedDatabase()
        {
            Byte[] passwordBytes = null;
            using (SHA512CryptoServiceProvider provider = new SHA512CryptoServiceProvider())
            {
                passwordBytes = provider.ComputeHash(Encoding.UTF8.GetBytes("password123"));
            }

            var user1 = new User()
            {
                Name = "User Name",
                UserName = "UsNa",
                Password = passwordBytes
            };

            _context.Users.Add(user1);
            
            if(bool.Parse(_configuration["SeedTodoItems"]))
                SeedTodoItems(user1);

            _context.SaveChanges();
        }

        ///<summary>
        /// A loop to create more TodoItem
        ///</summary>
        ///<param name="user">This user gets 13 TodoItems</param>
        public static void SeedTodoItems(User user)
        {
            for (int i = 1; i < 13; i++)
            {
                var todo = new TodoItem()
                {
                    Name = "Todo " + i.ToString() + ".",
                    CreationDate = DateTime.Now,
                    Done = i % 2 == 0,
                    TodoDate = DateTime.Now.AddDays(20),
                    User = user
                };
                _context.Todos.Add(todo);
            }
        }
    }
}
