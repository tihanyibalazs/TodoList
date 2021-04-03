using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.ViewModel;

namespace TodoList.Models
{
    public interface ITodoListService
    {
        Task<List<TodoViewModel>> UserTodoList(int currentId, int currentPageNumber, int todoNumberOnOnePage);
        Task<Boolean> CreateNewTodoAsync(int currentId, TodoCreateViewModel todoViewModel);
        Task<Boolean> MakeTodoItemDoneAsync(int id);
        Task<Boolean> TodoMadeByIdAsync(int userId, int todoId);
        Task<int> MaxPageNumberAsync(int userId, int pageElementNumber);
    }
}
