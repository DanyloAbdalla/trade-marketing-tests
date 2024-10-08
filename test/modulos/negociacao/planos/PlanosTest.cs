using System.Runtime.CompilerServices;
using OpenQA.Selenium;

namespace MeuClienteWebTestProject;

/// <summary>
/// Classe com os testes do cadastro de planos\contratos
/// </summary>
[TestFixture]
public class PlanosTest
{
    private IWebDriver webDriver;
    private readonly BrowserType browserType = BrowserType.Chrome;
    private string nomeCampanha = "MassaAutomatizada";

    /// <summary>
    /// Método que será executado antes de cada teste
    /// </summary>
    [SetUp]
    public void Setup()
    {
        webDriver = DriverFactory.CreateDriver(browserType);
        webDriver.Navigate().GoToUrl(GlobalVariables.urlPlataforma);

        new LoginPage(webDriver)
        .PreencherEmailUsuario(GlobalVariables.emailUsuario)
        .PreencherSenhaUsuario(GlobalVariables.senhaUsuario)
        .SubmeterLogin();
    }

    /// <summary>
    /// Testar a criação de um plano
    /// 
    /// Como comercial de trade marketing
    /// Eu quero criar um novo plano
    /// E inicar uma nova negociação
    /// Para enviar a proposta para o cliente
    /// 
    /// Dado que eu tenho espaços disponíveis para realização de trade marketing na loja
    /// Quando eu criar um plano
    /// E escolher os ativos, colocar as quantidades, selecionar as lojas
    /// E clicar no botão Gerar Pré-Plano
    /// Então um novo plano será criado com Status = Simulado e Farol = Planejado, com o incicio da vigência em d(data corrente), com fim em d+30
    /// </summary>
    [Test, Order(1)]
    public void TestCriarPlano()
    {
        var statusPlanoEsperado = "Simulado";
        var farolPlanoEsperado = "PLANEJADO";

        new HomePage(webDriver)
        .AbrirCadastroPlanos();

        new PlanosContratosPage(webDriver)
        .NovaSimulacaoDePlano()
        .PreencherCampoIndustria()
        .PreencherCampoCampanha(nomeCampanha)
        .SelecionarAtivos()
        .PreencherQuantidadeAtivos()
        .SelecionarLojas()
        .GerarPrePlano()
        .SalvarPlano()
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
    /// Dado que eu tenho um plano criado
    /// Quando acessar a tela de edição
    /// E alterar as datas início e fim da vigência
    /// E clicar no botão Salvar Plano
    /// Então um o plano será salvo com a nova vigência
    /// </summary>
    [Test, Order(2)]
    public void TestEditarVigenciaNoPlanoExistente()
    {
        var contexto = "EditarPlano";

        new HomePage(webDriver)
        .AbrirCadastroPlanos();

        new PlanosContratosPage(webDriver)
        .BuscarPlanos(nomeCampanha)
        .AbrirEditacaoDoPlano()
        .EditarInicioVigencia(contexto)
        .EditarFimVigencia(contexto)
        .SalvarPlano()
        .FecharDadosDoPlano();
    }

    /// <summary>
    /// Testar edição dos ativos alocados em um plano existente
    /// 
    /// Como comercial de trade marketing
    /// Eu quero alterar as quantidades dos ativos alocados por loja
    /// Para negociar a alocação de novos espaços
    /// 
    /// Dado que eu tenho um plano criado com valor de receita\despesa
    /// Quando acessar a tela de edição
    /// E alterar as quantidades dos ativos
    /// E clicar no botão Salvar Plano
    /// Então o plano será salvo
    /// E o valor de receita/despesa será atualizado conforme as novas quantidades
    /// </summary>
    [Test, Order(3)]
    public void TestEditarAtivosAlocadosNoPlanoExistente()
    {
        new HomePage(webDriver)
        .AbrirCadastroPlanos();

        new PlanosContratosPage(webDriver)
        .BuscarPlanos(nomeCampanha)
        .AbrirEditacaoDoPlano()
        .AbrirAbaAtivosAlocados()
        .EditarQuantidadesDosAtivosNoPlano()
        .SalvarPlano()
        .FecharDadosDoPlano();
    }

    /// <summary>
    /// Testar alocação de um novo ativo em um plano existente
    /// 
    /// Como comercial de trade marketing
    /// Eu quero alocar um novo ativo nas lojas
    /// Para simular os novos valores, com o novo ativo
    /// 
    /// Dado que eu tenho um plano criado com ativos em 5 lojas
    /// Quando acessar a tela de edição
    /// E incluir um novo ativo para as lojas
    /// E clicar no botão Salvar Plano
    /// Então o plano será salvo
    /// E o valor de receita/despesa será atualizado conforme o novo ativo
    /// </summary>
    [Test, Order(4)]
    public void TestAlocarNovoAtivoNoPlanoExistente()
    {
        var nomeAtivo = "Aplicativo";

        new HomePage(webDriver)
        .AbrirCadastroPlanos();

        new PlanosContratosPage(webDriver)
        .BuscarPlanos(nomeCampanha)
        .AbrirEditacaoDoPlano()
        .AbrirAbaAtivosAlocados()
        .AlocarNovosAtivosNoPlano(nomeAtivo)
        .SalvarPlano()
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
        var contextoSituacao = "Contrato Aprovado";
        var statusPlanoEsperado = "Aprovado";
        var farolPlanoEsperado = "APROVADO";

        new HomePage(webDriver)
        .AbrirCadastroPlanos();

        new PlanosContratosPage(webDriver)
        .BuscarPlanos(nomeCampanha)
        .AbrirEditacaoDoPlano()
        .EditarSituacaoDoPlano(contextoSituacao)
        .SalvarPlano()
        .FecharDadosDoPlano()
        .ValidarStatusFarolDoPlano(statusPlanoEsperado, farolPlanoEsperado);
    }

    /// <summary>
    /// Testar alerta de inventário na criação de planos
    /// 
    /// Como comercial de trade marketing
    /// Eu quero selecionar ativos sem disponibilidade de alocação
    /// Para que eu seja informado de que não há quantidade suficente no inventário
    /// 
    /// Dado que eu não tenho espaços disponíveis para realização de trade marketing na loja, em um determinado período de vigência
    /// Quando eu criar um novo plano
    /// E escolher os ativos, colocar as quantidades, selecionar as lojas
    /// Então será apresentado o botão de alerta para as lojas com indisponibilidade
    /// E uma mensagem será apresentada ao lado do botão Gerar Pré-Plano, com o mesmo desabilitado
    /// </summary>
    [Test, Order(6)]
    public void TestCriarPlanoComAlertaDeInventario()
    {
        var contexto = "NovoPlano";

        new HomePage(webDriver)
        .AbrirCadastroPlanos();

        new PlanosContratosPage(webDriver)
        .NovaSimulacaoDePlano()
        .PreencherCampoIndustria()
        .PreencherCampoCampanha(nomeCampanha)
        .EditarInicioVigencia(contexto)
        .EditarFimVigencia(contexto)
        .SelecionarAtivos()
        .PreencherQuantidadeAtivos()
        .SelecionarLojas()
        .ValidarAlertaInventario()
        .FecharDadosDoPlano();
    }

    /// <summary>
    /// Testar cancelamento de plano
    /// 
    /// Como comercial de trade marketing
    /// Eu quero cancelar uma plano
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

        new HomePage(webDriver)
        .AbrirCadastroPlanos();

        new PlanosContratosPage(webDriver)
        .NovaSimulacaoDePlano()
        .PreencherCampoIndustria()
        .PreencherCampoCampanha(nomeCampanha)
        .SelecionarAtivos()
        .PreencherQuantidadeAtivos()
        .SelecionarLojas()
        .GerarPrePlano()
        .SalvarPlano()
        .FecharDadosDoPlano()
        .BuscarPlanos(nomeCampanha)
        .AbrirEditacaoDoPlano()
        .EditarSituacaoDoPlano(situacaoPlano)
        .SalvarPlano()
        .FecharDadosDoPlano()
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
        new HomePage(webDriver)
        .AbrirCadastroPlanos();

        new PlanosContratosPage(webDriver)
        .BuscarPlanos(nomeCampanha)
        .ConfirmarExclusaoDoPlano();
    }

    /// <summary>
    /// Método que será executado ao final de cada teste
    /// </summary>
    [TearDown]
    public void TearDown()
    { webDriver.Close(); }
}