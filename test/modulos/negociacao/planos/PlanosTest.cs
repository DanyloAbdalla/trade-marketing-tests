using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
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
    private RunSettings _runSettings;
    private IWebDriver _webDriver;
    private readonly BrowserType _browserType = BrowserType.Chrome;
    private bool _previousTestFalied;
    private readonly string _testContext;
    private readonly string _className;
    private readonly string _nomeCampanha = "PlanosMassaAutomatizada";

    public PlanosTest(string testContext)
    {
        _testContext = testContext;
        _className = TestContext.CurrentContext.Test.ClassName.Split('.').Last();
    }

    /// <summary>
    /// Método que será executado antes de cada teste
    /// </summary>
    [SetUp]
    public void Setup()
    {
        _runSettings = RunSettings.LoadSettings();
        _webDriver = DriverFactory.CreateDriver(_browserType);
        var _testName = TestContext.CurrentContext.Test.MethodName;

        if (_previousTestFalied)
            Assert.Ignore("Pular o próximo teste, pois o teste anterior falhou");
        else if (_runSettings.ToSkip(_className, _testContext, _testName))
            Assert.Ignore("Teste ignorado pelas configurações de execução");

        if (_testContext.Contains("SemPlantaLoja"))
        {
            new LoginPage(_webDriver)
            .RealizarLogin(GlobalVariables.emailUsuarioSemPlanta, GlobalVariables.senhaUsuarioSemPlanta);

            new HomePage(_webDriver)
            .AcessarCadastroPlanos();
        }
        else if (_testContext.Contains("ComPlantaLoja"))
        {
            new LoginPage(_webDriver)
            .RealizarLogin(GlobalVariables.emailUsuarioComPlanta, GlobalVariables.senhaUsuarioComPlanta);

            new HomePage(_webDriver)
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

        new PlanosContratosPage(_webDriver)
        .NovaSimulacaoDePlano()
        .PreencherCampoIndustria()
        .PreencherCampoCampanha(_nomeCampanha)
        .SelecionarAtivos()
        .PreencherQuantidadeAtivos(_testContext)
        .SelecionarLojas()
        .GerarPrePlano()
        .SalvarPlano(contextoDeExecucao, _testContext)
        .ValidarPlanoCriado()
        .FecharDadosDoPlano()
        .BuscarPlanos(_nomeCampanha)
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
        var executionContext = "EditarPlano";

        new PlanosContratosPage(_webDriver)
        .BuscarPlanos(_nomeCampanha)
        .AbrirEdicaoDoPlano()
        .EditarInicioVigencia(executionContext)
        .EditarFimVigencia(executionContext)
        .SalvarPlano(executionContext)
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
        var executionContext = "EditarPlanoAlterandoQuantidadeAtivo";

        new PlanosContratosPage(_webDriver)
        .BuscarPlanos(_nomeCampanha)
        .AbrirEdicaoDoPlano()
        .AbrirAbaAtivosAlocados()
        .EditarQuantidadesDosAtivosNoPlano(_testContext)
        .SalvarPlano(executionContext, _testContext)
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
        var executionContext = "EditarPlanoIncluindoAtivo";

        new PlanosContratosPage(_webDriver)
        .BuscarPlanos(_nomeCampanha)
        .AbrirEdicaoDoPlano()
        .AbrirAbaAtivosAlocados()
        .AlocarNovosAtivosNoPlano(_testContext)
        .SalvarPlano(executionContext, _testContext)
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
        var executionContext = "EditarPlano";

        new PlanosContratosPage(_webDriver)
        .BuscarPlanos(_nomeCampanha)
        .AbrirEdicaoDoPlano()
        .EditarSituacaoDoPlano(situacaoPlano)
        .SalvarPlano(executionContext)
        .FecharDadosDoPlano()
        .BuscarPlanos(_nomeCampanha)
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
        var executionContext = "NovoPlano";

        new PlanosContratosPage(_webDriver)
        .NovaSimulacaoDePlano()
        .PreencherCampoIndustria()
        .PreencherCampoCampanha(_nomeCampanha)
        .EditarInicioVigencia(executionContext)
        .EditarFimVigencia(executionContext)
        .SelecionarAtivos()
        .PreencherQuantidadeAtivos(_testContext)
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
        var executionContext = "CancelarPlano";

        new PlanosContratosPage(_webDriver)
        .BuscarPlanos(_nomeCampanha)
        .AbrirEdicaoDoPlano()
        .EditarSituacaoDoPlano(situacaoPlano)
        .SalvarPlano(executionContext)
        .FecharDadosDoPlano()
        .BuscarPlanos(_nomeCampanha)
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
        new PlanosContratosPage(_webDriver)
        .BuscarPlanos(_nomeCampanha)
        .ConfirmarExclusaoDoPlano();
    }

    /// <summary>
    /// Método que será executado ao final de cada teste
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Skipped)
        {
            _webDriver.Close();
        }
        else if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            _previousTestFalied = true;
            _webDriver.Close();
        }
        else
        {
            //Retorna para o Dashboard de Operações no final de cada teste, realizando logout
            new HomePage(_webDriver).AcessarDashboardOperacoes();
            new HomePage(_webDriver).RealizarLogout();

            _webDriver.Close();
        }

    }
}