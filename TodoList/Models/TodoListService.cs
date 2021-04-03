using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.ViewModel;

namespace TodoList.Models
{
    public class TodoListService : ITodoListService
    {
        private readonly TodoListContext _context;

        public TodoListService(TodoListContext context)
        {
            _context = context;
        }

        public async Task<List<TodoViewModel>> UserTodoList(int currentId, int currentPageNumber, int todoNumberOnOnePage)
        {
            if (currentPageNumber < 1)
                currentPageNumber = 1;

            var list = await _context.Todos.OrderByDescending(x => x.Id).Where(x => x.User.Id == currentId)
                .Skip((currentPageNumber - 1) * todoNumberOnOnePage)
                .Take(todoNumberOnOnePage).ToListAsync();

            var viewModelList = new List<TodoViewModel>();

            foreach (TodoItem elem in list)
            {
                var newViewModel = new TodoViewModel(elem.Name, elem.TodoDate, elem.Done, elem.Id);
                viewModelList.Add(newViewModel);
            }
            return viewModelList;
        }

        public async Task<Boolean> CreateNewTodoAsync(int currentId, TodoCreateViewModel todoViewModel)
        {
            var user = await _context.Users.Where(x => x.Id == currentId).FirstOrDefaultAsync();
            var todo = new TodoItem(todoViewModel.Name, todoViewModel.TodoDate, DateTime.Now, false, user);

            _context.Todos.Add(todo);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<Boolean> MakeTodoItemDoneAsync(int id)
        {
            var todo = await _context.Todos.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (todo == null)
                return false;

            todo.Done = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<Boolean> TodoMadeByIdAsync(int userId, int todoId)
        {
            var todo = await _context.Todos.Where(x => x.Id == todoId).Include(x => x.User).FirstOrDefaultAsync();

            if (todo.User.Id != userId)
                return false;
            else
                return true;
        }

        public async Task<int> MaxPageNumberAsync(int userId, int pageElementNumber)
        {
            var maxElements = await _context.Todos.Where(x => x.User.Id == userId).CountAsync();

            var value = (maxElements / (double)pageElementNumber);
            value = Math.Ceiling(value);
            return (int)value;
        }
    }
}
