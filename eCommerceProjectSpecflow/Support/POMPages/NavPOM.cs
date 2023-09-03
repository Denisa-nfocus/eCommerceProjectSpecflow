using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using static eCommerceProjectSpecflow.Support.StaticHelpers;
using System.Collections.ObjectModel;

namespace uk.co.nfocus.denisa.ecommerce.POM_Pages
{
    internal class NavPOM
    {
        private readonly IWebDriver _driver; // Driver to work with

        public NavPOM(IWebDriver driver) // Get driver from test at instantiation time
        {
            _driver = driver; // Classes private field
        }

        // Locators
        private IWebElement _homeNav => _driver.FindElement(By.LinkText("Home"));
        private IWebElement _shopNav => _driver.FindElement(By.LinkText("Shop"));
        private IWebElement _cartNav => _driver.FindElement(By.LinkText("Cart"));
        private IWebElement _checkoutNav => _driver.FindElement(By.LinkText("Checkout"));
        private IWebElement _myAccountNav => WaitForElementThenReturn(_driver, By.LinkText("My account"));
        private IWebElement _blogNav => _driver.FindElement(By.LinkText("Blog"));
            
        // Service Methods
        public void Home()
        {
            _homeNav.Click();
        }
        public void Shop()
        {
            _shopNav.Click();
        }
        public void Cart()
        {
            _cartNav.Click();
        }
        public void Checkout()
        {
            _checkoutNav.Click();
        }
        public void MyAccount()
        {
            try
            {
                Actions action = new(_driver);
                action.MoveToElement(_myAccountNav).Perform();
                _myAccountNav.Click();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Caught exception " + ex);

                Actions action = new(_driver);
                action.MoveToElement(_myAccountNav).Perform();
                _myAccountNav.Click();
            }
        }
        public void Blog()
        {
            _blogNav.Click();
        }
    }
}