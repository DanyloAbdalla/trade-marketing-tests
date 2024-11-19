using OpenQA.Selenium;

namespace MeuClienteWebTestProject;

/// <summary>
/// Classe com os testes para o Cadastro de Planos\Contratos
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

        new LoginPage(webDriver)
        .PreencherEmailUsuario(GlobalVariables.emailUsuario)
        .PreencherSenhaUsuario(GlobalVariables.senhaUsuario)
        .SubmeterLogin();

        new HomePage(webDriver)
        .AcessarCadastroPlanos();
    }

    /// <summary>
    /// Testar a criação de um plano, estando o cliente sem permissão de planta de loja
    /// 
    /// Como comercial de trade marketing
    /// Eu quero criar um novo plano
    /// E inicar uma nova negociação
    /// Para enviar a proposta para o cliente
    /// 
    /// Dado que eu não tenho permissão de planta de loja
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
    /// Dado que eu tenho um plano criado na Negociação
    /// Quando acessar a tela de edição
    /// E alterar as datas início e fim da vigência
    /// E clicar no botão Salvar Plano
    /// Então um o plano será salvo com a nova vigência
    /// </summary>
    [Test, Order(2)]
    public void TestEditarVigenciaNoPlanoExistente()
    {
        var contexto = "EditarPlano";

        new PlanosContratosPage(webDriver)
        .BuscarPlanos(nomeCampanha)
        .AbrirEdicaoDoPlano()
        .EditarInicioVigencia(contexto)
        .EditarFimVigencia(contexto)
        .SalvarPlano()
        .FecharDadosDoPlano();
    }

    /// <summary>
    /// Testar edição das quantidades dos ativos alocados em um plano existente, estando o cliente sem permissão de planta de loja
    /// 
    /// Como comercial de trade marketing
    /// Eu quero alterar as quantidades dos ativos alocados por loja
    /// Para negociar a alocação de mais espaços
    /// 
    /// Dado que eu não tenho permissão de planta de loja
    /// E que eu tenho um plano criado, contendo um ativo com disponibilidade de inventário
    /// Quando eu acessar a tela de edição do plano
    /// E alterar a quantidade do ativo
    /// E clicar no botão Salvar Plano
    /// Então o plano será salvo com sucesso com a nova quantidade
    /// </summary>
    [Test, Order(3)]
    public void TestEditarAtivoDisponivelAlocadoNoPlanoExistente()
    {
        new PlanosContratosPage(webDriver)
        .BuscarPlanos(nomeCampanha)
        .AbrirEdicaoDoPlano()
        .AbrirAbaAtivosAlocados()
        .EditarQuantidadesDosAtivosNoPlano()
        .SalvarPlano()
        .FecharDadosDoPlano();
    }

    /// <summary>
    /// Testar alocação de um novo ativo em um plano existente, estando o cliente sem permissão de planta de loja
    /// 
    /// Como comercial de trade marketing
    /// Eu quero alocar um novo ativo para as lojas
    /// Para atualizar meu plano com um novo ativo
    /// 
    /// Dado que eu não tenho permissão de planta de loja
    /// E que eu tenho um plano criado
    /// Quando eu acessar a tela de edição do plano
    /// E incluir um novo ativo para a loja com disponibilidade de inventário
    /// Então o plano será salvo com sucesso com o novo ativo
    /// </summary>
    [Test, Order(4)]
    public void TestIncluirNovoAtivoDisponivelNoPlanoExistente()
    {
        var nomeAtivo = "Cestão 01 - ";

        new PlanosContratosPage(webDriver)
        .BuscarPlanos(nomeCampanha)
        .AbrirEdicaoDoPlano()
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

        new PlanosContratosPage(webDriver)
        .BuscarPlanos(nomeCampanha)
        .AbrirEdicaoDoPlano()
        .EditarSituacaoDoPlano(contextoSituacao)
        .SalvarPlano()
        .FecharDadosDoPlano()
        .BuscarPlanos(nomeCampanha)
        .ValidarStatusFarolDoPlano(statusPlanoEsperado, farolPlanoEsperado);
    }

    /// <summary>
    /// Testar alerta de inventário na criação de planos, estando o cliente sem permissão de planta de loja
    /// 
    /// Como comercial de trade marketing
    /// Eu quero selecionar ativos sem indisponibilidade de alocação
    /// Para que eu seja informado de que não há quantidade suficente no inventário
    /// 
    /// Dado que eu não tenho permissão de planta de loja
    /// E que eu não tenho disponibilidade de inventário, em um determinado período de vigência
    /// Quando eu simular um novo plano
    /// E escolher os ativos, colocar as quantidades, selecionar as lojas
    /// Então será apresentado o botão de alerta para as lojas com indisponibilidade
    /// E uma mensagem será apresentada ao lado do botão Gerar Pré-Plano, com o mesmo desabilitado
    /// </summary>
    [Test, Order(6)]
    public void TestCriarPlanoComAlertaDeInventario()
    {
        var contexto = "NovoPlano";

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
        var contexto = "CancelarPlano";

        new PlanosContratosPage(webDriver)
        .BuscarPlanos(nomeCampanha)
        .AbrirEdicaoDoPlano()
        .EditarSituacaoDoPlano(situacaoPlano)
        .SalvarPlano(contexto)
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
    { webDriver.Close(); System.Diagnostics.Process.Start("taskkill_chromedriver.bat"); }
}