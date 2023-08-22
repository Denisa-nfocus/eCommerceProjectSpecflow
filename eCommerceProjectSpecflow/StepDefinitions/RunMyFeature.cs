using FluentAssertions.Execution;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using uk.co.nfocus.denisa.ecommerce.POM_Pages;
using static eCommerceProjectSpecflow.StepDefinitions.Hooks;

namespace eCommerceProjectSpecflow.StepDefinitions
{
    [Binding]
    public class StepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;

        public StepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _driver = scenarioContext["my_driver"] as IWebDriver;
            // _scenarioContext["my_driver"] = _driver; Hooks
        }
        
        [Given(@"I have logged in as a registered user")]
        public void GivenIHaveLoggedInAsARegisteredUser()
        {
            // Step 1: Log-in with valid credentials
            LoginPagePOM login = new(_driver);
            login.LoginWithValidCredentials(Environment.GetEnvironmentVariable("USERNAME_1"), Environment.GetEnvironmentVariable("PASSWORD_1"));
        }


        [Given(@"I have added a '(.*)' to cart")]
        public void GivenIHaveAddedAToCart(string addItem)
        {
            _driver.Url = Environment.GetEnvironmentVariable("URL");

            // Step 1: Log-in with valid credentials
            // LoginPagePOM login = new(_driver);
            //login.LoginWithValidCredentials(Environment.GetEnvironmentVariable("USERNAME_1"), Environment.GetEnvironmentVariable("PASSWORD_1"));

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

            Assert.Multiple(() =>
            {
                // Step 6: Check that the coupon takes off 15%
                Assert.That(cartPage.Discount(), Is.EqualTo(cartPage.ItemPrice() * discountPercentage / 100), $"Coupon doesn't take off {discountPercentage}%");

                // Step 7: Check that the total calculated after coupon & shipping is correct
                Assert.That(cartPage.ItemPrice() - cartPage.Discount() + cartPage.Shipping(), Is.EqualTo(cartPage.TotalPrice()), "Total is incorrect");
            });

            // Clear Cart
            cartPage.ClearCart();

            // Navigate to 'My account'
            NavPOM nav = new(_driver);
            nav.MyAccount();

            // Step 8: Log Out - In TearDown()
        }

        [Given(@"I have proceeded to checkout")]
        public void GivenIHaveProceededToCheckout()
        {
            _driver.Url = Environment.GetEnvironmentVariable("URL");

            // Step 1: Log-in 
            //LoginPagePOM login = new(_driver);
            //login.LoginWithValidCredentials(Environment.GetEnvironmentVariable("USERNAME_2"), Environment.GetEnvironmentVariable("PASSWORD_2"));

            // Step 2: Enter the shop using top nav link ‘Shop’
            NavPOM nav = new(_driver);
            nav.Shop();

            // Step 3: Add 'Beanie' to Cart, Step 4: View the Cart
            ShopPagePOM shopPage = new(_driver);
            shopPage.ItemToCart("Beanie");
            shopPage.ViewCart();

            // Step 5: Proceed to checkout
            CartPagePOM cartPage = new(_driver);
            cartPage.Checkout();
        }

        [Given(@"I provide the billing details")]
        public void GivenIProvideTheBillingDetails()
        {
            // Step 6: Complete Billing details
            CheckoutPagePOM checkoutPage = new(_driver);
            checkoutPage.FirstName(Environment.GetEnvironmentVariable("FIRST_NAME"));
            checkoutPage.LastName(Environment.GetEnvironmentVariable("LAST_NAME"));
            checkoutPage.Address(Environment.GetEnvironmentVariable("ADDRESS"));
            checkoutPage.City(Environment.GetEnvironmentVariable("CITY"));
            checkoutPage.Postcode(Environment.GetEnvironmentVariable("POSTCODE"));
            checkoutPage.Phone(Environment.GetEnvironmentVariable("PHONE"));

            // Step 7: Select ‘Check payments’ as payment method
            checkoutPage.CheckButton();
        }

        [When(@"I place an order")]
        public void WhenIPlaceAnOrder()
        {
            CheckoutPagePOM checkoutPage = new(_driver);

            // Step 8: Place the order
            checkoutPage.PlaceOrder();

            // Step 9: Capture the Order Number and write it to the 'results' folder
            checkoutPage.Screenshot();
        }

        [Then(@"that same order is displayed in my account")]
        public void ThenThatSameOrderIsDisplayedInMyAccount()
        {
            // Step 10.1: Navigate to My Account->Orders
            NavPOM nav = new(_driver);
            nav.MyAccount();
            MyAccountPagePOM myAccount = new(_driver);
            myAccount.Orders();

            // Step 10.2: Check the same order shows in the account
            CheckoutPagePOM checkoutPage = new(_driver);
            OrdersPagePOM orderPage = new(_driver);

            Assert.That(checkoutPage.CheckoutOrder(), Is.EqualTo(orderPage.LatestOrder()), "The same order doesn't show in the account");

            // Step 11: Log Out - In TearDown()

            // Write to the console that the test has completed
            Console.WriteLine("Test 2 has been completed!!!");

        }
    }
}