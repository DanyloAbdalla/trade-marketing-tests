using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace MeuClienteWebTestProject;

/// <summary>
/// Classe com os testes para o Cadastro de Planos\Contratos
/// </summary>
[TestFixture("SemPlantaLoja", Category = "PlanosSemPlantaDeLoja")]
[TestFixture("ComPlantaLoja", Category = "PlanosComPlantaDeLoja")]
[Parallelizable(ParallelScope.Fixtures)]
public class PlanosTest
{
    private IWebDriver webDriver;
    private readonly BrowserType browserType = BrowserType.Chrome;
    private bool _previousTestFalied;
    private string nomeCampanha = "MassaAutomatizada";
    private string _contextoDeTeste = "";

    public PlanosTest(string contextoDeTeste)
    {
        _contextoDeTeste = contextoDeTeste;
    }

    /// <summary>
    /// Método que será executado antes de cada teste
    /// </summary>
    [SetUp]
    public void Setup()
    {
        if(_previousTestFalied) Assert.Ignore("Pular os próximos teste, pois o teste anterior falhou");

        webDriver = DriverFactory.CreateDriver(browserType);

        if (_contextoDeTeste.Contains("SemPlantaLoja"))
        {
            new LoginPage(webDriver)
            .PreencherEmailUsuario(GlobalVariables.emailUsuarioSemPlanta)
            .PreencherSenhaUsuario(GlobalVariables.senhaUsuarioSemPlanta)
            .SubmeterLogin();

            new HomePage(webDriver)
            .AcessarCadastroPlanos();
        }
        else if (_contextoDeTeste.Contains("ComPlantaLoja"))
        {
            new LoginPage(webDriver)
            .PreencherEmailUsuario(GlobalVariables.emailUsuarioComPlanta)
            .PreencherSenhaUsuario(GlobalVariables.senhaUsuarioComPlanta)
            .SubmeterLogin();

            new HomePage(webDriver)
            .AcessarCadastroPlanos();
        }
    }

    /// <summary>
    /// Testar a criação de um plano
    /// 
    /// Como comercial de trade marketing
    /// Eu quero criar um novo plano
    /// E inicar uma nova negociação
    /// Para enviar a proposta para o cliente
    /// 
    /// Dado que eu tenho uma nova negociação
    /// E que eu tenho disponibilidade de inventário, em um determinado período de vigência
    /// Quando eu simular um novo plano
    /// E escolher os ativos, colocar as quantidades, selecionar as lojas
    /// Então será apresentado o botão de sucesso para as lojas com disponibilidade, com o botão Gerar Pré-Plano habilitado
    /// Quando eu clicar no botão “Gerar Pré-Plano”
    /// Então o plano\contrato será criado com o ativo, com Status = Simulado e Farol = Planejado, com vigência em d+30
    /// </summary>
    [Test, Order(1)]
    public void TestCriarPlano()
    {
        var statusPlanoEsperado = "Simulado";
        var farolPlanoEsperado = "PLANEJADO";
        var contextoDeExecucao = "NovoPlano";

        new PlanosContratosPage(webDriver)
        .NovaSimulacaoDePlano()
        .PreencherCampoIndustria()
        .PreencherCampoCampanha(nomeCampanha)
        .SelecionarAtivos()
        .PreencherQuantidadeAtivos(_contextoDeTeste)
        .SelecionarLojas()
        .GerarPrePlano()
        .SalvarPlano(contextoDeExecucao, _contextoDeTeste)
        .ValidarPlanoCriado()
        .FecharDadosDoPlano()
        .BuscarPlanos(nomeCampanha)
        .ValidarStatusFarolDoPlano(statusPlanoEsperado, farolPlanoEsperado);
    }

    /// <summary>
    /// Testar a edição da vigência em um plano existente
    /// 
    /// Como comercial de trade marketing
    /// Eu quero alterar a vigência
    /// Para negociar um novo período
    /// 
    /// Dado que eu tenho um plano criado na Negociação
    /// Quando acessar a tela de edição
    /// E alterar as datas início e fim da vigência
    /// E clicar no botão Salvar Plano
    /// Então um o plano será salvo com a nova vigência
    /// </summary>
    [Test, Order(2)]
    public void TestEditarPlanoExistenteAlterandoVigencia()
    {
        var contextoDeExecucao = "EditarPlano";

        new PlanosContratosPage(webDriver)
        .BuscarPlanos(nomeCampanha)
        .AbrirEdicaoDoPlano()
        .EditarInicioVigencia(contextoDeExecucao)
        .EditarFimVigencia(contextoDeExecucao)
        .SalvarPlano(contextoDeExecucao)
        .FecharDadosDoPlano();
    }

    /// <summary>
    /// Testar edição das quantidades dos ativos alocados em um plano existente
    /// 
    /// Como comercial de trade marketing
    /// Eu quero alterar as quantidades dos ativos alocados por loja
    /// Para negociar a alocação de mais espaços
    /// 
    /// Dado que eu tenho um plano criado, contendo um ativo com disponibilidade de inventário
    /// Quando eu acessar a tela de edição do plano
    /// E alterar a quantidade do ativo
    /// E clicar no botão Salvar Plano
    /// Então o plano será salvo com sucesso com a nova quantidade
    /// </summary>
    [Test, Order(3)]
    public void TestEditarPlanoExistenteAlterandoQuantidadeAlocadaDoAtivoDisponivel()
    {
        var contextoDeExecucao = "EditarPlanoAlterandoQuantidadeAtivo";

        new PlanosContratosPage(webDriver)
        .BuscarPlanos(nomeCampanha)
        .AbrirEdicaoDoPlano()
        .AbrirAbaAtivosAlocados()
        .EditarQuantidadesDosAtivosNoPlano(_contextoDeTeste)
        .SalvarPlano(contextoDeExecucao)
        .FecharDadosDoPlano();
    }

    /// <summary>
    /// Testar alocação de um novo ativo em um plano existente
    /// 
    /// Como comercial de trade marketing
    /// Eu quero alocar um novo ativo para as lojas
    /// Para atualizar meu plano com um novo ativo
    /// 
    /// Dado que eu tenho um plano criado
    /// Quando eu acessar a tela de edição do plano
    /// E incluir um novo ativo para a loja com disponibilidade de inventário
    /// Então o plano será salvo com sucesso com o novo ativo
    /// </summary>
    [Test, Order(4)]
    public void TestEditarPlanoExistenteIncluindoNovoAtivoDisponivel()
    {
        var contextoDeExecucao = "EditarPlanoIncluindoAtivo";

        new PlanosContratosPage(webDriver)
        .BuscarPlanos(nomeCampanha)
        .AbrirEdicaoDoPlano()
        .AbrirAbaAtivosAlocados()
        .AlocarNovosAtivosNoPlano(_contextoDeTeste)
        .SalvarPlano(contextoDeExecucao)
        .FecharDadosDoPlano();
    }

    /// <summary>
    /// Testar aprovação de um plano existente
    /// 
    /// Como comercial de trade marketing
    /// Eu quero aprovar o plano
    /// Para finalizar a venda, alocando os ativos nos espaços das lojas
    /// 
    /// Dado que eu tenho um plano simulado
    /// Quando acessar a tela de edição
    /// E aprovar, clicando no campo Situação do Plano
    /// E clicar no botão Salvar Plano
    /// Então o plano será salvo, com Status = Aprovado e Farol = Aprovado
    /// </summary>
    [Test, Order(5)]
    public void TestAprovarPlano()
    {
        var situacaoPlano = "Contrato Aprovado";
        var statusPlanoEsperado = "Aprovado";
        var farolPlanoEsperado = "APROVADO";
        var contextoDeExecucao = "EditarPlano";

        new PlanosContratosPage(webDriver)
        .BuscarPlanos(nomeCampanha)
        .AbrirEdicaoDoPlano()
        .EditarSituacaoDoPlano(situacaoPlano)
        .SalvarPlano(contextoDeExecucao)
        .FecharDadosDoPlano()
        .BuscarPlanos(nomeCampanha)
        .ValidarStatusFarolDoPlano(statusPlanoEsperado, farolPlanoEsperado);
    }

    /// <summary>
    /// Testar alerta de inventário na criação de planos
    /// 
    /// Como comercial de trade marketing
    /// Eu quero selecionar ativos sem indisponibilidade de alocação
    /// Para que eu seja informado de que não há quantidade suficente no inventário
    /// 
    /// Dado que eu não tenho disponibilidade de inventário, em um determinado período de vigência
    /// Quando eu simular um novo plano
    /// E escolher os ativos, colocar as quantidades, selecionar as lojas
    /// Então será apresentado o botão de alerta para as lojas com indisponibilidade
    /// E uma mensagem será apresentada ao lado do botão Gerar Pré-Plano, com o mesmo desabilitado
    /// </summary>
    [Test, Order(6)]
    public void TestCriarPlanoComAlertaDeInventario()
    {
        var contextoDeExecucao = "NovoPlano";

        new PlanosContratosPage(webDriver)
        .NovaSimulacaoDePlano()
        .PreencherCampoIndustria()
        .PreencherCampoCampanha(nomeCampanha)
        .EditarInicioVigencia(contextoDeExecucao)
        .EditarFimVigencia(contextoDeExecucao)
        .SelecionarAtivos()
        .PreencherQuantidadeAtivos(_contextoDeTeste)
        .SelecionarLojas()
        .ValidarAlertaInventario()
        .FecharDadosDoPlano();
    }

    /// <summary>
    /// Testar cancelamento de plano
    /// 
    /// Como comercial de trade marketing
    /// Eu quero cancelar um plano
    /// Para que o mesmo seja desconsiderado, gerenciando as minhas negociações
    /// 
    /// Dado que eu tenho um plano
    /// Quando acessar a tela de edição
    /// E cancelar, clicando no campo Situação do Plano
    /// E clicar no botão Salvar Plano
    /// Então o plano será salvo, com Status = Cancelado e Farol = Cancelado
    /// </summary>
    [Test, Order(7)]
    public void TestCancelarPlano()
    {
        var situacaoPlano = "Cancelado";
        var statusPlanoEsperado = "Cancelado";
        var farolPlanoEsperado = "CANCELADO";
        var contextoDeExecucao = "CancelarPlano";

        new PlanosContratosPage(webDriver)
        .BuscarPlanos(nomeCampanha)
        .AbrirEdicaoDoPlano()
        .EditarSituacaoDoPlano(situacaoPlano)
        .SalvarPlano(contextoDeExecucao)
        .FecharDadosDoPlano()
        .BuscarPlanos(nomeCampanha)
        .ValidarStatusFarolDoPlano(statusPlanoEsperado, farolPlanoEsperado);
    }

    /// <summary>
    /// Testar exclusão de plano
    /// 
    /// Como comercial de trade marketing
    /// Eu quero excluir uma plano
    /// Para que o mesmo seja desconsiderado, gerenciando as minhas negociações
    /// 
    /// Dado que eu tenho um plano
    /// Quando acessar a tela de planos
    /// E excluir (desativar), clicando no campo Excluir do Plano
    /// Então o plano será excluído com mensagem de sucesso, não sendo mais apresentado na lista
    /// </summary>
    [Test, Order(8)]
    public void TestExcluirPlano()
    {
        new PlanosContratosPage(webDriver)
        .BuscarPlanos(nomeCampanha)
        .ConfirmarExclusaoDoPlano();
    }

    /// <summary>
    /// Método que será executado ao final de cada teste
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        if(TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed) _previousTestFalied = true;
        
        HomePage homePage = new HomePage(webDriver);
        homePage.AcessarDashBoardOperacoes();
        Dsl.Esperar();

        webDriver.Close();
    }
}