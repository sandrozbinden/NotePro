using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotePro.Controllers;
using NotePro.Data;
using Xunit;
using System.Net;
using Microsoft.AspNetCore.Http;
using NotePro.Models.TodoViewModels;
using NotePro.ExtensionMethods;
using NotePro.Models;

namespace NotePro.Tests.Controllers
{
    public class TodoControllerTest
    {
        private readonly IServiceProvider _serviceProvider;

        public TodoControllerTest()
        {
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("NoteProTest"));
            _serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public void CreateTodoItem()
        {
            //arrange
            var controller = new TodoController(null, _serviceProvider.GetService<ApplicationDbContext>());
            var model = new Todo() { Title = "Cleaning", Text = "Clean Kitchen", Priority = 3, FinishDate = DateTime.Now };
            
            //act
            var result = (RedirectToActionResult)controller.Create(model);

            //assert
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public void CreateTodoItemInvalidModelState()
        {
            //arrange
            var controller = new TodoController(null, _serviceProvider.GetService<ApplicationDbContext>());
            var model = new Todo() { Title = "Cleaning", Text = "Clean Kitchen", FinishDate = DateTime.Now };
            controller.ModelState.AddModelError("", "Error");

            //act
            var result = (StatusCodeResult) controller.Create(model);

            //assert
            Assert.Equal(((int)HttpStatusCode.BadRequest), result.StatusCode);
        }

        [Fact]
        public void ShowNonExistingItem()
        {
            //arrange
            var dbContext = _serviceProvider.GetService<ApplicationDbContext>();
            dbContext.DeleteAll<Todo>();
            dbContext.SaveChanges();
            var controller = new TodoController(null, dbContext);

            //act
            var result = (StatusCodeResult)controller.Edit(1);

            //assert
            Assert.Equal(((int)HttpStatusCode.NotFound), result.StatusCode);
        }
    }
}
