using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static eCommerceProjectSpecflow.Support.StaticHelpers;

namespace uk.co.nfocus.denisa.ecommerce.POM_Pages
{
    internal class CartPagePOM
    {
        private readonly IWebDriver _driver; // Driver to work with

        public CartPagePOM(IWebDriver driver) // Get driver from test at instantiation time
        {
            _driver = driver; // Classes private field
        }

        // Locators
        private IWebElement _couponCode => WaitForElementThenReturn(_driver, By.Id("coupon_code"));
        private IWebElement _submit => _driver.FindElement(By.CssSelector("button[name='apply_coupon']"));
        private IWebElement _subTotal => _driver.FindElement(By.CssSelector(".cart-subtotal bdi"));
        private IWebElement _discount => WaitForElementThenReturn(_driver, By.CssSelector(".cart-discount.coupon-edgewords > td > .amount.woocommerce-Price-amount"), 10);
        private IWebElement _shipping => _driver.FindElement(By.CssSelector("#shipping_method > li > label > span > bdi"));
        private IWebElement _totalPrice => _driver.FindElement(By.CssSelector(".order-total > td bdi"));
        private IWebElement _clearFromCart => WaitForElementThenReturn(_driver, By.CssSelector(".remove"), 7);
        private string _cartTotalString => _driver.FindElement(By.CssSelector(".amount.woocommerce-Price-amount")).Text;
        ReadOnlyCollection<IWebElement> _removeButtonsCart => _driver.FindElements(By.CssSelector(".remove"));
        private IWebElement _checkoutButton => _driver.FindElement(By.CssSelector(".alt.button.checkout-button.wc-forward"));
        private IWebElement _basket => _driver.FindElement(By.CssSelector("[title='View your shopping cart']"));

        // Service Methods
        public void ApplyCoupon(string code)
        {
            _couponCode.SendKeys(code);
            _submit.Click();
        }
        public decimal SubTotal() =>
            // Item Price
            Convert.ToDecimal(_subTotal.Text.Replace("£", ""));

        public decimal Discount() =>
            // Discount
            Convert.ToDecimal(_discount.Text.Replace("£", ""));

        public decimal Shipping() =>
            // Shipping
            Convert.ToDecimal(_shipping.Text.Replace("£", ""));

        public decimal TotalPrice() =>
            // Total Price
            Convert.ToDecimal(_totalPrice.Text.Replace("£", ""));

        public bool ItemsInCart()
        {
            if(_cartTotalString != "£0.00")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // Empty Cart
        public void ClearCart()
        {
            // Count the number of remove buttons
            int removeButtonCount = _removeButtonsCart.Count;

            while (removeButtonCount > 0)
            {
                // Try to clear the cart. If an element exception occurs, catch it and try again.
                try
                {
                    // Clear the cart
                    _clearFromCart.Click();
                    // The number of remove buttons has been reduced
                    removeButtonCount -= 1;
                }
                catch
                {
                    // Loads the cart from the top of the page.
                    // Prevents a 'miss' due to scrolling.
                    _basket.Click();
                    // Clear the cart
                    _clearFromCart.Click();
                    // The number of remove buttons has been reduced
                    removeButtonCount -= 1;
                }
            }
        }
        public void Checkout()
        {
            _checkoutButton.Click();
        }

    }
}

