﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SwagLabsTestingApp.PageObjectModels;
using SwagLabsTestingApp.Pages;
using SwagLabsTestingApp.Utilities;

namespace SwagLabsTestingApp.Tests;

public class HomeTests
{
    private IWebDriver _driver;
    private HomePage _homePage;
    private LoginPage _loginPage;
    private WebDriverWait _wait;

    [SetUp]
    public void SetUp()
    {
        _driver = new ChromeDriver();
        _homePage = new HomePage(_driver);
        _loginPage = new LoginPage(_driver);
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

        _driver.Navigate().GoToUrl(LoginHelper.loginUrl);
        _loginPage.AttemptLogin(LoginHelper.confirmedUser, LoginHelper.password);
        _driver.Manage().Window.Maximize();
    }

    [Test]
    public void HomePageElements()
    {
        IEnumerable<IWebElement> enabledElements = _homePage.EnabledElementsCollector();

        foreach (IWebElement element in enabledElements)
        {
            Assert.That(element.Enabled, Is.True, "Element is not enabled");
            Assert.That(element.Displayed, Is.True, "Element is not displayed");
        }
    }

    [Test]
    public void TestGetAllInventoryItemsOnHomePage()
    {
        List<HomeModel> inventoryItems = _homePage.GetInventoryItems();

        Assert.That(inventoryItems.Count(), Is.EqualTo(HomeHelper.maxNumberOfItems), "Number of Items is not as expected");

        foreach (HomeModel item in inventoryItems)
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
        }
    }

    [Test]
    public void TestAddItemToCartRemoveItemFromCartAndCartLink()
    {
        IWebElement cartLink = _homePage.ShoppingCartLink;
        AddItemsToCart();
        RemoveItemsFromCart();

        Assert.That(cartLink, Is.Not.Null, "The cart link is null");
        Assert.That(cartLink.Displayed, Is.True, "The cart link is not displayed");
        Assert.That(cartLink.Enabled, Is.True, "The cart link is not enabled");
        cartLink.Click();
    }

    [Test]
    public void TestFooterSocialLinks()
    {
        IEnumerable<IWebElement> socials = _homePage.GetElements(_homePage.SocialContainer, "a");

        foreach (IWebElement social in socials)
        {
            Assert.That(social, Is.Not.Null, "The element is null");
            Assert.That(social.Displayed, "The element is not displayed");
            Assert.That(social.Enabled, "The element is not enabled");
            social.Click();
        }
    }

    [Test]
    public void TestSortingDropdown()
    {
        SelectAndVerifyDropdown(HomeHelper.SortingOptions["NameAscending"]);
        SelectAndVerifyDropdown(HomeHelper.SortingOptions["NameDescending"]);
        SelectAndVerifyDropdown(HomeHelper.SortingOptions["PriceLowToHigh"]);
        SelectAndVerifyDropdown(HomeHelper.SortingOptions["PriceHighToLow"]);
    }

    [Test]
    public void TestMainMenuDropDownButtonWithAllItemsClickableButton()
    {
        MainMenuButtonClicker(_homePage.AllItemsMainMenu);
    }

    [Test]
    public void TestMainMenuDropDownWithAboutClickableButton()
    {
        MainMenuButtonClicker(_homePage.AboutMainMenu);
    }

    [Test]
    public void TestMainMenuDropDownWithLogoutClickableButton()
    {
        MainMenuButtonClicker(_homePage.LogoutMainMenu);
    }

    [Test]
    public void TestMainMenuDropDownWithResetAppStateClickableButton()
    {
        MainMenuButtonClicker(_homePage.ResetAppStateMainMenu);
    }

    [TearDown]
    public void TearDown()
    {
        _driver.Dispose();
    }

    #region Additional Helper Methods
    private void AddItemsToCart()
    {
        IEnumerable<IWebElement> addToCartButtons = _homePage.AddToCartButtons;
        int expectedItemCount = 0;

        Assert.That(addToCartButtons.Count(), Is.EqualTo(HomeHelper.maxNumberOfItems), "Number of Add to Cart buttons is not as expected");

        foreach (IWebElement addButton in addToCartButtons)
        {
            addButton.Click();
            expectedItemCount++;
            int cartItemCount = _homePage.GetCartItemCount();

            Assert.That(cartItemCount, Is.EqualTo(expectedItemCount), "Number of items in the cart did not increment as expected after adding an item");
        }
    }

    private void RemoveItemsFromCart()
    {
        IEnumerable<IWebElement> itemNames = _homePage.ItemNames;
        int expectedItemCount = _homePage.GetCartItemCount();

        foreach (IWebElement itemName in itemNames)
        {
            IWebElement removeButton = _homePage.RemoveFromCartButton(itemName.Text);
            removeButton.Click();
            expectedItemCount--;
            int cartItemCount = _homePage.GetCartItemCount();

            Assert.That(cartItemCount, Is.EqualTo(expectedItemCount), "Number of items in the cart did not decrement as expected after removing an item");
        }
    }

    private void SelectAndVerifyDropdown(string value)
    {
        var selectElement = _homePage.GetDropdownOptions();
        selectElement.SelectByValue(value);

        _wait.Until(d => _homePage.GetDropdownOptions().SelectedOption.GetAttribute("value") == value);
        selectElement = _homePage.GetDropdownOptions();
        Assert.That(selectElement, Is.Not.Null, "The element is null");
        Assert.That(selectElement.SelectedOption.GetAttribute("value"), Is.EqualTo(value), "The element has no value");

        if (value == HomeHelper.SortingOptions["NameAscending"] || value == HomeHelper.SortingOptions["NameDescending"])
        {
            var itemNames = _homePage.GetInventoryItemNames();
            var sortedItemNames = value == HomeHelper.SortingOptions["NameAscending"] ?
                                  itemNames.OrderBy(x => x).ToList() :
                                  itemNames.OrderByDescending(x => x).ToList();
            CollectionAssert.AreEqual(sortedItemNames, itemNames);
        }
        else if (value == HomeHelper.SortingOptions["PriceLowToHigh"] || value == HomeHelper.SortingOptions["PriceHighToLow"])
        {
            var itemPrices = _homePage.GetInventoryItemPrices();
            var sortedItemPrices = value == HomeHelper.SortingOptions["PriceLowToHigh"] ?
                                   itemPrices.OrderBy(p => p).ToList() :
                                   itemPrices.OrderByDescending(p => p).ToList();
            CollectionAssert.AreEqual(sortedItemPrices, itemPrices);
        }
    }

    private void MainMenuButtonClicker(IWebElement inputElement)
    {
        MainDropdownClicker();
        Thread.Sleep(500);
        Assert.That(inputElement, Is.Not.Null, "The element is null");
        Assert.That(inputElement.Displayed, "The element is not displayed");
        Assert.That(inputElement.Enabled, "The element is not enabled");
        inputElement.Click();
    }

    private void MainDropdownClicker()
    {
        IWebElement dropdownButton = _homePage.MainMenuButton;
        dropdownButton.Click();
    }
    #endregion
}
