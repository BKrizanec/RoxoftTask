using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SwagLabsTestingApp.PageObjectModels;

namespace SwagLabsTestingApp.Pages;

public class HomePage
{
    private readonly IWebDriver _driver;

    public HomePage(IWebDriver driver)
    {
        _driver = driver;
    }

    public IWebElement MainMenuButton => _driver.FindElement(By.ClassName("bm-burger-button"));   
    public IWebElement SortingDropdown => _driver.FindElement(By.ClassName("product_sort_container"));
    public IWebElement ShoppingCartLink => _driver.FindElement(By.ClassName("shopping_cart_link"));    
    public IWebElement SocialContainer => _driver.FindElement(By.ClassName("social"));
    public IWebElement AllItemsMainMenu => _driver.FindElement(By.Id("inventory_sidebar_link"));
    public IWebElement AboutMainMenu => _driver.FindElement(By.Id("about_sidebar_link"));
    public IWebElement LogoutMainMenu => _driver.FindElement(By.Id("logout_sidebar_link"));
    public IWebElement ResetAppStateMainMenu => _driver.FindElement(By.Id("reset_sidebar_link"));

    public IEnumerable<IWebElement> ItemNames => _driver.FindElements(By.ClassName("inventory_item_name"));
    public IEnumerable<IWebElement> ItemPrices => _driver.FindElements(By.ClassName("inventory_item_price"));
    public IEnumerable<IWebElement> AddToCartButtons => _driver.FindElements(By.CssSelector(".btn_primary.btn_inventory"));

    public IEnumerable<IWebElement> ItemContainers => _driver.FindElements(By.ClassName("inventory_item"));
      

    public List<HomeModel> GetInventoryItems()
    {
        var items = new List<HomeModel>();

        var itemContainers = ItemContainers;

        foreach (var itemContainer in itemContainers)
        {
            var item = new HomeModel
            {
                ItemDescription = WaitForElement(By.ClassName("inventory_item_desc"), itemContainer),
                ItemImage = itemContainer.FindElement(By.ClassName("inventory_item_img")),
                ItemTitle = itemContainer.FindElement(By.ClassName("inventory_item_name")),
                ItemPrice = itemContainer.FindElement(By.ClassName("inventory_item_price"))
            };
            items.Add(item);
        }

        return items;
    }

    public IEnumerable<IWebElement> GetElements(IWebElement element, string value)
    {
        IWebElement parentElement = element;
        IEnumerable<IWebElement> childElements = parentElement.FindElements(By.TagName(value));

        return childElements;
    }

    public IWebElement RemoveFromCartButton(string itemName)
    {
        return _driver.FindElement(By.XPath($"//div[text()='{itemName}']/ancestor::div[@class='inventory_item']//button[contains(@class, 'btn_secondary')]"));
    }

    public int GetCartItemCount()
    {
        try
        {
            IWebElement cartIcon = _driver.FindElement(By.ClassName("shopping_cart_badge"));
            string itemCountText = cartIcon.Text;
            int itemCount = int.Parse(itemCountText);

            return itemCount;
        }
        catch (Exception)
        {

            return 0;
        }        
    }

    public SelectElement GetDropdownOptions()
    {
        var selectElement = SortingDropdown;
        return new SelectElement(selectElement);
    }

    public List<string> GetInventoryItemNames()
    {
        return ItemNames.Select(i => i.Text).ToList();
    }

    public List<decimal> GetInventoryItemPrices()
    {
        return ItemPrices.Select(p => Decimal.Parse(p.Text.Replace("$", ""))).ToList();
    }

    private IWebElement WaitForElement(By by, IWebElement container, int timeoutInSeconds = 10)
    {
        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutInSeconds));
        return wait.Until(driver => container.FindElement(by));
    }
}
