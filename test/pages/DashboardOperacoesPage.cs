using System.Diagnostics;
using OpenQA.Selenium;

namespace MeuClienteWebTestProject;

/// <summary>
/// Classe com métodos específicos de manipulação\interação dos elementos, pertinentes a tela Dashboard de Operações
/// </summary>
public class DashboardOperacoesPage
{
    private IWebDriver webDriver;
    private string nomeAtivo = "Adesivo de Check Out";
    private string nomeCampanha = "CampanhaInd04NF";

    public DashboardOperacoesPage(IWebDriver webDriver)
    {
        this.webDriver = webDriver;
    }

    /// <summary>
    /// Método para acessar a tela de Detalhes no card Lojas Ativas
    /// </summary>
    /// <returns></returns>
    public DashboardOperacoesPage AcessarDetalhesLojasAtivas()
    {
        Dsl.Clicar(webDriver, GlobalVariables.DetalhesLojasAtivas, "Botão Visualizar Lojas Ativas");
        Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.PaginacaoTela, "Paginação Tela");

        var lojasAtivas = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.TabelaListagemLojasAtivas) - 1;

        Thread.Sleep(2000);
        Debug.Assert(lojasAtivas == 6, "Lojas não foram exibidas corretamente");

        return new DashboardOperacoesPage(webDriver);
    }

    /// <summary>
    /// Método para acessar a tela de Detalhes da Disponibilidade dos Ativos no card Ativos Alocados
    /// </summary>
    /// <returns></returns>
    public DashboardOperacoesPage AcessarDetalhesDaDiponibilidade()
    {
        Dsl.Clicar(webDriver, GlobalVariables.DetalhesDisponibilidadeAtivos, "Botão Visualizar Disponibilidade de Ativos");
        Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.FiltrarAtivoPorNome, "Botão Filtro");

        Dsl.BuscarRegistros(webDriver, GlobalVariables.FiltrarAtivoPorNome, GlobalVariables.PreencherFiltro, GlobalVariables.BuscarRegistro, nomeAtivo);
        Thread.Sleep(3000);
        Dsl.ValidarTextosNoElemento(webDriver, GlobalVariables.PrimeiraLinhaTabelaColuna1, nomeAtivo);

        return new DashboardOperacoesPage(webDriver);
    }

    /// <summary>
    /// Método para acessar a tela de Detalhes das Negociações dos Ativos no card Ativos Alocados
    /// </summary>
    /// <returns></returns>
    public DashboardOperacoesPage AcessarDetalhesDasNegociacoes()
    {
        Dsl.Clicar(webDriver, GlobalVariables.DetalhesNegociacaoAtivos, "Botão Visualizar Ativos Negociados");
        Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.FiltrarAtivoPorNome, "Botão Filtro");

        Dsl.BuscarRegistros(webDriver, GlobalVariables.FiltrarAtivoPorNome, GlobalVariables.PreencherFiltro, GlobalVariables.BuscarRegistro, nomeAtivo);
        Thread.Sleep(3000);
        Dsl.ValidarTextosNoElemento(webDriver, GlobalVariables.PrimeiraLinhaTabelaColuna1, nomeAtivo);

        Dsl.Clicar(webDriver, GlobalVariables.ContratosVinculados, "Botão Visualizar Contratos Vinculados");
        Dsl.Clicar(webDriver, GlobalVariables.FecharTelaContratosEAtivosVinculados, "Botão Fechar Contratos Vinculados");
        Thread.Sleep(500);

        return new DashboardOperacoesPage(webDriver);
    }

    /// <summary>
    /// Método para acessar a tela de Detalhes do Potencial de Receita dos Ativos no card Ativos Alocados
    /// </summary>
    /// <returns></returns>
    public DashboardOperacoesPage AcessarDetalhesDoPotencialDeReceita()
    {
        Dsl.Clicar(webDriver, GlobalVariables.DetalhesPotencialReceitaAtivos, "Botão Visualizar Listagem Potencial Receita");
        Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.FiltrarAtivoPorNomePotencialReceita, "Botão Filtro");

        Dsl.BuscarRegistros(webDriver, GlobalVariables.FiltrarAtivoPorNomePotencialReceita, GlobalVariables.PreencherFiltro, GlobalVariables.BuscarRegistro, nomeAtivo);
        Thread.Sleep(3000);
        Dsl.ValidarTextosNoElemento(webDriver, GlobalVariables.PrimeiraLinhaTabelaColuna1, nomeAtivo);

        return new DashboardOperacoesPage(webDriver);
    }

    /// <summary>
    /// Método para acessar a tela de Detalhes dos Contratos Ativos no card Contratos Vigentes, Total de Receitas
    /// </summary>
    /// <returns></returns>
    public DashboardOperacoesPage AcessarDetalhesDeContratosAtivos(string contexto)
    {
        if (contexto.Contains("ContratosVigentes"))
            Dsl.Clicar(webDriver, GlobalVariables.DetalhesContratosAtivosContratosVigentes, "Botão Visualizar Contratos Ativos");
        if (contexto.Contains("TotalReceita"))
        {
            Dsl.ScrollParaElemento(webDriver, GlobalVariables.TextoCardTotalReceita);
            Dsl.Clicar(webDriver, GlobalVariables.DetalhesContratosAtivosTotalReceita, "Botão Visualizar Contratos Ativos");
        }

        Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.FiltrarContratoPorCampanha, "Botão Filtro");

        Dsl.BuscarRegistros(webDriver, GlobalVariables.FiltrarContratoPorCampanha, GlobalVariables.PreencherFiltro, GlobalVariables.BuscarRegistro, nomeCampanha);
        Thread.Sleep(2000);
        Dsl.ValidarTextosNoElemento(webDriver, GlobalVariables.PrimeiraLinhaTabelaColuna2, nomeCampanha);

        Dsl.Clicar(webDriver, GlobalVariables.AtivosVinculados, "Botão Visualizar Ativos Vinculados");
        Dsl.Clicar(webDriver, GlobalVariables.FecharTelaContratosEAtivosVinculados, "Botão Fechar Ativos Vinculados");
        Thread.Sleep(500);

        return new DashboardOperacoesPage(webDriver);
    }

    /// <summary>
    /// Método para acessar a tela de Detalhes dos Contratos Ativos no card Contratos Vigentes
    /// </summary>
    /// <returns></returns>
    public DashboardOperacoesPage AcessarDetalhesDeContratosVencendo()
    {
        Dsl.Clicar(webDriver, GlobalVariables.DetalhesContratosVencendo, "Botão Visualizar Contratos Vencendo");
        if (Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.AtivosVinculados) > 0)
        {
            Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.FiltrarContratoPorCampanha, "Botão Filtro");

            Dsl.BuscarRegistros(webDriver, GlobalVariables.FiltrarContratoPorCampanha, GlobalVariables.PreencherFiltro, GlobalVariables.BuscarRegistro, nomeCampanha);
            Thread.Sleep(2000);
            Dsl.ValidarTextosNoElemento(webDriver, GlobalVariables.PrimeiraLinhaTabelaColuna2, nomeCampanha);

            Dsl.Clicar(webDriver, GlobalVariables.AtivosVinculados, "Botão Visualizar Ativos Vinculados");
            Dsl.Clicar(webDriver, GlobalVariables.FecharTelaContratosEAtivosVinculados, "Botão Fechar Ativos Vinculados");
            Thread.Sleep(500);
        }

        return new DashboardOperacoesPage(webDriver);
    }

    /// <summary>
    /// Método para acessar a tela de Detalhes de Aterrissagem de Receita no card Evolução Performance Receita, Evolução Performance Receita Bandeira e Evolução Performance Receita Tipo Fornecedor
    /// </summary>
    /// <returns></returns>
    public DashboardOperacoesPage AcessarDetalhesDeAterrissagemReceita(string card)
    {
        if (card.Contains("EvolucaoReceita"))
        {
            Dsl.ScrollParaElemento(webDriver, GlobalVariables.TextoCardTotalReceita);
            Dsl.Clicar(webDriver, GlobalVariables.DetalhesAterrissagemReceita, "Botão Visualizar Listagem de Aterrisagem Receita");
        }
        else if (card.Contains("EvolucaoReceitaBandeira"))
        {
            Dsl.ScrollParaElemento(webDriver, GlobalVariables.DetalhesListaParceirosPerformance);
            Dsl.Clicar(webDriver, GlobalVariables.DetalhesAterrissagemReceitaPorBandeira, "Botão Visualizar Listagem de Aterrisagem Receita");
        }
        else if (card.Contains("EvolucaoReceitaTipoFornecedor"))
        {
            Dsl.ScrollParaElemento(webDriver, GlobalVariables.DetalhesListaParceirosPerformance);
            Dsl.Clicar(webDriver, GlobalVariables.DetalhesAterrissagemReceitaPorTipoFornecedor, "Botão Visualizar Listagem de Aterrisagem Receita");
        }

        Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.PaginacaoTela, "Paginação Tela");

        //Validando se a tabela é apresentada em tela
        webDriver.FindElement(By.XPath("//*[@role='tabpanel']/div/div"));
        webDriver.FindElement(By.XPath("//*[text()='JANEIRO']"));

        return new DashboardOperacoesPage(webDriver);
    }

    /// <summary>
    /// Método para acessar a tela de Detalhes de Lista Parceiros no card Evolução Performance Parceiro e Investimento por Parceiro
    /// </summary>
    /// <returns></returns>
    public DashboardOperacoesPage AcessarDetalhesDeListaParceiros(string card)
    {
        if (card.Contains("EvolucaoPerformanceParceiros"))
        {
            Dsl.ScrollParaElemento(webDriver, GlobalVariables.FiltrarNegociacoes);
            Dsl.Clicar(webDriver, GlobalVariables.DetalhesListaParceirosPerformance, "Botão Visualizar Lista Parceiro");
        }
        else if (card.Contains("InvestimentoParceiro"))
        {
            Dsl.ScrollParaElemento(webDriver, GlobalVariables.DetalhesDesempenhoDosAtivos);
            Dsl.Clicar(webDriver, GlobalVariables.DetalhesListaParceirosInvestimento, "Botão Visualizar Lista Parceiro");
        }

        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.AvisoInexistenciaDados);

        if (!(Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.AvisoInexistenciaDados) > 0))
            Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.PaginacaoTela, "Paginação Tela");

        //Validando se a tabela é apresentada em tela
        webDriver.FindElement(By.XPath("//*[@role='tabpanel']/div/div"));
        webDriver.FindElement(By.XPath("//*[text()='Indústria/Parceiro']"));

        return new DashboardOperacoesPage(webDriver);
    }

    /// <summary>
    /// Método para acessar a tela de Detalhes de Desempenho por Loja
    /// </summary>
    /// <returns></returns>
    public DashboardOperacoesPage AcessarDetalhesDesempenhoPorLoja()
    {
        Dsl.ScrollParaElemento(webDriver, GlobalVariables.DetalhesDesempenhoDosAtivos);
        Dsl.Clicar(webDriver, GlobalVariables.DetalhesDesempenhoPorLoja, "Botão Visualizar Desempenho por Loja");

        //Validando se o gráfico é apresentado em tela, com o campo filtro
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.GraficoDesempenhoLoja);
        Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.FiltroDesempenhoLoja, "Filtro Desempenho por Loja");

        return new DashboardOperacoesPage(webDriver);
    }

    /// <summary>
    /// Método para acessar a tela de Detalhes de Desempenho por Ativo
    /// </summary>
    /// <returns></returns>
    public DashboardOperacoesPage AcessarDetalhesDesempenhoDeAtivo()
    {
        Dsl.ScrollParaElemento(webDriver, GlobalVariables.TextoCardMaisVendidosDepartamento);
        Dsl.Clicar(webDriver, GlobalVariables.DetalhesDesempenhoDosAtivos, "Botão Visualizar Desempenho dos Ativos");

        //Validando se o gráfico é apresentado em tela, com o campo filtro
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.GraficoDesempenhoAtivo);
        Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.FiltroDesempenhoAtivo, "Filtro Desempenho por Ativo");

        return new DashboardOperacoesPage(webDriver);
    }

    /// <summary>
    /// Método para fechar a tela de detalhes
    /// </summary>
    /// <returns></returns>
    public DashboardOperacoesPage FecharDetalhes()
    {
        Thread.Sleep(500);
        Dsl.Clicar(webDriver, GlobalVariables.FecharTela, "Botão Fechar Detalhes");

        return this;
    }
}