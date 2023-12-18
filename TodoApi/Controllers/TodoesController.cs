using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Services.Interfaces;
using static TodoApi.Models.Enums;

namespace TodoApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class TodoesController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoesController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // GET: Todoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
        {
            var todos = await _todoService.GetAllTodosAsync();
            return Ok(todos);
        }

        // GET: Todoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
            var todo = await _todoService.GetTodoByIdAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        // PUT: Todoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo(int id, TodoRequest todoRequest)
        {
            var success = await _todoService.UpdateTodoAsync(id, todoRequest);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: Todoes
        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodo(TodoRequest todoRequest)
        {
            var createdTodo = await _todoService.CreateTodoAsync(todoRequest);

            return CreatedAtAction(nameof(GetTodo), new { id = createdTodo.Id }, createdTodo);
        }

        // DELETE: Todoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var success = await _todoService.DeleteTodoAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        // GET: Todoes/status/{status}
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodosByStatus(Status status)
        {
            var todos = await _todoService.GetTodosByStatusAsync(status);
            return Ok(todos);
        }

        // GET: Todoes/due-date/{dueDate}
        [HttpGet("due-date/{dueDate}")]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodosByDueDate(DateTime dueDate)
        {
            var todos = await _todoService.GetTodosByDueDateAsync(dueDate);
            return Ok(todos);
        }

        // Additional action to complete multiple tasks
        [HttpPost("complete-multiple")]
        public async Task<IActionResult> CompleteMultipleTasks(List<int> taskIds)
        {
            var success = await _todoService.CompleteMultipleTasksAsync(taskIds);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
