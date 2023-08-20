using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static eCommerceProjectSpecflow.StepDefinitions.StaticHelpers;

namespace uk.co.nfocus.denisa.ecommerce.POM_Pages
{
    internal class CartPagePOM
    {
        private IWebDriver _driver; //Driver to work with

        public CartPagePOM(IWebDriver driver) //Get driver from test at instantiation time
        {
            _driver = driver; //Classes private field
        }

        //Locators
        private IWebElement _couponCode => WaitForElementThenReturn(_driver, By.Id("coupon_code"));
        private IWebElement _submit => _driver.FindElement(By.CssSelector("button[name='apply_coupon']"));
        private IWebElement _itemPrice => _driver.FindElement(By.CssSelector(".cart-subtotal bdi"));
        private IWebElement _discount => WaitForElementThenReturn(_driver, By.CssSelector(".cart-discount.coupon-edgewords > td > .amount.woocommerce-Price-amount"), 10);
        private IWebElement _shipping => _driver.FindElement(By.CssSelector("#shipping_method > li > label > span > bdi"));
        private IWebElement _totalPrice => _driver.FindElement(By.CssSelector(".order-total > td bdi"));
        private IWebElement _clearCart => _driver.FindElement(By.CssSelector(".remove"));
        private IWebElement _checkoutButton => _driver.FindElement(By.CssSelector(".alt.button.checkout-button.wc-forward"));

        //Service Methods
        public void ApplyCoupon(string code)
        {
            _couponCode.SendKeys(code);
            _submit.Click();
        }
        public double ItemPrice() =>
            // Item Price
            Convert.ToDouble(_itemPrice.Text.Replace("£", ""));

        public double Discount() =>
            // Discount
            Convert.ToDouble(_discount.Text.Replace("£", ""));

        public double Shipping() =>
            // Shipping
            Convert.ToDouble(_shipping.Text.Replace("£", ""));

        public double TotalPrice() =>
            // Total Price
            Convert.ToDouble(_totalPrice.Text.Replace("£", ""));
        public void ClearCart()
        {
            _clearCart.Click();
        }
        public void Checkout()
        {
            _checkoutButton.Click();
        }

    }
}

