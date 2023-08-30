using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace eCommerceProjectSpecflow.Support
{
    internal static class StaticHelpers
    {

        public static IWebElement WaitForElementThenReturn(IWebDriver driver, By Locator, int TimeInSeconds = 5)
        {
            WebDriverWait explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(TimeInSeconds));
            explicitWait.Until(anythingatall => anythingatall.FindElement(Locator).Displayed);
            return driver.FindElement(Locator);
        }

        public static void Screenshot(IWebDriver driver, string fileName)
        {
            Screenshot orderSS = ((ITakesScreenshot)driver).GetScreenshot();
            orderSS.SaveAsFile($@".\..\{fileName}.jpeg");
        }
    }
}