using System.Threading.Tasks;
using TodoList.ViewModel;

namespace TodoList.Models
{
    public interface IAccountService
    {
        Task<bool> LoginAsync(LoginViewModel vm);
        bool LogOut();
        public int? CurrentId { get; }
    }
}
