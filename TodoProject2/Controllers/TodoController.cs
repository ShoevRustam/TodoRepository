using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoProject2.Models;
using TodoProject2.Services;

namespace TodoProject2.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoServices _todoServices;
        public TodoController(ITodoServices todoServices)
        {
            _todoServices = todoServices;
        }
        public async Task<IActionResult> Index()
        {
            var items = await _todoServices.GetIncompleteItemsAsync();
            var todomodel = new TodoViewModel
            {
                Items = items
            };
            return View(todomodel);
        }
        public async Task<IActionResult> AddItem(TodoItem newItem)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var success = await _todoServices.AddItemAsync(newItem);
            if (!success)
            {
                return BadRequest("Could not add item.");
            }
            return RedirectToAction("Index");
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkDone(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var successful = await _todoServices.MarkDoneAsync(id);
            if (!successful)
            {
                return BadRequest("Could not mark item as done.");
            }

            return RedirectToAction("Index");
        }
    }
}