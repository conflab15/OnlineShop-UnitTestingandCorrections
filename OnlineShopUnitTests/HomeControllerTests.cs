using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.Logging;
using OnlineShop2022.Controllers;
using OnlineShop2022.Data;
using System;
using Xunit;

namespace OnlineShopUnitTests
{
    public class HomeControllerTests
    {
        //Preparing necessary variables/props to implement a controller.
        private ILogger<HomeController> _logger;
        private AppDbContext _context;

        private void CreateMockDb()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            _context = new AppDbContext(options); //Creates a new db using the listed options above.
            _context.Database.EnsureCreated(); //Ensures the db is created before continuing.
        }

        [Fact]
        public void HomeControllerIndexAction_NotNull()
        {
            //Testing to see if the Home Controller Index Action does not return null...
            //Arrange Test
            CreateMockDb();
            var controller = new HomeController(_logger, _context);

            //Act
            var result = controller.Index();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void HomeControllerProductAction_NotNull()
        {
            //Testing to see if the Home Controller Product Action does not return null...
            //Arrange - CreateMockDb isn't required here as one already exists...
            var controller = new HomeController(_logger, _context);

            //Act
            var result = controller.Products("1");
            
            //Assert
            Assert.NotNull(result);
        }
    }
}
