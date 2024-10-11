
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
    /// E clicar no botão Salvar Campanha
    /// Então uma campanha será criada com Status = Criando
    /// </summary>
    [Test, Order(1)]
    public void TestCriarCampanhaSmartIA()
    {
        var contexto = "NovaCampanha";
        var statusCampanha = "Criando";

        new SmartIaPage(webDriver)
        .NovaCampanhaSmartIA()
        .PreencherCamposCampanha()
        .SalvarCampanha(contexto)
        .FecharCampanha()
        .BuscarCampanhas()
        .ValidarStatusDaCampanha(statusCampanha);
    }

    /// <summary>
    /// Testar a inclusão dos ativos na campanha
    /// 
    /// Como comercial de trade marketing
    /// Eu quero adicionar na campanha, o varejo e os ativos com disponibilidade no inventário da loja
    /// Para montar minha venda por unidade ou por pacote
    /// 
    /// Dado que eu tenho uma campanha criada no SmartIA
    /// Quando eu editar a campanha
    /// E selecionar o varejo 
    /// E reservar os ativos para as lojas
    /// E clicar no botão Salvar Campanha
    /// Então os ativos com disponibilidade de inventário serão reservados para a campanha, com venda por unidade OU por pacote, para o varejo selecionado
    /// </summary>
    [Test, Order(2)]
    public void TestAdicionarVarejoEAtivosNaCampanha()
    {
        var contexto = "EditarCampanha";

        new SmartIaPage(webDriver)
        .BuscarCampanhas()
        .AbrirEdicaoDaCampanha()
        .AdicionarVarejo()
        .RealizarVarredura()
        .SelecionarEReservarAtivos()
        .SalvarAtivosReservados()
        .SalvarCampanha(contexto);
    }

    /// <summary>
    /// Testar a edição das quantidades dos ativos reservados na campanha
    /// 
    /// Como comercial de trade marketing
    /// Eu quero alterar as quantidades dos ativos reservados
    /// Para atualizar minha campanha
    /// 
    /// Dado que eu tenho uma campanha criada no SmartIA
    /// Quando acessar a tela de edição
    /// E alterar as quantidades dos ativos reservados para as lojas
    /// E clicar no botão Salvar Campanha
    /// Então a campanha será salva com as novas quantidades
    /// </summary>
    [Test, Order(3)]
    public void TestEditarAtivosReservadosNaCampanhaExistente()
    {
        var contexto = "EditarCampanha";

        new SmartIaPage(webDriver)
        .BuscarCampanhas()
        .AbrirEdicaoDaCampanha()
        .AbrirMenuSuspensoVarejos()
        .EditarQuantidadesDosAtivosReservados()
        .SalvarAtivosReservados()
        .SalvarCampanha(contexto);
    }

    /// <summary>
    /// Testar a reserva de um novo ativo em uma campanha existente
    /// 
    /// Como comercial de trade marketing
    /// Eu quero reservar um novo ativo para as lojas
    /// Para os valores da minha campanha, com o novo ativo
    /// 
    /// Dado que eu tenho uma campanha criada no SmartIA
    /// Quando acessar a tela de edição
    /// E reservar um novo ativo para as lojas
    /// E clicar no botão Salvar Campanha
    /// Então a campanha será salva com o novo ativo
    /// </summary>
    [Test, Order(4)]
    public void TestReservarNovoAtivoNaCampanhaExistente()
    {
        var contexto = "EditarCampanha";
        var nomeAtivo = "Aplicativo";

        new SmartIaPage(webDriver)
        .BuscarCampanhas()
        .AbrirEdicaoDaCampanha()
        .AbrirMenuSuspensoVarejos()
        .ReservarNovosAtivosPorLoja(nomeAtivo)
        .SalvarAtivosReservados()
        .SalvarCampanha(contexto);
    }

    /// <summary>
    /// Método que será executado ao final de cada teste
    /// </summary>
    [TearDown]
    public void TearDown()
    { webDriver.Close(); System.Diagnostics.Process.Start("taskkill_chromedriver.bat"); }
}