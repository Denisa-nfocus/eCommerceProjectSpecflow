using OpenQA.Selenium;
using static eCommerceProjectSpecflow.StepDefinitions.StaticHelpers;

namespace uk.co.nfocus.denisa.ecommerce.POM_Pages
{
    internal class NavPOM
    {
        private readonly IWebDriver _driver; //Driver to work with

        public NavPOM(IWebDriver driver) //Get driver from test at instantiation time
        {
            _driver = driver; //Classes private field
        }

        //Locators
        private IWebElement _homeNav => _driver.FindElement(By.LinkText("Home"));
        private IWebElement _shopNav => _driver.FindElement(By.LinkText("Shop"));
        private IWebElement _cartNav => _driver.FindElement(By.LinkText("Cart"));
        private IWebElement _checkoutNav => _driver.FindElement(By.LinkText("Checkout"));
        private IWebElement _myAccountNav => WaitForElementThenReturn(_driver, By.LinkText("My account"), 10);
        private IWebElement _blogNav => _driver.FindElement(By.LinkText("Blog"));

        //Service Methods
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
            _myAccountNav.Click();
        }
        public void Blog()
        {
            _blogNav.Click();
        }
    }
}