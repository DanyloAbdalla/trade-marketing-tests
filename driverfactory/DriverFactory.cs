using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MeuClienteWebTestProject;

/// <summary>
/// Classe de criação da fabrica de Drivers
/// </summary>
public class DriverFactory
{
    [ThreadStatic]
    private static IWebDriver webDriver;
    private static ChromeOptions chromeOptions = new ChromeOptions();

    /// <summary>
    /// Método para criação do WebDriver para múltiplos browsers
    /// </summary>
    /// <param name="browserType"></param>
    /// <returns>Retorna a instância webDriver do Selenium</returns>
    /// <exception cref="NotSupportedException"></exception>
    public static IWebDriver CreateDriver(BrowserType browserType)
    {
        switch (browserType)
        {
            case BrowserType.Chrome:
                if (!GlobalVariables.headLessMode)
                {
                    chromeOptions.AddArgument("--start-maximized");
                }
                else
                {
                    chromeOptions.AddArgument("--headless=new");
                    chromeOptions.AddArgument("--no-sandbox");
                    chromeOptions.AddArgument("--disable-dev-shm-usage");
                    chromeOptions.AddArgument("--disable-gpu");
                    chromeOptions.AddArgument("--window-size=1920,1080");
                    chromeOptions.AddArgument("--disable-software-rasterizer");
                    chromeOptions.AddArgument("--remote-allow-origins=*");
                    chromeOptions.BinaryLocation = "/usr/bin/google-chrome";
                    chromeOptions.SetLoggingPreference(LogType.Browser, LogLevel.All);
                }

                webDriver = new ChromeDriver(chromeOptions);
                webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                if (GlobalVariables.devMode)
                    webDriver.Navigate().GoToUrl(GlobalVariables.urlDevPlataforma);
                if (GlobalVariables.hmlMode)
                    webDriver.Navigate().GoToUrl(GlobalVariables.urlHmlPlataforma);
                if (GlobalVariables.prodMode)
                    webDriver.Navigate().GoToUrl(GlobalVariables.urlPrdPlataforma);
                break;
            default:
                throw new NotSupportedException($"{browserType} is not supported.");
        }

        return webDriver;
    }
}