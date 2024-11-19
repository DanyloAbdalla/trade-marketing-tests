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
    /// Método para acessar a tela de cadastro de Planos, acessando o mesmo pelo menu suspenso no canto superior esquerdo
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage AbrirCadastroPlanos()
    {
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MenuPrincipal);
        webDriver.FindElement(By.XPath(GlobalVariables.MenuPrincipal)).Click();
        Dsl.Clicar(webDriver, GlobalVariables.MenuPrincipal, "Menu Principal Superior Esquerdo");

        Dsl.Clicar(webDriver, GlobalVariables.MenuVarejo, "Menu Varejo");
        Dsl.Clicar(webDriver, GlobalVariables.MenuNegociacao, "Menu Negociação");
        Dsl.Clicar(webDriver, GlobalVariables.CadastroPlanosContratos, "Cadastro de Planos");

        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.NovoRegistro);
        Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.EditarPlano, "Botão Editar Plano");

        return new PlanosContratosPage(webDriver);
    }

    public SmartIaPage AbrirCadastroSmartIa()
    {
        AbrirMenuVarejo();

        webDriver.FindElement(By.XPath(GlobalVariables.MenuCadastros)).Click();

        Dsl.ScrollParaElemento(webDriver, GlobalVariables.CadastroSmartIa);
        webDriver.FindElement(By.XPath(GlobalVariables.CadastroSmartIa)).Click();

        return new SmartIaPage(webDriver);
    }

    public HomePage AbrirMenuVarejo()
    {
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MenuPrincipal);
        webDriver.FindElement(By.XPath(GlobalVariables.MenuPrincipal)).Click();

        webDriver.FindElement(By.XPath(GlobalVariables.MenuVarejo)).Click();
        return this;
    }
}