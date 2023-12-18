using Microsoft.EntityFrameworkCore;
using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Services.Interfaces;
using static TodoApi.Models.Enums;

namespace TodoApi.Services
{
    public class TodoService : ITodoService
    {
        private readonly MasterDBContext _context;

        public TodoService(MasterDBContext context)
        {
            _context = context;
        }

        public async Task<List<Todo>> GetAllTodosAsync()
        {
            return await _context.Todos.ToListAsync();
        }

        public async Task<Todo> GetTodoByIdAsync(int id)
        {
            return await _context.Todos.FindAsync(id);
        }

        public async Task<bool> UpdateTodoAsync(int id, TodoRequest todoRequest)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                return false;
            }

            todo.Description = todoRequest.Description;
            todo.Title = todoRequest.Title;
            todo.UserId = todoRequest.UserId;
            todo.Status = todoRequest.Status;
            todo.DueDate = todoRequest.DueDate;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<Todo> CreateTodoAsync(TodoRequest todoRequest)
        {
            Todo todo = new Todo
            {
                Description = todoRequest.Description,
                Title = todoRequest.Title,
                UserId = todoRequest.UserId,
                Status = todoRequest.Status,
                DueDate = todoRequest.DueDate
            };

            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            return todo;
        }

        public async Task<bool> DeleteTodoAsync(int id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                return false;
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Todo>> GetTodosByStatusAsync(Status status)
        {
            return await _context.Todos.Where(t => t.Status == status).ToListAsync();
        }

        public async Task<List<Todo>> GetTodosByDueDateAsync(DateTime dueDate)
        {
            return await _context.Todos.Where(t => t.DueDate == dueDate).ToListAsync();
        }

        public async Task<bool> CompleteMultipleTasksAsync(List<int> taskIds)
        {
            var tasks = await _context.Todos.Where(t => taskIds.Contains(t.Id)).ToListAsync();

            if (tasks.Count == 0)
            {
                return false; // No tasks found with the given IDs
            }

            foreach (var task in tasks)
            {
                task.Status = Status.Resolved;
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
