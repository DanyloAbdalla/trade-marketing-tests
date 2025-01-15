using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace MeuClienteWebTestProject;

/// <summary>
/// Classe que contem métodos para ajudar na manipulação\interação dos elementos
/// E que compartilham do mesmo funcionamento, em diferentes telas da plataforma
/// /// </summary>
public class Dsl
{
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
        var fluentWait = new DefaultWait<IWebDriver>(webDriver)
        {
            Timeout = TimeSpan.FromSeconds(40),
            PollingInterval = TimeSpan.FromMilliseconds(100)
        };

        fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));

        try
        {
            fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath(XPath)));
        }
        catch (Exception ex)
        { Console.WriteLine("Erro ao esperar o elemento na página" + ex.Message); }

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

        var fluentWait = new DefaultWait<IWebDriver>(webDriver)
        {
            Timeout = TimeSpan.FromSeconds(40),
            PollingInterval = TimeSpan.FromMilliseconds(100)
        };

        fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));

        try
        {
            IWebElement element = webDriver.FindElement(By.XPath(XPath));
            if(fluentWait.Until(ExpectedConditions.StalenessOf(element)) || fluentWait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(XPath)))){}
            //fluentWait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(XPath)));
            //fluentWait.Until(ExpectedConditions.StalenessOf(element));
        }
        catch (Exception ex)
        { Console.WriteLine("Erro ao processar a invisibilidade do elemento" + ex.Message); }
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
            PollingInterval = TimeSpan.FromMilliseconds(100)
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
    /// <param name="XPath"></param>
    /// <param name="nomeElemento"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static void EsperarElementoParaClicar(IWebDriver webDriver, string XPath, string nomeElemento)
    {
        var fluentWait = new DefaultWait<IWebDriver>(webDriver)
        {
            Timeout = TimeSpan.FromSeconds(30),
            PollingInterval = TimeSpan.FromMilliseconds(100)
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
    /// Método para aguardar o load da tela
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="xpath"></param>*
    public static void EsperarLoadDaTela(IWebDriver webDriver, string XPath)
    {
        EsperarVisibilidadeDoElemento(webDriver, XPath);
        EsperarInvisibilidadeDoElemento(webDriver, XPath);
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
            IWebElement element = webDriver.FindElement(By.XPath(XPath));
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)webDriver;
            jsExecutor.ExecuteScript("arguments[0].click();", element);
        }
        catch (Exception ex)
        { throw new Exception(ex.Message + "\n" + nomeElemento); }
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
    /// Método para obter a quantidade de linhas (tag tr) em um elemento do tipo tabela
    /// </summary>
    /// <param name="driver"></param>
    /// <param name="XPath"></param>
    /// <returns>Retorna um número inteiro</returns>
    public static int ObterQuantidadeLinhasNoElementoTabelaComLinhaInvisivel(IWebDriver webDriver, string XPath)
    {
        IWebElement tabela = webDriver.FindElement(By.XPath(XPath));
        IList<IWebElement> linhas = tabela.FindElements(By.XPath("tr"));

        var quantidadeLinhas = linhas.Count() - 1;

        return quantidadeLinhas;
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

        var quantidadeLinhas = linhas.Count();

        return quantidadeLinhas;
    }

    /// <summary>
    /// Método para retornar os dados do atributo value de um elemento
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <param name="nomeElemento"></param>
    /// <returns>Retorna os dados contidos no atributo como uma string</returns>
    public static string ObterDadosDoAtributoValueDoElemento(IWebDriver webDriver, string XPath, string nomeElemento)
    {
        try
        {
            EsperarVisibilidadeDoElemento(webDriver, XPath);
            var dados = webDriver.FindElement(By.XPath(XPath)).GetAttribute("value");
            return dados;
        }
        catch (Exception ex)
        { throw new Exception(ex.Message + "\n" + nomeElemento); }
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
            //EsperarVisibilidadeDoElemento(webDriver, XPath);
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
    /// <param name="XPath"></param>
    /// <param name="quantidadeAvancarMeses"></param>
    public static void PreencherCalendariosInicioVigencia(IWebDriver webDriver, string XPath, int quantidadeAvancarMeses)
    {
        DateTime dataAtual = DateTime.Now;
        var diaAtual = dataAtual.Day;

        EsperarVisibilidadeDoElemento(webDriver, XPath);

        if (quantidadeAvancarMeses == 0)
        {
            webDriver.FindElement(By.XPath($"((//div[@class='ant-picker-body'])[1]//div[text()='{diaAtual}'])[1]")).Click();
        }
        else if (quantidadeAvancarMeses > 0)
        {
            for (int i = 0; i < quantidadeAvancarMeses; i++)
            {
                Esperar();
                webDriver.FindElement(By.XPath(XPath)).Click();
            }

            if (ContarExistenciaDoElemento(webDriver, $"((//div[@class='ant-picker-body'])[1]//div[text()='{diaAtual}'])[2]/ancestor::td") == 0)
                webDriver.FindElement(By.XPath($"((//div[@class='ant-picker-body'])[1]//div[text()='{diaAtual}'])[1]")).Click();
            else
            {
                var classAttribute = webDriver.FindElement(By.XPath($"((//div[@class='ant-picker-body'])[1]//div[text()='{diaAtual}'])[2]/ancestor::td")).GetAttribute("class");

                if (classAttribute.Contains("ant-picker-cell-in-view"))
                    webDriver.FindElement(By.XPath($"((//div[@class='ant-picker-body'])[1]//div[text()='{diaAtual}'])[2]")).Click();
                else
                    webDriver.FindElement(By.XPath($"((//div[@class='ant-picker-body'])[1]//div[text()='{diaAtual}'])[1]")).Click();
            }
            /*else
            {
                var classAttribute = webDriver.FindElement(By.XPath($"((//div[@class='ant-picker-body'])[1]//div[text()='{diaAtual}'])[2]/ancestor::td")).GetAttribute("class");

                if (classAttribute.Contains("ant-picker-cell-in-view"))
                    webDriver.FindElement(By.XPath($"((//div[@class='ant-picker-body'])[1]//div[text()='{diaAtual}'])[2]")).Click();
                else
                    webDriver.FindElement(By.XPath($"((//div[@class='ant-picker-body'])[1]//div[text()='{diaAtual}'])[1]")).Click();
            }*/

        }
    }

    /// <summary>
    /// Método para selecionar datas de fim vingencia, baseado na data atual
    /// Avançando para os meses seguintes se quantidadeAvancarMeses for maior que 0
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <param name="quantidadeAvancarMeses"></param>
    public static void PreencherCalendariosFimVigencia(IWebDriver webDriver, string XPath, int quantidadeAvancarMeses)
    {
        DateTime dataAtual = DateTime.Now;
        var diaAtual = dataAtual.Day;

        EsperarVisibilidadeDoElemento(webDriver, XPath);

        if (quantidadeAvancarMeses == 0)
        {
            webDriver.FindElement(By.XPath($"((//div[@class='ant-picker-body'])[2]//div[text()='{diaAtual}'])[1]")).Click();
        }
        else if (quantidadeAvancarMeses > 0)
        {
            for (int i = 0; i < quantidadeAvancarMeses; i++)
            {
                Esperar();
                webDriver.FindElement(By.XPath(XPath)).Click();
            }

            if (ContarExistenciaDoElemento(webDriver, $"((//div[@class='ant-picker-body'])[2]//div[text()='{diaAtual}'])[2]/ancestor::td") == 0)
                webDriver.FindElement(By.XPath($"((//div[@class='ant-picker-body'])[2]//div[text()='{diaAtual}'])[1]")).Click();
            else
            {
                var classAttribute = webDriver.FindElement(By.XPath($"((//div[@class='ant-picker-body'])[2]//div[text()='{diaAtual}'])[2]/ancestor::td")).GetAttribute("class");

                if (classAttribute.Contains("ant-picker-cell-in-view"))
                    webDriver.FindElement(By.XPath($"((//div[@class='ant-picker-body'])[2]//div[text()='{diaAtual}'])[2]")).Click();
                else
                    webDriver.FindElement(By.XPath($"((//div[@class='ant-picker-body'])[2]//div[text()='{diaAtual}'])[1]")).Click();
            }

            /*var quantiadadedDiasCalendario = Dsl.ContarExistenciaDoElemento(webDriver, $"(//div[@class='ant-picker-body'])[2]//div[text()='{diaAtual}']");
            var classAttribute = webDriver.FindElement(By.XPath($"((//div[@class='ant-picker-body'])[2]//div[text()='{diaAtual}'])[2]/ancestor::td")).GetAttribute("class");

            if (classAttribute.Contains("ant-picker-cell-in-view"))
                webDriver.FindElement(By.XPath($"((//div[@class='ant-picker-body'])[2]//div[text()='{diaAtual}'])[2]")).Click();
            else
                webDriver.FindElement(By.XPath($"((//div[@class='ant-picker-body'])[2]//div[text()='{diaAtual}'])[1]")).Click();*/

        }
    }

    /// <summary>
    /// Método para remover números e espaços em branco de um texto
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <param name="nomeElemento"></param>
    /// <returns>Retorna o texto somente com letras maiúsculas ou minúsculas</returns>
    public static string RemoverNumerosEspacosDeUmTexto(IWebDriver webDriver, string XPath, string nomeElemento)
    {
        try
        {
            EsperarVisibilidadeDoElemento(webDriver, XPath);

            var texto = PegarTextoDoElemento(webDriver, XPath, nomeElemento);
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
    /// Método para remover lestras maiúsculas, minúsculas, caracteres especiais e espaços em branco de um valor numérico int ou double
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <param name="nomeElemento"></param>
    /// <param name="captura">0: atributo value do elemento OU 1: texto no elemento</param>
    /// <returns>Retorna o valor numérico</returns>
    public static object RemoverLetrasEspacosDeUmTexto(IWebDriver webDriver, string XPath, string nomeElemento, int captura)
    {
        object retorno = null;
        try
        {
            EsperarVisibilidadeDoElemento(webDriver, XPath);

            if (captura == 0)
            {
                var texto = ObterDadosDoAtributoValueDoElemento(webDriver, XPath, nomeElemento);
                var textoTratado = Regex.Replace(texto, @"[a-zA-Z\s:$]", "");

                if (int.TryParse(textoTratado, out int numeroInteiro))
                    retorno = numeroInteiro;
                else if (double.TryParse(textoTratado, out double numeroDecimal))
                    retorno = numeroDecimal;
                else
                    throw new FormatException("A string não tem um número válido");
            }
            else if (captura == 1)
            {
                var texto = PegarTextoDoElemento(webDriver, XPath, nomeElemento);
                var textoTratado = Regex.Replace(texto, @"[a-zA-Z\s:$]", "");

                if (int.TryParse(textoTratado, out int numeroInteiro))
                    retorno = numeroInteiro;
                else if (double.TryParse(textoTratado, out double numeroDecimal))
                    retorno = numeroDecimal;
                else
                    throw new FormatException("A string não tem um número válido");
            }
            return retorno;
        }
        catch (Exception ex)
        { throw new Exception(ex.Message + "\n" + nomeElemento); }
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
    /// Método para validar a verificação feita para a disponibilidade do inventário por loja, na simulção do plano
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPathInventario"></param>
    /// <param name="XPathCheckLojas"></param>
    public static void ValidarDisponbilidadeDeInventarioParaLoja(IWebDriver webDriver, string XPathCheckInventario, string XPathLojasPlano)
    {
        var quantidadeCheck = ContarExistenciaDoElemento(webDriver, XPathCheckInventario);
        var quantidadeLojas = ContarExistenciaDoElemento(webDriver, XPathLojasPlano) - 1; //Contar linhas no elemento tbody da listagem de lojas na simulação do plano, ignorando a tag tr sem dados

        Debug.Assert(quantidadeCheck == quantidadeLojas, "Checkin da disponibilidade de inventário apresentada incorretamente - quantidadeCheck: " + quantidadeCheck + " quantidadeLojas: " + quantidadeLojas);
    }

    /// <summary>
    /// Método para validar textos dentro de um elemento
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <param name="textoEsperado"></param>
    /// <param name="elementoCampo"></param>
    public static void ValidarTextosNoElemento(IWebDriver webDriver, string XPath, string textoEsperado, string elementoCampo)
    {
        var textoAtual = RemoverNumerosEspacosDeUmTexto(webDriver, XPath, elementoCampo);
        Assert.That(textoAtual, Does.Contain(textoEsperado), "Textos não correspondem - TextoAtual: " + textoAtual + " TextoEsperado: " + textoEsperado);
    }

    /// <summary>
    /// Método para validar valor inteiro ou valor decimal dentro de um elemento
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="XPath"></param>
    /// <param name="numeroEsperado"></param>
    /// <param name="elementoCampo"></param>
    public static void ValidarNumerosNoElemento(IWebDriver webDriver, string XPath, object numeroEsperado, string elementoCampo)
    {
        object numeroAtual = RemoverLetrasEspacosDeUmTexto(webDriver, XPath, elementoCampo, 0);
        if (numeroAtual is int)
        {
            int valorAtual = (int)numeroAtual;
            int valorEsperado = (int)numeroEsperado;
            Debug.Assert(valorAtual == valorEsperado, "Valores não correspondem - ValorAtual: " + valorAtual + " ValorEspeado: " + valorEsperado);
        }
        else if (numeroAtual is double)
        {
            double valorAtual = (double)numeroAtual;
            double valorEsperado = (double)numeroEsperado;
            Debug.Assert(valorAtual == valorEsperado, "Valores não correspondem - ValorAtual: " + valorAtual + " ValorEsperado: " + valorEsperado);
        }
    }

    /// <summary>
    /// Método para buscar registros dentro do grid das telas
    /// </summary>
    /// <param name="webDriver"></param>
    /// <param name="xPathFiltrar"></param>
    /// <param name="xPathPesquisar"></param>
    /// <param name="xPathBuscar"></param>
    /// <param name="textoValor"></param>
    public static void BuscarRegistros(IWebDriver webDriver, string xPathFiltrar, string xPathPreencherFiltro, string xPathBuscar, string textoValor)
    {
        Esperar(2000);
        Clicar(webDriver, xPathFiltrar, "Botão Filtrar");
        webDriver.FindElement(By.XPath(xPathPreencherFiltro)).SendKeys(Keys.Control + "a" + Keys.Backspace);
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
}