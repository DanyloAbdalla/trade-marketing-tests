using System.Diagnostics;
using OpenQA.Selenium;

namespace MeuClienteWebTestProject;

/// <summary>
/// Classe com métodos específicos de manipulação\interação dos elementos, pertinentes a tela Dashboard de Operações
/// </summary>
public class DashboardOperacoesPage
{
    private IWebDriver webDriver;

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
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.DetalhesLojasAtivas, "Botão Visualizar Lojas Ativas");
        //Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.PaginacaoTela, "Paginação Tela");
        Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTela);

        var lojasAtivas = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.TabelaListagemLojasAtivas) - 1;
        Dsl.Esperar();
        Debug.Assert(lojasAtivas == 6, "Lojas não foram exibidas corretamente");

        return new DashboardOperacoesPage(webDriver);
    }

    /// <summary>
    /// Método para acessar a tela de Detalhes da Disponibilidade dos Ativos no card Ativos Alocados
    /// </summary>
    /// <returns></returns>
    public DashboardOperacoesPage AcessarDetalhesDaDiponibilidade(string nomeAtivo, string nomeAtivoEsperado)
    {
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.DetalhesDisponibilidadeAtivos, "Botão Visualizar Disponibilidade de Ativos");
        Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTela);
        Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.FiltrarAtivoPorNome, "Botão Filtro");

        Dsl.BuscarRegistros(webDriver, GlobalVariables.FiltrarAtivoPorNome, GlobalVariables.PreencherFiltro, GlobalVariables.BuscarRegistro, nomeAtivo);
        Dsl.Esperar(2000);
        
        var texto = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.ColunaAtivoListagemDisponibilidadeAtivos, "Coluna Ativo");
        var nomeAtivoAtual = Dsl.RemoverNumerosEspacosDeUmTexto(texto, "Coluna Ativo");
        Dsl.ValidarTextosNoElemento(nomeAtivoAtual, nomeAtivoEsperado);

        return new DashboardOperacoesPage(webDriver);
    }

    /// <summary>
    /// Método para acessar a tela de Detalhes das Negociações dos Ativos no card Ativos Alocados
    /// </summary>
    /// <returns></returns>
    public DashboardOperacoesPage AcessarDetalhesDasNegociacoes(string nomeAtivo, string nomeAtivoEsperado)
    {
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.DetalhesNegociacaoAtivos, "Botão Visualizar Ativos Negociados");
        Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTela);
        Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.FiltrarAtivoPorNome, "Botão Filtro");

        Dsl.BuscarRegistros(webDriver, GlobalVariables.FiltrarAtivoPorNome, GlobalVariables.PreencherFiltro, GlobalVariables.BuscarRegistro, nomeAtivo);
        Dsl.Esperar();
        
        var texto = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.ColunaAtivoListagemAtivosNegociados, "Coluna Ativo");
        var nomeAtivoAtual = Dsl.RemoverNumerosEspacosDeUmTexto(texto, "Coluna Ativo");
        Dsl.ValidarTextosNoElemento(nomeAtivoAtual, nomeAtivoEsperado);

        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.ColunaDetalhesBotaoContratosVinculado, "Botão Visualizar Contratos Vinculados");
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.FecharTelaContratosEAtivosVinculados, "Botão Fechar Contratos Vinculados");
        Dsl.Esperar(500);

        return new DashboardOperacoesPage(webDriver);
    }

    /// <summary>
    /// Método para acessar a tela de Detalhes do Potencial de Receita dos Ativos no card Ativos Alocados
    /// </summary>
    /// <returns></returns>
    public DashboardOperacoesPage AcessarDetalhesDoPotencialDeReceita(string nomeAtivo, string nomeAtivoEsperado)
    {
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.DetalhesPotencialReceitaAtivos, "Botão Visualizar Listagem Potencial Receita");
        Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.FiltrarAtivoPorNomePotencialReceita, "Botão Filtro");

        Dsl.BuscarRegistros(webDriver, GlobalVariables.FiltrarAtivoPorNomePotencialReceita, GlobalVariables.PreencherFiltro, GlobalVariables.BuscarRegistro, nomeAtivo);
        Dsl.Esperar();

        var texto = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.ColunaNomeListagemPotencialReceita, "Coluna Ativo");
        var nomeAtivoAtual = Dsl.RemoverNumerosEspacosDeUmTexto(texto, "Coluna Ativo");
        Dsl.ValidarTextosNoElemento(nomeAtivoAtual, nomeAtivoEsperado);

        return new DashboardOperacoesPage(webDriver);
    }

    /// <summary>
    /// Método para acessar a tela de Detalhes dos Contratos Ativos no card Contratos Vigentes, Total de Receitas
    /// </summary>
    /// <returns></returns>
    public DashboardOperacoesPage AcessarDetalhesDeContratosAtivos(string card, string nomeConratoCampanha)
    {
        if (card.Contains("ContratosVigentes"))
        {
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.DetalhesContratosAtivos, "Botão Visualizar Contratos Ativos");
            Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTela);
        }
        else if (card.Contains("TotalReceita"))
        {
            Dsl.ScrollParaElemento(webDriver, GlobalVariables.TextoCardTotalReceita);
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.DetalhesContratosAtivosTotalReceita, "Botão Visualizar Contratos Ativos");
            Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTela);
        }

        Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.FiltrarContratoPorCampanha, "Botão Filtro");

        Dsl.BuscarRegistros(webDriver, GlobalVariables.FiltrarContratoPorCampanha, GlobalVariables.PreencherFiltro, GlobalVariables.BuscarRegistro, nomeConratoCampanha);
        Dsl.Esperar();

        var nomeContratoAtual = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.ColunaContratoListagemContratosAtivos, "Coluna Contrato");
        var nomecontratoEsperado = nomeConratoCampanha;
        Dsl.ValidarTextosNoElemento(nomeContratoAtual, nomecontratoEsperado);

        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.ColunaAcoesBotaoListagemContratosAtivos, "Botão Visualizar Ativos Vinculados");
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.FecharTelaContratosEAtivosVinculados, "Botão Fechar Ativos Vinculados");
        Dsl.Esperar(500);

        return new DashboardOperacoesPage(webDriver);
    }

    /// <summary>
    /// Método para acessar a tela de Detalhes dos Contratos Vencendo no card Contratos Vigentes
    /// </summary>
    /// <returns></returns>
    public DashboardOperacoesPage AcessarDetalhesDeContratosVencendo(string nomeContratoCampanhaEsperado)
    {
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.DetalhesContratosVencendo, "Botão Visualizar Contratos Vencendo");
        Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTela);
        Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.FiltrarContratoPorCampanha, "Botão Filtro");

        Dsl.BuscarRegistros(webDriver, GlobalVariables.FiltrarContratoPorCampanha, GlobalVariables.PreencherFiltro, GlobalVariables.BuscarRegistro, nomeContratoCampanhaEsperado);
        Dsl.Esperar();

        var nomeContratoCampanhaAtual = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.ColunaContratoListagemContratosVencendo, "Coluna Contrato");
        Dsl.ValidarTextosNoElemento(nomeContratoCampanhaAtual, nomeContratoCampanhaEsperado);

        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.ColunaAcoesBotaoListagemContratosVencendo, "Botão Visualizar Ativos Vinculados");
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.FecharTelaContratosEAtivosVinculados, "Botão Fechar Ativos Vinculados");
        Dsl.Esperar(500);

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
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.DetalhesAterrissagemReceita, "Botão Visualizar Listagem de Aterrisagem Receita");
        }
        else if (card.Contains("EvolucaoReceitaBandeira"))
        {
            Dsl.ScrollParaElemento(webDriver, GlobalVariables.DetalhesListaParceirosPerformance);
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.DetalhesAterrissagemReceitaPorBandeira, "Botão Visualizar Listagem de Aterrisagem Receita");
        }
        else if (card.Contains("EvolucaoReceitaTipoFornecedor"))
        {
            Dsl.ScrollParaElemento(webDriver, GlobalVariables.DetalhesListaParceirosPerformance);
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.DetalhesAterrissagemReceitaPorTipoFornecedor, "Botão Visualizar Listagem de Aterrisagem Receita");
        }

        Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.PaginacaoTela, "Paginação Tela");

        var countTabelaDeDados = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.TabelaListagemAterrissagem);
        Debug.Assert(countTabelaDeDados > 0, "Tabela de dados não foi apresentada corretamente");

        var countColunaMes = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.ColunaMesAterrissagem);
        Debug.Assert(countColunaMes > 0, "Dado na coluna Mês não foi apresentado corretamente");

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
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.DetalhesListaParceirosPerformance, "Botão Visualizar Lista Parceiro");
        }
        else if (card.Contains("InvestimentoParceiro"))
        {
            Dsl.ScrollParaElemento(webDriver, GlobalVariables.DetalhesDesempenhoDosAtivos);
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.DetalhesListaParceirosInvestimento, "Botão Visualizar Lista Parceiro");
        }

        Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTela);
        Dsl.Esperar();
        //Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.AvisoInexistenciaDados);

        if (!(Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.AvisoInexistenciaDados) > 0))
            Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.PaginacaoTela, "Paginação Tela");

        var countTabelaDeDados = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.TabelaListagemParceiros);
        Debug.Assert(countTabelaDeDados > 0, "Tabela de dados não foi apresentada corretamente");

        var countColunaIndustriaParceiro = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.ColunaIndustriaParcerio);
        Debug.Assert(countColunaIndustriaParceiro > 0, "Coluna Indústria/Parceiro não foi apresentada corretamente");

        return new DashboardOperacoesPage(webDriver);
    }

    /// <summary>
    /// Método para acessar a tela de Detalhes de Desempenho por Loja
    /// </summary>
    /// <returns></returns>
    public DashboardOperacoesPage AcessarDetalhesDesempenhoPorLoja()
    {
        Dsl.ScrollParaElemento(webDriver, GlobalVariables.DetalhesDesempenhoDosAtivos);
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.DetalhesDesempenhoPorLoja, "Botão Visualizar Desempenho por Loja");

        Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaBarra);

        var countGraficoDesempenhoLoja = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.GraficoDesempenhoLoja);
        Debug.Assert(countGraficoDesempenhoLoja > 0, "Gráfico não foi apresentado corretamente");

        var countCampoFiltroDesempenhoLoja = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.FiltroDesempenhoLoja);
        Debug.Assert(countCampoFiltroDesempenhoLoja > 0, "Campo filtro não foi apresentado corretamente");

        return new DashboardOperacoesPage(webDriver);
    }

    /// <summary>
    /// Método para acessar a tela de Detalhes de Desempenho por Ativo
    /// </summary>
    /// <returns></returns>
    public DashboardOperacoesPage AcessarDetalhesDesempenhoDeAtivo()
    {
        Dsl.ScrollParaElemento(webDriver, GlobalVariables.TextoCardMaisVendidosDepartamento);
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.DetalhesDesempenhoDosAtivos, "Botão Visualizar Desempenho dos Ativos");

        Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaBarra);

        var countGraficoDesempenhoAtivo = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.GraficoDesempenhoAtivo);
        Debug.Assert(countGraficoDesempenhoAtivo > 0, "Gráfico não foi apresentado corretamente");

        var countCampoFiltroDesempenhoAtivo = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.FiltroDesempenhoAtivo);
        Debug.Assert(countCampoFiltroDesempenhoAtivo > 0, "Campo filtro não foi apresentado corretamente");

        return new DashboardOperacoesPage(webDriver);
    }

    /// <summary>
    /// Método para fechar a tela de detalhes
    /// </summary>
    /// <returns></returns>
    public DashboardOperacoesPage FecharDetalhes()
    {
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.FecharTela, "Botão Fechar Detalhes Card");

        return this;
    }
}