using LoginTest = SwagLabsTestingApp.Tests.LoginTests;
using LoginPage = SwagLabsTestingApp.Pages.LoginPage;
using OpenQA.Selenium;
using SwagLabsTestingApp.PageObjectModels;
using OpenQA.Selenium.Chrome;
using SwagLabsTestingApp.Utilities;
using NUnit.Framework;

namespace SwagLabs.SpecFlow.StepDefinitions.LoginPageDefinitions;

[Binding]
public class LoginProcessSteps
{
    private readonly ScenarioContext _scenarioContext;
    private IWebDriver _driver;
    private LoginPage _loginPage;

    public LoginProcessSteps(ScenarioContext scenarioContext)
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

    [Given(@"I navigate to the login page")]
    public void GivenINavigateToTheLoginPage()
    {        
        _driver.Navigate().GoToUrl(LoginHelper.loginUrl);
        _driver.Manage().Window.Maximize();

        _loginPage = new LoginPage(_driver);
    }

    [When(@"I attempt to login with valid credentials (.*) and (.*)")]
    public void WhenIAttemptToLoginWithValidCredentials(string username, string password)
    {
        if(!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
        {
            _loginPage.AttemptLogin(username, password);
        }
    }

    [Then(@"the login should be successful if the user is not (.*)")]
    public void ThenTheLoginShouldBeSuccessfulIfIsFalse(bool isLocked)
    {
        if(!isLocked) 
        {
            DateTime startTime = DateTime.Now;
            bool isLoggedIn = _driver.Url.Contains(LoginHelper.successfulLoginEndpoint);
            DateTime endTime = DateTime.Now;
            TimeSpan loginTime = endTime - startTime;

            Assert.That(isLoggedIn, Is.True, "Expected login to succeed for an unlocked user.");
            
            if (loginTime.TotalSeconds > LoginHelper.acceptableLoginProcess)
                Console.WriteLine($"CAUTION: The login time exceeded the acceptable limit. There may be performance issues.");
        }
        else
        {
            bool isLoggedIn = _driver.Url.Contains(LoginHelper.successfulLoginEndpoint);
            Assert.That(isLoggedIn, Is.False, "Expected login to fail for a locked user.");
        }
    }

    [When(@"I attempt to login with invalid credentials (.*) and (.*)")]
    public void WhenIAttemptToLoginWithInvalidCredentials(string invalidUsername, string invalidPassword)
    {       
        _loginPage.AttemptLogin(invalidUsername, invalidPassword);
    }

    [Then(@"I should see the error message (.*)")]
    public void ThenIShouldSeeTheErrorMessage(string expectedErrorMessage)
    {
        string actualErrorMessage = _loginPage.ErrorMessageContainer.Text;
        Assert.That(actualErrorMessage, Is.EqualTo(expectedErrorMessage), "Error message does not match expected.");        
    }

    [AfterScenario]
    public void AfterScenario()
    {
        _driver = _scenarioContext.Get<IWebDriver>("WebDriver");
        WebDriverManager.QuitDriver(_driver);
    }
}

