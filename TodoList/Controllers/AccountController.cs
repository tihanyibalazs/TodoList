using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoList.Models;
using TodoList.ViewModel;

namespace TodoList.Controllers
{
    public class AccountController : Controller
    {
        protected readonly IAccountService _accountservice;

        public AccountController(IAccountService service)
        {
            _accountservice = service;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (await _accountservice.LoginAsync(vm))
                {
                    return RedirectToAction("Index", "Todo");
                }
                else
                {
                    ModelState.AddModelError("", "Login failed");
                }
            }
            return View(vm);
        }

        public IActionResult LogOut()
        {
            _accountservice.LogOut();

            return RedirectToAction("Login");
        }
    }
}
