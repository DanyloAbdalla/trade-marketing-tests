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
                var chromeOptions = new ChromeOptions();
                var environment = Environment.GetEnvironmentVariable("CI");

                if (environment != null && environment == "true")
                {
                    chromeOptions.AddArgument("--headless=new");
                    chromeOptions.AddArgument("--no-sandbox");
                    chromeOptions.AddArgument("--disable-dev-shm-usage");
                    chromeOptions.AddArgument("--disable-gpu");
                    chromeOptions.AddArgument("--window-size=1920,1080");
                    chromeOptions.AddArgument("--remote-allow-origins=*");
                    chromeOptions.AddArgument("--remote-debugging-port=9222");
                    chromeOptions.AddArgument("--no-first-run");
                    chromeOptions.AddArgument("--disable-extensions");
                    chromeOptions.AddArgument("--disable-sync");
                    chromeOptions.AddArgument("--disable-translate");
                    chromeOptions.AddArgument("--disable-background-networking");
                    chromeOptions.AddArgument("--disable-component-extensions-with-background-pages");
                    chromeOptions.SetLoggingPreference(LogType.Browser, LogLevel.All);

                    try
                    {
                        webDriver = new ChromeDriver(chromeOptions);
                    }
                    catch (WebDriverException ex)
                    {
                        Console.Error.WriteLine("Erro ao iniciar o ChromeDriver em modo headless: ");
                        Console.Error.WriteLine(ex.Message);
                        Console.Error.WriteLine($"Inner: {ex.InnerException?.Message}");
                        Console.Error.WriteLine($"ChromeDriver Path: {Environment.GetEnvironmentVariable("PATH")}");

                        throw new InvalidOperationException(
                            "Falha ao iniciar o ChromeDriver. Verifique se o ChromeDriver está instalado corretamente e acessível no PATH do sistema.", ex);
                    }

                    webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(90);
                    webDriver.Navigate().GoToUrl(GlobalVariables.urlHmlPlataforma);
                }
                else
                {
                    chromeOptions.AddArgument("--start-maximized");
                    webDriver = new ChromeDriver(chromeOptions);
                    webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                    webDriver.Navigate().GoToUrl(GlobalVariables.urlDevPlataforma);
                }

                break;
            default:
                throw new NotSupportedException($"{browserType} is not supported.");
        }

        return webDriver;
    }
}