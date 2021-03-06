using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NUnitTestRozetka
{
    public class Tests
    {
        IWebDriver driver;
        ChromeOptions chromeOptions = new ChromeOptions();
        [OneTimeSetUp]
        public void Setup()
        {
         driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),chromeOptions);
         driver.Navigate().GoToUrl(" https://rozetka.com.ua/ua/");
         driver.Manage().Window.Maximize();
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
        public int returnPrice(string textElement)
        {
            return Convert.ToInt32(string.Join("", textElement.ToCharArray().Where(Char.IsDigit)));
        }
        public IWebElement searchSuggestPrice(string locator)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement result = wait.Until(e => e.FindElement(By.XPath(locator)));
            return  result;
        }
        public void writeRequest(bool tearDown = false)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement searchInput = wait.Until(e => e.FindElement(By.Name("search")));
            if (tearDown == false)
            {
                string searchRequest = "dell xps 15 9500";
                searchInput.SendKeys(searchRequest);
            }
            else
            {
                searchInput.Clear();
            }
        }

        [Test]
        public void ComparePrices()
        {
            writeRequest();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            string suggestPriceLocator = "//ul/li[2]/a/span[2]/span[2]";
            string pagePriceLocator = "//product-main-info/div/div/div/p";
            int firstPrice, secondPrice;
            IWebElement firstElement = searchSuggestPrice(suggestPriceLocator);
            firstPrice = returnPrice(firstElement.Text);
            firstElement.Click();
            IWebElement secondElement = searchSuggestPrice(pagePriceLocator);
            secondPrice = returnPrice(secondElement.Text);
            Assert.AreEqual(firstPrice, secondPrice);
            driver.Navigate().Back();
            writeRequest();
            Assert.AreEqual(firstPrice,returnPrice(searchSuggestPrice(suggestPriceLocator).Text));
            writeRequest(true);
        }
    }
}