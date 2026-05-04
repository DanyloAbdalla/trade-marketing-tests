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

    public static DefaultWait<IWebDriver> CreateFluentWait(IWebDriver webDriver)
    {
        implicitWaitOriginal = webDriver.Manage().Timeouts().ImplicitWait;
        webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;

        var wait = new DefaultWait<IWebDriver>(webDriver)
        {
            Timeout = TimeSpan.FromSeconds(40),
            PollingInterval = TimeSpan.FromMilliseconds(1000)
        };

        wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

        return wait;
    }

    /// <summary>
    /// Método para criar um wait fluente específico para múltiplas mensagens de feedback
    /// </summary>
    /// <param name="webDriver"></param>
    /// <returns></returns>
    public static DefaultWait<IWebDriver> CreateFluentWaitPairMessage(IWebDriver webDriver)
    {
        implicitWaitOriginal = webDriver.Manage().Timeouts().ImplicitWait;
        webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;

        var wait = new DefaultWait<IWebDriver>(webDriver)
        {
            Timeout = TimeSpan.FromSeconds(10),
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
    /// Método para aguardar a visibilidade de um elemento
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <returns></returns>
    public static bool ValidarMensagensDeErroOuAviso(IWebDriver webDriver)
    {
        try
        {
            var wait = new WebDriverWait(webDriver, GlobalVariables.ExplicitWait);
            return wait.Until(drv =>
            {
                var element = drv.FindElement(By.XPath("//div[div[@data-testid='Mc-message-error'] or div[@data-testid='Mc-message-warning']]"));
                return element.Displayed;
            });
        }
        catch (WebDriverTimeoutException)
        {
            return false; // não ficou visível dentro do timeout
        }
        catch (NoSuchElementException)
        {
            return false; // elemento não encontrado
        }
    }

    /// <summary>
    /// Método para consultar o elemento a cada meio segundo, até que ele esteja vísivel em tela em uma espera de 10 segundos
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <exception cref="Exception"></exception>
    public static void EsperarVisibilidadeDoElemento(IWebDriver webDriver, string XPath, string elemento)
    {
        try
        {
            WebDriverWait driverWait = new WebDriverWait(webDriver, GlobalVariables.ExplicitWait);
            driverWait.Until(ExpectedConditions.ElementIsVisible(By.XPath(XPath)));
        }
        catch (WebDriverTimeoutException)
        { Console.WriteLine("Tempo esgotado para espera da visibilidade do elemento: " + elemento); }
        catch (Exception ex)
        { Console.WriteLine("Ocorreu o erro: " + ex.Message + " ao esperar a visibilidade do elemento " + elemento); }
    }

    /// <summary>
    /// Método para esperar que um elemento não esteja mais presente no DOM
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <param name="elemento"></param>
    /// <exception cref="Exception"></exception>
    public static void EsperarInvisibilidadeDoElemento(IWebDriver webDriver, string XPath, string elemento)
    {
        try
        {
            WebDriverWait driverWait = new WebDriverWait(webDriver, GlobalVariables.ExplicitWait);
            driverWait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(XPath)));
        }
        catch (WebDriverTimeoutException)
        { Console.WriteLine("Tempo esgotado para espera da invisibilidade do elemento: " + elemento); }
        catch (Exception ex)
        { Console.WriteLine("Ocorreu o erro: " + ex.Message + " ao esperar a invisibilidade do elemento " + elemento); }
    }

    public static void WaitForElementToBeStale(IWebDriver webDriver, IWebElement element, string elementDescription)
    {
        try
        {
            WebDriverWait driverWait = new WebDriverWait(webDriver, GlobalVariables.ExplicitWait);
            driverWait.Until(ExpectedConditions.StalenessOf(element));
        }
        catch (WebDriverTimeoutException)
        { Console.WriteLine("Tempo esgotado para espera da invisibilidade do elemento: " + elementDescription); }
        catch (Exception ex)
        { Console.WriteLine("Ocorreu o erro: " + ex.Message + " ao esperar a invisibilidade do elemento " + elementDescription); }
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
            WebDriverWait wait = new WebDriverWait(webDriver, GlobalVariables.ExplicitWait);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(XPath)));
        }
        catch (Exception ex)
        { throw new Exception("Ocorreu o erro: " + ex.Message + " ao esperar o elemento " + elemento + " ficar apto ao click"); }
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
            WebDriverWait wait = new WebDriverWait(webDriver, GlobalVariables.ExplicitWait);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(XPath))).Click();
        }
        catch (Exception ex)
        { throw new Exception("Ocorreu o erro: " + ex.Message + " ao esperar o elemento " + elemento + " para realizar o click"); }
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
            WebDriverWait wait = new WebDriverWait(webDriver, GlobalVariables.ExplicitWait);
            IWebElement element = wait.Until(ExpectedConditions.ElementExists(By.XPath(XPath)));

            return element;
        }
        catch (WebDriverTimeoutException)
        { throw new Exception("Elemento \"" + elemento + "\" não localizado"); }
        catch (Exception ex)
        { throw new Exception("Ocorreu o erro: " + ex.Message + " ao encontrar o elemento " + elemento); }
    }

    public static IWebElement FindElement(IWebDriver webDriver, string XPath, string elemento)
    {
        try
        {
            WebDriverWait wait = new WebDriverWait(webDriver, GlobalVariables.ExplicitWait);
            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(XPath)));

            return element;
        }
        catch (WebDriverTimeoutException)
        { throw new Exception("Elemento \"" + elemento + "\" não localizado"); }
        catch (Exception ex)
        { throw new Exception("Ocorreu o erro: " + ex.Message + " ao encontrar o elemento " + elemento); }
    }

    /// <summary>
    /// Método para localizar um elemento mensagem na tela
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <returns>Retorna o elemento encontrado e um valor booleano indicando se a operação foi bem-sucedida</returns>
    public static (IWebElement Element, bool Success) LocalizarElementoMensagem(IWebDriver webDriver, string XPath)
    {
        try
        {
            var fluentWait = CreateFluentWaitPairMessage(webDriver);
            var elemento = fluentWait.Until(ExpectedConditions.ElementExists(By.XPath(XPath)));
            return (elemento, true);
        }
        catch (NoSuchElementException)
        { return (null, false); }
        catch (StaleElementReferenceException)
        { return (null, false); }
        catch (WebDriverTimeoutException)
        { return (null, false); }
        finally
        { webDriver.Manage().Timeouts().ImplicitWait = implicitWaitOriginal; }
    }

    /// <summary>
    /// Método para aguardar o load da tela
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <param name="elemento"></param>
    public static void EsperarLoadDaTela(IWebDriver webDriver, string XPath, string elemento)
    {
        EsperarVisibilidadeDoElemento(webDriver, XPath, elemento);
        EsperarInvisibilidadeDoElemento(webDriver, XPath, elemento);
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
        catch (NoSuchElementException ex)
        { throw new NoSuchElementException("Elemento \"" + elemento + "\" não localizado " + ex.Message); }
        catch (Exception ex)
        { throw new Exception("Ocorreu o erro: " + ex.Message + " ao clicar no elemento " + elemento + " " + XPath); }
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
        {
            throw new Exception("Ocorreu o erro " + ex.Message + " ao clicar no elemento " + elemento);
        }
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
        {
            throw new Exception("Ocorreu o erro " + ex.Message + " ao contar a existência do elemento na tela.");
        }
    }

    /// <summary>
    /// Método para contar as linhas dentro de um elemento tabela
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <returns>Retorna um número inteiro</returns>
    /// <exception cref="Exception"></exception>/
    public static int ContarLinhasEmTabela(IWebDriver webDriver, string XPath)
    {
        try
        {
            // Localiza o tbody
            var tbody = webDriver.FindElement(By.XPath(XPath));

            // Busca todas as linhas tr dentro do tbody
            var linhas = tbody.FindElements(By.TagName("tr"));

            // Filtra apenas as que possuem o atributo data-row-key
            var linhasComDataRowKey = linhas.Where(tr =>
            {
                var dataRowKey = tr.GetAttribute("data-row-key");
                return !string.IsNullOrEmpty(dataRowKey);
            });

            return linhasComDataRowKey.Count();
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
        {
            throw new Exception("Ocorreu o erro " + ex.Message + " ao obter dados o texto do elemento " + elemento);
        }
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
        bool temNovaMensagem = true;
        int count = 1;

        while (temNovaMensagem)
        {
            string xpathElemento = $"({XPath})[{count}]";
            var resultado = LocalizarElementoMensagem(webDriver, xpathElemento);

            if (resultado.Success == false)
                break;

            //var elemento = EncontrarElemento(webDriver, xpathElemento, "Mensagem Feedback");
            atributoValor = resultado.Element.GetAttribute("data-testid");
            mensagemTexto = resultado.Element.Text;

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
        {
            throw new Exception("Ocorreu o erro " + ex.Message + " ao obter dados do atributo do elemento " + elemento);
        }
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
    /// <param name="elemento"></param>
    /// <exception cref="Exception"></exception>
    public static void DigitarNoCampoTextoComboList(IWebDriver webDriver, string XPath, string textoValor, string elemento)
    {
        try
        {
            for (int i = 0; i < textoValor.Length; i++)
            {
                webDriver.FindElement(By.XPath(XPath)).SendKeys(textoValor[i].ToString());
            }
            Esperar();
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu o erro " + ex.Message + " ao digitar no elemento " + elemento);
        }

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
    /// Método para selecionar datas de vingencia, baseado na data atual
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="elemento"></param>
    public static void PreencherCalendarios(IWebDriver webDriver, string elemento)
    {
        DateTime dataAtual = DateTime.Now;
        string xpathElementoData;
        bool mesApresentadoCaledarioTem30Dias = MesApresentadoTem30Dias(webDriver);
        bool mesApresentadoCalendarioTem31Dias = MesApresentadoTem31Dias(webDriver);
        bool mesApresentadoEhFevereiro = MesApresentadoFevereiro(webDriver);

        if (dataAtual.Day == 1)
            xpathElementoData = GlobalVariables.CalendarioDataInicioMes(GlobalVariables.Calendario, dataAtual.Day.ToString());
        else if (EhUltimoDiaDeFevereiro(dataAtual))
        {
            if (mesApresentadoCaledarioTem30Dias || mesApresentadoCalendarioTem31Dias)
                xpathElementoData = GlobalVariables.CalendarioData(GlobalVariables.Calendario, dataAtual.Day.ToString());
            else
                xpathElementoData = GlobalVariables.CalendarioDataFimMes(GlobalVariables.Calendario, dataAtual.Day.ToString());
        }
        else if (EhUltimoDiaDoMes(dataAtual) || (mesApresentadoCaledarioTem30Dias && dataAtual.Day == 30))
        {
            if (mesApresentadoEhFevereiro)
                xpathElementoData = GlobalVariables.CalendarioDataFimMes(GlobalVariables.Calendario, "28");
            else if (mesApresentadoCaledarioTem30Dias)
                xpathElementoData = GlobalVariables.CalendarioDataFimMes(GlobalVariables.Calendario, "30");
            else if (dataAtual.Day < 31)
                xpathElementoData = GlobalVariables.CalendarioData(GlobalVariables.Calendario, dataAtual.Day.ToString());
            else
                xpathElementoData = GlobalVariables.CalendarioDataFimMes(GlobalVariables.Calendario, dataAtual.Day.ToString());
        }
        else if (mesApresentadoEhFevereiro)
            if (dataAtual.Day >= 28)
                xpathElementoData = GlobalVariables.CalendarioDataFimMes(GlobalVariables.Calendario, "28");
            else
                xpathElementoData = GlobalVariables.CalendarioData(GlobalVariables.Calendario, dataAtual.Day.ToString());
        else
            xpathElementoData = GlobalVariables.CalendarioData(GlobalVariables.Calendario, dataAtual.Day.ToString());

        Esperar();
        Clicar(webDriver, xpathElementoData, elemento);
    }

    public static void AvancarMesesNoCalendario(IWebDriver webDriver, int quantidadeAvancarMeses)
    {
        for (int i = 0; i < quantidadeAvancarMeses; i++)
        {
            Esperar();
            Clicar(webDriver, GlobalVariables.AvancarCalendarioMes(GlobalVariables.Calendario), "Botão Avançar Mês Calendário");
            Esperar();
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
            throw new Exception("Ocorreu o erro " + ex.Message + " ao remover numeros do texto no elemento " + elemento);
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
                return textoTratado;
            else
                throw new FormatException("Texto não contém letras/pontuação");
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu o erro " + ex.Message + " ao remover numeros e espaços do texto no elemento " + elemento);
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
    public static double RemoverLetrasEspacosDeUmTexto(string texto, string elemento)
    {
        double numero;
        try
        {
            var valorTratado = Regex.Replace(texto, @"[a-zA-Z\s:$]", "");

            if (double.TryParse(valorTratado, NumberStyles.Any, new CultureInfo("pt-BR"), out double numeroDouble))
                numero = numeroDouble;
            else
                throw new FormatException("O texto capturado não contém um número válido.");

            return numero;
        }
        catch (Exception ex)
        {
            throw new Exception("Ocorreu o erro " + ex.Message + " ao remover letras e espaços do texto no elemento " + elemento);
        }
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
        //var erros = mensagensAtuais.Where(m => m.Atributo == "MC-message-error").ToList();
        var errosOuWarnings = mensagensAtuais.Where(m => m.Atributo == "MC-message-error" || m.Atributo == "MC-message-warning").ToList();


        if (errosOuWarnings.Any())
        {
            string mensagensErro = string.Join(Environment.NewLine, errosOuWarnings.Select(e => e.Mensagem));
            Assert.Fail($"Foram encontrados erros ou warnings:\n{mensagensErro}");
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
    public static void ValidarNumerosNoElemento(double numeroAtual, double numeroEsperado, string elemento)
    {
        Debug.Assert(numeroAtual == numeroEsperado, "Valores não correspondem para elemento " + elemento + " - ValorAtual: " + numeroAtual + " ValorEsperado: " + numeroEsperado);
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
        Clicar(webDriver, xPathFiltrar, "Botão Filtrar");
        Esperar();
        webDriver.FindElement(By.XPath(xPathPreencherFiltro)).SendKeys(Keys.Control + "a" + Keys.Backspace);
        Esperar();
        DigitarNoCampoTexto(webDriver, xPathPreencherFiltro, textoValor);
        Clicar(webDriver, xPathBuscar, "Botão Buscar");

        Esperar();
    }

    /// <summary>
    /// Método para realizar scroll até um elemento específico
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <param name="elemento"></param>
    public static void ScrollParaElemento(IWebDriver webDriver, string XPath, string elemento)
    {
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

    /// <summary>
    /// Método para verificar se a data é o último dia do mês
    /// </summary>
    /// <param name="data"></param>
    public static bool EhUltimoDiaDoMes(DateTime data)
    {
        return data.Day == DateTime.DaysInMonth(data.Year, data.Month);
    }

    public static bool EhUltimoDiaDeFevereiro(DateTime data)
    {
        if (data.Month == 2)
        {
            int ultimoDia = DateTime.DaysInMonth(data.Year, data.Month);
            return data.Day == ultimoDia;
        }

        return false; // Não é fevereiro
    }

    public static bool MesApresentadoFevereiro(IWebDriver webDriver)
    {
        string mes = ObterTextoDoElemento(webDriver, GlobalVariables.MesCalendario, "Avançar Mês Calendário");

        if (mes == "Feb")
            return true;

        return false;
    }

    public static void ValidarModaisAbertas(IWebDriver webDriver, ClienteUpSell clienteUpSellAtual)
    {
        if (ContarExistenciaDoElemento(webDriver, GlobalVariables.AbaAlocacaoPorLojaAtivo) > 0)
        {
            //Se a modal da alocação por loja do ativo está aberta, a mesma é fechada para não comprometer a execução do próximo teste
            new PlanosContratosPage(webDriver, clienteUpSellAtual).FecharAlocacaoPorLoja();
        }

        if (ContarExistenciaDoElemento(webDriver, GlobalVariables.AbaDadosPlano) > 0)
        {
            //Se a modal do plano está aberta, a mesma é fechada para não comprometer a execução do próximo teste
            new PlanosContratosPage(webDriver, clienteUpSellAtual).FecharDadosDoPlano();
        }

        if (ContarExistenciaDoElemento(webDriver, GlobalVariables.AbaSimulacao) > 0)
        {
            //Se a modal da simulação do plano está aberta, a mesma é fechada para não comprometer a execução do próximo teste
            new PlanosContratosPage(webDriver, clienteUpSellAtual).FecharSimulacaoPlano();
        }
    }

    internal static bool MesApresentadoTem30Dias(IWebDriver webDriver)
    {
        int mesSelecionado = ObterMesCalendarioDe30Dias(webDriver);

        if (mesSelecionado == 4) // Abril
            return true;
        if (mesSelecionado == 6) // Junho
            return true;
        if (mesSelecionado == 9) // Setembro
            return true;
        if (mesSelecionado == 11) // Novembro
            return true;

        return false;
    }

    internal static bool MesApresentadoTem31Dias(IWebDriver webDriver)
    {
        int mesSelecionado = ObterMesCalendarioDe31Dias(webDriver);

        if (mesSelecionado == 1) // Janeiro
            return true;
        if (mesSelecionado == 3) // Março
            return true;
        if (mesSelecionado == 5) // Maio
            return true;
        if (mesSelecionado == 7) // Julho
            return true;
        if (mesSelecionado == 8) // Agosto
            return true;
        if (mesSelecionado == 10) // Outubro
            return true;
        if (mesSelecionado == 12) // Dezembro
            return true;

        return false;
    }

    internal static int ObterMesCalendarioDe30Dias(IWebDriver webDriver)
    {
        string mes = ObterTextoDoElemento(webDriver, GlobalVariables.MesCalendario, "Avançar Mês Calendário");

        switch (mes)
        {
            case "Apr": return 4;
            case "Abr": return 4;
            case "Jun": return 6;
            case "Sep":
            case "Set": return 9;
            case "Nov": return 11;
            default: return 0;
        }
    }

    internal static int ObterMesCalendarioDe31Dias(IWebDriver webDriver)
    {
        string mes = ObterTextoDoElemento(webDriver, GlobalVariables.MesCalendario, "Avançar Mês Calendário");

        switch (mes)
        {
            case "Jan": return 1;
            case "Mar": return 3;
            case "May":
            case "Mai": return 5;
            case "Jul": return 7;
            case "Aug": return 8;
            case "Oct":
            case "Out": return 10;
            case "Dec":
            case "Dez": return 12;
            default: return 0;
        }
    }

    internal static void CalcularAvancoMesesVigencia(IWebDriver webDriver, out int avancarMesCalendarioFimVigenciaEm, out int avancarMesCalendarioInicioVigenciaEm)
    {
        string fimVigenciaPlano = ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.FimVigenciaPlano, "Campo Fim Vigência do Plano", "value");
        string inicioVigenciaPlano = ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.InicioVigenciaPlano, "Campo Início Vigência do Plano", "value");
        string diaFimVigenciaPlano = fimVigenciaPlano.Substring(0, 2);
        string diaInicioVigenciaPlano = inicioVigenciaPlano.Substring(0, 2);
        string mesFimVigenciaPlano = fimVigenciaPlano.Substring(3, 3);
        string mesInicioVigenciaPlano = inicioVigenciaPlano.Substring(3, 3);
        int quantidadeDiasVigencia = CalcularDiasEntreDatas(inicioVigenciaPlano, fimVigenciaPlano);

        if (diaInicioVigenciaPlano.Equals("01") && quantidadeDiasVigencia >= 31 && diaInicioVigenciaPlano != diaFimVigenciaPlano || (mesFimVigenciaPlano == mesInicioVigenciaPlano))
        {
            avancarMesCalendarioFimVigenciaEm = 3;
            avancarMesCalendarioInicioVigenciaEm = 2;
        }
        else if (diaFimVigenciaPlano.Equals("01") && quantidadeDiasVigencia == 30)
        {
            avancarMesCalendarioFimVigenciaEm = 1;
            avancarMesCalendarioInicioVigenciaEm = 2;
        }
        else if (diaFimVigenciaPlano.Equals("01") && quantidadeDiasVigencia >= 31)
        {
            avancarMesCalendarioFimVigenciaEm = 1;
            avancarMesCalendarioInicioVigenciaEm = 1;
        }
        else
        {
            avancarMesCalendarioFimVigenciaEm = 2;
            avancarMesCalendarioInicioVigenciaEm = 2;
        }
    }

    internal static void CalcularAvancoMesesVigenciaTradeLoja(IWebDriver webDriver, string nomeLoja, out int avancarMesCalendarioFimVigenciaEm, out int avancarMesCalendarioInicioVigenciaEm)
    {
        string fimVigenciaPlano = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.FimVigenciaLoja(nomeLoja), "Campo Fim Vigência do Plano", "value");
        string inicioVigenciaPlano = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.InicioVigenciaLoja(nomeLoja), "Campo Início Vigência do Plano", "value");
        string diaInicioVigenciaPlano = inicioVigenciaPlano.Substring(0, 2);
        string diaFimVigenciaPlano = fimVigenciaPlano.Substring(0, 2);
        string mesFimVigenciaPlano = fimVigenciaPlano.Substring(3, 3);
        string mesInicioVigenciaPlano = inicioVigenciaPlano.Substring(3, 3);
        int quantidadeDiasVigencia = Dsl.CalcularDiasEntreDatas(inicioVigenciaPlano, fimVigenciaPlano);

        if (diaInicioVigenciaPlano.Equals("01") && quantidadeDiasVigencia >= 31)
        {
            avancarMesCalendarioFimVigenciaEm = 3;
            avancarMesCalendarioInicioVigenciaEm = 2;
        }
        else if (diaFimVigenciaPlano.Equals("01") && quantidadeDiasVigencia >= 31)
        {
            avancarMesCalendarioFimVigenciaEm = 1;
            avancarMesCalendarioInicioVigenciaEm = 1;
        }
        else if (mesFimVigenciaPlano == mesInicioVigenciaPlano)
        {
            avancarMesCalendarioFimVigenciaEm = 2;
            avancarMesCalendarioInicioVigenciaEm = 1;
        }
        else
        {
            avancarMesCalendarioFimVigenciaEm = 2;
            avancarMesCalendarioInicioVigenciaEm = 2;
        }
    }
}