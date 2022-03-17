using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShop2022.Controllers;
using OnlineShop2022.Data;
using OnlineShop2022.Models;
using System;
using Xunit;

namespace OnlineShopUnitTests
{
    public class ShoppingCartControllerTests
    {
        //Preparing necessary variables/props to implement a controller.
        private readonly IProductRepository _ProductRepository;
        private ShoppingCartModel _shoppingCart;
        private AppDbContext _context;

        private void CreateMockCart()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            _context = new AppDbContext(options); //Creates a new db using the listed options above.
            _context.Database.EnsureCreated(); //Ensures the db is created before continuing.

            _shoppingCart = new ShoppingCartModel(_context);
        }

        [Fact]
        public void ShoppingCartControllerIndexAction_NotNull()
        {
            //Testing to see if the Home Controller Index Action does not return null...
            //Arrange Test
            CreateMockCart();
            var controller = new ShoppingCartController(_ProductRepository, _shoppingCart);

            //Act
            var result = controller.Index();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ShoppingCartControllerIndexAction_ReturnsToView()
        {
            //Testing - Home Controller Index Action returns a View Model
            //Arrange Test
            CreateMockCart();
            var controller = new ShoppingCartController(_ProductRepository, _shoppingCart);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ShoppingCartControllerClearCart_ReturnsToIndex()
        {
            //Testing - Home Controller Index Action returns a View Model
            //Arrange Test
            CreateMockCart();
            var controller = new ShoppingCartController(_ProductRepository, _shoppingCart);

            //Act
            var result = controller.ClearCart();

            //Assert
            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
