using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop2022.Areas.Admin.Controllers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace OnlineShopUnitTests
{
    public class RoleManagerControllerTests
    {
        //Preparing necessary variables/props to implement a controller.
        private readonly RoleManager<IdentityRole> _roleManager;

        [Fact]
        public void RoleManagerIndexAction_NotNull()
        {
            //Testing to see if the RoleManager Controller Index Action does not return null...
            //Arrange Test
            var controller = new RolesManagerController(_roleManager);

            //Act
            var result = controller.Index();

            //Assert
            Assert.NotNull(result);
        }
    }
}
