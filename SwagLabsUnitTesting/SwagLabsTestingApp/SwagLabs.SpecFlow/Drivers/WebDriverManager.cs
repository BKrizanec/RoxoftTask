using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

public class WebDriverManager
{
    public static IWebDriver CreateDriver()
    {
        return new ChromeDriver();
    }
    public static void QuitDriver(IWebDriver driver)
    {
        if (driver != null)
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}