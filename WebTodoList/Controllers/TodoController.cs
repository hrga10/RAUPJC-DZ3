using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoRepository;
using WebTodoList.Models;

namespace WebTodoList.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ITodoRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        // Inject user manager into constructor
        public TodoController(ITodoRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            List<TodoItem> list = _repository.GetActive(Guid.Parse(currentUser.Id));

            return View(list);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTodoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            Guid userId = Guid.Parse(currentUser.Id);
            
            TodoItem item = new TodoItem(model.Text, userId);
            _repository.Add(item);

            // pocetna stranica
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Completed()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            Guid userId = Guid.Parse(currentUser.Id);

            var completed = _repository.GetCompleted(userId);

            return View(completed);
        }

        public async Task<IActionResult> MarkAsCompleted(Guid itemId)
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            Guid userId = Guid.Parse(currentUser.Id);

            _repository.MarkAsCompleted(itemId, userId);

            // pocetna stranica
            return RedirectToAction("Index");
        }

    }
}
