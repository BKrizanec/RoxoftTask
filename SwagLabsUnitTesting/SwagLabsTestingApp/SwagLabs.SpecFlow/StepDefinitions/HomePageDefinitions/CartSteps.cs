using NUnit.Framework;
using OpenQA.Selenium;
using SwagLabsTestingApp.Pages;
using SwagLabsTestingApp.Utilities;
using TechTalk.SpecFlow;

namespace SwagLabsTestingApp.StepDefinitions
{
    [Binding]
    public class CartSteps
    {
        private IWebDriver _driver;
        private HomePage _homePage;
        private readonly ScenarioContext _scenarioContext;

        public CartSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void SetUp()
        {
            if (!_scenarioContext.ContainsKey("WebDriver"))
            {
                _driver = WebDriverManager.CreateDriver();
                _scenarioContext.Set(_driver, "WebDriver");
            }
            else
            {
                _driver = _scenarioContext.Get<IWebDriver>("WebDriver");
            }

            _homePage = new HomePage(_driver);
        }

        [Given(@"I want to purchase items")]
        public void GivenIWantToPurchaseItems()
        {
            _driver.Navigate().GoToUrl(LoginHelper.loginUrl);
            var loginPage = new LoginPage(_driver);
            loginPage.AttemptLogin(LoginHelper.confirmedUser, LoginHelper.password);
            _driver.Manage().Window.Maximize();
        }

        [When(@"I add all items to the cart")]
        public void WhenIAddAllItemsToTheCart()
        {
            var addToCartButtons = _homePage.AddToCartButtons;
            Assert.That(addToCartButtons.Count, Is.EqualTo(HomeHelper.maxNumberOfItems), "Number of Add to Cart buttons is not as expected");

            foreach (var addButton in addToCartButtons)
            {
                addButton.Click();
            }
        }

        [Then(@"the cart should reflect the correct number of items added")]
        public void ThenTheCartShouldReflectTheCorrectNumberOfItemsAdded()
        {
            var expectedItemCount = HomeHelper.maxNumberOfItems;
            var cartItemCount = _homePage.GetCartItemCount();
            Assert.That(cartItemCount, Is.EqualTo(expectedItemCount), "Number of items in the cart is not as expected after adding items");
        }

        [Given(@"I have added all items to the cart")]
        public void GivenIHaveAddedAllItemsToTheCart()
        {
            WhenIAddAllItemsToTheCart();
        }

        [When(@"I remove all items from the cart")]
        public void WhenIRemoveAllItemsFromTheCart()
        {
            var itemNames = _homePage.ItemNames;
            foreach (var itemName in itemNames)
            {
                var removeButton = _homePage.RemoveFromCartButton(itemName.Text);
                removeButton.Click();
            }
        }

        [Then(@"the cart should be empty")]
        public void ThenTheCartShouldBeEmpty()
        {
            var expectedItemCount = 0;
            var cartItemCount = _homePage.GetCartItemCount();
            Assert.That(cartItemCount, Is.EqualTo(expectedItemCount), "Number of items in the cart is not as expected after removing items");
        }

        [When(@"I click on the cart icon")]
        public void WhenIClickOnTheCartIcon()
        {
            var cartLink = _homePage.ShoppingCartLink;
            Assert.Multiple(() =>
            {
                Assert.That(cartLink, Is.Not.Null, "The cart link is null");
                Assert.That(cartLink.Displayed, Is.True, "The cart link is not displayed");
                Assert.That(cartLink.Enabled, Is.True, "The cart link is not enabled");
            });
            cartLink.Click();
        }

        [Then(@"I should be redirected to the cart page")]
        public void ThenIShouldBeRedirectedToTheCartPage()
        {
            var currentUrl = _driver.Url;
            Assert.That(currentUrl, Is.EqualTo(HomeHelper.cartUrl), $"Expected URL: {HomeHelper.cartUrl}, Actual URL: {currentUrl}");
        }

        [AfterScenario]
        public void TearDown()
        {
            if (_scenarioContext.ContainsKey("WebDriver"))
            {
                _driver = _scenarioContext.Get<IWebDriver>("WebDriver");
                WebDriverManager.QuitDriver(_driver);
            }
        }
    }
}
