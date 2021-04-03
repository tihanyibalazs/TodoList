using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TodoList.Models;
using TodoList.ViewModel;

namespace TodoListTest
{
    public class TodoListxUnitTest : IDisposable
    {
        private TodoListContext _context;
        private TodoListService _service;

        readonly User User = new()
        {
            Id = 1,
            Name = "Name",
            UserName = "UserName"
        };

        readonly TodoCreateViewModel vm = new()
        {
            Name = "v",
            TodoDate = DateTime.Now.AddDays(10)
        };

        public TodoListxUnitTest()
        {
            var options = new DbContextOptionsBuilder<TodoListContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;
            _context = new TodoListContext(options);
            _context.Database.EnsureCreated();
            _service = new TodoListService(_context);

            _context.Users.Add(User);
            _context.SaveChanges();
        }

        [Fact]
        public void CreateNewTodoItem()
        {
            Assert.True(_service.CreateNewTodoAsync(1, vm).Result);
            Assert.Equal(1, _context.Todos.Count());
            var todo = _context.Todos.Where(x => x.Id == 1).Include(x => x.User).FirstOrDefault();
            Assert.Equal("v", todo.Name);
            Assert.Equal("Name", todo.User.Name);
        }

        [Fact]
        public void MakeTodoItemDone()
        {
            Assert.True(_service.CreateNewTodoAsync(1, vm).Result);
            Assert.True(_service.MakeTodoItemDoneAsync(1).Result);
            var todo = _context.Todos.Where(x => x.Id == 1).FirstOrDefault();
            Assert.True(todo.Done);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
