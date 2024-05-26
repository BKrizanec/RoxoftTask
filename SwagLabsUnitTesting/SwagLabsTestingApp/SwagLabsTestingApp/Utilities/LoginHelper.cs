namespace SwagLabsTestingApp.Utilities;

public static class LoginHelper
{
    public const string confirmedUser = "standard_user";
    public const string password = "secret_sauce";
    public const string lockedOut = "locked_out_user";
    public const string loginUrl = "https://www.saucedemo.com/";
    public const string successfulLoginEndpoint = "/inventory.html";
    public const string placeholderUsername = "Username";
    public const string placeholderPassword = "Password";
    public const string errorMessageContainer = "error-message-container";
    public const string passwordRequired = "Epic sadface: Password is required";
    public const string usernameRequired = "Epic sadface: Username is required";
    public const string credentialsMismatch = "Epic sadface: Username and password do not match any user in this service";
    public const string lockedOutMessage = "Epic sadface: Sorry, this user has been locked out.";
    public const int acceptableLoginProcess = 3;
}
