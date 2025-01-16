using OpenQA.Selenium;

namespace MeuClienteWebTestProject;

/// <summary>
/// Classe com métodos específicos de manipulação\interação dos elementos, pertinentes a tela Home da plataforma
/// </summary>
public class HomePage
{
    private IWebDriver webDriver;

    public HomePage(IWebDriver webDriver)
    {
        this.webDriver = webDriver;
    }

    /// <summary>
    /// Método para acessar a tela do DashBoard de Operações, acessando o mesmo pelo menu suspenso no canto superior esquerdo
    /// </summary>
    /// <returns></returns>
    public DashboardOperacoesPage AcessarDashboardOperacoes()
    {
        AbrirMenuVarejo();

        Dsl.Clicar(webDriver, GlobalVariables.MenuGestao, "Menu Gestão");
        Dsl.Clicar(webDriver, GlobalVariables.DashboardOperacoes, "Tela DashBoard de Operações");

        return new DashboardOperacoesPage(webDriver);
    }

    /// <summary>
    /// Método para acessar a tela de Cadastro de Planos, acessando o mesmo pelo menu suspenso no canto superior esquerdo
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage AcessarCadastroPlanos()
    {
        VoltarParaDashboardOperacoes();

        Dsl.Esperar();
        AbrirMenuVarejo();

        Dsl.Clicar(webDriver, GlobalVariables.MenuNegociacao, "Menu Negociação");
        Dsl.Clicar(webDriver, GlobalVariables.CadastroPlanosContratos, "Cadastro de Planos");

        Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTela);
        Dsl.Esperar(2000);

        if(Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.AvisoInexistenciaDados) > 0)
            return new PlanosContratosPage(webDriver);        
        else if (Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.PaginacaoTela) > 0)
            Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.EditarPlano, "Botão Editar Plano");

        return new PlanosContratosPage(webDriver);
    }

    /// <summary>
    /// Método para acessar a tela de Cadastro de Campanhas (smartIA), acessando o mesmo pelo menu suspenso no canto superior esquerdo
    /// </summary>
    /// <returns></returns>
    public SmartIaPage AcessarCadastroSmartIa()
    {
        VoltarParaDashboardOperacoes();

        Dsl.Esperar();
        AbrirMenuVarejo();

        Dsl.Clicar(webDriver, GlobalVariables.MenuCadastros, "Menu Cadastros");

        Dsl.ScrollParaElemento(webDriver, GlobalVariables.CadastroSmartIa);
        Dsl.Clicar(webDriver, GlobalVariables.CadastroSmartIa, "Cadastro de Campanhas SmartIA");

        return new SmartIaPage(webDriver);
    }

    /// <summary>
    /// Método para acessar o menu Varejo, acessando o mesmo pelo menu suspenso no canto superior esquerdo
    /// </summary>
    /// <returns></returns>
    public HomePage AbrirMenuVarejo()
    {
        Dsl.Clicar(webDriver, GlobalVariables.MenuPrincipal, "Menu Principal Superior Esquerdo");
        Dsl.Clicar(webDriver, GlobalVariables.MenuVarejo, "Menu Varejo");

        return this;
    }

    /// <summary>
    /// Método para retonar ao Dashboard de Operacoes, ponto de partida de todos os testes
    /// </summary>
    /// <returns></returns>
    public HomePage VoltarParaDashboardOperacoes()
    {
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.UltimoCadastroAcessado);
        var ultimoCadastroAcessado = Dsl.PegarTextoDoElemento(webDriver, GlobalVariables.UltimoCadastroAcessado, "Label Último Cadastro Acessado");
        Dsl.Esperar();

        if (!ultimoCadastroAcessado.Contains("Dashboard Opera..."))
            AcessarDashboardOperacoes();

        return this;
    }
}