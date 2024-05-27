using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SwagLabsTestingApp.Pages;
using SwagLabsTestingApp.Utilities;
using TechTalk.SpecFlow;
using System;
using System.Linq;

namespace SwagLabsTestingApp.StepDefinitions
{
    [Binding]
    public class SortingSteps
    {
        private IWebDriver _driver;
        private HomePage _homePage;
        private WebDriverWait _wait;
        private readonly ScenarioContext _scenarioContext;

        public SortingSteps(ScenarioContext scenarioContext)
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
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [Given(@"I want to sort items")]
        public void GivenIWantToSortItems()
        {
            _driver.Navigate().GoToUrl(LoginHelper.loginUrl);
            var loginPage = new LoginPage(_driver);
            loginPage.AttemptLogin(LoginHelper.confirmedUser, LoginHelper.password);
            _driver.Manage().Window.Maximize();
        }

        [When(@"I select sorting option ""(.*)""")]
        public void WhenISelectSortingOption(string sortingOption)
        {
            var sortingOptionKey = sortingOption switch
            {
                "Name (A to Z)" => "NameAscending",
                "Name (Z to A)" => "NameDescending",
                "Price (low to high)" => "PriceLowToHigh",
                "Price (high to low)" => "PriceHighToLow",
                _ => throw new KeyNotFoundException($"The given key '{sortingOption}' was not present in the dictionary.")
            };

            var value = HomeHelper.SortingOptions[sortingOptionKey];
            SelectAndVerifyDropdown(value);
        }

        [Then(@"the items should be sorted by name in ascending order")]
        public void ThenTheItemsShouldBeSortedByNameInAscendingOrder()
        {
            var itemNames = _homePage.GetInventoryItemNames();
            var sortedItemNames = itemNames.OrderBy(x => x).ToList();
            Assert.That(itemNames, Is.EqualTo(sortedItemNames));
        }

        [Then(@"the items should be sorted by name in descending order")]
        public void ThenTheItemsShouldBeSortedByNameInDescendingOrder()
        {
            var itemNames = _homePage.GetInventoryItemNames();
            var sortedItemNames = itemNames.OrderByDescending(x => x).ToList();
            Assert.That(itemNames, Is.EqualTo(sortedItemNames));
        }

        [Then(@"the items should be sorted by price in ascending order")]
        public void ThenTheItemsShouldBeSortedByPriceInAscendingOrder()
        {
            var itemPrices = _homePage.GetInventoryItemPrices();
            var sortedItemPrices = itemPrices.OrderBy(p => p).ToList();
            Assert.That(itemPrices, Is.EqualTo(sortedItemPrices));
        }

        [Then(@"the items should be sorted by price in descending order")]
        public void ThenTheItemsShouldBeSortedByPriceInDescendingOrder()
        {
            var itemPrices = _homePage.GetInventoryItemPrices();
            var sortedItemPrices = itemPrices.OrderByDescending(p => p).ToList();
            Assert.That(itemPrices, Is.EqualTo(sortedItemPrices));
        }

        #region Helper Methods
        private void SelectAndVerifyDropdown(string value)
        {
            var selectElement = _homePage.GetDropdownOptions();
            selectElement.SelectByValue(value);

            _wait.Until(d => _homePage.GetDropdownOptions().SelectedOption.GetAttribute("value") == value);
            selectElement = _homePage.GetDropdownOptions();
            Assert.Multiple(() =>
            {
                Assert.That(selectElement, Is.Not.Null, "The element is null");
                Assert.That(selectElement.SelectedOption.GetAttribute("value"), Is.EqualTo(value), "The element has no value");
            });
        }
        #endregion

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
