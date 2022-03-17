using Microsoft.EntityFrameworkCore;
using OnlineShop2022.Data;
using OnlineShop2022.Areas.Admin;
using OnlineShop2022.Models;
using System;
using System.Linq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OnlineShopUnitTests
{
    public class CategoryControllerTests
    {
        //Preparing necessary variables/props to implement a controller.
        private AppDbContext _db;

        private void CreateMockDb()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            _db = new AppDbContext(options); //Creates a new db using the listed options above.
            _db.Database.EnsureCreated(); //Ensures the db is created before continuing.
        }

        [Fact]
        public void CategoryControllerIndexAction_IsNotNull()
        {
            //Tests - ensures the Category Controller Index action is not null
            //Arrange Test
            CreateMockDb();
            var controller = new CategoryController(_db);

            //Act
            var result = controller.Index();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CategoryControllerEditAction_ReturnNotFoundIfIDNull()
        {
            //Tests - Edit action, if ID is null, return NotFound()
            CreateMockDb();
            var controller = new CategoryController(_db);

            //Act
            var result = await controller.Edit(null);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CategoryControllerCreateAction_CreateCategory()
        {
            //Tests - Create Action, add a new category
            CreateMockDb();
            var controller = new CategoryController(_db);

            //Act
            CategoryModel cat = new CategoryModel();
            cat.Id = 3;
            cat.Name = "Test Category";
            var result = await controller.Create(cat);

            //Assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task CategoryControllerDetailsAction_ReturnsCategoryIfIDValid()
        {
            //Tests - Details Action, if the ID is valid, returns Category Details.
            CreateMockDb();
            var controller = new CategoryController(_db); //Controller
            CategoryModel cat = new CategoryModel();
            cat.Id = 3;
            cat.Name = "Test Category";
            var result = await controller.Create(cat); //Creating the category first....
            Assert.IsType<RedirectToActionResult>(result);

            //Act
            var result2 = await controller.Details(3); //Testing the Details Action...

            //Assert
            Assert.NotNull(result2);
            Assert.IsType<ViewResult>(result2); //Checking if the returned result is a ViewResult
        }

        [Fact]
        public async Task CategoryControllerDeleteAction_ReturnsNotFoundIfIDNull()
        {
            //Test - Call the delete method, with no ID, which should return a NotFound result
            CreateMockDb();
            var controller = new CategoryController(_db);

            //Act
            var result = await controller.Delete(null);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CategoryControllerDeleteAction_DeleteIfIDIsValid()
        {
            //Tests - Details Action, Create a Category, then delete it.
            CreateMockDb();
            var controller = new CategoryController(_db); //Controller
            CategoryModel cat = new CategoryModel();
            cat.Id = 3;
            cat.Name = "Test Category";
            var result = await controller.Create(cat);

            var deleteResult = await controller.DeleteConfirmed(3);
            Assert.IsType<RedirectToActionResult>(deleteResult);
        }
    }
}
