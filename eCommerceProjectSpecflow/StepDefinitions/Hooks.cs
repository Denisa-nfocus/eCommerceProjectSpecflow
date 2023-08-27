using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using uk.co.nfocus.denisa.ecommerce.POM_Pages;

namespace eCommerceProjectSpecflow.StepDefinitions
{
    [Binding]
    public class Hooks
    {
        private IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;

        // Locators
        private IWebElement _logoutButton => _driver.FindElement(By.LinkText("Logout"));

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Hooks(ScenarioContext scenarioContext)

        //driver not set inside the contructor, this always throws a warning.
        {
            _scenarioContext = scenarioContext;
        }

        [Before]
        public void SetUp()
        {

            string Browser = Environment.GetEnvironmentVariable("BROWSER")
            ?? throw new Exception("Environement variable not set.");
                //if the first thing is null, set to chrome

            switch (Browser)
            {
                case "firefox":
                    _driver = new FirefoxDriver();
                    break;
                case "chrome":
                    _driver = new ChromeDriver();
                    break;
                case "edge":
                    _driver = new EdgeDriver();
                    break;
                case "ie":
                    _driver = new InternetExplorerDriver();
                    break;
                case "remotechrome":
                    ChromeOptions options = new ();
                    _driver = new RemoteWebDriver(new Uri("http://172.30.190.244:4444/wd/hub"), options);
                    break;
                default:
                    Console.WriteLine("No browser set - launching chrome");
                    _driver = new ChromeDriver();
                    break;
            };

            _scenarioContext["my_driver"] = _driver; 

            // Browser friendly
            _driver.Manage().Window.Maximize();

            // Navigate to the start page in [SetUp]
            _driver.Url = "https://www.edgewordstraining.co.uk/demo-site/my-account/";

            // Dismiss Notice
            LoginPagePOM login = new(_driver);
            login.Dismiss();
        }

        [After]
        public void Teardown()
        {
            try
            {
                // Logout
                _logoutButton.Click();
                //Close the web browser
                _driver.Quit();
            }
            catch
            {
                //Close the web browser
                _driver.Quit();
            }
        }
    }
}
