using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;

namespace MeuClienteWebTestProject;

/// <summary>
/// Classe de criação da fabrica de Drivers
/// </summary>
public class DriverFactory
{
    /// <summary>
    /// Método para criação do WebDriver para múltiplos browsers
    /// </summary>
    /// <param name="browserType"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static IWebDriver CreateDriver(BrowserType browserType)
    {
        IWebDriver webDriver;

        switch (browserType)
        {
            case BrowserType.Chrome:
                webDriver = new ChromeDriver();
                break;
            case BrowserType.Firefox:
                webDriver = new FirefoxDriver();
                break;
            case BrowserType.Edge:
                webDriver = new EdgeDriver();
                break;
            default:
                throw new NotSupportedException($"{browserType} is not supported.");
        }

        // Configurações comuns
        webDriver.Manage().Window.Maximize();
        webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        return webDriver;
    }
}