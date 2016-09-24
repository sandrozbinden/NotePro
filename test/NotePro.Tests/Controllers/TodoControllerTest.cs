using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotePro.Controllers;
using NotePro.Data;
using Xunit;
using System.Net;
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
        public void CreateItem()
        {
            var controller = new TodoController(null, _serviceProvider.GetService<ApplicationDbContext>());
            var model = new TodoViewModel() { Title = "Cleaning", Text = "Clean Kitchen", Priority = 3, FinishDate = DateTime.Now };
            var result = (RedirectToActionResult)controller.Create(model);
            Assert.Equal("List", result.ActionName);
        }

        [Fact]
        public void ShowNonExistingItem()
        {
            var dbContext = _serviceProvider.GetService<ApplicationDbContext>();
            dbContext.DeleteAll<Todo>();
            dbContext.SaveChanges();
            var controller = new TodoController(null, dbContext);
            var result = (StatusCodeResult)controller.Edit(1);
            Assert.Equal(((int)HttpStatusCode.NotFound), result.StatusCode);
        }
    }
}
