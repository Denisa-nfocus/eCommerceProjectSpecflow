using OpenQA.Selenium;
using static eCommerceProjectSpecflow.Support.StaticHelpers;

namespace eCommerceProjectSpecflow.Support.POMPages
{
    internal class OrderReceivedPagePOM
    {
        private readonly IWebDriver _driver; // Driver to work with

        public OrderReceivedPagePOM(IWebDriver driver) // Get driver from test at instantiation time
        {
            _driver = driver; // Classes private field
        }

        // Locators
        private string _orderNumber => WaitForElementThenReturn(_driver, By.CssSelector(".order > strong")).Text;

        // Service Methods
        // Order number displayed at checkout.
        public string OrderNumber()
        {
            return _orderNumber;
        }

        // Take a screenshot of the order confirmation
        public void ScreenshotOfOrderConfirmation()
        {
            Screenshot(_driver, "Order " + _orderNumber.ToString() + " Confirmation");
            Console.WriteLine("A screenshot of order " + _orderNumber.ToString() + " has been taken");
        }
    }
}
