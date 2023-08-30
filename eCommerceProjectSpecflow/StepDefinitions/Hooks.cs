using NUnit.Framework.Interfaces;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using uk.co.nfocus.denisa.ecommerce.POM_Pages;
using static eCommerceProjectSpecflow.Support.StaticHelpers;
using System.Drawing;

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

        // 'driver' not set inside the contructor. This always throws a warning.
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void SetUp()
        {
            // If environment variable BROWSER can't be found and referenced, throw an error "Environement variable not set.".
            string Browser = Environment.GetEnvironmentVariable("BROWSER") ?? throw new Exception("Environement variable not set.");

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
                    // If BROWSER is null or invalid, launch chrome by default.
                    Console.WriteLine("No valid browser set - launching chrome");
                    _driver = new ChromeDriver();
                    break;
            };

            _scenarioContext["my_driver"] = _driver;

            // Browser set to 1520x900
            _driver.Manage().Window.Size = new Size(1520, 900);

            // Navigate to the start page in [SetUp]
            _driver.Url = "https://www.edgewordstraining.co.uk/demo-site/my-account/";

            // Dismiss Notice
            LoginPagePOM login = new(_driver);
            login.Dismiss();

        }

        [AfterStep]
        public void ErrorScreenshot()
        {
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            string failedScenario = _scenarioContext.ScenarioInfo.Title;
            string failedStep = _scenarioContext.StepContext.StepInfo.Text;
            if (testStatus == TestStatus.Failed)
            {
                // Screenshot of the failed BDD step
                // The file name has been reduced to prevent corruption of the screenshot
                Screenshot(_driver, failedStep.Substring(0, 25) + "...");

                // Failed Scenario and Failed BDD step written to Console
                Console.WriteLine($"Failed Scenario: \"{failedScenario}\"");
                Console.WriteLine($"Failed Step: \"{failedStep}\"");
            }
        }

        [AfterScenario]
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
