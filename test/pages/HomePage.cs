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
    public DashboardOperacoesPage AcessarDashBoardOperacoes()
    {
        AbrirMenuVarejo();

        Dsl.Clicar(webDriver, GlobalVariables.MenuGestao, "Menu Gestão");
        Dsl.Clicar(webDriver, GlobalVariables.DashboardOperacoes, "Tela DashBoard de Operações");
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.TextoCardAtivosAlocados);
        
        return new DashboardOperacoesPage(webDriver);
    }

    /// <summary>
    /// Método para acessar a tela de Cadastro de Planos, acessando o mesmo pelo menu suspenso no canto superior esquerdo
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage AcessarCadastroPlanos()
    {
        AbrirMenuVarejo();

        Dsl.Clicar(webDriver, GlobalVariables.MenuNegociacao, "Menu Negociação");
        Dsl.Clicar(webDriver, GlobalVariables.CadastroPlanosContratos, "Cadastro de Planos");

        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.NovoRegistro);
        Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.EditarPlano, "Botão Editar Plano");

        return new PlanosContratosPage(webDriver);
    }

    /// <summary>
    /// Método para acessar a tela de Cadastro de Campanhas (smartIA), acessando o mesmo pelo menu suspenso no canto superior esquerdo
    /// </summary>
    /// <returns></returns>
    public SmartIaPage AcessarCadastroSmartIa()
    {
        AbrirMenuVarejo();

        webDriver.FindElement(By.XPath(GlobalVariables.MenuCadastros)).Click();

        Dsl.ScrollParaElemento(webDriver, GlobalVariables.CadastroSmartIa);
        webDriver.FindElement(By.XPath(GlobalVariables.CadastroSmartIa)).Click();

        return new SmartIaPage(webDriver);
    }

    /// <summary>
    /// Método para acessar o menu Varejo, acessando o mesmo pelo menu suspenso no canto superior esquerdo
    /// </summary>
    /// <returns></returns>
    public HomePage AbrirMenuVarejo()
    {
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MenuPrincipal);
        Dsl.Clicar(webDriver, GlobalVariables.MenuPrincipal, "Menu Principal Superior Esquerdo");

        Dsl.Clicar(webDriver, GlobalVariables.MenuVarejo, "Menu Varejo");

        return this;
    }
}