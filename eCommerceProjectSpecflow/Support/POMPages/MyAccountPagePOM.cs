using OpenQA.Selenium;
using static eCommerceProjectSpecflow.Support.StaticHelpers;

namespace uk.co.nfocus.denisa.ecommerce.POM_Pages
{
    internal class MyAccountPagePOM
    {
        private readonly IWebDriver _driver; // Driver to work with

        public MyAccountPagePOM(IWebDriver driver) // Get driver from test at instantiation time
        {
            _driver = driver; // Classes private field
        }

        // Locators
        private IWebElement _ordersButton => WaitForElementThenReturn(_driver, By.LinkText("Orders"));
        private IWebElement _logoutButton => _driver.FindElement(By.LinkText("Logout"));

        // Service Methods
        public void Orders()
        {
            _ordersButton.Click();
        }
        public void Logout()
        {
            _logoutButton.Click();
        }
    }
}