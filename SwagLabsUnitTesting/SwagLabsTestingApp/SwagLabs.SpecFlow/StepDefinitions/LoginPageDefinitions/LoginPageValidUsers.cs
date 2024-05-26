using SwagLabsTestingApp.PageObjectModels;
using LoginTest = SwagLabsTestingApp.Tests.LoginTests;

namespace SwagLabs.SpecFlow.StepDefinitions.LoginPageDefinitions;

[Binding]
public class LoginPageValidUsers
{
    private readonly LoginTest _loginTest;

    public LoginPageValidUsers(LoginTest loginTest)
    {
        _loginTest = loginTest;
    }

    [Given(@"I navigate to the login page")]
    public void GivenINavigateToTheLoginPage()
    {
        _loginTest.SetUp();
    }

    [When(@"I attempt to login the user (.*) (.*) (.*)")]
    public void WhenIAttemptToLoginWithUsernameAndPassword(string userName, string password, bool isLocked)
    {
        LoginModel userModel = new LoginModel()
        {
            Username = userName,
            Password = password,
            IsLocked = isLocked
        };

        _loginTest.Login_WhenCredentials_Valid(userModel);
    }

    [AfterScenario]
    public void AfterScenario()
    {
        _loginTest.TearDown();
    }
}