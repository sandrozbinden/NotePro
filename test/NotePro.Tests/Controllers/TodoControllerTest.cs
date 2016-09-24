using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotePro.Controllers;
using NotePro.Data;
using Xunit;
using System.Net;

namespace NotePro.Tests.Controllers
{
    public class TodoControllerTest
    {
        private IServiceProvider _serviceProvider;

        public TodoControllerTest()
        {
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("NoteProTest"));
            _serviceProvider = services.BuildServiceProvider();

        }

        [Fact]
        public void ShowNonExistingItem()
        {
            var controller = new TodoController(null, _serviceProvider.GetService<ApplicationDbContext>());
            var result = (StatusCodeResult) controller.Edit(1);
            Assert.Equal(((int)HttpStatusCode.NotFound), result.StatusCode);
        }
    }
}
