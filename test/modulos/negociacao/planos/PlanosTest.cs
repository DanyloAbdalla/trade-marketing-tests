using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;

namespace MeuClienteWebTestProject;

/// <summary>
/// Classe com os testes para o Cadastro de Planos\Contratos
/// </summary>
//[TestFixture("SemPlantaLoja", Category = "PlanosSemPlantaDeLoja")]
//[TestFixture("ComPlantaLoja", Category = "PlanosComPlantaDeLoja")]
[TestFixture("ClienteStart")]
[TestFixture("ClientePro")]
[TestFixture("ClienteExpert")]
//[Parallelizable(ParallelScope.Fixtures)]
public class PlanosTest
{
    private RunSettings runSettings;
    private IWebDriver webDriver;
    private readonly BrowserType browserType = BrowserType.Chrome;
    private bool testeAnteriorPulouFalhou = false;
    private bool primeiroTeste;
    private readonly string nomeClasse;
    private readonly string contextoDeTeste;
    private ClienteUpSell clienteUpSellAtual => Enum.TryParse<ClienteUpSell>(contextoDeTeste, out var cliente) ? cliente : ClienteUpSell.ClienteStart;
    private readonly string nomeCampanha;
    private readonly string statusEsperado;
    private readonly string farolEsperado;
    private string tipoMidiaAtivo;

    public PlanosTest(string contextoDeTeste)
    {
        this.contextoDeTeste = contextoDeTeste;
        DataLoader.CarregarArquivo();
        nomeClasse = TestContext.CurrentContext.Test.ClassName.Split('.').Last();
        nomeCampanha = DataLoader.ObterDados("negociacoes_planos", "TestGlobalData", "nomeCampanha");
        statusEsperado = DataLoader.ObterDados("negociacoes_planos", "TestGlobalData", "statusEsperado");
        farolEsperado = DataLoader.ObterDados("negociacoes_planos", "TestGlobalData", "farolEsperado");
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

        if (nomeTeste.Equals("TestCriarPlanoComAtivosTipoMidiaFisica"))
            tipoMidiaAtivo = DataLoader.ObterDados("negociacoes_planos", "TestCriarPlanoComAtivosTipoMidiaFisica", "tipoMidiaAtivo");
        else
            tipoMidiaAtivo = DataLoader.ObterDados("negociacoes_planos", "TestGlobalData", "tipoMidiaAtivo");

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
    /// Testar a criação de um plano com workflow padrão (sem vínculo com tipo mídia)
    /// 
    /// Como comercial de trade marketing
    /// Eu quero criar um plano
    /// E inicar uma negociação
    /// Para enviar a proposta para o cliente
    /// 
    /// Dado que eu tenho uma nova negociação
    /// E que eu tenho disponibilidade de inventário, em um determinado período de vigência
    /// Quando eu simular um novo plano, preenchendo indústria E vigência
    /// E escolher os ativos, alocar as quantidades, selecionar as lojas
    /// Então será apresentado o botão de sucesso para as lojas com disponibilidade, com o botão Gerar Pré-Plano habilitado
    /// Quando eu clicar no botão “Gerar Pré-Plano”
    /// Então o plano\contrato será criado, com Status = Simulado e Farol = Planejado
    /// E Workflow padrão
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
            .PreencherCampoCampanha(nomeCampanha)
            .SelecionarAtivos(tipoMidiaAtivo)
            .SelecionarLojas()
            .PreencherQuantidadeAtivos(tipoMidiaAtivo)
            .GerarPrePlano()
            .SalvarPlano()
            .FecharDadosDoPlano()
            .BuscarPlanos(nomeCampanha)
            .AbrirEdicaoDoPlano()
            .ValidarReceitasDoPlano()
            .ValidarPlanoCriado()
            .FecharDadosDoPlano()
            .ValidarStatusFarolDoPlano(statusEsperado, farolEsperado);
        }
        else
        {
            new PlanosContratosPage(webDriver, clienteUpSellAtual)
            .NovaSimulacaoDePlano()
            .PreencherCampoIndustria()
            .PreencherCampoCampanha(nomeCampanha)
            .SelecionarAtivos(tipoMidiaAtivo)
            .PreencherQuantidadeAtivos(tipoMidiaAtivo)
            .SelecionarLojas()
            .GerarPrePlano()
            .SalvarPlano()
            .FecharDadosDoPlano()
            .BuscarPlanos(nomeCampanha)
            .AbrirEdicaoDoPlano()
            .ValidarReceitasDoPlano()
            .ValidarPlanoCriado()
            .FecharDadosDoPlano()
            .ValidarStatusFarolDoPlano(statusEsperado, farolEsperado);
        }
    }

    /// <summary>
    /// Testar a criação de um plano com workflow
    /// 
    /// Como comercial de trade marketing
    /// Eu quero criar um plano
    /// E inicar uma negociação
    /// Para enviar a proposta para o cliente
    /// 
    /// Dado que eu tenho uma nova negociação
    /// E que eu tenho disponibilidade de inventário, em um determinado período de vigência
    /// Quando eu simular um novo plano, preenchendo indústria E vigência
    /// E escolher os ativos, alocar as quantidades, selecionar as lojas
    /// Então será apresentado o botão de sucesso para as lojas com disponibilidade, com o botão Gerar Pré-Plano habilitado
    /// Quando eu clicar no botão “Gerar Pré-Plano”
    /// Então o plano\contrato será criado, com Status = Simulado e Farol = Planejado
    /// E com as etapas do Workflow
    /// </summary>
    [Test, Order(2)]
    public void TestCriarPlanoComAtivosTipoMidiaFisica()
    {
        string nomeCampanha = DataLoader.ObterDados("negociacoes_planos", "TestCriarPlanoComAtivosTipoMidiaFisica", "nomeCampanha");

        if (clienteUpSellAtual == ClienteUpSell.ClienteExpert)
        {
            new PlanosContratosPage(webDriver, clienteUpSellAtual)
            .NovaSimulacaoDePlano()
            .PreencherCampoIndustria()
            .PreencherCampoCampanha(nomeCampanha)
            .SelecionarAtivos(tipoMidiaAtivo)
            .SelecionarLojas()
            .PreencherQuantidadeAtivos(tipoMidiaAtivo)
            .GerarPrePlano()
            .SalvarPlano()
            .FecharDadosDoPlano()
            .RecarregarPlanos()
            .BuscarPlanos(nomeCampanha)
            .AbrirEdicaoDoPlano()
            .ValidarReceitasDoPlano()
            .ValidarPlanoCriado()
            .FecharDadosDoPlano()
            .ValidarStatusFarolDoPlano(statusEsperado, farolEsperado);
        }
        else
        {
            new PlanosContratosPage(webDriver, clienteUpSellAtual)
            .NovaSimulacaoDePlano()
            .PreencherCampoIndustria()
            .PreencherCampoCampanha(nomeCampanha)
            .SelecionarAtivos(tipoMidiaAtivo)
            .PreencherQuantidadeAtivos(tipoMidiaAtivo)
            .SelecionarLojas()
            .GerarPrePlano()
            .SalvarPlano()
            .FecharDadosDoPlano()
            .RecarregarPlanos()
            .BuscarPlanos(nomeCampanha)
            .AbrirEdicaoDoPlano()
            .ValidarReceitasDoPlano()
            .ValidarPlanoCriado()
            .FecharDadosDoPlano()
            .ValidarStatusFarolDoPlano(statusEsperado, farolEsperado);
        }

    }

    /// <summary>
    /// Testar a edição da vigência em um plano existente
    /// 
    /// Como comercial de trade marketing
    /// Eu quero alterar a vigência do plano
    /// Para negociar um novo período
    /// 
    /// Dado que eu tenho um plano criado na Negociação
    /// Quando acessar a tela de edição
    /// E alterar as datas início e fim da vigência
    /// E clicar no botão Salvar
    /// Então o plano será salvo com a nova vigência
    /// </summary>
    [Test, Order(3)]
    public void TestEditarPlanoExistenteAlterandoVigenciaDoPlano()
    {
        new PlanosContratosPage(webDriver, clienteUpSellAtual)
        .BuscarPlanos(nomeCampanha)
        .AbrirEdicaoDoPlano()
        .SelecionarVigenciaDoPlano()
        .SalvarPlano()
        .FecharDadosDoPlano();
    }

    /// <summary>
    /// Testar a edição da vigência em um plano existente
    /// 
    /// Como comercial de trade marketing
    /// Eu quero alterar a vigência do trade
    /// Para negociar um novo período
    /// 
    /// Dado que eu tenho um plano criado na Negociação
    /// Quando acessar a aba "Ativos Alocados" na edição do plano
    /// E editar um ativo alocado
    /// E alterar as datas início e fim da vigência do trade
    /// E clicar no botão Salvar
    /// Então o plano será salvo com a nova vigência
    /// </summary>
    [Test, Order(4)]
    public void TestEditarPlanoExistenteAlterandoVigenciaDoTrade()
    {
        new PlanosContratosPage(webDriver, clienteUpSellAtual)
        .BuscarPlanos(nomeCampanha)
        .AbrirEdicaoDoPlano()
        .AbrirAbaAtivosAlocados()
        .EditarVigenciaDoAtivoAlocado()
        .SalvarPlano()
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
    [Test, Order(5)]
    public void TestEditarPlanoExistenteAlterandoQuantidadeAlocadaDoAtivoDisponivel()
    {
        new PlanosContratosPage(webDriver, clienteUpSellAtual)
        .BuscarPlanos(nomeCampanha)
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
    [Test, Order(6)]
    public void TestEditarPlanoExistenteIncluindoNovoAtivoDisponivel()
    {
        new PlanosContratosPage(webDriver, clienteUpSellAtual)
        .BuscarPlanos(nomeCampanha)
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
    [Test, Order(7)]
    public void TestAprovarPlano()
    {
        string statusEsperado = DataLoader.ObterDados("negociacoes_planos", "TestAprovarPlano", "statusEsperado");
        string farolEsperado = DataLoader.ObterDados("negociacoes_planos", "TestAprovarPlano", "farolEsperado");

        new PlanosContratosPage(webDriver, clienteUpSellAtual)
        .BuscarPlanos(nomeCampanha)
        .AbrirEdicaoDoPlano()
        .EditarSituacaoDoPlano()
        .SalvarPlano()
        .FecharDadosDoPlano()
        .RecarregarPlanos()
        .ValidarStatusFarolDoPlano(statusEsperado, farolEsperado);
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
    [Test, Order(8)]
    public void TestCriarPlanoComAlertaDeInventario()
    {
        if (clienteUpSellAtual == ClienteUpSell.ClienteExpert)
        {
            new PlanosContratosPage(webDriver, clienteUpSellAtual)
            .NovaSimulacaoDePlano()
            .PreencherCampoIndustria()
            .PreencherCampoCampanha(nomeCampanha)
            .SelecionarVigenciaDoPlano()
            .SelecionarAtivos(tipoMidiaAtivo)
            .PreencherQuantidadeAtivos(tipoMidiaAtivo)
            .SelecionarLojas()
            .ValidarIndisponibilidadeDeInventario()
            .FecharDadosDoPlano();
        }
        else
        {
            new PlanosContratosPage(webDriver, clienteUpSellAtual)
            .NovaSimulacaoDePlano()
            .PreencherCampoIndustria()
            .PreencherCampoCampanha(nomeCampanha)
            .SelecionarVigenciaDoPlano()
            .SelecionarAtivos(tipoMidiaAtivo)
            .PreencherQuantidadeAtivos(tipoMidiaAtivo)
            .SelecionarLojas()
            .ValidarIndisponibilidadeDeInventario()
            .FecharDadosDoPlano();
        }
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
    [Test, Order(9)]
    public void TestCancelarPlano()
    {
        string statusEsperado = DataLoader.ObterDados("negociacoes_planos", "TestCancelarPlano", "statusEsperado");
        string farolEsperado = DataLoader.ObterDados("negociacoes_planos", "TestCancelarPlano", "farolEsperado");

        new PlanosContratosPage(webDriver, clienteUpSellAtual)
        .BuscarPlanos(nomeCampanha)
        .AbrirEdicaoDoPlano()
        .EditarSituacaoDoPlano()
        .SalvarPlano()
        .FecharDadosDoPlano()
        .RecarregarPlanos()
        .ValidarStatusFarolDoPlano(statusEsperado, farolEsperado);
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
    [Test, Order(10)]
    public void TestExcluirPlano()
    {
        List<string> nomeCampanhas = DataLoader.ObterDadosEmLista("negociacoes_planos", "TestExcluirPlano", "nomeCampanhas");

        foreach (var nomeCampanha in nomeCampanhas)
        {
            new PlanosContratosPage(webDriver, clienteUpSellAtual)
            .BuscarPlanos(nomeCampanha)
            .ConfirmarExclusaoDoPlano(nomeCampanha);
        }
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