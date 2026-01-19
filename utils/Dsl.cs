using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Globalization;

namespace MeuClienteWebTestProject;

/// <summary>
/// Classe que contem métodos para ajudar na manipulação\interação dos elementos
/// E que compartilham do mesmo funcionamento, em diferentes telas da plataforma
/// /// </summary>
public class Dsl
{
    private static TimeSpan implicitWaitOriginal;
    private static TimeSpan explicitWait = TimeSpan.FromSeconds(50);

    public static DefaultWait<IWebDriver> CreateFluentWait(IWebDriver webDriver)
    {
        implicitWaitOriginal = webDriver.Manage().Timeouts().ImplicitWait;
        webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;

        var wait = new DefaultWait<IWebDriver>(webDriver)
        {
            Timeout = TimeSpan.FromSeconds(30),
            PollingInterval = TimeSpan.FromMilliseconds(250)
        };

        wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

        return wait;
    }

    /// <summary>
    /// Método com uma espera padrão de 1 segundo
    /// </summary>
    public static void Esperar(int time = 1000)
    {
        Thread.Sleep(time);
    }

    /// <summary>
    /// Método para consultar o elemento a cada meio segundo, até que ele esteja vísivel em tela em uma espera de 10 segundos
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <exception cref="Exception"></exception>
    public static bool EsperarVisibilidadeDoElemento(IWebDriver webDriver, string XPath)
    {
        try
        {
            var fluentWait = CreateFluentWait(webDriver);
            fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath(XPath)));
        }
        catch (WebDriverTimeoutException ex)
        { Console.WriteLine("Tempo esgotado para espera da visibilidade do elemento:" + "\n" + ex.Message); }
        catch (Exception ex)
        { Console.WriteLine("Erro ao esperar a visibilidade do elemento na página:" + "\n" + ex.Message); }
        finally
        { webDriver.Manage().Timeouts().ImplicitWait = implicitWaitOriginal; }

        return false;
    }

    /// <summary>
    /// Método para esperar que um elemento não esteja mais presente no DOM
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <exception cref="Exception"></exception>
    public static void EsperarInvisibilidadeDoElemento(IWebDriver webDriver, string XPath)
    {
        try
        {
            var fluentWait = CreateFluentWait(webDriver);
            fluentWait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(XPath)));
        }
        catch (WebDriverTimeoutException ex)
        { Console.WriteLine("Tempo esgotado para espera da invisibilidade do elemento:" + "\n" + ex.Message); }
        catch (Exception ex)
        { Console.WriteLine("Erro ao esperar a invisibilidade do elemento na página:" + "\n" + ex.Message); }
        finally
        { webDriver.Manage().Timeouts().ImplicitWait = implicitWaitOriginal; }
    }

    /// <summary>
    /// Método que espera o elemento ficar disponível para o click
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <param name="elemento"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static void EsperarElementoFicarClicavel(IWebDriver webDriver, string XPath, string elemento)
    {
        try
        {
            WebDriverWait wait = new WebDriverWait(webDriver, explicitWait);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(XPath)));
        }
        catch (Exception ex)
        { throw new Exception("Erro ao esperar elemento ficar clicavel: " + "\n" + ex.Message + "\n" + elemento); }
    }

    /// <summary>
    /// Método que espera o elemento para receber o clique
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <param name="elemento"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static void EsperarElementoParaClicar(IWebDriver webDriver, string XPath, string elemento)
    {
        try
        {
            WebDriverWait wait = new WebDriverWait(webDriver, explicitWait);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(XPath))).Click();
        }
        catch (Exception ex)
        { throw new Exception("Erro ao esperar elemento para clicar: " + "\n" + ex.Message + "\n" + elemento); }
    }

    /// <summary>
    /// Método para encontrar um elemento na tela
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <param name="elemento"></param>
    /// <returns>Retorna o elemento encontrado</returns>
    public static IWebElement EncontrarElemento(IWebDriver webDriver, string XPath, string elemento)
    {
        try
        {
            WebDriverWait wait = new WebDriverWait(webDriver, explicitWait);
            IWebElement element = wait.Until(ExpectedConditions.ElementExists(By.XPath(XPath)));

            return element;
        }
        catch (WebDriverTimeoutException)
        { throw new Exception("Elemento \"" + elemento + "\" não localizado"); }
        catch (Exception ex)
        { throw new Exception("Ocorreu um erro: " + ex.Message + " no elemento: " + elemento); }
    }

    /// <summary>
    /// Método para localizar um elemento mensagem na tela
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <returns>Retorna verdadeiro se o elemento for localizado, ou falso caso contrário</returns>
    public static bool LocalizarElementoMensagem(IWebDriver webDriver, string XPath)
    {
        try
        {
            var fluentWait = CreateFluentWait(webDriver);
            var elemento = fluentWait.Until(ExpectedConditions.ElementExists(By.XPath(XPath)));
            return elemento.Displayed;
        }
        catch (NoSuchElementException)
        { return false; }
        catch (StaleElementReferenceException)
        { return false; }
        catch (WebDriverTimeoutException)
        { return false; }
        finally
        { webDriver.Manage().Timeouts().ImplicitWait = implicitWaitOriginal; }
    }

    /// <summary>
    /// Método para aguardar o load da tela
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>*
    public static void EsperarLoadDaTela(IWebDriver webDriver, string XPath)
    {
        EsperarVisibilidadeDoElemento(webDriver, XPath);
        EsperarInvisibilidadeDoElemento(webDriver, XPath);
    }

    /// <summary>
    /// Método para clicar em um elemento
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <param name="elemento"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static void Clicar(IWebDriver webDriver, string XPath, string elemento)
    {
        try
        {
            IWebElement element = webDriver.FindElement(By.XPath(XPath));
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)webDriver;
            jsExecutor.ExecuteScript("arguments[0].click();", element);
        }
        catch (NoSuchElementException)
        { throw new Exception("Elemento \"" + elemento + "\" não localizado"); }
        catch (Exception ex)
        { throw new Exception("Ocorreu um erro: " + ex.Message + " no elemento: " + elemento); }
    }

    /// <summary>
    /// Método para clicar em um elemento já localizado
    /// </summary>
    /// <param name="elementId"></param>
    /// <param name="elemento"></param>
    /// <returns></returns>
    public static void ClicarNoElementoId(IWebElement elementId, string elemento)
    {
        try
        {
            elementId.Click();
        }
        catch (Exception ex)
        { throw new Exception(ex.Message + "\n" + elemento); }
    }

    /// <summary>
    /// Método para contar quantas vezes o elemento existe na tela
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <returns>Retorna uma número inteiro</returns>
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
        catch (Exception ex)
        { throw new Exception(ex.Message); }
    }

    /// <summary>
    /// Método para contar as linhas dentro de um elemento tabela
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <returns>Retorna um número inteiro</returns>
    /// <exception cref="Exception"></exception>/
    public static long ContarLinhasEmTabela(IWebDriver webDriver, string XPath)
    {
        try
        {
            // Executando o script JavaScript para contar as linhas no elemento tbody
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)webDriver;
            long trCount = (long)jsExecutor.ExecuteScript("return document.evaluate(arguments[0], document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue.getElementsByTagName('tr').length;", XPath);

            return trCount;
        }
        catch (Exception ex)
        { throw new Exception(ex.Message); }
    }

    /// <summary>
    /// Método para obter o texto contido no elemento
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <param name="elemento"></param>
    /// <returns>Retorna um texto</returns>
    /// <exception cref="Exception"></exception>
    public static string ObterTextoDoElemento(IWebDriver webDriver, string XPath, string elemento)
    {
        try
        {
            var textoElemento = webDriver.FindElement(By.XPath(XPath)).Text;

            return textoElemento;
        }
        catch (Exception ex)
        { throw new Exception(ex.Message + "\n" + elemento); }
    }

    /// <summary>
    /// Método para obter as linhas de um elemento tabela
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <returns>Retorna uma lista de elementos</returns>
    public static IList<IWebElement> ObterLinhasDoElementoTabela(IWebDriver webDriver, string XPath)
    {
        IWebElement tabela = webDriver.FindElement(By.XPath(XPath));
        IList<IWebElement> linhas = tabela.FindElements(By.XPath("tr"));

        return linhas;
    }

    /// <summary>
    /// Método para obter as mensagens de feedback exibidas na tela
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <returns>Retorna uma lista de mensagens de feedback</returns>
    public static List<MensagemFeedback> ObterMensagensDeFeedback(IWebDriver webDriver, string XPath)
    {
        var mensagensFeedback = new List<MensagemFeedback>();
        string atributoValor;
        string mensagemTexto;
        bool temMensagemNova = true;
        int count = 1;

        while (temMensagemNova)
        {
            string xpathElemento = $"({XPath})[{count}]";
            temMensagemNova = LocalizarElementoMensagem(webDriver, xpathElemento);

            if (temMensagemNova == false)
                break;

            var elemento = EncontrarElemento(webDriver, xpathElemento, "Mensagem Feedback");
            atributoValor = elemento.GetAttribute("data-testid");
            mensagemTexto = elemento.Text;

            mensagensFeedback.Add(new MensagemFeedback
            {
                Atributo = atributoValor,
                Mensagem = mensagemTexto
            });

            count++;
        }

        return mensagensFeedback;
    }

    /// <summary>
    /// Método para obter as colunas de um elemento tabela
    /// </summary>
    /// <param name="linhaTabela"></param>
    /// <returns>Retorna as colunas como uma lista de elementos</returns>
    public static IList<IWebElement> ObterColunasDoElementoTabela(IWebElement linhaTabela)
    {
        IList<IWebElement> colunas = linhaTabela.FindElements(By.XPath("td"));

        return colunas;
    }


    /// <summary>
    /// Método para obter uma lista de elementos
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <returns>Retorna uma lista de elementos</returns>
    public static IList<IWebElement> ObterListaDeElementos(IWebDriver webDriver, string XPath)
    {
        IList<IWebElement> elementos = webDriver.FindElements(By.XPath(XPath));

        return elementos;
    }

    /// <summary>
    /// Método para retornar os dados do atributo de um elemento
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <param name="elemento"></param>
    /// <param name="nomeAtributo"></param>
    /// <returns>Retorna os dados contidos no atributo como uma string</returns>
    /// <exception cref="Exception"></exception>
    public static string ObterDadosDoAtributoDoElemento(IWebDriver webDriver, string XPath, string elemento, string nomeAtributo)
    {
        try
        {
            string valor = webDriver.FindElement(By.XPath(XPath)).GetAttribute(nomeAtributo);

            return valor;
        }
        catch (Exception ex)
        { throw new Exception(ex.Message + "\n" + elemento); }
    }

    /// <summary>
    /// Método para retornar os dados do atributo de um elemento já localizado
    /// </summary>
    /// <param name="elementId"></param>
    /// <param name="elemento"></param>
    /// <param name="nomeAtributo"></param>
    /// <returns>Retorna os dados contidos no atributo como uma string</returns>
    public static string ObterDadosDoAtributoDoElementoId(IWebElement elementId, string elemento, string nomeAtributo)
    {
        try
        {
            var valor = elementId.GetAttribute(nomeAtributo);

            return valor;
        }
        catch (Exception ex)
        { throw new Exception(ex.Message + "\n" + elemento); }
    }

    /// <summary>
    /// Método para digitar nos elementos campos de texto que possuem lista de registros
    /// Filtrando os registros conforme o campo é preenchido
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <param name="textoValor"></param>
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
            Esperar();
        }
        catch (Exception ex)
        { throw new Exception(ex.Message); }
    }

    /// <summary>
    /// Método para digitar nos elementos campos de texto
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <param name="textoValor"></param>
    /// <exception cref="Exception"></exception>
    public static void DigitarNoCampoTexto(IWebDriver webDriver, string XPath, string textoValor)
    {
        try
        {
            Actions action = new Actions(webDriver);
            action.SendKeys(webDriver.FindElement(By.XPath(XPath)), textoValor).Perform();
        }
        catch (Exception ex)
        { throw new Exception(ex.Message); }
    }

    /// <summary>
    /// Método para selecionar datas de início vingencia, baseado na data atual
    /// Avançando para os meses seguintes se quantidadeAvancarMeses for maior que 0
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="quantidadeAvancarMeses"></param>
    public static void PreencherCalendariosInicioVigencia(IWebDriver webDriver, int quantidadeAvancarMeses)
    {
        DateTime dataAtual = DateTime.Now;
        var diaAtual = dataAtual.Day;
        string xpathElemento;

        if (quantidadeAvancarMeses == 0)
        {
            webDriver.FindElement(By.XPath($"((//div[@class='ant-picker-body'])[1]//div[text()='{diaAtual}'])[1]")).Click();
        }
        else if (quantidadeAvancarMeses > 0)
        {
            for (int i = 0; i < quantidadeAvancarMeses; i++)
            {
                Esperar();
                if (ContarExistenciaDoElemento(webDriver, GlobalVariables.AvancarMesesCalendariosBotton) == 1)
                    webDriver.FindElement(By.XPath(GlobalVariables.AvancarMesesCalendariosBotton)).Click();
                else if (ContarExistenciaDoElemento(webDriver, GlobalVariables.AvancarMesesCalendariosTop) == 1)
                    webDriver.FindElement(By.XPath(GlobalVariables.AvancarMesesCalendariosTop)).Click();
            }

            var xpathElementoCalendarioBotton = "//div[@class='ant-picker-dropdown ant-picker-dropdown-placement-bottomLeft ']";
            var xpathElementoCalendarioTop = "//div[@class='ant-picker-dropdown ant-picker-dropdown-placement-topLeft ']";

            if (ContarExistenciaDoElemento(webDriver, xpathElementoCalendarioBotton) == 1)
            {

                if (diaAtual == 1)
                {
                    xpathElemento = $"{xpathElementoCalendarioBotton}//td[@class='ant-picker-cell ant-picker-cell-start ant-picker-cell-in-view']//div[text()='{diaAtual}']";
                    Clicar(webDriver, xpathElemento, "Campo Data Início Vigencia");
                }
                else
                {
                    xpathElemento = $"{xpathElementoCalendarioBotton}//td[@class='ant-picker-cell ant-picker-cell-in-view']//div[text()='{diaAtual}']";
                    Clicar(webDriver, xpathElemento, "Campo Data Início Vigencia");
                }
            }
            else if (ContarExistenciaDoElemento(webDriver, xpathElementoCalendarioTop) == 1)
            {
                if (diaAtual == 1)
                {
                    xpathElemento = $"{xpathElementoCalendarioTop}//td[@class='ant-picker-cell ant-picker-cell-start ant-picker-cell-in-view']//div[text()='{diaAtual}']";
                    Clicar(webDriver, xpathElemento, "Campo Data Início Vigencia Trade");
                }
                else
                {
                    xpathElemento = $"{xpathElementoCalendarioTop}//td[@class='ant-picker-cell ant-picker-cell-in-view']//div[text()='{diaAtual}']";
                    Clicar(webDriver, xpathElemento, "Campo Data Início Vigencia Trade");
                }
            }
        }
    }

    /// <summary>
    /// Método para selecionar datas de fim vingencia, baseado na data atual
    /// Avançando para os meses seguintes se quantidadeAvancarMeses for maior que 0
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="quantidadeAvancarMeses"></param>
    public static void PreencherCalendariosFimVigencia(IWebDriver webDriver, int quantidadeAvancarMeses)
    {
        DateTime dataAtual = DateTime.Now;
        var diaAtual = dataAtual.Day;
        string xpathElemento;

        if (quantidadeAvancarMeses == 0)
        {
            webDriver.FindElement(By.XPath($"((//div[@class='ant-picker-body'])[2]//div[text()='{diaAtual}'])[1]")).Click();
        }
        else if (quantidadeAvancarMeses > 0)
        {
            for (int i = 0; i < quantidadeAvancarMeses; i++)
            {
                Esperar();
                if (ContarExistenciaDoElemento(webDriver, GlobalVariables.AvancarMesesCalendariosBotton) == 1)
                    webDriver.FindElement(By.XPath(GlobalVariables.AvancarMesesCalendariosBotton)).Click();
                else if (ContarExistenciaDoElemento(webDriver, GlobalVariables.AvancarMesesCalendariosTop) == 1)
                    webDriver.FindElement(By.XPath(GlobalVariables.AvancarMesesCalendariosTop)).Click();
            }

            var xpathElementoCalendarioBotton = "//div[@class='ant-picker-dropdown ant-picker-dropdown-placement-bottomLeft ']";
            var xpathElementoCalendarioTop = "//div[@class='ant-picker-dropdown ant-picker-dropdown-placement-topLeft ']";

            if (ContarExistenciaDoElemento(webDriver, xpathElementoCalendarioBotton) == 1)
            {
                if (diaAtual == 1)
                {
                    xpathElemento = $"{xpathElementoCalendarioBotton}//td[contains(@class,'ant-picker-cell ant-picker-cell-start ant-picker-cell-in-view')]//div[text()='{diaAtual}']";
                    Clicar(webDriver, xpathElemento, "Campo Data Fim Vigencia");
                }
                else
                {
                    xpathElemento = $"{xpathElementoCalendarioBotton}//td[@class='ant-picker-cell ant-picker-cell-in-view']//div[text()='{diaAtual}']";
                    Clicar(webDriver, xpathElemento, "Campo Data Fim Vigencia");
                }
            }
            else if (ContarExistenciaDoElemento(webDriver, xpathElementoCalendarioTop) == 1)
            {
                if (diaAtual == 1)
                {
                    xpathElemento = $"{xpathElementoCalendarioTop}//td[@class='ant-picker-cell ant-picker-cell-start ant-picker-cell-in-view']//div[text()='{diaAtual}']";
                    Clicar(webDriver, xpathElemento, "Campo Data Fim Vigencia Trade");
                }
                else
                {
                    xpathElemento = $"{xpathElementoCalendarioTop}//td[@class='ant-picker-cell ant-picker-cell-in-view']//div[text()='{diaAtual}']";
                    Clicar(webDriver, xpathElemento, "Campo Data Fim Vigencia Trade");
                }
            }
        }
    }

    /// <summary>
    /// Método para remover apenas números de um texto
    /// </summary>
    /// <param name="texto"></param>
    /// <param name="elemento"></param>
    /// <returns>Retorna o texto sem os números, mantendo espaços e caracteres especiais, e removendo espaços duplos</returns>
    /// <exception cref="Exception"></exception>
    public static string RemoverApenasNumeros(string texto, string elemento)
    {
        try
        {
            var textoTratado = Regex.Replace(texto, @"\d", "");
            textoTratado = Regex.Replace(textoTratado, @" +", " ");
            return textoTratado.Trim();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + "\n" + elemento);
        }
    }

    /// <summary>
    /// Método para remover números e espaços em branco de um texto
    /// </summary>
    /// <param name="texto"></param>
    /// <param name="elemento"></param>
    /// <returns>Retorna o texto somente com letras maiúsculas ou minúsculas</returns>
    /// <exception cref="FormatException"></exception>
    /// <exception cref="Exception"></exception>
    public static string RemoverNumerosEspacosDeUmTexto(string texto, string elemento)
    {
        try
        {
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
            throw new Exception(ex.Message + "\n" + elemento);
        }
    }

    /// <summary>
    /// Método para remover lestras maiúsculas, minúsculas, caracteres especiais e espaços em branco, obtendo um dado tratado em formato de número,
    /// que se encontra como texto ou atributo de um elemento
    /// </summary>
    /// <param name="texto"></param>
    /// <param name="elemento">atributo OU texto no elemento</param>
    /// <returns>Retorna o valor numérico inteiro OU decimal contido em uma string</returns>
    /// <exception cref="FormatException"></exception>
    /// <exception cref="Exception"></exception>
    public static object RemoverLetrasEspacosDeUmTexto(string texto, string elemento)
    {
        object numero;
        try
        {
            var valorTratado = Regex.Replace(texto, @"[a-zA-Z\s:$]", "");

            if (int.TryParse(valorTratado, out int numeroInteiro))
                numero = numeroInteiro;
            else if (double.TryParse(valorTratado, out double numeroDecimal))
                numero = numeroDecimal;
            else
                throw new FormatException("A string não tem um número válido");

            return numero;
        }
        catch (Exception ex)
        { throw new Exception(ex.Message + "\n" + elemento); }
    }

    /// <summary>
    /// Método para validar mensagens de comunicação
    /// </summary>
    /// <param name="mensagemAtual"></param>
    /// <param name="mensagemEsperada"></param>
    /// <param name="valorAtributoDataTestId"></param>
    public static void ValidarMensagemDeComunicacao(string mensagemAtual, string mensagemEsperada, string valorAtributoDataTestId)
    {
        switch (valorAtributoDataTestId)
        {
            case "Mc-message-success":
            case "Mc-message-info":
            case "Mc-message-warning":
            case "Mc-message-loading":
                Assert.That(mensagemAtual, Does.Contain(mensagemEsperada), "Mensagem atual não corresponde com a esperada");
                break;
            case "Mc-message-error":
                Assert.Fail("Teste falhou apresentando a mensagem: " + mensagemAtual);
                break;
        }
    }

    /// <summary>
    /// Método para validar mensagens de feedback
    /// </summary>
    /// <param name="mensagensEsperadas"></param>
    /// <param name="mensagensAtuais"></param>
    /// <returns></returns>
    public static void ValidarMensagemDeFeedbacak(List<MensagemFeedback> mensagensEsperadas, List<MensagemFeedback> mensagensAtuais)
    {
        var erros = mensagensAtuais.Where(m => m.Atributo == "MC-message-error").ToList();

        if (erros.Any())
        {
            string mensagensErro = string.Join(Environment.NewLine, erros.Select(e => e.Mensagem));
            Assert.Fail($"Foram encontrados erros:\n{mensagensErro}");
        }

        foreach (var atual in mensagensAtuais)
        {
            Assert.That(mensagensEsperadas.Any(esperada => esperada.Mensagem == atual.Mensagem && esperada.Atributo == atual.Atributo), Is.True);
        }
    }

    /// <summary>
    /// Método para validar mensagens em modal
    /// </summary>
    /// <param name="mensagemAtual"></param>
    /// <param name="mensagemEsperada"></param>
    /// <returns></returns>
    public static void ValidarMensagensDeConfirmacao(string mensagemAtual, string mensagemEsperada)
    {
        Assert.That(mensagemAtual, Does.Contain(mensagemEsperada), "Mensagem atual não corresponde com a esperada");
    }


    /// <summary>
    /// Método para validar mensagens em telas
    /// </summary>
    /// <param name="mensagemAtual"></param>
    /// <param name="mensagemEsperada"></param>
    /// <returns></returns>
    public static void ValidarMensagemDeSucessoEAlerta(string mensagemAtual, string mensagemEsperada)
    {
        Assert.That(mensagemAtual, Does.Contain(mensagemEsperada), "Mensagem atual não corresponde com a esperada");
    }

    /// <summary>
    /// Método para validar textos dentro de um elemento
    /// </summary>
    /// <param name="textoAtual"></param>
    /// <param name="textoEsperado"></param>
    public static void ValidarTextosNoElemento(string textoAtual, string textoEsperado)
    {
        Assert.That(textoAtual, Does.Contain(textoEsperado), $"Textos não correspondem - TextoAtual: {textoAtual} - TextoEsperado: {textoEsperado}");
    }

    /// <summary>
    /// Método para validar valor inteiro ou valor decimal dentro de um elemento
    /// </summary>
    /// <param name="numeroAtual"></param>
    /// <param name="numeroEsperado"></param>
    /// <param name="elemento"></param>
    public static void ValidarNumerosNoElemento(object numeroAtual, object numeroEsperado, string elemento)
    {
        if (numeroAtual is int)
        {
            int valorAtual = (int)numeroAtual;
            int valorEsperado = (int)numeroEsperado;
            Debug.Assert(valorAtual == valorEsperado, "Valores não correspondem para " + elemento + " - ValorAtual: " + valorAtual + " ValorEspeado: " + valorEsperado);
        }
        else if (numeroAtual is double)
        {
            double valorAtual = (double)numeroAtual;
            double valorEsperado = (double)numeroEsperado;
            Debug.Assert(valorAtual == valorEsperado, "Valores não correspondem para " + elemento + " - ValorAtual: " + valorAtual + " ValorEsperado: " + valorEsperado);
        }
    }

    /// <summary>
    /// Método para buscar registros dentro do grid das telas
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="xPathFiltrar"></param>
    /// <param name="xPathPreencherFiltro"></param>
    /// <param name="xPathBuscar"></param>
    /// <param name="textoValor"></param>
    public static void BuscarRegistros(IWebDriver webDriver, string xPathFiltrar, string xPathPreencherFiltro, string xPathBuscar, string textoValor)
    {
        Esperar(2000);
        Clicar(webDriver, xPathFiltrar, "Botão Filtrar");
        Esperar(500);
        webDriver.FindElement(By.XPath(xPathPreencherFiltro)).SendKeys(Keys.Control + "a" + Keys.Backspace);
        Esperar(500);
        DigitarNoCampoTexto(webDriver, xPathPreencherFiltro, textoValor);
        Clicar(webDriver, xPathBuscar, "Botão Buscar");

        Esperar();
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
    /// Método para realizar scroll horizontal dentro de um elemento tabela
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="tabelaXPath"></param>
    /// <param name="colunaXPath"></param>
    /// returns></returns>
    public static void ScrollHorizontalDentroDoElementoTabela(IWebDriver webDriver, string tabelaXPath, string colunaXPath)
    {
        IWebElement tabela = webDriver.FindElement(By.XPath(tabelaXPath));
        IWebElement coluna = tabela.FindElement(By.XPath(colunaXPath));

        IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)webDriver;
        jsExecutor.ExecuteScript("arguments[0].scrollIntoView({ inline: 'center' });", coluna);
    }

    /// <summary>
    /// Método para realizar scroll para baixo em uma modal
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    public static void ScrollModalElemento(IWebDriver webDriver, string XPath)
    {
        IWebElement modalElement = webDriver.FindElement(By.XPath(XPath));

        IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)webDriver;
        jsExecutor.ExecuteScript("arguments[0].scrollHeight = arguments[0].scrollHeight;", modalElement);
    }

    /// <summary>
    /// Método para carregar uma imagem em um campo do tipo file 
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    public static void CarregarImagens(IWebDriver webDriver, string XPath)
    {
        IWebElement imageInput = webDriver.FindElement(By.XPath(XPath));

        imageInput.SendKeys("C:\\TestProjectMeuCliente\\logomeucliente.png");
    }

    /// <summary>
    /// Método para calcular a quantidade de dias entre duas datas
    /// </summary>
    /// <param name="inicioStr"></param>
    /// <param name="fimStr"></param>
    /// <returns>Retorna a quantidade de dias entre as duas datas, incluindo o dia inicial e o dia final</returns>
    public static int CalcularDiasEntreDatas(string inicioStr, string fimStr)
    {
        string[] formatosAceitos = { "dd/MM/yyyy", "dd/MMM/yyyy" };

        if (DateTime.TryParseExact(inicioStr, formatosAceitos, CultureInfo.InvariantCulture,
                                   DateTimeStyles.None, out DateTime inicio) &&
            DateTime.TryParseExact(fimStr, formatosAceitos, CultureInfo.InvariantCulture,
                                   DateTimeStyles.None, out DateTime fim))
        {
            if (fim < inicio)
                throw new ArgumentException("A data final não pode ser anterior à data inicial.");

            return (fim - inicio).Days + 1;
        }
        else
        {
            throw new FormatException("As datas fornecidas não estão no formato esperado 'dd/MMM/yyyy'.");
        }
    }
}