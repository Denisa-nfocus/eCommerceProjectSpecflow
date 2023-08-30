using OpenQA.Selenium;

namespace uk.co.nfocus.denisa.ecommerce.POM_Pages
{
    internal class OrdersPagePOM
    {
        private readonly IWebDriver _driver; // Driver to work with

        public OrdersPagePOM(IWebDriver driver) // Get driver from test at instantiation time
        {
            _driver = driver; // Classes private field
        }

        // Locators
        private string _latestOrderNumber => _driver.FindElement(By.CssSelector("tr:nth-of-type(1) > .woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number")).Text;

        // Service Methods
        public string LatestOrder() {
            return _latestOrderNumber.Replace("#", "");
        }
    }
}