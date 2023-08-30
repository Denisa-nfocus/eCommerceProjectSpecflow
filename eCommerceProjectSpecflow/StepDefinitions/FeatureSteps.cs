using eCommerceProjectSpecflow.Support.POMPages;
using NUnit.Framework;
using OpenQA.Selenium;
using Org.BouncyCastle.Asn1.X509;
using System.Text.RegularExpressions;
using uk.co.nfocus.denisa.ecommerce.POM_Pages;

namespace eCommerceProjectSpecflow.StepDefinitions
{
    [Binding]
    public class StepDefinitions
    {
        private readonly IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;

        public StepDefinitions(ScenarioContext scenarioContext)
        {
            _driver = (IWebDriver)scenarioContext["my_driver"];
            _scenarioContext = scenarioContext;
        }
        
        [Given(@"I have logged in as a registered user")]
        public void GivenIHaveLoggedInAsARegisteredUser()
        {
            _driver.Url = Environment.GetEnvironmentVariable("URL");

            // Step 1: Log-in with valid credentials
            LoginPagePOM login = new(_driver);
            // If username is null, throw an error "Username not set".
            string username = Environment.GetEnvironmentVariable("USERNAME_1") ?? throw new Exception("Username not set.");
            // If password is null, throw an error "Password not set".
            string password = Environment.GetEnvironmentVariable("PASSWORD_1") ?? throw new Exception("Password not set.");

            login.LoginWithValidCredentials(username, password);

        }

        [Given(@"I have added a '(.*)' to cart")]
        public void GivenIHaveAddedAToCart(string addItem)
        {
            // Step 2: Enter the shop using top nav link ‘Shop’
            NavPOM nav = new(_driver);
            nav.Shop();

            // Step 3: Add 'Belt' to Cart, Step 4: View the Cart
            ShopPagePOM shopPage = new(_driver);
            
            // Ensure there are no items in the cart already
            nav.EmptyCart();

            shopPage.ItemToCart(addItem);
            shopPage.ViewCart();
        }

        [When(@"I apply a valid coupon '([^']*)'")]
        public void WhenIApplyAValidCoupon(string code)
        {
            // Step 5: Apply a coupon ‘edgewords’
            CartPagePOM cartPage = new(_driver);
            cartPage.ApplyCoupon(code);
            
        }

        [Then(@"the coupon takes off '(.*)'%")]
        public void ThenTheCouponTakesOff(int discountPercentage)
        {
            CartPagePOM cartPage = new(_driver);

            // Step 6: Check that the coupon takes off 15%
            Assert.That(cartPage.Discount(), Is.EqualTo(cartPage.ItemPrice() * discountPercentage / 100), $"Coupon doesn't take off {discountPercentage}% It takes off {(int)(cartPage.Discount() / cartPage.ItemPrice() * 100)}%");
        }

        [Then(@"the total is correct")]
        public void ThenTheTotalIsCorrect()
        {
            CartPagePOM cartPage = new(_driver);

            // Step 7: Check that the total calculated after coupon & shipping is correct
            Assert.That(cartPage.ItemPrice() - cartPage.Discount() + cartPage.Shipping(), Is.EqualTo(cartPage.TotalPrice()), "Total is incorrect");

            // Clear Cart
            cartPage.ClearCart();

            // Navigate to 'My account'
            NavPOM nav = new(_driver);
            nav.MyAccount();

            // Step 8: Log Out - In TearDown()

            // Write to the console that the test has completed
            Console.WriteLine("Test 1 has been completed!!!");

        }

        [Given(@"I have proceeded to checkout")]
        public void GivenIHaveProceededToCheckout()
        {
            // Step 5: Proceed to checkout
            CartPagePOM cartPage = new(_driver);
            cartPage.Checkout();
        }

        [Given(@"I provide the billing details with id: '([^']*)'")]
        public void GivenIProvideTheBillingDetailsWithId(string iD, Table table)
        {
            
            // Step 6: Complete Billing details
            CheckoutPagePOM checkoutPage = new(_driver);

            // Count the number of rows in the table
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Boolean foundID = false;
                foreach (var row in table.Rows)
                {
                    // For each row, check if the parameter 'iD' is equal to any ID's found in the table.
                    if (row["ID"] == iD)
                    {
                        // Match found. Retrieve billing details linked to this ID.
                        checkoutPage.BillingDetails(
                            row["first name"],
                            row["last name"],
                            row["address"],
                            row["city"],
                            row["postcode"],
                            row["phone number"]
                            );
                      
                        foundID = true;
                    }
                    // ID's are unique. If an ID is found, the search is over.
                    if (foundID == true)
                    {
                        break;
                    }
                } 
                // If the foundID == false, then the parameter entered does not equal to any ID in the table. As such, Fail the test.
                if(foundID == false) {
                    Assert.Fail($"ID {iD} could not be found");
                }
            }

            // Step 7: Select ‘Check payments’ as payment method
            checkoutPage.CheckButton();
        }

        [When(@"I place an order")]
        public void WhenIPlaceAnOrder()
        {
            CheckoutPagePOM checkoutPage = new(_driver);

            // Step 8: Place the order
            checkoutPage.PlaceOrder();

            // Step 9: Capture the order number from the order confirmation page
            OrderReceivedPagePOM orderReceivedPage = new(_driver);
            _scenarioContext["confirmation_order_number"] = orderReceivedPage.OrderNumber();

            // Take a Screenshot of the Confirmation page
            orderReceivedPage.OrderConfirmation();
        }

        [Then(@"that same order is displayed in my account")]
        public void ThenThatSameOrderIsDisplayedInMyAccount()
        {
            // Step 10.1: Navigate to My Account->Orders
            NavPOM nav = new(_driver);
            nav.MyAccount();
            MyAccountPagePOM myAccount = new(_driver);
            myAccount.Orders();

            /* Step 10.2: Check that the order number from the order confirmation page,
            * is the same as the latest order number that displays on the orders page */
            OrdersPagePOM orderPage = new(_driver);
            Assert.That(orderPage.LatestOrder(), Is.EqualTo((string)_scenarioContext["confirmation_order_number"]), "The same order doesn't show in the account");

            // Step 11: Log Out - In TearDown()

            // Write to the console that the test has completed
            Console.WriteLine("Test 2 has been completed!!!");

        }
    }
}