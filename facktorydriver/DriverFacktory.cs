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
                
                if (GlobalVariables.devMode)
                {
                    options.AddArgument("--start-maximized");
                }
                else
                {
                    options.AddArgument("--headless"); //desativa a abertura do navegador
                    options.AddArgument("--no-sandbox"); //desativa o recurso de segurança sandbox do Chrome para o uso do mesmo em contêineres Docker
                    options.AddArgument("--disable-dev-shm-usage"); //direciona o Chrome a usar o diretório /tmp, previnindo falhas em ambientes com memória compartilhada limitada em contêineres Docker
                    options.AddArgument("--window-size=1920x1080");
                }

                webDriver = new ChromeDriver(options);
                webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                webDriver.Navigate().GoToUrl(GlobalVariables.urlPlataforma);
                break;
            default:
                throw new NotSupportedException($"{browserType} is not supported.");
        }

        return webDriver;
    }
}