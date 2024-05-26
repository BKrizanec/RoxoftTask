using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SwagLabsTestingApp.Pages;
using SwagLabsTestingApp.Utilities;

namespace SwagLabs.SpecFlow.StepDefinitions.LoginPageDefinitions;

[Binding]
public class LoginPageElements
{
    private readonly ScenarioContext _scenarioContext;
    private IWebDriver _driver;
    private LoginPage _loginPage;

    public LoginPageElements(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [BeforeScenario]
    public void DriverSetup()
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
    }

    [Given(@"I open the login page")]
    public void GivenIOpenTheLoginPage()
    {
        
        _driver.Navigate().GoToUrl(LoginHelper.loginUrl);
        _driver.Manage().Window.Maximize();

        _loginPage = new LoginPage(_driver);
    }

    [Then(@"the login form elements should be displayed")]
    public void ThenTheLoginFormElementsShouldBeDisplayed()
    {
        IEnumerable<IWebElement> elements = _loginPage.ElementsCollector();
        foreach (IWebElement element in elements)
        {
            Assert.That(element.Enabled, Is.True);
            Assert.That(element.Displayed, Is.True);
        }
    }

    [Then(@"the placeholder text for the (.*) field should be (.*)")]
    public void ThenThePlaceholderTextForTheFieldShouldBe(string expectedPlaceholder, string elementName)
    {
        IWebElement element = _driver.FindElement(By.Name(elementName));
        string actualPlaceholder = element.GetAttribute("placeholder");

        Assert.That(actualPlaceholder, Is.EqualTo(expectedPlaceholder));
        if (actualPlaceholder == expectedPlaceholder)
            Console.WriteLine($"{elementName} placeholder is correct.");
    }

    [AfterScenario]
    public void AfterScenario()
    {
        _driver = _scenarioContext.Get<IWebDriver>("WebDriver");
        WebDriverManager.QuitDriver(_driver);
    }
}
