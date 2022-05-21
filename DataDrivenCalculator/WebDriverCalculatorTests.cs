using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DataDrivenCalculator
{
    public class WebDriverCalculatorTests
    {
        private ChromeDriver driver;
        IWebElement textBoxFirtsNum;
        IWebElement textBoxSecondNum;
        IWebElement operation;
        IWebElement calculateButton;
        IWebElement resultField;
        IWebElement resetButton;

        [OneTimeSetUp]
        public void OpenAndNavigate()
        {
            this.driver = new ChromeDriver();
            driver.Url = "https://number-calculator.nakov.repl.co";

            textBoxFirtsNum = driver.FindElement(By.Id("number1"));
            textBoxSecondNum = driver.FindElement(By.Id("number2"));
            operation = driver.FindElement(By.Id("operation"));
            calculateButton = driver.FindElement(By.Id("calcButton"));
            resultField = driver.FindElement(By.Id("result"));
            resetButton = driver.FindElement(By.Id("resetButton"));
        }

        [OneTimeTearDown]
        public void ShutDown()
        {
            driver.Close();
        }

        [TestCase("5", "6", "+", "Result: 11")]
        [TestCase("5", "4", "-", "Result: 1")]
        [TestCase("12", "6", "/", "Result: 2")]
        [TestCase("5", "6", "*", "Result: 30")]
        [TestCase("ahas", "6", "*", "Result: invalid input")]
        [TestCase("8", "6", "@", "Result: invalid operation")]
        public void Test_Calculator(string num1, string num2, string operato, string result)
        {
            // Arrange
           

            // Act
            textBoxFirtsNum.SendKeys(num1);
            operation.SendKeys(operato);
            textBoxSecondNum.SendKeys(num2);

            calculateButton.Click();

           // string expectedValue = "Result: 11";

            Assert.That(result, Is.EqualTo(resultField.Text));

            resetButton.Click();

        }
    }
}