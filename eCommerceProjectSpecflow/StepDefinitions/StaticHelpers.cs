using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceProjectSpecflow.StepDefinitions
{
    internal static class StaticHelpers
    {

        public static IWebElement WaitForElementThenReturn(IWebDriver driver, By Locator, int TimeInSeconds = 5)
        {
            WebDriverWait explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(TimeInSeconds));
            explicitWait.Until(anythingatall => anythingatall.FindElement(Locator).Displayed);
            return driver.FindElement(Locator);
        }
    }
}