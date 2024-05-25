namespace SwagLabsTestingApp.Utilities;

public class HomeHelper
{
    public const string inventoryUrl = LoginHelper.loginUrl + LoginHelper.successfulLoginEndpoint;
    public const int maxNumberOfItems = 6;
    public const string cartUrl = "https://www.saucedemo.com/cart.html";
    public const string aboutUrl = "https://saucelabs.com/";
    public static readonly Dictionary<string, string> SortingOptions = new Dictionary<string, string>
    {
        { "NameAscending", "az" },
        { "NameDescending", "za" },
        { "PriceLowToHigh", "lohi" },
        { "PriceHighToLow", "hilo" }
    };
}
