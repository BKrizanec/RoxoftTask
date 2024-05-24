namespace SwagLabsTestingApp.Utilities;

public class HomeHelper
{
    public const string inventoryUrl = LoginHelper.loginUrl + LoginHelper.successfulLoginEndpoint;
    public const int maxNumberOfItems = 6;
    public static readonly Dictionary<string, string> SortingOptions = new Dictionary<string, string>
    {
        { "NameAscending", "az" },
        { "NameDescending", "za" },
        { "PriceLowToHigh", "lohi" },
        { "PriceHighToLow", "hilo" }
    };
}
