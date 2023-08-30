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
        private string _cartTotalString => _driver.FindElement(By.CssSelector(".amount.woocommerce-Price-amount")).Text;
        private IWebElement _cartTotal => _driver.FindElement(By.CssSelector(".amount.woocommerce-Price-amount"));
        private IWebElement _removeFromCart => WaitForElementThenReturn(_driver, By.CssSelector(".remove.remove_from_cart_button"));
        ReadOnlyCollection<IWebElement> _removeButtons => _driver.FindElements(By.CssSelector(".remove.remove_from_cart_button"));
        
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
            catch
            {
                Actions action = new(_driver);
                action.MoveToElement(_myAccountNav).Perform();
                _myAccountNav.Click();
            }
        }
        public void Blog()
        {
            _blogNav.Click();
        }


        // Check for items already in the cart.
        public void EmptyCart()
        {
            if (_cartTotalString != "£0.00")
            {
                // Count the number of remove buttons
                int removeButtonCount = _removeButtons.Count;

                // Move mouse to cart
                Actions action = new(_driver);
                action.MoveToElement(_cartTotal).Perform();

                while (removeButtonCount > 0)
                {
                    try
                    {
                        // Remove item from cart
                        _removeFromCart.Click();
                        // Reduce removeButtonCount by -1
                        removeButtonCount -= 1;
                    }
                    catch
                    {
                        _removeFromCart.Click();
                        removeButtonCount -= 1;
                    }
                }
            }
        }
    }
}