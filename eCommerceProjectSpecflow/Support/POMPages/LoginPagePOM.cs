using OpenQA.Selenium;
using static eCommerceProjectSpecflow.StepDefinitions.StaticHelpers;

namespace uk.co.nfocus.denisa.ecommerce.POM_Pages
{
    internal class LoginPagePOM
    {
        private readonly IWebDriver _driver; //Driver to work with

        public LoginPagePOM(IWebDriver driver) //Get driver from test at instantiation time
        {
            _driver = driver; //Classes private field
        }

        //Locators
        private IWebElement _dismiss => _driver.FindElement(By.LinkText("Dismiss"));
        private IWebElement _usernameField => _driver.FindElement(By.Id("username"));
        private IWebElement _passwordField => WaitForElementThenReturn(_driver, By.Id("password"), 3);
        private IWebElement _loginButton => _driver.FindElement(By.CssSelector("button[name='login']"));

        //Service methods
        public LoginPagePOM SetUsername(string username)
        {
            _usernameField.Clear();
            _usernameField.SendKeys(username);
            return this;
        }

        public LoginPagePOM SetPasssword(string password)
        {
            _passwordField.Clear();
            _passwordField.SendKeys(password);
            return this;
        }

        public void Login()
        {
            _loginButton.Click();
        }

        // Higher level service methods
        public void Login(string username, string password)
        {
            SetUsername(username);
            SetPasssword(password);
            Login();
        }

        public void LoginWithValidCredentials(string validUsername, string validPassword)
        {
            Login(validUsername, validPassword);
        }

        public void Dismiss()
        {
            _dismiss.Click(); // Dismiss Notice
        }
    }
}