using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SwagLabsTestingApp.PageObjectModels;
using SwagLabsTestingApp.Pages;
using SwagLabsTestingApp.Utilities;

namespace SwagLabsTestingApp.Tests;

public class LoginTests
{
    private IWebDriver _driver;
    private LoginPage _loginPage;

    [SetUp]
    public void SetUp()
    {
        _driver = new ChromeDriver();
        _driver.Navigate().GoToUrl(LoginHelper.loginUrl);
        _driver.Manage().Window.Maximize();

        _loginPage = new LoginPage(_driver);
    }

    [Test]
    public void LoginForm_Element_IsDisplayed()
    {
        IEnumerable<IWebElement> elements = _loginPage.ElementsCollector();
        foreach(IWebElement element in elements) 
        {
            Assert.That(element.Enabled, Is.True);
            Assert.That(element.Displayed, Is.True);
        }
    }

    [Test]
    [TestCase(LoginHelper.placeholderUsername, "user-name")]
    [TestCase(LoginHelper.placeholderPassword, "password")]
    public void LoginForm_PlaceholderText_IsCorrect(string expectedPlaceholder, string elementName)
    {
        IWebElement element = _driver.FindElement(By.Name(elementName));
        string actualPlaceholder = element.GetAttribute("placeholder");

        Assert.That(actualPlaceholder, Is.EqualTo(expectedPlaceholder));
        if (actualPlaceholder == expectedPlaceholder)
            Console.WriteLine($"{elementName} placeholder is correct.");
    }

    [TestCaseSource(typeof(LoginPage), nameof(LoginPage.GetUsers))]
    public void Login_WhenCredentials_Valid(LoginModel user)
    {
        DateTime startTime = DateTime.Now;

        _driver.Navigate().GoToUrl(LoginHelper.loginUrl);
        _loginPage.AttemptLogin(user.Username, user.Password);

        DateTime endTime = DateTime.Now;
        TimeSpan loginTime = endTime - startTime;

        bool isLoggedIn = _driver.Url.Contains(LoginHelper.successfulLoginEndpoint);

        if (user.IsLocked)
            Assert.That(isLoggedIn, Is.False, $"{user.InfoText}");
        else
            Assert.That(isLoggedIn, Is.True, $"{user.InfoText}");

        if(loginTime.TotalSeconds > LoginHelper.acceptableLoginProcess)
            Console.WriteLine($"CAUTION: The login time for {user.Username} excedeed the acceptable limit. There may be performance issues.");
    }

    [TestCaseSource(typeof(LoginPage), nameof(LoginPage.GetInvalidUsers))]
    public void Login_WhenCredentials_Invalid(LoginModel user)
    {
        _driver.Navigate().GoToUrl(LoginHelper.loginUrl);
        _loginPage.AttemptLogin(user.Username, user.Password);

        IWebElement element = _driver.FindElement(By.ClassName(LoginHelper.errorMessageContainer));
        string actualErrorMessage = element.FindElement(By.TagName("h3")).Text;

        Assert.That(element.Displayed);
        Assert.That(actualErrorMessage, Is.EqualTo(user.ExpectedErrorMessage));
    }

    [TearDown]
    public void TearDown()
    {
        _driver.Dispose();
    }
}
