using eCommerceProjectSpecflow.Support.POMPages;
using NUnit.Framework;
using OpenQA.Selenium;
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

        [Given(@"I have cleared the cart")]
        public void GivenIHaveClearedTheCart()
        {
            NavPOM nav = new(_driver);
            CartPagePOM cartPage = new(_driver);

            // If there are items in the cart, remove them.
            if (cartPage.ItemsInCart() == true)
            {
                nav.Cart();
                cartPage.ClearCart();
            }
        }


        [Given(@"I have added a '(.*)' to cart")]
        public void GivenIHaveAddedAToCart(string addItem)
        {
            // Step 2: Enter the shop using top nav link ‘Shop’
            NavPOM nav = new(_driver);
            nav.Shop();

            // Step 3: Add 'Belt' to Cart, Step 4: View the Cart
            ShopPagePOM shopPage = new(_driver);
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
            var DisplayedDiscount = cartPage.Discount();
            var ActualDiscount = (int)(cartPage.Discount() / cartPage.SubTotal() * 100);
            var ExpectedDiscount = cartPage.SubTotal() * discountPercentage / 100;

            Assert.That(DisplayedDiscount, Is.EqualTo(ExpectedDiscount), $"Coupon doesn't take off {discountPercentage}% It takes off {ActualDiscount}%");
        }

        [Then(@"the total is correct")]
        public void ThenTheTotalIsCorrect()
        {
            CartPagePOM cartPage = new(_driver);

            // Step 7: Check that the total calculated after coupon & shipping is correct
            Assert.That(cartPage.SubTotal() - cartPage.Discount() + cartPage.Shipping(), Is.EqualTo(cartPage.TotalPrice()), "Total is incorrect");

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

        [Given(@"I provide the billing details")]
        public void GivenIProvideTheBillingDetails(Table table)
        {            
            // Step 6: Complete Billing details
            CheckoutPagePOM checkoutPage = new(_driver);
            checkoutPage.BillingDetails(
                table.Rows[0]["first name"],
                table.Rows[0]["last name"],
                table.Rows[0]["address"],
                table.Rows[0]["city"],
                table.Rows[0]["postcode"],
                table.Rows[0]["phone number"]
                );
          
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
            Console.WriteLine("Order " + (string)_scenarioContext["confirmation_order_number"] + " has been captured");

            // Take a Screenshot of the Confirmation page
            orderReceivedPage.ScreenshotOfOrderConfirmation();
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