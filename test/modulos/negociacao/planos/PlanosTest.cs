using System.Diagnostics;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using OpenQA.Selenium;

namespace MeuClienteWebTestProject;

/// <summary>
/// Classe com os testes para o Cadastro de Planos\Contratos
/// </summary>
[TestFixture("ClienteStart")]
[TestFixture("ClientePro")]
[TestFixture("ClienteExpert")]
public class PlanosTest
{
    private RunSettings runSettings;
    private IWebDriver webDriver;
    private readonly BrowserType browserType = BrowserType.Chrome;
    private ClienteUpSell clienteUpSellAtual => Enum.TryParse<ClienteUpSell>(contextoDeTeste, out var cliente) ? cliente : ClienteUpSell.ClienteStart;
    private bool testeAnteriorPulouFalhou = false;
    private bool primeiroTeste;
    private readonly string nomeClasse;
    private readonly string contextoDeTeste;

    public PlanosTest(string contextoDeTeste)
    {
        this.contextoDeTeste = contextoDeTeste;
        DataLoader.CarregarArquivo();
        nomeClasse = TestContext.CurrentContext.Test.ClassName.Split('.').Last();
    }

    /// <summary>
    /// Método que será executado uma única vez, antes de todos os testes
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        runSettings = RunSettings.LoadSettings();
        webDriver = DriverFactory.CreateDriver(browserType);
    }

    /// <summary>
    /// Método que será executado antes de cada teste
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var nomeTeste = TestContext.CurrentContext.Test.MethodName;

        if (string.IsNullOrEmpty(nomeTeste))
            throw new ArgumentException("Nome do teste é inválido");

        if (testeAnteriorPulouFalhou)
            Assert.Ignore("Pular teste, o teste anterior falhou");
        if (runSettings.ToSkip(nomeClasse, contextoDeTeste, nomeTeste))
            Assert.Ignore("Teste ignorado pelas configurações de execução");

        int indiceUsuario = clienteUpSellAtual switch
        {
            ClienteUpSell.ClienteStart => 0,
            ClienteUpSell.ClientePro => 1,
            ClienteUpSell.ClienteExpert => 2,
            _ => throw new ArgumentOutOfRangeException(nameof(clienteUpSellAtual), "Cliente upsell não reconhecido")
        };

        new LoginPage(webDriver, clienteUpSellAtual)
            .RealizarLogin(GlobalVariables.emailUsuarios[indiceUsuario], GlobalVariables.senhaUsuarios[indiceUsuario]);

        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.UltimoCadastroAcessado, "Label Último Cadastro Acessado");
        if (!Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.UltimoCadastroAcessado, "Label Último Cadastro Acessado").Contains("Plano"))
        {
            new HomePage(webDriver, clienteUpSellAtual)
                .AcessarCadastroPlanos(nomeTeste);
        }
        else
        {
            if (clienteUpSellAtual == ClienteUpSell.ClientePro)
            {
                Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.LoadListaPlanos, "Load Lista Planos");
            }
            else
            {
                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.TotalReceitaPlanos, "Label Total Receita Planos");
            }
        }
    }

    /// <summary>
    /// Testar a simulação do plano com tipo de mídia gráfica, criando um contrato com status simulado
    /// 
    /// Como comercial
    /// Eu quero simular o plano, criando um contrato de venda
    /// Para que eu possa negociar com a indústria a alocação de ativos, realizando o trade marketing
    /// </summary>
    [Test, Order(1)]
    public void TestCriarPlanoComAtivosTipoMidiaGrafica()
    {
        var stopwatchTest = Stopwatch.StartNew();
        primeiroTeste = true;

        if (clienteUpSellAtual == ClienteUpSell.ClienteExpert) //criando o plano através da nova tela de simulação
        {
            new PlanosContratosPage(webDriver, clienteUpSellAtual)
            .NovaSimulacaoDePlano()
            .PreencherCampoIndustria()
            .PreencherCampoCampanha()
            .SelecionarAtivosELojas()
            .PreencherQuantidadeAtivos()
            .GerarPrePlano()
            .AguardandoCricaoDoPlano()
            .SalvarPlano()
            .ValidarReceitasDoPlano()
            .ValidarPlanoCriado()
            .FecharDadosDoPlano()
            .BuscarPlanos()
            .ValidarStatusFarolDoPlano();
        }
        else
        {
            new PlanosContratosPage(webDriver, clienteUpSellAtual)
            .NovaSimulacaoDePlano()
            .PreencherCampoIndustria()
            .PreencherCampoCampanha()
            .SelecionarAtivos()
            .PreencherQuantidadeAtivos()
            .SelecionarLojas()
            .GerarPrePlano()
            .AguardandoCricaoDoPlano()
            .SalvarPlano()
            .ValidarReceitasDoPlano()
            .ValidarPlanoCriado()
            .FecharDadosDoPlano()
            .BuscarPlanos()
            .ValidarStatusFarolDoPlano();
        }

        stopwatchTest.Stop();
        TestContext.WriteLine($"Tempo gasto no teste em minutos: {stopwatchTest.Elapsed}");
    }

    /// <summary>
    /// Testar a simulação do plano com tipo de mídia física, criando um contrato com status simulado
    /// 
    /// Como comercial
    /// Eu quero simular o plano, criando um contrato de venda
    /// Para que eu possa negociar com a indústria a alocação de ativos, realizando o trade marketing
    /// </summary>
    [Test, Order(2)]
    public void TestCriarPlanoComAtivosTipoMidiaFisica()
    {
        var stopwatchTest = Stopwatch.StartNew();

        if (clienteUpSellAtual == ClienteUpSell.ClienteExpert)
        {
            new PlanosContratosPage(webDriver, clienteUpSellAtual)
            .NovaSimulacaoDePlano()
            .PreencherCampoIndustria()
            .PreencherCampoCampanha()
            .SelecionarAtivosELojas()
            .PreencherQuantidadeAtivos()
            .GerarPrePlano()
            .AguardandoCricaoDoPlano()
            .SalvarPlano()
            .ValidarReceitasDoPlano()
            .ValidarPlanoCriado()
            .FecharDadosDoPlano()
            .BuscarPlanos()
            .ValidarStatusFarolDoPlano();
        }
        else
        {
            new PlanosContratosPage(webDriver, clienteUpSellAtual)
            .NovaSimulacaoDePlano()
            .PreencherCampoIndustria()
            .PreencherCampoCampanha()
            .SelecionarAtivos()
            .PreencherQuantidadeAtivos()
            .SelecionarLojas()
            .GerarPrePlano()
            .AguardandoCricaoDoPlano()
            .SalvarPlano()
            .ValidarReceitasDoPlano()
            .ValidarPlanoCriado()
            .FecharDadosDoPlano()
            .BuscarPlanos()
            .ValidarStatusFarolDoPlano();
        }

        stopwatchTest.Stop();
        TestContext.WriteLine($"Tempo gasto no teste em minutos: {stopwatchTest.Elapsed}");
    }

    /// <summary>
    /// Testar a edição da vigência no plano existente
    /// 
    /// Como comercial
    /// Eu quero editar a vigência do plano
    /// Para negociar um novo período com a indústria
    /// </summary>
    [Test, Order(3)]
    public void TestEditarPlanoExistenteAlterandoVigenciaDoPlano()
    {
        var stopwatchTest = Stopwatch.StartNew();

        new PlanosContratosPage(webDriver, clienteUpSellAtual)
        .BuscarPlanos()
        .AbrirEdicaoDoPlano()
        .SelecionarVigenciaDoPlano()
        .SalvarPlano()
        .FecharDadosDoPlano();

        stopwatchTest.Stop();
        TestContext.WriteLine($"Tempo gasto no teste em minutos: {stopwatchTest.Elapsed}");
    }

    /// <summary>
    /// Testar a edição da vigência do trade no plano existente
    /// 
    /// Como comercial
    /// Eu quero editar a vigência do trade
    /// Para negociar um novo período com a indústria
    /// </summary>
    [Test, Order(4)]
    public void TestEditarPlanoExistenteAlterandoVigenciaDoTrade()
    {
        var stopwatchTest = Stopwatch.StartNew();

        new PlanosContratosPage(webDriver, clienteUpSellAtual)
        .BuscarPlanos()
        .AbrirEdicaoDoPlano()
        .AbrirAbaAtivosAlocados()
        .EditarVigenciaDoAtivoAlocado()
        .SalvarPlano()
        .FecharDadosDoPlano();

        stopwatchTest.Stop();
        TestContext.WriteLine($"Tempo gasto no teste em minutos: {stopwatchTest.Elapsed}");
    }

    /// <summary>
    /// Testar edição das quantidades alocadas para um ativo no plano existente
    /// 
    /// Como comercial
    /// Eu quero alterar as quantidades alocadas nas lojas selecionadas para os ativos
    /// Para negociar mais espaços com a indústria
    /// </summary>
    [Test, Order(5)]
    public void TestEditarPlanoExistenteAlterandoQuantidadeAlocadaDoAtivoDisponivel()
    {
        var stopwatchTest = Stopwatch.StartNew();

        new PlanosContratosPage(webDriver, clienteUpSellAtual)
        .BuscarPlanos()
        .AbrirEdicaoDoPlano()
        .AbrirAbaAtivosAlocados()
        .EditarQuantidadesDosAtivosNoPlano()
        .SalvarPlano()
        .ValidarReceitasDoPlano()
        .FecharDadosDoPlano();

        stopwatchTest.Stop();
        TestContext.WriteLine($"Tempo gasto no teste em minutos: {stopwatchTest.Elapsed}");
    }

    /// <summary>
    /// Testar a inclusão de um ativo no plano existente
    /// 
    /// Como comercial
    /// Eu quero incluir um ativo com as lojas
    /// Para negociar mais ativos com a indústria
    /// </summary>
    [Test, Order(6)]
    public void TestEditarPlanoExistenteIncluindoNovoAtivoDisponivel()
    {
        var stopwatchTest = Stopwatch.StartNew();

        new PlanosContratosPage(webDriver, clienteUpSellAtual)
        .BuscarPlanos()
        .AbrirEdicaoDoPlano()
        .AbrirAbaAtivosAlocados()
        .AlocarNovosAtivosNoPlano()
        .SalvarPlano()
        .ValidarReceitasDoPlano()
        .FecharDadosDoPlano();

        stopwatchTest.Stop();
        TestContext.WriteLine($"Tempo gasto no teste em minutos: {stopwatchTest.Elapsed}");
    }

    /// <summary>
    /// Testar aprovação do plano
    /// 
    /// Como comercial
    /// Eu quero aprovar o plano
    /// Para concluir a venda, bloqueando o inventário dos ativos nas lojas para novas negociações
    /// </summary>
    [Test, Order(7)]
    public void TestAprovarPlano()
    {
        var stopwatchTest = Stopwatch.StartNew();

        new PlanosContratosPage(webDriver, clienteUpSellAtual)
        .BuscarPlanos()
        .AbrirEdicaoDoPlano()
        .EditarSituacaoDoPlano()
        .SalvarPlano()
        .FecharDadosDoPlano()
        .RecarregarPlanos()
        .ValidarStatusFarolDoPlano();

        stopwatchTest.Stop();
        TestContext.WriteLine($"Tempo gasto no teste em minutos: {stopwatchTest.Elapsed}");
    }

    /// <summary>
    /// Testar alerta de inventário ao criar um plano
    /// 
    /// Como QA
    /// Eu quero selecionar ativos sem disponibilidade de inventario
    /// Para que eu seja informado de que os ativos não possuem quantidade suficente
    /// </summary>
    [Test, Order(8)]
    public void TestCriarPlanoComAlertaDeInventario()
    {
        var stopwatchTest = Stopwatch.StartNew();

        if (clienteUpSellAtual == ClienteUpSell.ClienteExpert)
        {
            new PlanosContratosPage(webDriver, clienteUpSellAtual)
            .NovaSimulacaoDePlano()
            .PreencherCampoIndustria()
            .PreencherCampoCampanha()
            .SelecionarVigenciaDoPlano()
            .FiltrarInventarios()
            .ValidarIndisponibilidadeDeInventario()
            .FecharDadosDoPlano();
        }
        else
        {
            new PlanosContratosPage(webDriver, clienteUpSellAtual)
            .NovaSimulacaoDePlano()
            .PreencherCampoIndustria()
            .PreencherCampoCampanha()
            .SelecionarAtivos()
            .PreencherQuantidadeAtivos()
            .SelecionarLojas()
            .ValidarIndisponibilidadeDeInventario()
            .FecharDadosDoPlano();
        }

        stopwatchTest.Stop();
        TestContext.WriteLine($"Tempo gasto no teste em minutos: {stopwatchTest.Elapsed}");
    }

    /// <summary>
    /// Testar alerta de inventário ao editar um plano
    /// 
    /// Como QA
    /// Eu quero editar um plano com ativos sem disponibilidade de inventario
    /// Para que eu seja informado de que os ativos não possuem quantidade suficente
    /// </summary>
    [Test, Order(9)]
    public void TestEditarPlanoComAlertaDeInventario()
    {
        var stopwatchTest = Stopwatch.StartNew();

        new PlanosContratosPage(webDriver, clienteUpSellAtual)
        .BuscarPlanos()
        .AbrirEdicaoDoPlanoFiltrado()
        .ValidarMensagensDeIndisponibilidadeDeInventario()
        .FecharMensagensDeConfirmacao()
        .AbrirAbaAtivosAlocados()
        .ValidarAlertasDeIndisponibilidadeDeInventario()
        .FecharDadosDoPlano();

        stopwatchTest.Stop();
        TestContext.WriteLine($"Tempo gasto no teste em minutos: {stopwatchTest.Elapsed}");
    }

    /// <summary>
    /// Testar cancelamento de plano
    /// 
    /// Como comercial
    /// Eu quero cancelar um plano
    /// Para que o mesmo seja desconsiderado das minhas negociações
    /// </summary>
    [Test, Order(10)]
    public void TestCancelarPlano()
    {
        var stopwatchTest = Stopwatch.StartNew();

        new PlanosContratosPage(webDriver, clienteUpSellAtual)
        .BuscarPlanos()
        .EditarPlanoFiltrado()
        .EsperarCarregarDadosDoPlano()
        .EditarSituacaoDoPlano()
        .SalvarPlano()
        .FecharDadosDoPlano()
        .RecarregarPlanos()
        .ValidarStatusFarolDoPlano();

        stopwatchTest.Stop();
        TestContext.WriteLine($"Tempo gasto no teste em minutos: {stopwatchTest.Elapsed}");
    }

    /// <summary>
    /// Testar exclusão de plano
    /// 
    /// Como comercial
    /// Eu quero excluir um plano
    /// Para que o mesmo seja removido da minha lista de negociações
    /// </summary>
    [Test, Order(11)]
    public void TestExcluirPlano()
    {
        var stopwatchTest = Stopwatch.StartNew();

        new PlanosContratosPage(webDriver, clienteUpSellAtual)
        .ConfirmarExclusaoDoPlano();

        stopwatchTest.Stop();
        TestContext.WriteLine($"Tempo gasto no teste em minutos: {stopwatchTest.Elapsed}");
    }

    /// <summary>
    /// Método que será executado ao final de cada teste
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        var statusTeste = TestContext.CurrentContext.Result.Outcome.Status;

        if (primeiroTeste)
        {
            if (statusTeste == TestStatus.Failed || statusTeste == TestStatus.Skipped)
            {
                testeAnteriorPulouFalhou = true;
            }

            primeiroTeste = false;

            Dsl.ValidarModaisAbertas(webDriver, clienteUpSellAtual);

            Dsl.Esperar();
            new HomePage(webDriver, clienteUpSellAtual).RealizarLogout();
        }
        else if (statusTeste == TestStatus.Passed || statusTeste == TestStatus.Failed)
        {
            if (statusTeste == TestStatus.Failed)
            {
                Dsl.ValidarModaisAbertas(webDriver, clienteUpSellAtual);
            }

            Dsl.Esperar();
            new HomePage(webDriver, clienteUpSellAtual).RealizarLogout();
        }
    }

    /// <summary>
    /// Método que será executado uma única vez, depois de todos os teste
    /// </summary>
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        webDriver.Quit();
        webDriver.Dispose();
    }
}