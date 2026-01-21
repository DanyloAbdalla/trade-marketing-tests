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
        Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaSpiner, "Load Tela Lojas Ativas no Card Lojas Ativas");

        var lojasAtivas = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.TabelaListagemLojasAtivas) - 1;
        Dsl.Esperar();
        Debug.Assert(lojasAtivas == 5, "Lojas não foram exibidas corretamente");

        return new DashboardOperacoesPage(webDriver);
    }

    /// <summary>
    /// Método para acessar a tela de Detalhes da Disponibilidade dos Ativos no card Ativos Alocados
    /// </summary>
    /// <returns></returns>
    public DashboardOperacoesPage AcessarDetalhesDaDiponibilidade()
    {
        string nomeAtivoSemCalendario = DataLoader.ObterDados("dashboard_operacaoes", "TestAcessarVisaoDetalhadaDeAtivosAlocados", "nomeAtivoSemCalendario");
        string nomeAtivoComCalendario = DataLoader.ObterDados("dashboard_operacaoes", "TestAcessarVisaoDetalhadaDeAtivosAlocados", "nomeAtivoComCalendario");

        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.DetalhesDisponibilidadeAtivos, "Botão Visualizar Disponibilidade de Ativos");
        Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaSpiner, "Load Tela Disponibilidade de Ativos no Card Ativos Alocados");
        Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.FiltrarAtivoPorNome, "Botão Filtro");

        BuscarDisponibilidadeDoAtivo(nomeAtivoSemCalendario);
        BuscarDisponibilidadeDoAtivo(nomeAtivoComCalendario);

        return new DashboardOperacoesPage(webDriver);
    }

    /// <summary>
    /// Método para buscar e validar o ativo, pesquisando a disponibilidade
    /// </summary>
    /// <param name="nomeAtivo"></param>
    /// <returns></returns>
    public DashboardOperacoesPage BuscarDisponibilidadeDoAtivo(string nomeAtivo)
    {
        Dsl.BuscarRegistros(webDriver, GlobalVariables.FiltrarAtivoPorNome, GlobalVariables.PreencherFiltro, GlobalVariables.BuscarRegistro, nomeAtivo);
        Dsl.Esperar(2000);

        var texto = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.ColunaAtivoListagemDisponibilidadeAtivos, "Coluna Ativo");
        var nomeAtual = Dsl.RemoverNumerosEspacosDeUmTexto(texto, "Coluna Ativo");
        var nomeEsperado = Dsl.RemoverNumerosEspacosDeUmTexto(nomeAtivo, "Ativo Esperado");
        Dsl.ValidarTextosNoElemento(nomeAtual, nomeEsperado);
        AcessarDetalhesDaDisponibilidadePorLoja(nomeAtivo);

        return new DashboardOperacoesPage(webDriver);
    }

    /// <summary>
    /// Método para acessar a tela de Disponibilidade dos Ativos por Loja
    /// </summary>
    /// <param name="nomeAtivo"></param>
    /// <returns></returns>
    public DashboardOperacoesPage AcessarDetalhesDaDisponibilidadePorLoja(string nomeAtivo)
    {
        switch (nomeAtivo)
        {
            case "Adesivo de Elevador":
                Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.ColunaQtdLojasListagemDisponibilidadeAtivos, "Coluna Qtd Lojas");
                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.TabelaDisponibilidadeAtivoPorLoja, "Tabela Disponibilidade do Ativo por Loja");
                Dsl.Clicar(webDriver, GlobalVariables.FecharTelaDisponibilidadeAtivosPorLoja, "Botão Fechar Disponibilidade Por Loja");
                break;
            case "Adesivo de Chão 01":
                Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.ColunaQtdLojasListagemDisponibilidadeAtivos, "Coluna Qtd Lojas");
                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.TabelaDisponibilidadeCalendariosAtivo, "Tabela Disponibilidade Calendários do Ativo");
                Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.ColunaDisponibilidadeCalendariosAtivo, "Coluna Disponibilidade Por Calendário");
                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.TabelaDisponibilidadeAtivoPorLoja, "Tabela Disponibilidade do Ativo por Loja");
                Dsl.Clicar(webDriver, GlobalVariables.FecharTelaCalendariosAtivo, "Botão Fechar Calendários do Ativo");
                Dsl.Clicar(webDriver, GlobalVariables.FecharTelaDisponibilidadeAtivosPorLoja, "Botão Fechar Disponibilidade Por Loja");
                Dsl.Esperar();
                break;
        }

        return new DashboardOperacoesPage(webDriver);
    }

    /// <summary>
    /// Método para acessar a tela de Detalhes das Negociações dos Ativos no card Ativos Alocados
    /// </summary>
    /// <returns></returns>
    public DashboardOperacoesPage AcessarDetalhesDasNegociacoes(string nomeAtivo, string nomeAtivoEsperado)
    {
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.DetalhesNegociacaoAtivos, "Botão Visualizar Ativos Negociados");
        Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaSpiner, "Load Tela Ativos Negociados no Card Ativos Alocados");
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
        if (card.Equals("ContratosVigentes"))
        {
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.DetalhesContratosAtivos, "Botão Visualizar Contratos Ativos");
            Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaSpiner, "Load Tela Contratos Ativos no Card Contratos Vigentes");
        }
        else if (card.Equals("TotalReceita"))
        {
            Dsl.ScrollParaElemento(webDriver, GlobalVariables.TextoCardTotalReceita, "Texto Indíce de Cresctimento de Receita");
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.DetalhesContratosAtivosTotalReceita, "Botão Visualizar Contratos Ativos");
            Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaSpiner, "Load Tela Contratos Ativos no Card Total de Receita");
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
        Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaSpiner, "Load Tela Contratos Vencendo no Card Contratos Vigentes");
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
        string xpathElementoScroll = null;
        string xpathElementoBotao = null;
        string telaElemento = "Botão Visualizar Listagem de Aterrisagem Receita";

        switch (card)
        {
            case "EvolucaoReceita":
                xpathElementoScroll = GlobalVariables.TextoCardTotalReceita;
                xpathElementoBotao = GlobalVariables.DetalhesAterrissagemReceita;
                break;
            case "EvolucaoReceitaBandeira":
                xpathElementoScroll = GlobalVariables.DetalhesAterrissagemReceitaPorTipoFornecedor;
                xpathElementoBotao = GlobalVariables.DetalhesAterrissagemReceitaPorBandeira;
                break;
            case "EvolucaoReceitaTipoFornecedor":
                xpathElementoScroll = GlobalVariables.DetalhesAterrissagemReceitaPorTipoFornecedor;
                xpathElementoBotao = GlobalVariables.DetalhesAterrissagemReceitaPorTipoFornecedor;
                break;
        }

        Dsl.ScrollParaElemento(webDriver, xpathElementoScroll, "Texto Indíce de Crescimento ou Botão Detalhes Aterrissagem Receita");
        Dsl.EsperarElementoParaClicar(webDriver, xpathElementoBotao, telaElemento);

        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.TabelaListagemAterrissagem, "Tabela Lista Aterrissagem de Receita");
        Dsl.Esperar();

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
        string xpathElementoScroll = null;
        string xpathElementoBotao = null;
        string telaElemento = "Botão Visualizar Lista Parceiro";

        switch (card)
        {
            case "EvolucaoPerformanceFornecedor":
                xpathElementoScroll = GlobalVariables.FiltrarNegociacoes;
                xpathElementoBotao = GlobalVariables.DetalhesListaPerformanceFornecedor;
                break;
            case "InvestimentoParceiro":
                xpathElementoScroll = GlobalVariables.DetalhesDesempenhoDosAtivos;
                xpathElementoBotao = GlobalVariables.DetalhesListaInvestimentoParceiro;
                break;
        }

        Dsl.ScrollParaElemento(webDriver, xpathElementoScroll, "Botão Filtrar Negociações ou Botão Detalhes Desempenho dos Ativos");
        Dsl.EsperarElementoParaClicar(webDriver, xpathElementoBotao, telaElemento);

        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.TabelaListagemParceiros, "Tabela Investimento por Parceiros");
        Dsl.Esperar();

        if (!(Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.AvisoInexistenciaDados) > 0))
            Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.PaginacaoTelaListagemParceiros, "Paginação Tela");

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
        Dsl.ScrollParaElemento(webDriver, GlobalVariables.DetalhesDesempenhoDosAtivos, "Botão Detalhes Desempenho dos Ativos");
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.DetalhesDesempenhoPorLoja, "Botão Visualizar Desempenho por Loja");

        Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaBarra, "Load Tela Desempenho por Loja no Card Desempenho por Loja");

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
        Dsl.ScrollParaElemento(webDriver, GlobalVariables.TextoCardMaisVendidosDepartamento, "Texto Mais Vendidos por Departamento");
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.DetalhesDesempenhoDosAtivos, "Botão Visualizar Desempenho dos Ativos");

        Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaBarra, "Load Tela Desempenho por Ativo no Card Desempenho de Ativos");

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