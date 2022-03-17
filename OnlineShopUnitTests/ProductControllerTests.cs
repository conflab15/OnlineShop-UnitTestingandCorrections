using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop2022.Areas.Admin;
using OnlineShop2022.Data;
using OnlineShop2022.Helpers;
using OnlineShop2022.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace OnlineShopUnitTests
{
    public class ProductControllerTests
    {
        //Preparing necessary variables/props to implement a controller.
        //private ILogger<HomeController> _logger;
        private AppDbContext _context;
        private IWebHostEnvironment _webHostEnv;
        private Images _imageHelp;

        private void CreateMockDb()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            _context = new AppDbContext(options); //Creates a new db using the listed options above.
            _context.Database.EnsureCreated(); //Ensures the db is created before continuing.
        }

        [Fact]
        public void ProductControllerIndexAction_NotNull()
        {
            //Testing to see if the Home Controller Index Action does not return null...
            //Arrange Test
            CreateMockDb();
            var controller = new ProductController(_context, _webHostEnv, _imageHelp);

            //Act
            var result = controller.Index();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GETProductControllerUpdate_NoIDReturnsNotFound()
        {
            //Test - if no ID is passed to update, return NotFound
            //Arrange
            CreateMockDb();
            var controller = new ProductController(_context, _webHostEnv, _imageHelp);

            //Act 
            var result = await controller.Update(null);

            //Assert
            Assert.IsType<NotFoundResult>(result); //Pass if the result is NotFound
        }

        [Fact]
        public void CreateProductControllerGET_ReturnsAViewModel()
        {
            //Test - when the GET Create action is called, return a ViewModel
            //Arrange
            CreateMockDb();
            var controller = new ProductController(_context, _webHostEnv, _imageHelp);

            //Act
            var result = controller.Create();

            //Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}