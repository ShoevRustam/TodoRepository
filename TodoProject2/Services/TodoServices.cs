using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoProject2.Data;
using TodoProject2.Models;

namespace TodoProject2.Services
{
    public class TodoServices : ITodoServices
    {
        private readonly ApplicationDbContext _context;
        public TodoServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TodoItem[]> GetIncompleteItemsAsync()
        {
            var item = await _context.TodoItems.Where(x => x.IsDone == false).ToArrayAsync();
            return item;
        }
        public async Task<bool> AddItemAsync(TodoItem newItem)
        {
            newItem.Id = Guid.NewGuid();
            newItem.IsDone = false;
            newItem.DueAt = DateTimeOffset.Now.AddDays(3);

            _context.TodoItems.Add(newItem);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> MarkDoneAsync(Guid id)
        {
            var item = await _context.TodoItems
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();

            if (item == null)
                return false;

            item.IsDone = true;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1; // One entity should have been updated
        }

        public List<TodoItem> GetAllTodos()
        {
            throw new NotImplementedException();
        }
    }
}
