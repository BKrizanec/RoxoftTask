using OpenQA.Selenium;
using SwagLabsTestingApp.PageObjectModels;
using SwagLabsTestingApp.Utilities;

namespace SwagLabsTestingApp.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement UserNameText => _driver.FindElement(By.Name("user-name"));
        public IWebElement PasswordText => _driver.FindElement(By.Name("password"));
        public IWebElement LoginButton => _driver.FindElement(By.Id("login-button"));
        public IWebElement ErrorMessageContainer => _driver.FindElement(By.ClassName(LoginHelper.errorMessageContainer));


        public void AttemptLogin(string username, string password)
        {
            UserNameText.SendKeys(username);
            PasswordText.SendKeys(password);
            LoginButton.Click();
        }

        public IEnumerable<IWebElement> ElementsCollector()
        {
            List<IWebElement> elementsToAssert = new List<IWebElement>
            {
                UserNameText ,
                PasswordText,
                LoginButton,
                ErrorMessageContainer
            };

            foreach (var element in elementsToAssert) 
            {
                yield return element;
            }
        }

        public static IEnumerable<LoginModel> GetUsers()
        {
            List<LoginModel> users = new List<LoginModel>()
            {
                new LoginModel { Username = "standard_user", Password = LoginHelper.password, IsLocked = false },
                new LoginModel { Username = "locked_out_user", Password = LoginHelper.password, IsLocked = true, ExpectedErrorMessage = LoginHelper.lockedOutMessage },
                new LoginModel { Username = "problem_user", Password = LoginHelper.password, IsLocked = false },
                new LoginModel { Username = "performance_glitch_user", Password = LoginHelper.password, IsLocked = false },
                new LoginModel { Username = "error_user", Password = LoginHelper.password, IsLocked = false },
                new LoginModel { Username = "visual_user", Password = LoginHelper.password, IsLocked = false }
            };

            foreach(var user in users)
            {
                yield return user;
            }
        }
        
        public static IEnumerable<LoginModel> GetInvalidUsers()
        {
            List<LoginModel> invalidUsers = new List<LoginModel>()
            {
                new LoginModel {Username = "standard_user", Password = "", ExpectedErrorMessage = LoginHelper.passwordRequired},
                new LoginModel {Username = "", Password = LoginHelper.password, ExpectedErrorMessage = LoginHelper.usernameRequired},
                new LoginModel {Username = "random_nonexistant_user", Password = "secret_sauce", ExpectedErrorMessage = LoginHelper.credentialsMismatch},
                new LoginModel {Username = "standard_user", Password = "random_wrong_password",  ExpectedErrorMessage = LoginHelper.credentialsMismatch}
            };

            foreach(var user in invalidUsers)
            {
                yield return user;
            }
        }
    }
}
