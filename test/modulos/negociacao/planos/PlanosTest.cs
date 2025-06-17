using System.Runtime.CompilerServices;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using OpenQA.Selenium;

namespace MeuClienteWebTestProject;

/// <summary>
/// Classe com os testes para o Cadastro de Planos\Contratos
/// </summary>
[TestFixture("SemPlantaLoja", Category = "PlanosSemPlantaDeLoja")]
[TestFixture("ComPlantaLoja", Category = "PlanosComPlantaDeLoja")]
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
    private readonly string nomeCampanha = "PlanoComWorkflowPadraoMassaAutomatizada";
    private readonly string statusPlanoEsperado = "Simulado";
    private readonly string farolPlanoEsperado = "PLANEJADO";

    public PlanosTest(string contextoDeTeste)
    {
        this.contextoDeTeste = contextoDeTeste;
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

        if (contextoDeTeste.Contains("SemPlantaLoja"))
        {
            new LoginPage(webDriver)
            .RealizarLogin(GlobalVariables.emailUsuarioSemPlanta, GlobalVariables.senhaUsuarioSemPlanta);

            new HomePage(webDriver)
            .AcessarCadastroPlanos(nomeTeste);
        }
        else if (contextoDeTeste.Contains("ComPlantaLoja"))
        {
            new LoginPage(webDriver)
            .RealizarLogin(GlobalVariables.emailUsuarioComPlanta, GlobalVariables.senhaUsuarioComPlanta);

            new HomePage(webDriver)
            .AcessarCadastroPlanos(nomeTeste);
        }
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
    public void TestCriarPlanoComWorkflowPadrao()
    {
        var ativoTipoMidia = "Grafica";
        var contextoDeExecucao = "CriarPlanoComWorkflowPadrao";
        primeiroTeste = true;

        new PlanosContratosPage(webDriver)
        .NovaSimulacaoDePlano()
        .PreencherCampoIndustria(contextoDeTeste)
        .PreencherCampoCampanha(nomeCampanha)
        .SelecionarAtivos(ativoTipoMidia)
        .PreencherQuantidadeAtivos(contextoDeTeste, ativoTipoMidia)
        .SelecionarLojas()
        .GerarPrePlano(contextoDeTeste, ativoTipoMidia)
        .SalvarPlano()
        .FecharDadosDoPlano()
        .BuscarPlanos(nomeCampanha)
        .AbrirEdicaoDoPlano()
        .ValidarReceitasDoPlano(contextoDeTeste, contextoDeExecucao)
        .ValidarPlanoCriado()
        .FecharDadosDoPlano()
        .BuscarPlanos(nomeCampanha)
        .ValidarStatusFarolDoPlano(statusPlanoEsperado, farolPlanoEsperado);
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
    public void TestCriarPlanoComWorkflow()
    {
        var ativoTipoMidia = "Fisica";
        var contextoDeExecucao = "CriarPlanoComWorkflow";
        var nomeCampanha = "PlanoComWorkflowMassaAutomatizada";

        new PlanosContratosPage(webDriver)
        .NovaSimulacaoDePlano()
        .PreencherCampoIndustria(contextoDeTeste)
        .PreencherCampoCampanha(nomeCampanha)
        .SelecionarAtivos(ativoTipoMidia)
        .PreencherQuantidadeAtivos(contextoDeTeste, ativoTipoMidia)
        .SelecionarLojas()
        .GerarPrePlano(contextoDeTeste, ativoTipoMidia)
        .SalvarPlano()
        .FecharDadosDoPlano()
        .BuscarPlanos(nomeCampanha)
        .AbrirEdicaoDoPlano()
        .ValidarReceitasDoPlano(contextoDeTeste, contextoDeExecucao)
        .ValidarPlanoCriado()
        .FecharDadosDoPlano()
        .BuscarPlanos(nomeCampanha)
        .ValidarStatusFarolDoPlano(statusPlanoEsperado, farolPlanoEsperado);
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
        var contextoDeExecucao = "EditarPlano";

        new PlanosContratosPage(webDriver)
        .BuscarPlanos(nomeCampanha)
        .AbrirEdicaoDoPlano()
        .SelecionarVigenciaDoPlano(contextoDeExecucao)
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
        //var contextoDeExecucao = "EditarPlano";

        new PlanosContratosPage(webDriver)
        .BuscarPlanos(nomeCampanha)
        .AbrirEdicaoDoPlano()
        .AbrirAbaAtivosAlocados()
        .EditarVigenciaDoAtivoAlocado(contextoDeTeste)
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
        var contextoDeExecucao = "EditarPlanoAlterandoQuantidadeAtivo";

        new PlanosContratosPage(webDriver)
        .BuscarPlanos(nomeCampanha)
        .AbrirEdicaoDoPlano()
        .AbrirAbaAtivosAlocados()
        .EditarQuantidadesDosAtivosNoPlano(contextoDeTeste)
        .SalvarPlano()
        .FecharDadosDoPlano()
        .AbrirEdicaoDoPlano()
        .ValidarReceitasDoPlano(contextoDeTeste, contextoDeExecucao)
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
        var contextoDeExecucao = "EditarPlanoIncluindoAtivo";

        new PlanosContratosPage(webDriver)
        .BuscarPlanos(nomeCampanha)
        .AbrirEdicaoDoPlano()
        .AbrirAbaAtivosAlocados()
        .AlocarNovosAtivosNoPlano(contextoDeTeste)
        .SalvarPlano()
        .FecharDadosDoPlano()
        .AbrirEdicaoDoPlano()
        .ValidarReceitasDoPlano(contextoDeTeste, contextoDeExecucao)
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
        var situacaoPlano = "Contrato Aprovado";
        var statusPlanoEsperado = "Aprovado";
        var farolPlanoEsperado = "APROVADO";

        new PlanosContratosPage(webDriver)
        .BuscarPlanos(nomeCampanha)
        .AbrirEdicaoDoPlano()
        .EditarSituacaoDoPlano(situacaoPlano)
        .SalvarPlano()
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
    [Test, Order(8)]
    public void TestCriarPlanoComAlertaDeInventario()
    {
        var ativoTipoMidia = "Grafica";
        var contextoDeExecucao = "CriarPlano";

        new PlanosContratosPage(webDriver)
        .NovaSimulacaoDePlano()
        .PreencherCampoIndustria(contextoDeTeste)
        .PreencherCampoCampanha(nomeCampanha)
        .SelecionarVigenciaDoPlano(contextoDeExecucao)
        .SelecionarAtivos(ativoTipoMidia)
        .PreencherQuantidadeAtivos(contextoDeTeste, ativoTipoMidia)
        .SelecionarLojas()
        .ValidarIndisponibilidadeDeInventario()
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
    [Test, Order(9)]
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
    [Test, Order(10)]
    public void TestExcluirPlano()
    {
        var nomeCampanharComWorkflowPadrao = "PlanoComWorkflowPadraoMassaAutomatizada";
        var nomeCampanhaComWorkflow = "PlanoComWorkflowMassaAutomatizada";

        new PlanosContratosPage(webDriver)
        .BuscarPlanos(nomeCampanharComWorkflowPadrao)
        .ConfirmarExclusaoDoPlano()
        .BuscarPlanos(nomeCampanhaComWorkflow)
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

            new HomePage(webDriver).AcessarDashboardOperacoes();
            new HomePage(webDriver).RealizarLogout();
        }
        else if (statusTeste == TestStatus.Passed || statusTeste == TestStatus.Failed)
        {
            if (statusTeste == TestStatus.Failed)
            {
                if (Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.AbaAlocacaoPorLojaAtivo) > 0)
                {
                    //Se a modal da alocação por loja do ativo está aberta, a mesma é fechada para não comprometer a execução do próximo teste
                    new PlanosContratosPage(webDriver).FecharAlocacaoPorLoja();

                    if (Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.AbaPlano) > 0)
                    {
                        //Se a modal do plano está aberta, a mesma é fechada para não comprometer a execução do próximo teste
                        new PlanosContratosPage(webDriver).FecharDadosDoPlano();
                    }
                }
            }

            Dsl.Esperar();
            new HomePage(webDriver).AcessarDashboardOperacoes();
            new HomePage(webDriver).RealizarLogout();
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