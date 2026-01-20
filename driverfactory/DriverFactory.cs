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
                    chromeOptions.AddArgument("--headless"); //desativa a abertura do navegador
                    chromeOptions.AddArgument("--no-sandbox"); //desativa o recurso de segurança sandbox do Browser para o uso do mesmo em contêineres Docker
                    chromeOptions.AddArgument("--disable-dev-shm-usage"); //direciona o Browser a usar o diretório /tmp, previnindo falhas em ambientes com memória compartilhada limitada em contêineres Docker
                    chromeOptions.AddArgument("--start-maximized"); //inicia com o Browser maximizado
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