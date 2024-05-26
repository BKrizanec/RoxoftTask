using OpenQA.Selenium;

namespace SwagLabsTestingApp.PageObjectModels;

public struct HomeModel
{
    public IWebElement ItemDescription { get; set; }
    public IWebElement ItemImage { get; set; }
    public IWebElement ItemTitle { get; set;}
    public IWebElement ItemPrice { get; set;}
}
