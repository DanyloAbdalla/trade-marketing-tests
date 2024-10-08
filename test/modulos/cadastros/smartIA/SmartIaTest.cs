
using OpenQA.Selenium;

namespace MeuClienteWebTestProject;

/// <summary>
/// Classe com os testes do cadastro de campanhas SmartIA
/// </summary>
[TestFixture]
public class SmartIaTest
{
    private IWebDriver webDriver;
    private readonly BrowserType browserType = BrowserType.Chrome;

    /// <summary>
    /// Método que será executado antes de cada teste
    /// </summary>
    [SetUp]
    public void Setup()
    {
        webDriver = DriverFactory.CreateDriver(browserType);
        webDriver.Navigate().GoToUrl(GlobalVariables.urlPlataforma);

        new LoginPage(webDriver)
        .PreencherEmailUsuario(GlobalVariables.emailUsuario)
        .PreencherSenhaUsuario(GlobalVariables.senhaUsuario)
        .SubmeterLogin();

        new HomePage(webDriver)
        .AbrirCadastroSmartIa();
    }

    /// <summary>
    /// Testar a criação de uma campanha
    /// 
    /// Como comercial de trade marketing
    /// Eu quero criar uma campanha
    /// Para começar a montar meu pacote de ativos, com disponibiliade de alocação no inventário da loja
    /// 
    /// Dado que eu tenho espaços disponíveis para realização de uma nova campanha no SmartIA
    /// Quando eu criar a campanha
    /// E preencher os campos Nome da Campanha, Inicio/Fim Vigência, Email, WhatsApp, Data Limite e Responsável da Campanha
    /// E clicar no botão Salvar
    /// Então uma campanha será criada com Status = Criando
    /// </summary>
    [Test]
    public void TestCriarCampanhaSmartIA()
    {
        var statusCampanha = "Criando";

        new SmartIaPage(webDriver)
        .NovaCampanhaSmartIA()
        .PreencherCamposCampanha()
        .SalvarCampanha()
        .FecharCampanha()
        .BuscarCampanhas()
        .ValidarStatusDaCampanha(statusCampanha);
    }


    /// <summary>
    /// Testar a inclusão dos ativos na campanha
    /// 
    /// Como comercial de trade marketing
    /// Eu quero adicionar na campanha, os ativos com disponibilidade no inventário da loja
    /// Para começar a montar meu pacote, que será enviado para os fornecedores
    /// 
    /// Dado que eu tenho espaços disponíveis para realização de uma nova campanha no SmartIA
    /// Quando eu criar uma campanha
    /// E relacionar os ativos a campanha
    /// E clicar no botão Salvar
    /// Então os ativos com disponibilidade de inventário serão relacionados a campanha, como pacote de venda
    /// </summary>
    [Test]
    public void TestAdicionarVarejoEAtivosNaCampanha()
    {
        new SmartIaPage(webDriver)
        .BuscarCampanhas();
    }


    /// <summary>
    /// Método que será executado ao final de cada teste
    /// </summary>
    [TearDown]
    public void TearDown()
    { webDriver.Close(); }
}