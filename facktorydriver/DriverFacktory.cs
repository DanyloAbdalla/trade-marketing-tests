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
                var options = new ChromeOptions();

                if (!GlobalVariables.handLessMode)
                {
                    options.AddArgument("--start-maximized");
                }
                else
                {
                    options.AddArgument("--headless"); //desativa a abertura do navegador
                    options.AddArgument("--no-sandbox"); //desativa o recurso de segurança sandbox do Browser para o uso do mesmo em contêineres Docker
                    options.AddArgument("--disable-dev-shm-usage"); //direciona o Browser a usar o diretório /tmp, previnindo falhas em ambientes com memória compartilhada limitada em contêineres Docker
                    options.AddArgument("--start-maximized"); //inicia com o Browser maximizado
                }

                webDriver = new ChromeDriver(options);
                webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                if(GlobalVariables.devMode)
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