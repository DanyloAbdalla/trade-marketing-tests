using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace MeuClienteWebTestProject;

/// <summary>
/// Classe com os testes para o DashBoard de Operações
/// </summary>
[TestFixture]
public class DashboardOperacoesTest
{
    private RunSettings runSettings;
    private IWebDriver webDriver;
    private readonly BrowserType browserType = BrowserType.Chrome;
    private readonly string nomeClasse;
    private readonly string nomeAtivo = "Adesivo de Elevador";
    private readonly string nomeAtivoEsperado = "AdesivodeElevador";
    private readonly string nomeCampanha = "DashboardOperacoesMassaAutomatizada";

    public DashboardOperacoesTest()
    {
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

        var nomeTeste = TestContext.CurrentContext.Test.MethodName;

        if (runSettings.ToSkip(nomeClasse, null, nomeTeste))
            Assert.Ignore("Teste ignorado pelas configurações de execução");

        new LoginPage(webDriver)
        .RealizarLogin(GlobalVariables.emailUsuarioSemPlanta, GlobalVariables.senhaUsuarioSemPlanta);

        //Retorna para o Dashboard de Operações, se no último logout a plataforma parou em outra tela
        new HomePage(webDriver)
        .VoltarParaDashboardOperacoes();

        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.TextoCardAtivosAlocados);
        Dsl.Esperar();
    }

    /// <summary>
    /// Testar acesso aos detalhes das lojas ativas no Dashboard de Operações
    /// 
    /// Como gerente de loja
    /// Eu quero acessar os detalhes do card de Lojas Ativas no Dashboard de Operações
    /// Para visualizar o aproveitamento para cada loja cadastrada na plataforma
    /// 
    /// Dado que eu tenho acesso ao Dashboard de Operações
    /// Quando acessar o Dashboard
    /// E clicar no botão "Visualizar Lojas Ativas" no card de Lojas Ativas 
    /// Então a tela será apresentada, mostrando o aproveitamento de alocação cada de loja
    /// </summary>
    [Test, Order(1)]
    public void TestAcessarVisaoDetalhadaLojasAtivas()
    {
        new DashboardOperacoesPage(webDriver)
        .AcessarDetalhesLojasAtivas()
        .FecharDetalhes();
    }

    /// <summary>
    /// Testar acesso aos detalhes de alocação dos ativos no Dashboard de Operações
    /// 
    /// Como gerente de loja
    /// Eu quero acessar os detalhes do card de Ativos Alocados no Dashboard de Operações
    /// Para visualizar a disponibiliade, negociação e potêncial receita de cada ativo na plataforma
    /// 
    /// Dado que eu tenho acesso ao Dashboard de Operações
    /// Quando acessar o Dashboard
    /// E clicar no botão "Visualizar Disponibilidade de Ativos" 
    /// E clicar no botão "Visualizar Ativos Negociados" 
    /// E clicar no botão "Visualizar Listagem Potencial Receita" no card de Ativos Alocados
    /// Então a tela será apresentada, mostrando a disponibilidade versos a alocação
    /// E as negociações dos planos vigentes
    /// E o potencial da receita de alocação de cada ativo
    /// </summary>
    [Test, Order(2)]
    public void TestAcessarVisaoDetalhadaDeAtivosAlocados()
    {
        new DashboardOperacoesPage(webDriver)
        .AcessarDetalhesDaDiponibilidade(nomeAtivo, nomeAtivoEsperado)
        .FecharDetalhes()
        .AcessarDetalhesDasNegociacoes(nomeAtivo, nomeAtivoEsperado)
        .FecharDetalhes()
        .AcessarDetalhesDoPotencialDeReceita(nomeAtivo, nomeAtivoEsperado)
        .FecharDetalhes();
    }

    /// <summary>
    /// Testar acesso aos detalhes de contratos vigentes no Dashboard de Operações
    /// 
    /// Como gerente de loja
    /// Eu quero acessar os detalhes do card de Contratos Vigentes no Dashboard de Operações
    /// Para visualizar os contratos ativos 
    /// E contratos vencendo na plataforma
    /// 
    /// Dado que eu tenho acesso ao Dashboard de Operações
    /// Quando acessar o Dashboard
    /// E clicar no botão "Visualizar Contratos Ativos" 
    /// E clicar no botão "Visualizar Contratos Vencendo" no card Contratos Vigentes 
    /// Então a tela será apresentada, mostrando os contratos ativos
    /// E os contratos vencendo
    /// </summary>
    [Test, Order(3)]
    public void TestAcessarVisaoDetalhadaDeContratosVigentes()
    {
        var card = "ContratosVigentes";

        new DashboardOperacoesPage(webDriver)
        .AcessarDetalhesDeContratosAtivos(card, nomeCampanha)
        .FecharDetalhes()
        .AcessarDetalhesDeContratosVencendo(nomeCampanha)
        .FecharDetalhes();
    }

    /// <summary>
    /// Testar acesso aos detalhes das receitas dos contratos no Dashboard de Operações
    /// 
    /// Como gerente de loja
    /// Eu quero acessar os detalhes do card Total de Receita no Dashboard de Operações
    /// Para visualizar a receita para cada contrato cadastrado na plataforma
    /// 
    /// Dado que eu tenho acesso ao Dashboard de Operações
    /// Quando acessar o Dashboard
    /// E clicar no botão "Visualizar Contratos" no card Total de Receita
    /// Então a tela será apresentada, mostrando o total de receita de cada contrato
    /// </summary>
    [Test, Order(4)]
    public void TestAcessarVisaoDetalhadaTotalReceita()
    {
        var card = "TotalReceita";

        new DashboardOperacoesPage(webDriver)
        .AcessarDetalhesDeContratosAtivos(card, nomeCampanha)
        .FecharDetalhes();
    }

    /// <summary>
    /// Testar acesso aos detalhes da evolução da receita no Dashboard de Operações
    /// 
    /// Como gerente de loja
    /// Eu quero acessar os detalhes do card Evolução Performance Receita
    /// E acessar os detalhes do card Evolução Performance Receita Bandeira
    /// E acessar os detalhes do card Evolução Performance Receita Tipo Fornecedor no Dashboard de Operações
    /// Para visualizar a evolução da receita para cada mês do ano
    /// 
    /// Dado que eu tenho acesso ao Dashboard de Operações
    /// Quando acessar o Dashboard
    /// E clicar no botão "Visualizar Listagem de Aterrisagem Receita" no card Evolução Performance Receita
    /// E clicar no botão "Visualizar Listagem de Aterrisagem Receita" no card Evolução Performance Receita Bandeira
    /// E clicar no botão "Visualizar Listagem de Aterrisagem Receita" no card Evolução Performance Receita Tipo Fornecedor
    /// Então a tela será apresentada, mostrando a evolução da receita de cada mês do ano
    /// </summary>
    [Test, Order(5)]
    public void TestAcessarVisaoDetalhadaAterrissagemReceita()
    {
        var cardReceita = "EvolucaoReceita";
        var cardReceitaBandeira = "EvolucaoReceitaBandeira";
        var cardReceitaTipoFornecedor = "EvolucaoReceitaTipoFornecedor";

        new DashboardOperacoesPage(webDriver)
        .AcessarDetalhesDeAterrissagemReceita(cardReceita)
        .FecharDetalhes()
        .AcessarDetalhesDeAterrissagemReceita(cardReceitaBandeira)
        .FecharDetalhes()
        .AcessarDetalhesDeAterrissagemReceita(cardReceitaTipoFornecedor)
        .FecharDetalhes();
    }

    /// <summary>
    /// Testar acesso aos detalhes da evolução do parceiro no Dashboard de Operações
    /// 
    /// Como gerente de loja
    /// Eu quero acessar os detalhes do card Evolução Performance Parceiro
    /// Para visualizar a evolução do parceiro em negociações realizadas, juntamento com seus investimentos
    /// 
    /// Dado que eu tenho acesso ao Dashboard de Operações
    /// Quando acessar o Dashboard
    /// E clicar no botão "Visualizar Lista Parceiro" no card Evolução Performance Parceiros
    /// E clicar no botão "Visualizar Lista Parceiro" no card Investimento por Parceiro
    /// Então a tela será apresentada, mostrando a evolução do parceiro em negociações realizadas
    /// </summary>
    [Test, Order(6)]
    public void TestAcessarVisaoDetalhadaListaParceiro()
    {
        var cardPerformanceParceiro = "EvolucaoPerformanceParceiros";
        var cardInvestimentoParceiro = "InvestimentoParceiro";

        new DashboardOperacoesPage(webDriver)
        .AcessarDetalhesDeListaParceiros(cardPerformanceParceiro)
        .FecharDetalhes()
        .AcessarDetalhesDeListaParceiros(cardInvestimentoParceiro)
        .FecharDetalhes();
    }

    /// <summary>
    /// Testar acesso aos detalhes do desempenho da loja no Dashboard de Operações
    /// 
    /// Como gerente de loja
    /// Eu quero acessar os detalhes do card Desempenho por Loja
    /// Para visualizar a evolução da loja em negociações realizadas
    /// 
    /// Dado que eu tenho acesso ao Dashboard de Operações
    /// Quando acessar o Dashboard
    /// E clicar no botão "Visualizar Detalhe Desempenho por Loja" no card Desempenho por Loja
    /// Então a tela será apresentada, mostrando o desempenho do por loja em negociações realizadas
    /// </summary>
    [Test, Order(7)]
    public void TestAcessarVisaoDetalhadaDesempenhoLoja()
    {
        new DashboardOperacoesPage(webDriver)
        .AcessarDetalhesDesempenhoPorLoja()
        .FecharDetalhes();
    }

    /// <summary>
    /// Testar acesso aos detalhes do desempenho do ativo no Dashboard de Operações
    /// 
    /// Como gerente de loja
    /// Eu quero acessar os detalhes do card Desempenho por Ativos
    /// Para visualizar a evolução do ativo em negociações realizadas
    /// 
    /// Dado que eu tenho acesso ao Dashboard de Operações
    /// Quando acessar o Dashboard
    /// E clicar no botão "Visualizar Detalhe Desempenho dos Ativos" no card Desempenho de Ativos
    /// Então a tela será apresentada, mostrando o desempenho do ativo em negociações realizadas
    /// </summary>
    [Test, Order(8)]
    public void TestAcessarVisaoDetalhadaDesempenhoAtivo()
    {
        new DashboardOperacoesPage(webDriver)
        .AcessarDetalhesDesempenhoDeAtivo()
        .FecharDetalhes();
    }

    /// <summary>
    /// Método que será executado ao final de cada teste
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed && Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.ModalDashboardDetalhesCards) > 0) //Identifica se o teste falhou e se a modal de detalhes do card está aberta, caso sim, a mesma é fechada para não comprometer a execução do próximo teste
            Dsl.Clicar(webDriver, GlobalVariables.FecharTela, "Botão Fechar Detalhes Card");
    }

    /// <summary>
    /// Método que será executado uma única vez, depois de todos os teste
    /// </summary>
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Skipped)
        {
            webDriver.Quit();
            webDriver.Dispose();
        }
        else
        {
            new HomePage(webDriver).RealizarLogout();
            webDriver.Quit();
            webDriver.Dispose();
        }
    }
}