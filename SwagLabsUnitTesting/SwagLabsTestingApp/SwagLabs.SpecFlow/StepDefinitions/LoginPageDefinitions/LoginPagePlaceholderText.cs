using LoginPage = SwagLabsTestingApp.Tests.LoginTests;

namespace SwagLabs.SpecFlow.StepDefinitions.LoginPageDefinitions;

[Binding]
public class LoginPagePlaceholderTextSteps
{
    private readonly LoginPage _loginPage;

    public LoginPagePlaceholderTextSteps(LoginPage loginPage)
    {
        _loginPage = loginPage;
    }

    [Given(@"The user enters the login page")]
    public void GivenIAmOnTheLoginPage()
    {
        _loginPage.SetUp();
    }

    [Then(@"the placeholder text for the (.*) field should be (.*)")]
    public void ThenThePlaceholderTextForTheFieldShouldBe(string expectedPlaceholder, string elementName)
    {
        _loginPage.LoginForm_PlaceholderText_IsCorrect(expectedPlaceholder, elementName);
    }

    [AfterScenario]
    public void TearDown()
    {
        _loginPage.TearDown();
    }
}
