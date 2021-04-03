using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoList.Models;
using TodoList.ViewModel;

namespace TodoList.Controllers
{
    public class TodoController : Controller
    {
        protected readonly ITodoListService _service;
        protected readonly IAccountService _accountservice;
        public TodoController(ITodoListService service, IAccountService accountservice)
        {
            _service = service;
            _accountservice = accountservice;
        }

        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            if (!_accountservice.CurrentId.HasValue)
                return RedirectToAction("Login", "Account");

            if (pageNumber <= 1)
                pageNumber = 1;

            int maxPage = await _service.MaxPageNumberAsync(_accountservice.CurrentId.Value, 10);

            if (pageNumber > maxPage)
                pageNumber = maxPage;

            var model = await _service.UserTodoList(_accountservice.CurrentId.Value, pageNumber, 10);

            TempData["pageNumber"] = pageNumber;

            return View(model);
        }

        public async Task<IActionResult> Done(int id)
        {
            if (!_accountservice.CurrentId.HasValue)
                return RedirectToAction("Login", "Account");

            if (!await _service.TodoMadeByIdAsync(_accountservice.CurrentId.Value, id))
                return RedirectToAction("Login", "Account");

            await _service.MakeTodoItemDoneAsync(id);

            return RedirectToAction("Index", new { pageNumber = TempData.Peek("pageNumber") });
        }

        public IActionResult Create()
        {
            if (!_accountservice.CurrentId.HasValue)
                return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoCreateViewModel model)
        {
            if (!_accountservice.CurrentId.HasValue)
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                if (await _service.CreateNewTodoAsync(_accountservice.CurrentId.Value, model))
                    ViewBag.Error = "Todo created";
                else
                    ViewBag.Errror = "Creation failed";

                return RedirectToAction("Index", "Todo");
            }

            return View(model);
        }

    }
}
