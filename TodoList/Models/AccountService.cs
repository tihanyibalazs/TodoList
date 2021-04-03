using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TodoList.ViewModel;

namespace TodoList.Models
{
    public class AccountService : IAccountService
    {
        private readonly TodoListContext _context;
        private readonly HttpContext _httpContext;

        public AccountService(TodoListContext context, IHttpContextAccessor httpcontext)
        {
            _context = context;
            _httpContext = httpcontext.HttpContext;
        }

        public int? CurrentId => _httpContext.Session.GetInt32("UserId");

        public async Task<bool> LoginAsync(LoginViewModel vm)
        {
            var user = await _context.Users.Where(x => x.UserName == vm.UserName).FirstOrDefaultAsync();

            if (user == null)
                return false;

            Byte[] passwordBytes = null;
            using (SHA512CryptoServiceProvider provider = new SHA512CryptoServiceProvider())
            {
                passwordBytes = provider.ComputeHash(Encoding.UTF8.GetBytes(vm.Password));
            }

            if (!passwordBytes.SequenceEqual(user.Password))
                return false;

            _httpContext.Session.SetInt32("UserId", user.Id);

            return true;
        }

        public bool LogOut()
        {
            _httpContext.Session.Remove("UserId");
            return true;
        }
    }
}
