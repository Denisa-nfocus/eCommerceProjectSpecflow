using OpenQA.Selenium;

namespace uk.co.nfocus.denisa.ecommerce.POM_Pages
{
    internal class OrdersPagePOM
    {
        private IWebDriver _driver; //Driver to work with

        public OrdersPagePOM(IWebDriver driver) //Get driver from test at instantiation time
        {
            _driver = driver; //Classes private field
        }

        // Getting the string for the 'order number' as text
        private string _cellVal => _driver.FindElement(By.CssSelector("tr:nth-of-type(1) > .woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number > a")).Text;

        //Service Methods
        public string LatestOrder() =>
            // The most recent order that displays on the 'Orders' page
            _cellVal.Replace("#", "");
    }
}