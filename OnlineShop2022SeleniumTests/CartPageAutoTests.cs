using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace OnlineShop2022SeleniumTests
{
    public class CartPageAutoTests
    {
        IWebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            //This one time setup means that the set up is performed once to perform a numerous amount of tests under one setup
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://onlineshop2022.azurewebsites.net/Identity/Account/Login?ReturnUrl=%2F");
            var inputEmail = driver.FindElement(By.Id("Input_Email"));
            inputEmail.SendKeys("admin@admin.com");
            var inputPass = driver.FindElement(By.Id("Input_Password"));
            inputPass.SendKeys("Admin123!");
            inputPass.Submit();
        }


        [Test]
        public void CartPage_LoadEmptyCartShowsErrorMessage()
        {
            driver.Navigate().GoToUrl("https://onlineshop2022.azurewebsites.net/ShoppingCart");
            var title = driver.Title;
            Assert.AreEqual(title, "My Cart - OnlineShop2022"); //Ensures the page is correct before proceeding...
            var warningElement = driver.FindElement(By.ClassName("alert-heading"));
            var message = warningElement.Text;
            Assert.AreEqual(message, "Whoops!");
        }

        [Test]
        public void CartPage_AddProductToCart()
        {
            //Copied from a previous test, finding an item...
            driver.Navigate().GoToUrl("https://onlineshop2022.azurewebsites.net/Home/Products");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var title = driver.Title;
            Assert.AreEqual(title, "Products - OnlineShop2022"); //Ensures the page is correct before proceeding...
            var searchField = driver.FindElement(By.Name("SearchString"));
            searchField.SendKeys("Nike");
            searchField.Submit();
            var productTitle = driver.FindElement(By.Name("productDesc"));
            var text = productTitle.Text;
            Assert.AreEqual(text, "Nike Air Force Ones (Size 12)");
            var addBtn = driver.FindElement(By.Name("addToCartBtn"));
            addBtn.SendKeys(Keys.Enter);
            var newtitle = driver.Title;
            Assert.AreEqual(newtitle, "My Cart - OnlineShop2022");
        }

        [Test]
        public void CartPage_EmptyCart()
        {
            CartPage_AddProductToCart(); //Run this test to populate the basket
            driver.Navigate().GoToUrl("https://onlineshop2022.azurewebsites.net/ShoppingCart");
            var title = driver.Title;
            Assert.AreEqual(title, "My Cart - OnlineShop2022"); //Check page is correct before proceeding
            var emptyBtn = driver.FindElement(By.Name("emptyCart"));
            emptyBtn.SendKeys(Keys.Enter);
            var warningElement = driver.FindElement(By.ClassName("alert-heading"));
            var message = warningElement.Text;
            Assert.AreEqual(message, "Whoops!");
        }

        [Test]
        public void CartPage_RemoveOneItemFromCart()
        {
            //Performing this test requires two of the same product to be added to the basket first.
            CartPage_AddProductToCart(); //Run this test to populate the basket
            CartPage_AddProductToCart(); //Run this test to populate the basket with this item again, quantity now equals two.
            var title = driver.Title;
            Assert.AreEqual(title, "My Cart - OnlineShop2022"); //Check page is correct before proceeding
            var removeItemBtn = driver.FindElement(By.Name("removeItem"));
            removeItemBtn.SendKeys(Keys.Enter);
            var quantityFigure = driver.FindElement(By.ClassName("text-center"));
            var quantity = quantityFigure.Text;
            Assert.AreEqual(quantity, "1");
        }

        [Test]
        public void CartPage_ProceedToCheckout()
        {
            CartPage_AddProductToCart(); //Run this test to populate the basket
            var proceedBtn = driver.FindElement(By.Name("checkout"));
            proceedBtn.SendKeys(Keys.Enter);
            var title = driver.Title;
            Assert.AreEqual(title, "Checkout - OnlineShop2022");
        }

        [OneTimeTearDown]
        public void End()
        {
            driver.Close();
        }
    }
}
