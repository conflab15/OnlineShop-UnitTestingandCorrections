using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace OnlineShop2022SeleniumTests
{
    public class HomePageAutoTests
    {
        IWebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            //This one time setup means that the set up is performed once to perform a numerous amount of tests under one setup
            driver = new ChromeDriver();
        }

        [Test]
        public void GoToHomePageCheckTitle()
        {
            driver.Navigate().GoToUrl("https://onlineshop2022.azurewebsites.net/Identity/Account/Login?ReturnUrl=%2F"); //Defining a URL to navigate to.
            string title = driver.Title; //Obtaining Test Values
            Assert.AreEqual(title, "Log in - OnlineShop2022"); //Assert - Checking if the test is successful
        }

        [Test]
        public void RegisterButtonOnHomePage()
        {
            driver.Navigate().GoToUrl("https://onlineshop2022.azurewebsites.net/Identity/Account/Login?ReturnUrl=%2F");
            var button = driver.FindElement(By.Id("register"));
            button.Click();
            string title = driver.Title;
            Assert.AreEqual(title, "Register - OnlineShop2022");
        }

        [Test]
        public void RegisterNewUser()
        {
            driver.Navigate().GoToUrl("https://onlineshop2022.azurewebsites.net/Identity/Account/Register");
            var title = driver.Title;
            Assert.AreEqual(title, "Register - OnlineShop2022"); //Ensuring the page is correct before inserting data into the fields.
            var inputForename = driver.FindElement(By.Id("Input_Fname"));
            inputForename.SendKeys("Automated");
            var inputSurname = driver.FindElement(By.Id("Input_Sname"));
            inputSurname.SendKeys("TestUser");
            var inputEmail = driver.FindElement(By.Id("Input_Email"));
            inputEmail.SendKeys("autouser2@gmail.com");
            var inputPassword = driver.FindElement(By.Id("Input_Password"));
            inputPassword.SendKeys("Password1!");
            var inputConfirmPass = driver.FindElement(By.Id("Input_ConfirmPassword"));
            inputConfirmPass.SendKeys("Password1!");

            inputConfirmPass.Submit();
            var newTitle = driver.Title;
            Assert.AreEqual(newTitle, "Home Page - OnlineShop2022");

        }

        [Test]
        public void LoginWithAdminUser()
        {
            driver.Navigate().GoToUrl("https://onlineshop2022.azurewebsites.net/Identity/Account/Login?ReturnUrl=%2F");
            var title = driver.Title;
            Assert.AreEqual(title, "Log in - OnlineShop2022");
            var inputEmail = driver.FindElement(By.Id("Input_Email"));
            inputEmail.SendKeys("admin@admin.com");
            var inputPass = driver.FindElement(By.Id("Input_Password"));
            inputPass.SendKeys("Admin123!");
            inputPass.Submit();
            var newTitle = driver.Title;
            Assert.AreEqual(newTitle, "Home Page - OnlineShop2022");
        }

        [OneTimeTearDown]
        public void End()
        {
            driver.Close();
        }
    }
}