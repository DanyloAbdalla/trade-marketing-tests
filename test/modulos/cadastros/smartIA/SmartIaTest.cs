using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace MeuClienteWebTestProject;

/// <summary>
/// Classe com os testes do cadastro de campanhas SmartIA
/// </summary>
[TestFixture]
public class SmartIaTest
{
    private IWebDriver _webDriver;
    private readonly BrowserType _browserType = BrowserType.Chrome;
    private RunSettings _runSettings;
    private bool _previousTestFalied;
    private string nomeCampanha = "MassaAutomatizada";
    private string whatsAppResponsavel = "15988086091";
    private string nomeResponsavel = "Usuário Homologacao";
    private string mensagemCabecalho = "Massa Automatizada";

    /// <summary>
    /// Método que será executado antes de cada teste
    /// </summary>
    [SetUp]
    public void Setup()
    {
        if (_previousTestFalied) Assert.Ignore("Pular o próximo teste, pois o teste anterior falhou");

        _runSettings = RunSettings.LoadSettings();
        _webDriver = DriverFactory.CreateDriver(_browserType);

        new LoginPage(_webDriver)
        .PreencherEmailUsuario(GlobalVariables.emailUsuarioSemPlanta)
        .PreencherSenhaUsuario(GlobalVariables.senhaUsuarioSemPlanta)
        .SubmeterLogin();

        new HomePage(_webDriver)
        .AcessarCadastroSmartIa();
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
        Dsl.PularTest(_runSettings, nameof(SmartIaTest), "", nameof(TestCriarCampanhaSmartIA));

        var contextoDeExecucao = "NovaCampanha";
        var statusCampanhaEsperado = "Criando";

        new SmartIaPage(_webDriver)
        .NovaCampanhaSmartIA()
        .PreencherCamposCampanha(nomeCampanha, whatsAppResponsavel, nomeResponsavel, mensagemCabecalho)
        .ValidarVarejoSelecionado()
        .RealizarVarredura()
        .SelecionarEReservarAtivos()
        .SalvarAtivosReservados()
        .SalvarCampanha(contextoDeExecucao)
        .FecharCampanha()
        .BuscarCampanhas(nomeCampanha)
        .ValidarStatusDaCampanha(statusCampanhaEsperado);
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
        Dsl.PularTest(_runSettings, nameof(SmartIaTest), "", nameof(TestEditarAtivosReservadosNaCampanhaExistente));

        var contexto = "EditarCampanha";

        new SmartIaPage(_webDriver)
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
        Dsl.PularTest(_runSettings, nameof(SmartIaTest), "", nameof(TestReservarNovoAtivoNaCampanhaExistente));

        var contexto = "EditarCampanha";
        var nomeAtivo = "Aplicativo";

        new SmartIaPage(_webDriver)
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
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed) _previousTestFalied = true;

        //Retorna para o Dashboard de Operações no final de cada teste, realizando logout
        new HomePage(_webDriver).AcessarDashboardOperacoes();
        new HomePage(_webDriver).RealizarLogout();

        _webDriver.Close();
    }
}