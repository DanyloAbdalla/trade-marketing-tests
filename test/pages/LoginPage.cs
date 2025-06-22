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
        Dsl.Clicar(webDriver, GlobalVariables.PreencherUsuarioEmail, "Campo Email Usuário");
        Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.PreencherUsuarioEmail, emailUsuario);

        return this;
    }

    /// <summary>
    /// Métodos para preencher o campo Senha do usuário
    /// </summary>
    /// <param name="senhaUsuario"></param>
    /// <returns></returns>
    public LoginPage PreencherSenhaUsuario(string senhaUsuario)
    {
        Dsl.Clicar(webDriver, GlobalVariables.PreencherUsuarioSenha, "Campo Usuário Senha");
        Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.PreencherUsuarioSenha, senhaUsuario);

        return this;
    }

    /// <summary>
    /// Método para entrar na página secreta da plataforma, clicando no botão Login
    /// </summary>
    /// <returns></returns>
    public HomePage SubmeterLogin()
    {
        Dsl.Clicar(webDriver, GlobalVariables.SubmeterLogin, "Botão Login");
        Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaSpiner);

        return new HomePage(webDriver);
    }

    /// <summary>
    /// Método para realizar o login do usuário da plataforma
    /// </summary>
    /// <param name="emailUsuario"></param>
    /// <param name="senhaUsuario"></param>
    /// <returns></returns>
    public HomePage RealizarLogin(string emailUsuario, string senhaUsuario)
    {
        PreencherEmailUsuario(emailUsuario);
        PreencherSenhaUsuario(senhaUsuario);
        SubmeterLogin();

        return new HomePage(webDriver);
    }
}