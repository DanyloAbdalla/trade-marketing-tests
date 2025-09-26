using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;

namespace MeuClienteWebTestProject;

/// <summary>
/// Classe com métodos específicos de manipulação\interação dos elementos, pertinentes a tela Home da plataforma
/// </summary>
public class HomePage
{
    private IWebDriver webDriver;
    private readonly ClienteUpSell clienteUpSellAtual;

    public HomePage(IWebDriver webDriver, ClienteUpSell clienteUpSell)
    {
        this.webDriver = webDriver;
        clienteUpSellAtual = clienteUpSell;
    }

    /// <summary>
    /// Método para acessar a tela do DashBoard de Operações, acessando o mesmo pelo menu suspenso no canto superior esquerdo
    /// </summary>
    /// <returns></returns>
    public DashboardOperacoesPage AcessarDashboardOperacoes()
    {
        AbrirMenuVarejo();

        Dsl.Clicar(webDriver, GlobalVariables.MenuGestao, "Submenu Gestão no menu Varejo");
        Dsl.Clicar(webDriver, GlobalVariables.DashboardOperacoes, "Tela DashBoard de Operações");
        Dsl.Esperar();

        return new DashboardOperacoesPage(webDriver);
    }

    /// <summary>
    /// Método para acessar a tela de Cadastro de Planos, acessando o mesmo pelo menu suspenso no canto superior esquerdo
    /// </summary>
    /// <param name="primeiroTeste"></param>
    /// <returns></returns>
    public PlanosContratosPage AcessarCadastroPlanos(string primeiroTeste)
    {
        //Retorna para o Dashboard de Operações, se no último logout a plataforma parou em outra tela
        VoltarParaDashboardOperacoes();

        Dsl.Esperar();
        AbrirMenuVarejo();

        Dsl.Clicar(webDriver, GlobalVariables.MenuNegociacao, "Menu Negociação");
        Dsl.Clicar(webDriver, GlobalVariables.CadastroPlanosContratos, "Cadastro de Planos");

        Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadListaPlanos);
        Dsl.Esperar(2000);

        if (Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.AvisoInexistenciaDados) > 0)
        {
            return new PlanosContratosPage(webDriver, clienteUpSellAtual);
        }
        else if (Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.PaginacaoTela) > 0)
        {
            FiltrarTodosPlanos();
            //Console.WriteLine(primeiroTeste);
            //Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.EditarPlano(primeiroTeste), "Botão Editar Plano");
        }

        return new PlanosContratosPage(webDriver, clienteUpSellAtual);
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
        var ultimoCadastroAcessado = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.UltimoCadastroAcessado, "Label Último Cadastro Acessado");
        Dsl.Esperar();

        if (!ultimoCadastroAcessado.Contains("Dashboard Opera..."))
            AcessarDashboardOperacoes();

        return this;
    }

    /// <summary>
    /// Método para realizar o logout do usuário da plataforma
    /// </summary>
    /// <returns></returns>
    public HomePage RealizarLogout()
    {
        Dsl.Esperar();
        Dsl.Clicar(webDriver, GlobalVariables.MenuUsuarioLogado, "Botão Usuário Logado");
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SairConta, "Botão Sair da Conta");

        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.PreencherUsuarioEmail);
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.PreencherUsuarioSenha);

        return this;
    }

    /// <summary>
    /// Método para selecionar todos os planos, vingentes e não vigentes
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage FiltrarTodosPlanos()
    {
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.FiltrarPlanosStatusVigencia, "Campo Filtro Vigência");
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarTodosPlanos, "Selecionar Todos Planos");
        Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadListaPlanos);

        return new PlanosContratosPage(webDriver, clienteUpSellAtual);
    }
}