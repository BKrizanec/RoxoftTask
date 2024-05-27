using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SwagLabsTestingApp.Pages;
using SwagLabsTestingApp.Utilities;
using TechTalk.SpecFlow;

namespace SwagLabsTestingApp.StepDefinitions
{
    [Binding]
    public class HomePageSteps
    {
        private IWebDriver _driver;
        private HomePage _homePage;
        private readonly ScenarioContext _scenarioContext;

        public HomePageSteps(ScenarioContext scenarioContext)
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

        [Given(@"I am logged in")]
        public void GivenIAmLoggedIn()
        {
            _driver.Navigate().GoToUrl(LoginHelper.loginUrl);
            var loginPage = new LoginPage(_driver);
            loginPage.AttemptLogin(LoginHelper.confirmedUser, LoginHelper.password);
            _driver.Manage().Window.Maximize();
        }

        [When(@"I navigate to the home page")]
        public void WhenINavigateToTheHomePage()
        {
            _driver.Navigate().GoToUrl(HomeHelper.inventoryUrl);
        }

        [Then(@"all inventory items should be visible and enabled")]
        public void ThenAllInventoryItemsShouldBeVisibleAndEnabled()
        {
            var inventoryItems = _homePage.GetInventoryItems();
            Assert.That(inventoryItems.Count, Is.EqualTo(HomeHelper.maxNumberOfItems), "Number of Items is not as expected");

            foreach (var item in inventoryItems)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(item.ItemDescription, Is.Not.Null, "Item description is null");
                    Assert.That(item.ItemDescription.Displayed, Is.True, "Item description is not displayed");
                    Assert.That(item.ItemDescription.Enabled, Is.True, "Item description is not enabled");

                    Assert.That(item.ItemImage, Is.Not.Null, "Item image is null");
                    Assert.That(item.ItemImage.Displayed, Is.True, "Item image is not displayed");
                    Assert.That(item.ItemImage.Enabled, Is.True, "Item image is not enabled");

                    Assert.That(item.ItemTitle, Is.Not.Null, "Item title is null");
                    Assert.That(item.ItemTitle.Displayed, Is.True, "Item title is not displayed");
                    Assert.That(item.ItemTitle.Enabled, Is.True, "Item title is not enabled");

                    Assert.That(item.ItemPrice, Is.Not.Null, "Item price is null");
                    Assert.That(item.ItemPrice.Displayed, Is.True, "Item price is not displayed");
                    Assert.That(item.ItemPrice.Enabled, Is.True, "Item price is not enabled");
                });
            }
        }

        [When(@"I click on the ""(.*)"" menu item")]
        public void WhenIClickOnTheMenuItem(string menuItem)
        {
            IWebElement menuElement = menuItem switch
            {
                "All Items" => _homePage.AllItemsMainMenu,
                "About" => _homePage.AboutMainMenu,
                "Logout" => _homePage.LogoutMainMenu,
                "Reset App State" => _homePage.ResetAppStateMainMenu,
                _ => throw new ArgumentException($"Unknown menu item: {menuItem}")
            };

            MainMenuButtonClicker(menuElement);
        }

        [Then(@"I should be redirected to the inventory page")]
        public void ThenIShouldBeRedirectedToTheInventoryPage()
        {
            Assert.That(_driver.Url, Is.EqualTo(HomeHelper.inventoryUrl), "Unexpected URL after clicking All Items menu item.");
        }

        [Then(@"I should be redirected to the about page")]
        public void ThenIShouldBeRedirectedToTheAboutPage()
        {
            Assert.That(_driver.Url, Is.EqualTo(HomeHelper.aboutUrl), "Unexpected URL after clicking About menu item.");
        }

        [Then(@"I should be logged out and redirected to the login page")]
        public void ThenIShouldBeLoggedOutAndRedirectedToTheLoginPage()
        {
            Assert.That(_driver.Url, Is.EqualTo(LoginHelper.loginUrl), "Unexpected URL after clicking Logout menu item.");
        }

        [Then(@"the cart should be empty and I should remain on the inventory page")]
        public void ThenTheCartShouldBeEmptyAndIShouldRemainOnTheInventoryPage()
        {
            Assert.That(_driver.Url, Is.EqualTo(HomeHelper.inventoryUrl), "Unexpected URL after clicking Reset App State menu item.");
            int cartItemCount = _homePage.GetCartItemCount();
            Assert.That(cartItemCount, Is.EqualTo(0), "There is an issue with the Reset App State");
        }

        #region Helper Methods
        private void MainMenuButtonClicker(IWebElement inputElement)
        {
            MainDropdownClicker();

            Thread.Sleep(1000);
            Assert.Multiple(() =>
            {
                Assert.That(inputElement, Is.Not.Null, "The element is null");
                Assert.That(inputElement.Displayed, "The element is not displayed");
                Assert.That(inputElement.Enabled, "The element is not enabled");
            });
            inputElement.Click();
        }

        private void MainDropdownClicker()
        {
            var dropdownButton = _homePage.MainMenuButton;

            Thread.Sleep(1000);
            Assert.Multiple(() =>
            {
                Assert.That(dropdownButton, Is.Not.Null, "The element is null");
                Assert.That(dropdownButton.Displayed, "The element is not displayed");
                Assert.That(dropdownButton.Enabled, "The element is not enabled");
            });
            dropdownButton.Click();
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
        #endregion
    }
}
