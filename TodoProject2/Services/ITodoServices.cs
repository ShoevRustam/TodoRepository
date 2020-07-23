using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoProject2.Models;

namespace TodoProject2.Services
{
    public interface ITodoServices
    {
        Task<TodoItem[]> GetIncompleteItemsAsync();
        Task<bool> AddItemAsync(TodoItem newItem);
        Task<bool> MarkDoneAsync(Guid id);
        List<TodoItem> GetAllTodos();
    }
}
