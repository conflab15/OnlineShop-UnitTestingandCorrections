using Microsoft.EntityFrameworkCore;
using OnlineShop2022.Data;
using OnlineShop2022.Areas.Admin;
using OnlineShop2022.Models;
using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OnlineShopUnitTests
{
    public class CategoryControllerTests
    {
        //Preparing necessary variables/props to implement a controller.
        private AppDbContext _context;

        private void CreateMockDb()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            _context = new AppDbContext(options); //Creates a new db using the listed options above.
            _context.Database.EnsureCreated(); //Ensures the db is created before continuing.
        }

        [Fact]
        public void CategoryControllerIndexAction_NotNull()
        {
            //Tests - ensures the Category Controller Index action is not null
            //Arrange Test
            CreateMockDb();
            var controller = new CategoryController(_context);

            //Act
            var result = controller.Index();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CategoryControllerEditAction_ReturnNotFoundIfIDNull()
        {
            //Tests - Edit action, if ID is null, return NotFound()
            var controller = new CategoryController(_context);

            //Act
            var result = await controller.Edit(null);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
