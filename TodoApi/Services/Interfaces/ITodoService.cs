using TodoApi.DTOs;
using TodoApi.Models;
using static TodoApi.Models.Enums;

namespace TodoApi.Services.Interfaces
{
    public interface ITodoService
    {
        
        Task<List<Todo>> GetAllTodosAsync();

        Task<Todo> GetTodoByIdAsync(int id);

        Task<bool> UpdateTodoAsync(int id, TodoRequest todoRequest);

        Task<Todo> CreateTodoAsync(TodoRequest todoRequest);

        Task<bool> DeleteTodoAsync(int id);

        Task<List<Todo>> GetTodosByStatusAsync(Status status);
        Task<List<Todo>> GetTodosByDueDateAsync(DateTime dueDate);
        Task<bool> CompleteMultipleTasksAsync(List<int> taskIds);

    }
}
