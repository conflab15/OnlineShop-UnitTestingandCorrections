using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShop2022.Areas.Admin;
using OnlineShop2022.Controllers;
using OnlineShop2022.Data;
using OnlineShop2022.Helpers;
using OnlineShop2022.Models;
using System;
using System.Linq;
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
            var controller = new ProductController(_context, _webHostEnv, _imageHelp);

            //Act 
            var result = await controller.Update(null);

            //Assert
            Assert.IsType<NotFoundResult>(result); //Pass if the result is NotFound
        }

        [Fact(Skip = "This doesn't work, do not run!")] //This is currently a mess
        public void ProductControllerCreateAction_RedirectToActionIfSuccessful()
        {
            //Test - if product is successfully added, a RedirectToAction index is returned
            //Arrange Test
            var controller = new ProductController(_context, _webHostEnv, _imageHelp);
            var catController = new CategoryController(_context);
            var category = new CategoryModel { Id = 50, Name = "TestCategory" }; //New Category
            var addCat = catController.Create(category);
            var product = new ProductModel
            {
                Id = 20,
                Description = "Test Product",
                Price = 10.99,
                Colour = "Blue",
                CategoryId = 50,
                Category = category
            };

            var viewModel = new ProductViewModel
            {
                Categories = _context.Categories.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                Product = product
            };
            
        }

    }
}