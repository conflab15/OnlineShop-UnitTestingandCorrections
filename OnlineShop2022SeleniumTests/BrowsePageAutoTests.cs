using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace OnlineShop2022SeleniumTests
{
    public class BrowsePageAutoTests
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
        public void BrowsePage_SearchForProduct()
        {
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
        }

        [Test]
        public void BrowsePage_SortByPrice()
        {
            driver.Navigate().GoToUrl("https://onlineshop2022.azurewebsites.net/Home/Products");
            var title = driver.Title;
            Assert.AreEqual(title, "Products - OnlineShop2022"); //Ensures the page is correct before proceeding...
            var sortButton = driver.FindElement(By.Name("sortByPrice"));
            sortButton.SendKeys(Keys.Enter);
            Assert.Pass();
        }

        [Test]
        public void BrowsePage_SortByName()
        {
            driver.Navigate().GoToUrl("https://onlineshop2022.azurewebsites.net/Home/Products");
            var title = driver.Title;
            Assert.AreEqual(title, "Products - OnlineShop2022"); //Ensures the page is correct before proceeding...
            var sortButton = driver.FindElement(By.Name("sortByName"));
            sortButton.SendKeys(Keys.Enter);
            Assert.Pass();
        }

        [Test]
        public void BrowsePage_ShowNextPage()
        {
            driver.Navigate().GoToUrl("https://onlineshop2022.azurewebsites.net/Home/Products");
            var title = driver.Title;
            Assert.AreEqual(title, "Products - OnlineShop2022"); //Ensures the page is correct before proceeding...
            var sortButton = driver.FindElement(By.Name("nextBtn"));
            sortButton.SendKeys(Keys.Enter);
            Assert.Pass();
        }

        [OneTimeTearDown]
        public void End()
        {
            driver.Close();
        }
    }
}
