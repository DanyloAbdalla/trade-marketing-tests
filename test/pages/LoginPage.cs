using OpenQA.Selenium;

namespace MeuClienteWebTestProject;

/// <summary>
/// Classe com métodos específicos de manipulação\interação dos elementos, pertinentes a tela de Login da plataforma
/// </summary>
public class LoginPage
{
    private IWebDriver webDriver;

    public LoginPage(IWebDriver webDriver)
    {
        this.webDriver = webDriver;
    }

    /// <summary>
    /// Métodos para preencher o campo E-mail do usuário
    /// </summary>
    /// <param name="emailUsuario"></param>
    /// <returns></returns>
    public LoginPage PreencherEmailUsuario(string emailUsuario)
    {
        webDriver.FindElement(By.XPath(GlobalVariables.PreencherUsuarioEmail)).Click();
        webDriver.FindElement(By.XPath(GlobalVariables.PreencherUsuarioEmail)).SendKeys(emailUsuario);

        return this;
    }

    /// <summary>
    /// Métodos para preencher o campo Senha do usuário
    /// </summary>
    /// <param name="senhaUsuario"></param>
    /// <returns></returns>
    public LoginPage PreencherSenhaUsuario(string senhaUsuario)
    {
        webDriver.FindElement(By.XPath(GlobalVariables.PreencherUsuarioSenha)).Click();
        webDriver.FindElement(By.XPath(GlobalVariables.PreencherUsuarioSenha)).SendKeys(senhaUsuario);

        return this;
    }

    /// <summary>
    /// Método para entrar na página secreta da plataforma, clicando no botão Login
    /// </summary>
    /// <returns></returns>
    public HomePage SubmeterLogin()
    {
        webDriver.FindElement(By.XPath(GlobalVariables.SubmeterLogin)).Click();

        return new HomePage(webDriver);
    }
}