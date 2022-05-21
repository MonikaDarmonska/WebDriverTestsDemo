using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace NUnitWebDriverTests
{
    public class SoftUniTests
    {
        private WebDriver driver;

        [OneTimeSetUp]
        public void Setup_OpenBrowserAndNavigate()
        {
            // Add option to chrome browse instance
            var options = new ChromeOptions();
            options.AddArgument("--headless");

            this.driver = new ChromeDriver(options); //this is a browser
            driver.Url = "https://softuni.bg";
            driver.Manage().Window.Maximize();
        }

        [OneTimeTearDown]
        public void ShutDown()
        {
            driver.Quit();
        }

        [Test]
        public void Test_AssertMainPageTitle()
        {
            // Act
            driver.Url = "https://softuni.bg";
            string expectedTitle = "Обучение по програмиране - Софтуерен университет";

            // Assert
            Assert.That(driver.Title, Is.EqualTo(expectedTitle));          
        }

        [Test]
        public void Test_AssertAboutUsTitle()
        {
            // Act            
            var zaNasElement = driver.FindElement(By.LinkText("За нас"));
            zaNasElement.Click();

            string expectedTitle = "За нас - Софтуерен университет";

            // Assert
            Assert.That(driver.Title, Is.EqualTo(expectedTitle));
        }

        [Test]
        public void Test_Login_InvalidUsernameAndPassword()
        {           
            driver.FindElement(By.CssSelector(".softuni-btn-primary")).Click();
            driver.FindElement(By.Id("username")).Click();
            driver.FindElement(By.Id("username")).SendKeys("username1");
            driver.FindElement(By.Id("password-input")).SendKeys("username1");
            driver.FindElement(By.CssSelector(".softuni-btn")).Click();
            driver.FindElement(By.CssSelector("li")).Click();

            Assert.That(driver.FindElement(By.CssSelector("li")).Text, Is.EqualTo("Невалидно потребителско име или парола"));
           
            driver.Close();
        }

        [Test]
        public void Test_Search_Positiveresults()
        {
            driver.Url = "https://softuni.bg";
            // Click on Search button
            driver.FindElement(By.CssSelector(".header-search-dropdown-link .fa-search")).Click();
            
            // Type search value and hit Enter
            var searchBox = driver.FindElement(By.CssSelector(".container > form #search-input"));
            searchBox.Click();
            searchBox.SendKeys("QA");
            searchBox.SendKeys(Keys.Enter);

            var resultField = driver.FindElement(By.CssSelector(".search-title")).Text;

            var expectedValue = "Резултати от търсене на “QA”";
            Assert.That(resultField, Is.EqualTo(expectedValue));
        }
    }
}