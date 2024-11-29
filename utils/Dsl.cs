using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;
using System.Text.RegularExpressions;

namespace MeuClienteWebTestProject;

/// <summary>
/// Classe que contem métodos para ajudar na manipulação\interação dos elementos
/// Que compartilham o mesmo funcionamento, em diferentes telas da plataforma
/// /// </summary>
public class Dsl
{
    /// <summary>
    /// Método com uma espera padrão de 1 segundo
    /// </summary>
    public static void Esperar1Segundo()
    {
        Thread.Sleep(1000);
    }

    /// <summary>
    /// Método para consultar o elemento a cada meio segundo, até que ele esteja vísivel em tela em uma espera de 10 segundos
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <exception cref="Exception"></exception>
    public static void EsperarVisibilidadeDoElemento(IWebDriver webDriver, string XPath)
    {
        var fluentWait = new DefaultWait<IWebDriver>(webDriver)
        {
            Timeout = TimeSpan.FromSeconds(30),
            PollingInterval = TimeSpan.FromMilliseconds(500)
        };

        fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));

        try
        {
            fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath(XPath)));
        }
        catch (WebDriverTimeoutException)
        { Console.WriteLine("O elemento não foi localizado na página"); }
    }

    /// <summary>
    /// Método para esperar que um elemento não esteja mais presente no DOM
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <exception cref="WebDriverTimeoutException"></exception>
    public static void EsperarInvisibilidadeDoElemento(IWebDriver webDriver, string XPath)
    {
        var fluentWait = new DefaultWait<IWebDriver>(webDriver)
        {
            Timeout = TimeSpan.FromSeconds(30),
            PollingInterval = TimeSpan.FromMilliseconds(500)
        };

        fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));

        try
        {
            fluentWait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(XPath)));
        }
        catch (WebDriverTimeoutException)
        { Console.WriteLine("O elemento não foi localizado na página"); }

    }

    /// <summary>
    /// Método para digitar nos elementos campos de texto que possuem lista de registros
    /// Filtrando os registros conforme o campo é preenchido
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <exception cref="Exception"></exception>
    public static void DigitarNoCampoTextoComboList(IWebDriver webDriver, string XPath, string textoValor)
    {
        try
        {
            EsperarVisibilidadeDoElemento(webDriver, XPath);

            for (int i = 0; i < textoValor.Length; i++)
            {
                webDriver.FindElement(By.XPath(XPath)).SendKeys(textoValor[i].ToString());
            }
        }
        catch (Exception ex)
        { throw new Exception(ex.Message); }
    }

    /// <summary>
    /// Método para digitar nos elementos campos de texto
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <exception cref="Exception"></exception>
    public static void DigitarNoCampoTexto(IWebDriver webDriver, string XPath, string textoValor)
    {
        try
        {
            EsperarVisibilidadeDoElemento(webDriver, XPath);
            Actions action = new Actions(webDriver);
            action.SendKeys(webDriver.FindElement(By.XPath(XPath)), textoValor).Perform();
        }
        catch (Exception ex)
        { throw new Exception(ex.Message); }
    }

    /// <summary>
    /// Método que espera o elemento ficar disponível para o click
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="xpath"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static void EsperarElementoFicarClicavel(IWebDriver webDriver, string XPath, string nomeElemento)
    {
        var fluentWait = new DefaultWait<IWebDriver>(webDriver)
        {
            Timeout = TimeSpan.FromSeconds(30),
            PollingInterval = TimeSpan.FromMilliseconds(500)
        };

        fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));

        try
        {
            fluentWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(XPath)));
        }
        catch (Exception ex)
        { throw new Exception(ex.Message + "\n" + nomeElemento); }
    }

    /// <summary>
    /// Método que espera o elemento para receber o clique
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="xpath"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static void EsperarElementoParaClicar(IWebDriver webDriver, string XPath, string nomeElemento)
    {
        var fluentWait = new DefaultWait<IWebDriver>(webDriver)
        {
            Timeout = TimeSpan.FromSeconds(30),
            PollingInterval = TimeSpan.FromMilliseconds(500)
        };

        fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));

        try
        {
            fluentWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(XPath))).Click();
        }
        catch (Exception ex)
        { throw new Exception(ex.Message + "\n" + nomeElemento); }
    }

    /// <summary>
    /// Método que espera até que um texto específico seja apresentado no elemento
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="xpath"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string PegarTextoDoElemento(IWebDriver webDriver, string XPath, string nomeElemento)
    {
        try
        {
            var textoElemento = webDriver.FindElement(By.XPath(XPath)).Text;
            
            return textoElemento;
        }
        catch (Exception ex)
        { throw new Exception(ex.Message + "\n" + nomeElemento); }
    }

    /// <summary>
    /// Método para clicar em um elemento
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="xpath"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static void Clicar(IWebDriver webDriver, string XPath, string nomeElemento)
    {
        try
        {
            webDriver.FindElement(By.XPath(XPath)).Click();
        }
        catch (Exception ex)
        { throw new Exception(ex.Message + "\n" + nomeElemento); }
    }

    /// <summary>
    /// Método para contar quantas vezes o elemento existe na tela
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <exception cref="Exception"></exception>
    public static long ContarExistenciaDoElemento(IWebDriver webDriver, string XPath)
    {
        try
        {
            // Definindo o script JavaScript usando XPath para consultar os elementos
            string script = $@"
                var xpath = ""{XPath.Replace("\"", "\\\"")}"";
                var elements = document.evaluate(xpath, document, null, XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null);
                return elements.snapshotLength;";

            // Executando o script JavaScript para contar os elementos
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)webDriver;
            long elementCount = (long)jsExecutor.ExecuteScript(script);

            return elementCount;
        }
        catch (WebDriverTimeoutException ex)
        { throw new WebDriverTimeoutException(ex.Message); }
    }

    /// <summary>
    /// Método para contar quantas vezes o elemento existe em um modal
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <exception cref="Exception"></exception>
    public static long ContarExistenciaDoElementoEmModal(IWebDriver webDriver, string XPathModal, string XPathElement)
    {
        try
        {
            EsperarVisibilidadeDoElemento(webDriver, XPathModal);

            // Definindo o script JavaScript usando XPath para consultar os elementos
            string script = $@"
                var xpath = ""{XPathElement.Replace("\"", "\\\"")}"";
                var elements = document.evaluate(xpath, document, null, XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null);
                return elements.snapshotLength;";

            // Executando o script JavaScript para contar os elementos
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)webDriver;
            long elementCount = (long)jsExecutor.ExecuteScript(script);

            return elementCount;
        }
        catch (WebDriverTimeoutException ex)
        { throw new WebDriverTimeoutException(ex.Message); }
    }

    /// <summary>
    /// Método para realizar scroll até um elemento específico
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    public static void ScrollParaElemento(IWebDriver webDriver, string XPath)
    {
        EsperarVisibilidadeDoElemento(webDriver, XPath);

        IWebElement webElement = webDriver.FindElement(By.XPath(XPath));

        Actions action = new Actions(webDriver);
        action.MoveToElement(webElement).Perform();
    }

    /// <summary>
    /// Método para obter a quantidade de linhas (tag tr) em um elemento do tipo tabela
    /// </summary>
    /// <param name="driver"></param>
    /// <param name="XPath"></param>
    /// <returns>Retorna um número inteiro</returns>
    public static int ObterQuantidadeLinhasNoElementoTabelaComLinhaInvisivel(IWebDriver webDriver, string XPath)
    {
        IWebElement tabela = webDriver.FindElement(By.XPath(XPath));
        IList<IWebElement> linhas = tabela.FindElements(By.XPath("tr"));

        var qtdLinhas = linhas.Count() - 1;

        return qtdLinhas;
    }

    /// <summary>
    /// Método para obter a quantidade de linhas (tag tr) em um elemento do tipo tabela
    /// Existem tabelas que são apresentadas no sistema com uma tag tr a mais (que não é apresentada em tela)
    /// Nesse cenário essa tag tr a mais é desconsiderada, retornando a quantidade real de linhas apresentadas em tela
    /// </summary>
    /// <param name="driver"></param>
    /// <param name="XPath"></param>
    /// <returns>Retorna um número inteiro</returns>
    public static int ObterQuantidadeLinhasNoElementoTabelaSemLinhaInvisivel(IWebDriver webDriver, string XPath)
    {
        EsperarVisibilidadeDoElemento(webDriver, XPath);

        IWebElement tabela = webDriver.FindElement(By.XPath(XPath));
        IList<IWebElement> linhas = tabela.FindElements(By.XPath("tr"));

        var qtdLinhas = linhas.Count();

        return qtdLinhas;
    }

    /// <summary>
    /// Método para retornar os dados do atributo value de um elemento
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <returns>Retorna os dados contidos no atributo como uma string</returns>
    public static string ObterDadosDoAtributoValueDoElemento(IWebDriver webDriver, string XPath)
    {
        EsperarVisibilidadeDoElemento(webDriver, XPath);
        var dados = webDriver.FindElement(By.XPath(XPath)).GetAttribute("value");

        return dados;
    }

    /// <summary>
    /// Método para selecionar datas de início vingencia, baseado na data atual
    /// Avançando para os meses seguintes se qtdAvancarMeses for maior que 0
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <param name="qtdAvancarMeses"></param>
    public static void PreencherCalendariosInicioVigencia(IWebDriver webDriver, string XPath, int qtdAvancarMeses)
    {
        DateTime dataAtual = DateTime.Now;
        var diaAtual = dataAtual.Day;

        EsperarVisibilidadeDoElemento(webDriver, XPath);

        if (qtdAvancarMeses == 0)
        {
            webDriver.FindElement(By.XPath($"((//div[@class='ant-picker-body'])[1]//div[text()='{diaAtual}'])[1]")).Click();
        }
        else if (qtdAvancarMeses > 0)
        {
            for (int i = 0; i < qtdAvancarMeses; i++)
            {
                webDriver.FindElement(By.XPath(XPath)).Click();
            }

            webDriver.FindElement(By.XPath($"((//div[@class='ant-picker-body'])[1]//div[text()='{diaAtual}'])[1]")).Click();
        }
    }

    /// <summary>
    /// Método para selecionar datas de fim vingencia, baseado na data atual
    /// Avançando para os meses seguintes se qtdAvancarMeses for maior que 0
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <param name="qtdAvancarMeses"></param>
    public static void PreencherCalendariosFimVigencia(IWebDriver webDriver, string XPath, int qtdAvancarMeses)
    {
        DateTime dataAtual = DateTime.Now;
        var diaAtual = dataAtual.Day;

        EsperarVisibilidadeDoElemento(webDriver, XPath);

        if (qtdAvancarMeses == 0)
        {
            webDriver.FindElement(By.XPath($"((//div[@class='ant-picker-body'])[2]//div[text()='{diaAtual}'])[1]")).Click();
        }
        else if (qtdAvancarMeses > 0)
        {
            for (int i = 0; i < qtdAvancarMeses; i++)
            {
                webDriver.FindElement(By.XPath(XPath)).Click();
            }

            var qtdDiasCalendario = Dsl.ContarExistenciaDoElemento(webDriver, $"(//div[@class='ant-picker-body'])[2]//div[text()='{diaAtual}']");

            if (qtdDiasCalendario > 1)
            {
                webDriver.FindElement(By.XPath($"((//div[@class='ant-picker-body'])[2]//div[text()='{diaAtual}'])[2]")).Click();
            }
            else
            {
                webDriver.FindElement(By.XPath($"((//div[@class='ant-picker-body'])[2]//div[text()='{diaAtual}'])[1]")).Click();
            }

        }
    }

    /// <summary>
    /// Método para remover números e espaços em branco de um texto
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <returns>Retorna o texto somente com letras maiúsculas e minúsculas</returns>
    public static string RemoverNumerosEspacosDeUmTexto(IWebDriver webDriver, string XPath)
    {
        try
        {
            EsperarVisibilidadeDoElemento(webDriver, XPath);

            var texto = webDriver.FindElement(By.XPath(XPath)).Text;
            var textoTratado = Regex.Replace(texto, @"[\d\s:]", "");

            if (textoTratado.Any(char.IsLetter) || textoTratado.Any(char.IsPunctuation))
            {
                return textoTratado;
            }
            else
            {
                throw new FormatException("Texto não contém letras/pontuação");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    /// <summary>
    /// Método para remover lestras maiúsculas, minúsculas e espaços em branco de um texto
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <returns>Retorna os números dentro de um texto</returns>
    public static int RemoverLetrasEspacosDeUmTexto(IWebDriver webDriver, string XPath)
    {
        EsperarVisibilidadeDoElemento(webDriver, XPath);

        var texto = webDriver.FindElement(By.XPath(XPath)).Text;
        var textoTratado = Regex.Replace(texto, @"[a-zA-Z\s:]", "");

        if (int.TryParse(textoTratado, out int numero))
        {
            return numero;
        }
        else
        {
            throw new FormatException("A string não tem um número válido");
        }
    }

    /// <summary>
    /// Método para validar mensagens de sucesso e mensagens de alertas em telas
    /// </summary>
    /// <param name="mensagemAtual"></param>
    /// <param name="mensagemEsperada"></param>
    /// <returns></returns>
    public static void ValidarMensagemDeSucessoEAlerta(string mensagemAtual, string mensagemEsperada)
    {
        Assert.That(mensagemAtual, Does.Contain(mensagemEsperada), "Mensagem atual não corresponde com a esperada");
    }

    /// <summary>
    /// Método para validar mensagens de sucesso e mensagens de alertas em telas
    /// </summary>
    /// <param name="mensagemAtual"></param>
    /// <param name="mensagemEsperada"></param>
    /// <returns></returns>
    public static void ValidarTextosNoElemento(IWebDriver webDriver, string XPath, string textoEsperado)
    {
        var textoAtual = PegarTextoDoElemento(webDriver, XPath, "Coluna Ativo");
        Assert.That(textoAtual, Does.Contain(textoEsperado), "Nome do ativo não corresponde a busca");
    }

    /// <summary>
    /// Método para buscar registros dentro do grid das telas
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="xPathFiltrar"></param>
    /// <param name="xPathPesquisar"></param>
    /// <param name="xPathBuscar"></param>
    /// <param name="textoValor"></param>
    public static void BuscarRegistros(IWebDriver webDriver, string xPathFiltrar, string xPathPesquisar, string xPathBuscar, string textoValor)
    {
        Esperar1Segundo();
        Clicar(webDriver, xPathFiltrar, "Botão Filtrar Plano");
        webDriver.FindElement(By.XPath(xPathPesquisar)).SendKeys(Keys.Control + "a" + Keys.Backspace);
        DigitarNoCampoTexto(webDriver, xPathPesquisar, textoValor);
        Clicar(webDriver, xPathBuscar, "Botão Buscar Plano");

        Thread.Sleep(1000);
    }
}