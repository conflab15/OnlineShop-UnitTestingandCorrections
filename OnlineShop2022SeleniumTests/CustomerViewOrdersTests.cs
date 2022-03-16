using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace OnlineShop2022SeleniumTests
{
    public class CustomerViewOrdersTests
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
        public void AccountPage_OpenOrdersPage()
        {
            driver.Navigate().GoToUrl("https://onlineshop2022.azurewebsites.net/ViewOrders");
            var title = driver.Title;
            Assert.AreEqual(title, "My Orders - OnlineShop2022"); //Ensures the page is correct before proceeding...
        }

        [Test]
        public void CustomerOrdersPage_OpenOrderDetails()
        {
            driver.Navigate().GoToUrl("https://onlineshop2022.azurewebsites.net/ViewOrders");
            var title = driver.Title;
            Assert.AreEqual(title, "My Orders - OnlineShop2022"); //Ensures the page is correct before proceeding...
            var openOrderBtn = driver.FindElement(By.Name("viewOrder"));
            openOrderBtn.Click();
            var pageTitle = driver.Title;
            Assert.AreEqual(pageTitle, "Order Details - OnlineShop2022");
        }

       

        [OneTimeTearDown]
        public void End()
        {
            driver.Close();
        }
    }
}
