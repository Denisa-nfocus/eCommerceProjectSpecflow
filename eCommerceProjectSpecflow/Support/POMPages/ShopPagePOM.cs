using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Xml.Linq;
using static eCommerceProjectSpecflow.StepDefinitions.StaticHelpers;

namespace uk.co.nfocus.denisa.ecommerce.POM_Pages
{
    internal class ShopPagePOM
    {
        private readonly IWebDriver _driver; //Driver to work with
        private string? item;

        public ShopPagePOM(IWebDriver driver) //Get driver from test at instantiation time
        {
            _driver = driver; //Classes private field
        }

        //Locators
        private IWebElement _locateElement => _driver.FindElement(By.CssSelector($"[aria-label=\"Add “{item}” to your cart\"]"));
        private IWebElement _viewCartLink => WaitForElementThenReturn(_driver, By.CssSelector("[title='View cart']"));
        private string _cartTotalString => _driver.FindElement(By.CssSelector(".amount.woocommerce-Price-amount")).Text;
        private IWebElement _cartTotal => _driver.FindElement(By.CssSelector(".amount.woocommerce-Price-amount"));
        private IWebElement _removeFromCart => _driver.FindElement(By.CssSelector(".remove.remove_from_cart_button"));

        //Service Methods
        public void ItemToCart(string chosenItem)
        {
            // Make the 'item' variable equal to the chosenItem
            item = chosenItem;
            _locateElement.Click();
        }
        public void ViewCart()
        {
            _viewCartLink.Click();
        }

        // Check for items already in the cart.
        public void CartTotal()
        {
            if (_cartTotalString != "£0.00")
            {
                // Move mouse to cart
                Actions action = new(_driver);
                action.MoveToElement(_cartTotal).Perform();
                // Remove item from cart
                _removeFromCart.Click();
            }

        }
    }
}