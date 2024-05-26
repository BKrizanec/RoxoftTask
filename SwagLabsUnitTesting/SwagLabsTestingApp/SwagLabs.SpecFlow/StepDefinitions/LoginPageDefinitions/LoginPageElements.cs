using LoginTest = SwagLabsTestingApp.Tests.LoginTests;

namespace SwagLabs.SpecFlow.StepDefinitions.LoginPageDefinitions;

[Binding]
public class LoginPageElements
{
    private readonly LoginTest _loginTest;

    public LoginPageElements(LoginTest loginTest)
    {
        _loginTest = loginTest;        
    }

    [Given(@"I am on the login page")]
    public void GivenIAmOnTheLoginPage()
    {
        _loginTest.SetUp();
    }

    [Then(@"the login form elements should be displayed")]
    public void ThenTheLoginFormElementsShouldBeDisplayed()
    {
        _loginTest.LoginForm_Element_IsDisplayed();
    }

    [AfterScenario]
    public void TearDown()
    {
        _loginTest.TearDown();    
    }
}
