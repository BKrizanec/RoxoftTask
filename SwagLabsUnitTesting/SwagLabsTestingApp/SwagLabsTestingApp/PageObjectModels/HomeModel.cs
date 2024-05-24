using OpenQA.Selenium;

namespace SwagLabsTestingApp.PageObjectModels;

public class HomeModel
{
    public IWebElement ItemDescription { get; set; }
    public IWebElement ItemImage { get; set; }
    public IWebElement ItemTitle { get; set;}
    public IWebElement ItemPrice { get; set;}
}
