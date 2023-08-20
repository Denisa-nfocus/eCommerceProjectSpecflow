﻿using OpenQA.Selenium;
using static eCommerceProjectSpecflow.StepDefinitions.StaticHelpers;

namespace uk.co.nfocus.denisa.ecommerce.POM_Pages
{
    internal class ShopPagePOM
    {
        private IWebDriver _driver; //Driver to work with
        private string? item;

        public ShopPagePOM(IWebDriver driver) //Get driver from test at instantiation time
        {
            _driver = driver; //Classes private field
        }

        //Locators
        private IWebElement _locateElement => _driver.FindElement(By.CssSelector($"[aria-label=\"Add “{item}” to your cart\"]"));
        private IWebElement _viewCartLink => WaitForElementThenReturn(_driver, By.CssSelector("[title='View cart']"));

        //new WebDriverWait(_driver, TimeSpan.FromSeconds(3)).
        //Until(drv => drv.FindElement(By.CssSelector("[title='View cart']")));


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
    }
}