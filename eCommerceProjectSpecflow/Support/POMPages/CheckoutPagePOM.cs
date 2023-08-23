using OpenQA.Selenium;
using static eCommerceProjectSpecflow.StepDefinitions.StaticHelpers;

namespace uk.co.nfocus.denisa.ecommerce.POM_Pages
{
    internal class CheckoutPagePOM
    {
        private IWebDriver _driver; //Driver to work with

        public CheckoutPagePOM(IWebDriver driver) //Get driver from test at instantiation time
        {
            _driver = driver; //Classes private field
        }

        //Locators
        private IWebElement _firstName => _driver.FindElement(By.Id("billing_first_name"));
        private IWebElement _lastName => _driver.FindElement(By.Id("billing_last_name"));
        private IWebElement _address => _driver.FindElement(By.Id("billing_address_1"));
        private IWebElement _city => _driver.FindElement(By.Id("billing_city"));
        private IWebElement _postcode => _driver.FindElement(By.Id("billing_postcode"));
        private IWebElement _phone => _driver.FindElement(By.Id("billing_phone"));
        private IWebElement _checkButton => WaitForElementThenReturn(_driver, By.Id("payment"));
        private IWebElement _orderButton => WaitForElementThenReturn(_driver, By.CssSelector("button#place_order"), 10);
        private IWebElement _formElm => WaitForElementThenReturn(_driver, By.CssSelector(".order > strong"));
        private string _formElmStr => _driver.FindElement(By.CssSelector("tr:nth-of-type(1) > .woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number > a")).Text;
        private IWebElement _accountButton => _driver.FindElement(By.LinkText("My account"));

        //Service Methods
        public void FirstName(string first_name)
        {
            _firstName.Clear();
            _firstName.SendKeys(first_name);
        }
        public void LastName(string last_name)
        {
            _lastName.Clear();
            _lastName.SendKeys(last_name);
        }
        public void Address(string address)
        {
            _address.Clear();
            _address.SendKeys(address);
        }
        public void City(string city)
        {
            _city.Clear();
            _city.SendKeys(city);
        }
        public void Postcode(string postcode)
        {
            _postcode.Clear();
            _postcode.SendKeys(postcode);
        }
        public void Phone(string phone_number)
        {
            _phone.Clear();
            _phone.SendKeys(phone_number);
        }

        public void CheckButton()
        {
            _checkButton.Click();
        }

        public void PlaceOrder()
        {
            // Try/Catch method. If the element isn't clicked the first time, try again.
            // Without using Thread.Sleep(), the test only runs a few times before failing. As such, Thread.Sleep() is a reliable way to make the test more sturdy.
            try
            {
                _orderButton.Click();
            }
            catch
            {
                Thread.Sleep(1000);
                _orderButton.Click();
            }
        }

        // Order number displayed at checkout.
        public string CheckoutOrder() =>_formElmStr.Replace("#", "");
        // Take a screenshot of the order number and save it as a '.png' file.
        public void Screenshot()
        {
            Screenshot orderSS = ((ITakesScreenshot)_formElm).GetScreenshot();
            orderSS.SaveAsFile(@".\..\order_number.png");
        }
        public void MyAccount()
        {
            _accountButton.Click();
        }
    }
}