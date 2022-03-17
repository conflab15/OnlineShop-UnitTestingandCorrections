using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop2022.Controllers;
using OnlineShop2022.Data;
using OnlineShop2022.Models;
using System;
using System.Threading.Tasks;
using Xunit;


namespace OnlineShopUnitTests
{
    public class ViewOrdersControllerTests
    {
        //Preparing necessary variables/props to implement a controller.
        //private ILogger<HomeController> _logger;
        private AppDbContext _context;
        private readonly UserManager<CustomUserModel> _userManager;

        private void CreateMockDb()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            _context = new AppDbContext(options); //Creates a new db using the listed options above.
            _context.Database.EnsureCreated(); //Ensures the db is created before continuing.
        }

        [Fact]
        public async Task ViewOrdersControllerDetailsAction_NoIDReturnsNotFound()
        {
            //Testing to see if the Home Controller Index Action does not return null...
            //Arrange Test
            CreateMockDb();
            var controller = new ViewOrdersController(_context, _userManager);

            //Act
            var result = await controller.Details(null);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
