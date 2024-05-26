using SwagLabsTestingApp.PageObjectModels;
using LoginTest = SwagLabsTestingApp.Tests.LoginTests;

namespace SwagLabs.SpecFlow.StepDefinitions.LoginPageDefinitions;

[Binding]
public class LoginPageInvalidUsers
{
    private readonly LoginTest _loginTest;

    public LoginPageInvalidUsers(LoginTest loginTest)
    {
        _loginTest = loginTest;
    }

    [Given(@"I navigated to the login page")]
    public void GivenINavigateToTheLoginPage()
    {
        _loginTest.SetUp();
    }

    [When(@"I attempt to login as user (.*) (.*) and expect message (.*)")]
    public void WhenIAttemptToLoginWithUsernamePasswordAndExpectErrorMessage(string username, string password, string expectedErrorMessage)
    {
        var userModel = new LoginModel
        {
            Username = username,
            Password = password,
            ExpectedErrorMessage = expectedErrorMessage
        };

        _loginTest.Login_WhenCredentials_Invalid(userModel);
    }

    [AfterScenario]
    public void AfterScenario()
    {
        _loginTest.TearDown();
    }
}
