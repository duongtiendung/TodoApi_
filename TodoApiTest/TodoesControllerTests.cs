using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Controllers;
using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Services.Interfaces;

namespace TodoApiTest
{
    public class TodoesControllerTests
    {
        [Fact]
        public async Task GetTodos_ReturnsOk()
        {
            // Arrange
            var todoServiceMock = new Mock<ITodoService>();
            var todoesController = new TodoesController(todoServiceMock.Object);

            var todosList = new List<Todo>
            {
                // Initialize todos list
            };

            todoServiceMock.Setup(x => x.GetAllTodosAsync())
                           .ReturnsAsync(todosList);

            // Act
            var result = await todoesController.GetTodos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var todos = Assert.IsType<List<Todo>>(okResult.Value);
            Assert.Equal(todosList, todos);
        }

        [Fact]
        public async Task GetTodo_ValidId_ReturnsOk()
        {
            // Arrange
            var todoServiceMock = new Mock<ITodoService>();
            var todoesController = new TodoesController(todoServiceMock.Object);

            var todoId = 1;
            var todo = new Todo
            {
                // Initialize todo
            };

            todoServiceMock.Setup(x => x.GetTodoByIdAsync(todoId))
                           .ReturnsAsync(todo);

            // Act
            var result = await todoesController.GetTodo(todoId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTodo = Assert.IsType<Todo>(okResult.Value);
            Assert.Equal(todo, returnedTodo);
        }

        [Fact]
        public async Task GetTodo_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var todoServiceMock = new Mock<ITodoService>();
            var todoesController = new TodoesController(todoServiceMock.Object);

            var todoId = 1;

            todoServiceMock.Setup(x => x.GetTodoByIdAsync(todoId))
                           .ReturnsAsync((Todo)null);

            // Act
            var result = await todoesController.GetTodo(todoId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task UpdateTodo_ValidRequest_ReturnsNoContent()
        {
            // Arrange
            var todoServiceMock = new Mock<ITodoService>();
            var todoesController = new TodoesController(todoServiceMock.Object);

            var todoId = 1;
            var todoRequest = new TodoRequest
            {
                // Initialize todo request
            };

            todoServiceMock.Setup(x => x.UpdateTodoAsync(todoId, todoRequest))
                           .ReturnsAsync(true);

            // Act
            var result = await todoesController.PutTodo(todoId, todoRequest);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }


        [Fact]
        public async Task CompleteMultipleTasks_ValidRequest_ReturnsNoContent()
        {
            // Arrange
            var todoServiceMock = new Mock<ITodoService>();
            var todoesController = new TodoesController(todoServiceMock.Object);

            var taskIds = new List<int>
            {
                // Initialize task ids
            };

            todoServiceMock.Setup(x => x.CompleteMultipleTasksAsync(It.IsAny<List<int>>()))
                           .ReturnsAsync(true);

            // Act
            var result = await todoesController.CompleteMultipleTasks(taskIds);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}