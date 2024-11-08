
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
    /// Eu quero criar uma campanha com pacotes de ativos disponíveis no inventário da loja para alocação
    /// Para comunicar meus clientes sobre vendas destes pacotes na campanha
    /// 
    /// Dado que eu não tenho permissão de planta de loja
    /// E que eu tenho disponibilidade de inventário
    /// Quando eu iniciar uma nova campanha, preenchendo os campos de cabeçalho
    /// E reservar o ativo, colocando as quantidades para as lojas, clicando no botão “Salvar”
    /// E clicar no botão “Salvar” da campanha
    /// Então a campanha será criada com sucesso com Status = Criando
    /// </summary>
    [Test, Order(1)]
    public void TestCriarCampanhaSmartIA()
    {
        var contexto = "NovaCampanha";
        var statusCampanha = "Criando";

        new SmartIaPage(webDriver)
        .NovaCampanhaSmartIA()
        .PreencherCamposCampanha()
        .AdicionarVarejo()
        .RealizarVarredura()
        .SelecionarEReservarAtivos()
        .SalvarAtivosReservados()
        .SalvarCampanha(contexto)
        .FecharCampanha()
        .BuscarCampanhas()
        .ValidarStatusDaCampanha(statusCampanha);
    }

    /// <summary>
    /// Testar a edição das quantidades dos ativos reservados na campanha
    /// 
    /// Como comercial de trade marketing
    /// Eu quero alterar as quantidades dos ativos reservados
    /// Para atualizar minha campanha
    /// 
    /// Dado que eu não tenho permissão de planta de loja
    /// E que eu tenho uma campanha criada, contendo um ativo com disponibilidade de inventário
    /// Quando eu acessar a tela de edição da campanha
    /// E alterar a quantidade do ativo, clicando no botão "Salvar"
    /// E clicar no botão “Salvar” da campanha
    /// Então a campanha será salva com a nova quantidade
    /// </summary>
    [Test, Order(2)]
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
    /// Dado que eu não tenho permissão de planta de loja
    /// E que eu tenho uma campanha criada no SmartIA
    /// Quando acessar a tela de edição
    /// E reservar um novo ativo para as lojas com disponibilidade de inventário, clicando no botão "Salvar"
    /// E clicar no botão "Salvar" da campanha
    /// Então a campanha será salva com o novo ativo
    /// </summary>
    [Test, Order(3)]
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