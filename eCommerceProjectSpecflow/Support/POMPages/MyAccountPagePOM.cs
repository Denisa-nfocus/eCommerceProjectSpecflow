﻿using OpenQA.Selenium;

namespace uk.co.nfocus.denisa.ecommerce.POM_Pages
{
    internal class MyAccountPagePOM
    {
        private IWebDriver _driver; //Driver to work with

        public MyAccountPagePOM(IWebDriver driver) //Get driver from test at instantiation time
        {
            _driver = driver; //Classes private field
        }

        //Locators
        private IWebElement _ordersButton => _driver.FindElement(By.LinkText("Orders"));

        //Service Methods
        public void Orders()
        {
            _ordersButton.Click();
        }
    }
}