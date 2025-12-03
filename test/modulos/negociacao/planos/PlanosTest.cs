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
//[Parallelizable(ParallelScope.Fixtures)]
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

        new HomePage(webDriver, clienteUpSellAtual)
            .AcessarCadastroPlanos(nomeTeste);
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
            .SalvarPlano()
            .FecharDadosDoPlano()
            .BuscarPlanos()
            .AbrirEdicaoDoPlano()
            .ValidarReceitasDoPlano()
            .ValidarPlanoCriado()
            .FecharDadosDoPlano()
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
            .SalvarPlano()
            .ValidarReceitasDoPlano()
            .FecharDadosDoPlano()
            .BuscarPlanos()
            .AbrirEdicaoDoPlano()
            .ValidarReceitasDoPlano()
            .ValidarPlanoCriado()
            .FecharDadosDoPlano()
            .ValidarStatusFarolDoPlano();
        }
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
        if (clienteUpSellAtual == ClienteUpSell.ClienteExpert)
        {
            new PlanosContratosPage(webDriver, clienteUpSellAtual)
            .NovaSimulacaoDePlano()
            .PreencherCampoIndustria()
            .PreencherCampoCampanha()
            .SelecionarAtivosELojas()
            .PreencherQuantidadeAtivos()
            .GerarPrePlano()
            .SalvarPlano()
            .FecharDadosDoPlano()
            .RecarregarPlanos()
            .BuscarPlanos()
            .AbrirEdicaoDoPlano()
            .ValidarReceitasDoPlano()
            .ValidarPlanoCriado()
            .FecharDadosDoPlano()
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
            .SalvarPlano()
            .FecharDadosDoPlano()
            .RecarregarPlanos()
            .BuscarPlanos()
            .AbrirEdicaoDoPlano()
            .ValidarReceitasDoPlano()
            .ValidarPlanoCriado()
            .FecharDadosDoPlano()
            .ValidarStatusFarolDoPlano();
        }

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
        new PlanosContratosPage(webDriver, clienteUpSellAtual)
        .BuscarPlanos()
        .AbrirEdicaoDoPlano()
        .SelecionarVigenciaDoPlano()
        .SalvarPlano()
        .FecharDadosDoPlano();
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
        new PlanosContratosPage(webDriver, clienteUpSellAtual)
        .BuscarPlanos()
        .AbrirEdicaoDoPlano()
        .AbrirAbaAtivosAlocados()
        .EditarVigenciaDoAtivoAlocado()
        .SalvarPlano()
        .FecharDadosDoPlano();
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
        new PlanosContratosPage(webDriver, clienteUpSellAtual)
        .BuscarPlanos()
        .AbrirEdicaoDoPlano()
        .AbrirAbaAtivosAlocados()
        .EditarQuantidadesDosAtivosNoPlano()
        .SalvarPlano()
        .FecharDadosDoPlano()
        .AbrirEdicaoDoPlano()
        .ValidarReceitasDoPlano()
        .FecharDadosDoPlano();
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
        new PlanosContratosPage(webDriver, clienteUpSellAtual)
        .BuscarPlanos()
        .AbrirEdicaoDoPlano()
        .AbrirAbaAtivosAlocados()
        .AlocarNovosAtivosNoPlano()
        .SalvarPlano()
        .FecharDadosDoPlano()
        .AbrirEdicaoDoPlano()
        .ValidarReceitasDoPlano()
        .FecharDadosDoPlano();
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
        new PlanosContratosPage(webDriver, clienteUpSellAtual)
        .BuscarPlanos()
        .AbrirEdicaoDoPlano()
        .EditarSituacaoDoPlano()
        .SalvarPlano()
        .FecharDadosDoPlano()
        .RecarregarPlanos()
        .ValidarStatusFarolDoPlano();
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
            .SelecionarVigenciaDoPlano()
            .SelecionarAtivos()
            .SelecionarLojas()
            .ValidarIndisponibilidadeDeInventario()
            .FecharDadosDoPlano();
        }
    }

    /// <summary>
    /// Testar cancelamento de plano
    /// 
    /// Como comercial
    /// Eu quero cancelar um plano
    /// Para que o mesmo seja desconsiderado das minhas negociações
    /// </summary>
    [Test, Order(9)]
    public void TestCancelarPlano()
    {
        new PlanosContratosPage(webDriver, clienteUpSellAtual)
        .BuscarPlanos()
        .AbrirEdicaoDoPlano()
        .EditarSituacaoDoPlano()
        .SalvarPlano()
        .FecharDadosDoPlano()
        .RecarregarPlanos()
        .ValidarStatusFarolDoPlano();
    }

    /// <summary>
    /// Testar exclusão de plano
    /// 
    /// Como comercial
    /// Eu quero excluir um plano
    /// Para que o mesmo seja removido da minha lista de negociações
    /// </summary>
    [Test, Order(10)]
    public void TestExcluirPlano()
    {
        new PlanosContratosPage(webDriver, clienteUpSellAtual)
        .ConfirmarExclusaoDoPlano();
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

            new HomePage(webDriver, clienteUpSellAtual).AcessarDashboardOperacoes();
            new HomePage(webDriver, clienteUpSellAtual).RealizarLogout();
        }
        else if (statusTeste == TestStatus.Passed || statusTeste == TestStatus.Failed)
        {
            if (statusTeste == TestStatus.Failed)
            {
                if (Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.AbaAlocacaoPorLojaAtivo) > 0)
                {
                    //Se a modal da alocação por loja do ativo está aberta, a mesma é fechada para não comprometer a execução do próximo teste
                    new PlanosContratosPage(webDriver, clienteUpSellAtual).FecharAlocacaoPorLoja();

                    if (Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.AbaPlano) > 0)
                    {
                        //Se a modal do plano está aberta, a mesma é fechada para não comprometer a execução do próximo teste
                        new PlanosContratosPage(webDriver, clienteUpSellAtual).FecharDadosDoPlano();
                    }
                }
            }

            Dsl.Esperar();
            new HomePage(webDriver, clienteUpSellAtual).AcessarDashboardOperacoes();
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