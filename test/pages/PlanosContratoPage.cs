using System.Runtime.InteropServices;
using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace MeuClienteWebTestProject;

/// <summary>
/// Classe com métodos específicos de manipulação\interação dos elementos, pertinentes a tela Planos da plataforma
/// </summary>
public class PlanosContratosPage
{
    private IWebDriver webDriver;
    private readonly string atributoTesteId;
    private readonly string nomeTeste;
    private readonly string nomeCampanha;
    private readonly string tipoCampanha;
    private readonly string Setor;
    private readonly string Departamento;
    private readonly string Categoria;
    private string tipoMidiaAtivo;
    private readonly string statusEsperado;
    private readonly string farolEsperado;
    private ClienteUpSell clienteUpSellAtual;
    private IList<string> inventarios;
    private IList<string> nomeLojas;
    private IList<string> ativosGraficos;
    private IList<string> ativosFisicos;
    private IList<string> abasPlanoEsperado;
    private IList<string> etapaNomeWorkflowPlanoEsperado;
    private List<string> nomeCampanhas;
    private List<MensagemFeedback> mensagensEsperadas;

    public PlanosContratosPage(IWebDriver webDriver, ClienteUpSell clienteUpSell)
    {
        this.webDriver = webDriver;
        clienteUpSellAtual = clienteUpSell;
        nomeTeste = TestContext.CurrentContext.Test.MethodName;
        atributoTesteId = DataLoader.ObterDados("negociacoes_planos", "TestGlobalData", "atributoTesteId");
        ativosGraficos = DataLoader.ObterDadosEmLista("negociacoes_planos", "TestGlobalData", "ativosGraficos");
        ativosFisicos = DataLoader.ObterDadosEmLista("negociacoes_planos", "TestGlobalData", "ativosFisicos");
        abasPlanoEsperado = DataLoader.ObterDadosEmLista("negociacoes_planos", "TestGlobalData", "abasPlanoEsperado");
        mensagensEsperadas = DataLoader.ObterMensagensDeFeedback("negociacoes_planos", "TestGlobalData", "mensagensDeFeedback");
        nomeLojas = DataLoader.ObterDadosEmLista("negociacoes_planos", "TestGlobalData", "lojas");
        inventarios = DataLoader.ObterDadosEmLista("negociacoes_planos", "TestGlobalData", "inventarios");

        if (nomeTeste.Equals("TestCriarPlanoComAtivosTipoMidiaFisica"))
        {
            nomeCampanha = DataLoader.ObterDados("negociacoes_planos", nomeTeste, "nomeCampanha");
            tipoMidiaAtivo = DataLoader.ObterDados("negociacoes_planos", nomeTeste, "tipoMidiaAtivo");
            etapaNomeWorkflowPlanoEsperado = DataLoader.ObterDadosEmLista("negociacoes_planos", nomeTeste, "etapaNomeWorkflowPlanoEsperado");
            statusEsperado = DataLoader.ObterDados("negociacoes_planos", nomeTeste, "statusEsperado");
            farolEsperado = DataLoader.ObterDados("negociacoes_planos", nomeTeste, "farolEsperado");
        }
        else if (nomeTeste.Equals("TestAprovarPlano"))
        {
            nomeCampanha = DataLoader.ObterDados("negociacoes_planos", nomeTeste, "nomeCampanha");
            tipoCampanha = DataLoader.ObterDados("negociacoes_planos", nomeTeste, "tipoCampanha");
            Setor = DataLoader.ObterDados("negociacoes_planos", nomeTeste, "Setor");
            Departamento = DataLoader.ObterDados("negociacoes_planos", nomeTeste, "Departamento");
            Categoria = DataLoader.ObterDados("negociacoes_planos", nomeTeste, "Categoria");
            statusEsperado = DataLoader.ObterDados("negociacoes_planos", nomeTeste, "statusEsperado");
            farolEsperado = DataLoader.ObterDados("negociacoes_planos", nomeTeste, "farolEsperado");
        }
        else if (nomeTeste.Equals("TestCancelarPlano"))
        {
            nomeCampanha = DataLoader.ObterDados("negociacoes_planos", nomeTeste, "nomeCampanha");
            statusEsperado = DataLoader.ObterDados("negociacoes_planos", nomeTeste, "statusEsperado");
            farolEsperado = DataLoader.ObterDados("negociacoes_planos", nomeTeste, "farolEsperado");
        }
        else if (nomeTeste.Equals("TestExcluirPlano"))
        {
            nomeCampanhas = DataLoader.ObterDadosEmLista("negociacoes_planos", nomeTeste, "nomeCampanhas");
        }
        else
        {
            nomeCampanha = DataLoader.ObterDados("negociacoes_planos", "TestGlobalData", "nomeCampanha");
            tipoMidiaAtivo = DataLoader.ObterDados("negociacoes_planos", "TestGlobalData", "tipoMidiaAtivo");
            etapaNomeWorkflowPlanoEsperado = DataLoader.ObterDadosEmLista("negociacoes_planos", "TestGlobalData", "etapaNomeWorkflowPlanoEsperado");
            statusEsperado = DataLoader.ObterDados("negociacoes_planos", "TestGlobalData", "statusEsperado");
            farolEsperado = DataLoader.ObterDados("negociacoes_planos", "TestGlobalData", "farolEsperado");
        }
    }

    /// <summary>
    /// Método para realizar uma nova simulação de plano\contrato
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage NovaSimulacaoDePlano()
    {
        var mensagemConfirmacaoEsperadaReutilizarDadosSalvosAnteriormente = "Existemdadossalvosdaultimasimulação,desejareutiliza-los?";

        Dsl.Clicar(webDriver, GlobalVariables.CadastrarPlano, "Botão Novo Plano");


        if (Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.TituloModalConfirmacao) > 0)
        {
            ValidarMensagensDeModalDoPlano(mensagemConfirmacaoEsperadaReutilizarDadosSalvosAnteriormente);
            Dsl.Clicar(webDriver, GlobalVariables.CancelarAcao, "Botão Cancelar Reutilização Dados");
        }

        if (clienteUpSellAtual == ClienteUpSell.ClienteExpert)
            Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.FiltrarInventarios, "Botão Filtrar Inventarios");
        else
            Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.SelecionarAtivos, "Botão Selecionar Ativos");

        return this;
    }

    /// <summary>
    /// Métodos para preencher o campo Indústria
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage PreencherCampoIndustria()
    {
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.PreencherIndustria, "Campo Indústria");
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.PreencherIndustria, "Campo Indústria");

        if (clienteUpSellAtual == ClienteUpSell.ClienteStart)
        {
            Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.SelecionarIndustriaClienteStart, "Campo Indústria Lista");
            Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.PreencherIndustria, "Indústria 01 F", "Campo Indústria");
            Dsl.Clicar(webDriver, GlobalVariables.SelecionarIndustriaClienteStart, "Campo Indústria Selecionar");
        }
        else if (clienteUpSellAtual == ClienteUpSell.ClientePro)
        {
            Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.SelecionarIndustriaClientePro, "Campo Indústria Listar");
            Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.PreencherIndustria, "Indústria 01 F", "Campo Indústria");
            Dsl.Clicar(webDriver, GlobalVariables.SelecionarIndustriaClientePro, "Campo Indústria Selecionar");
        }
        else if (clienteUpSellAtual == ClienteUpSell.ClienteExpert)
        {
            Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.SelecionarIndustriaClientExpert, "Campo Indústria Listar");
            Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.PreencherIndustria, "Indústria 01 F", "Campo Indústria");
            Dsl.Clicar(webDriver, GlobalVariables.SelecionarIndustriaClientExpert, "Campo Indústria Selecionar");
        }

        return this;
    }

    /// <summary>
    /// Método para preencher o campo Campanha
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage PreencherCampoCampanha()
    {
        Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.PreencherCampanha, nomeCampanha);

        return this;
    }

    /// <summary>
    /// Método para filtrar inventários por tipo de mídia com lojas e ativos na nova tela de simulação
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage FiltrarInventarios()
    {
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.FiltrarInventarios, "Botão Filtrar Inventários");
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.FiltroTipoMidia, "Campo Tipo Midia");

        if (tipoMidiaAtivo.Equals("Grafica"))
        {
            Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.FiltroTipoMidia, tipoMidiaAtivo, "Campo Tipo Midia");
            Dsl.Clicar(webDriver, GlobalVariables.SelecionarTipoMidiaGrafica, "Seleciona Tipo Midia Filtrada");
            Dsl.Esperar(500);

            if (nomeTeste.Equals("TestCriarPlanoComAlertaDeInventario"))
            {
                foreach (var nomeLoja in nomeLojas)
                {
                    Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.FiltroLoja, nomeLoja, "Campo Loja");
                    Dsl.Esperar(500);
                    Dsl.Clicar(webDriver, GlobalVariables.SelecionarLojasInventario(nomeLoja), "Campo Selecionar Lojas");
                }

                Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.FiltroAtivos, ativosGraficos[0], "Campo Ativos");
                Dsl.Clicar(webDriver, GlobalVariables.SelecionarAtivosInventario(ativosGraficos[0]), "Campo Seleciona Ativo Filtrado");
            }
        }
        else if (tipoMidiaAtivo.Equals("Fisica"))
            Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.TipoMidia, tipoMidiaAtivo, "Campo Tipo Midia");

        Dsl.Clicar(webDriver, GlobalVariables.ConfirmarFiltroInventario, "Botão Confirmar Filtro Inventário");

        return this;
    }

    /// <summary>
    /// Método para filtrar e selecionar ativos no momento da simulação do plano
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage SelecionarAtivos()
    {
        switch (clienteUpSellAtual)
        {
            case ClienteUpSell.ClienteStart:
            case ClienteUpSell.ClientePro:
                Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarAtivos, "Botão Selecionar Ativos");

                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.FiltrarAtivos, "Botão Filtrar Ativo");
                Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.FiltrarAtivos, "Botão Filtrar Ativo");

                if (tipoMidiaAtivo.Equals("Grafica"))
                {
                    foreach (var nomeAtivo in ativosGraficos)
                    {
                        Dsl.Esperar();
                        Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.PesquisarAtivos, nomeAtivo);
                        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarAtivosFiltro, "Campo Seleciona Ativo Filtrado");
                        webDriver.FindElement(By.XPath(GlobalVariables.PesquisarAtivos)).SendKeys(Keys.Control + "a" + Keys.Backspace); //Apagando o nome do ativo do campo de pesquisa
                        Dsl.Clicar(webDriver, GlobalVariables.TelaFiltrarAtivo, "Tela Filtrar Ativos");
                    }
                }
                else if (tipoMidiaAtivo.Equals("Fisica"))
                {
                    foreach (var nomeAtivo in ativosFisicos)
                    {
                        Dsl.Esperar();
                        Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.PesquisarAtivos, nomeAtivo);
                        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarAtivosFiltro, "Campo Seleciona Ativo Filtrado");
                        webDriver.FindElement(By.XPath(GlobalVariables.PesquisarAtivos)).SendKeys(Keys.Control + "a" + Keys.Backspace); //Apagando o nome do ativo do campo de pesquisa
                        Dsl.Clicar(webDriver, GlobalVariables.TelaFiltrarAtivo, "Tela Filtrar Ativos");
                    }
                }

                Dsl.Clicar(webDriver, GlobalVariables.OkFiltroAtivos, "Botão OK Ativos Selecionados no Filtro");
                Dsl.Clicar(webDriver, GlobalVariables.SelecionarTodosAtivos, "Campo Selecionar Todos os Ativos");
                Dsl.Clicar(webDriver, GlobalVariables.AplicarAtivos, "Botão Aplicar Ativos Selecionados");
                break;
        }

        return this;
    }

    /// <summary>
    /// Método para preencher as quantidades dos ativos por loja no momento da simulação do plano
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage PreencherQuantidadeAtivos()
    {
        switch (clienteUpSellAtual)
        {
            case ClienteUpSell.ClienteStart:
                if (tipoMidiaAtivo.Equals("Grafica"))
                {
                    Dsl.ScrollParaElemento(webDriver, GlobalVariables.CarregarLojas, "Botão Carregar Lojas");
                    foreach (var nomeAtivo in ativosGraficos)
                    {
                        //Informando a quantidade de ativos por loja
                        var quantidade = webDriver.FindElement(By.XPath(GlobalVariables.QuantidadeAlocacaoAtivo(nomeAtivo)));
                        quantidade.SendKeys(Keys.Control + "a" + Keys.Backspace);
                        quantidade.SendKeys("5");
                    }
                }
                else if (tipoMidiaAtivo.Equals("Fisica"))
                {
                    Dsl.ScrollParaElemento(webDriver, GlobalVariables.CarregarLojas, "Botão Carregar Lojas");
                    foreach (var nomeAtivo in ativosFisicos)
                    {
                        var quantidade = webDriver.FindElement(By.XPath(GlobalVariables.QuantidadeAlocacaoAtivo(nomeAtivo)));
                        quantidade.SendKeys(Keys.Control + "a" + Keys.Backspace);
                        quantidade.SendKeys("5");
                    }
                }
                break;
            case ClienteUpSell.ClientePro:
                Dsl.ScrollParaElemento(webDriver, GlobalVariables.AplicarAceleradorPorLojaSimulacao, "Botão Aplicar Quantidade para Todas as Lojas na Simulação do Plano");
                Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.AceleradorQuantidadeAlocarSimulacao, "5");
                Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.AplicarAceleradorPorLojaSimulacao, "Botão Aplicar Quantidade para Todas as Lojas na Simulação do Plano");
                Dsl.Esperar(500);
                Dsl.ScrollParaElemento(webDriver, GlobalVariables.CarregarLojas, "Botão Carregar Lojas");
                break;
            case ClienteUpSell.ClienteExpert:
                if (nomeTeste.Equals("TestCriarPlanoComAtivosTipoMidiaGrafica"))
                {
                    Dsl.Clicar(webDriver, GlobalVariables.EditarAtivoGraficoAlocacaoSimulacao, "Botão Editar Ativo Alocado na Simulação");
                    Dsl.Esperar(500);
                    Dsl.Clicar(webDriver, GlobalVariables.EditarAtivoGraficoAlocadoSimulado, "Botão Editar Dados Ativo Alocado na Simulação");
                }
                else
                {
                    Dsl.Clicar(webDriver, GlobalVariables.FiltrarInventarios, "Botão Filtrar Inventários");
                    Dsl.Esperar(500);
                    Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.FiltroLoja, Keys.Backspace);
                    Dsl.Esperar(500);
                    Dsl.Clicar(webDriver, GlobalVariables.ConfirmarFiltroInventario, "Botão Confirmar Ativos Selecionados no Filtro");
                    Dsl.Esperar();
                    Dsl.Clicar(webDriver, GlobalVariables.EditarAtivoFisicoAlocacaoSimulacao, "Botão Editar Ativo Alocado na Simulação");
                    Dsl.Esperar(500);
                    Dsl.Clicar(webDriver, GlobalVariables.EditarAtivoFisicoAlocadoSimulado, "Botão Editar Dados Ativo Alocado na Simulação");
                }

                //Depois de alocar o ativo clicando no botão "Cadeado", a quantidade de alocação é replicada para todas as lojas e endereços do ativos selecionados através da tela "Configuração de Vigências e Valores"
                IWebElement headers = Dsl.EncontrarElemento(webDriver, GlobalVariables.ColunasAtivoAlocadoSimulado, "Colunas Infos Ativo Alocado na Simulação");
                IList<IWebElement> theads = headers.FindElements(By.XPath("th"));

                foreach (IWebElement colun in theads)
                {
                    string nomeColuna = colun.Text;
                    if (nomeColuna.Equals("Qtd Alocada"))
                    {
                        int index = theads.IndexOf(colun) + 1;
                        var campoQuantidade = Dsl.EncontrarElemento(webDriver, GlobalVariables.QuantidadeAtivoAlocadoModalInfos(index), "Campo Quantidade Ativo Alocado Modal Infos");

                        campoQuantidade.SendKeys(Keys.Control + "a");
                        Dsl.Esperar(500);
                        campoQuantidade.SendKeys("5");
                        Dsl.Esperar(500);

                        if (nomeTeste.Equals("TestCriarPlanoComAtivosTipoMidiaGrafica"))
                            Dsl.Clicar(webDriver, GlobalVariables.EditarAtivoGraficoAlocadoSimulado, "Botão Editar Dados Ativo Alocado na Simulação");
                        else
                            Dsl.Clicar(webDriver, GlobalVariables.EditarAtivoFisicoAlocadoSimulado, "Botão Editar Dados Ativo Alocado na Simulação");

                        Dsl.Clicar(webDriver, GlobalVariables.ReplicarConfiguracoes, "Botão Replicar para Selecionados");
                        break;
                    }
                }
                break;
        }

        return this;
    }

    /// <summary>
    /// Método para selecionar as lojas no momento da simulação do plano
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage SelecionarLojas()
    {
        switch (clienteUpSellAtual)
        {
            case ClienteUpSell.ClienteStart:
            case ClienteUpSell.ClientePro:
                Dsl.Clicar(webDriver, GlobalVariables.CarregarLojas, "Botão Carregar Lojas");
                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MenuLojas, "Menu Suspenso de Lojas");
                Dsl.ScrollParaElemento(webDriver, GlobalVariables.GerarPrePlano, "Botão Gerar Pré-Plano");

                var quantidadeLojasCarregadas = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.TabelaLojasPlano) - 1; //Contar linhas no elemento tbody da listagem de lojas carregadas na simulação do plano, ignorando a tag tr sem dados

                for (var i = 1; i <= quantidadeLojasCarregadas; i++)
                {
                    webDriver.FindElement(By.XPath($"//tbody/tr[{i + 1}]/td[9]//input[@class='ant-checkbox-input']")).Click();
                }
                break;
        }

        return this;
    }

    /// <summary>
    /// Método para selecionar lojas e ativos na nova tela de simulação
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage SelecionarAtivosELojas()
    {
        if (nomeTeste.Equals("TestCriarPlanoComAtivosTipoMidiaGrafica"))
        {
            Dsl.Clicar(webDriver, GlobalVariables.FiltrarInventarios, "Botão Filtrar Inventários");
            Dsl.Esperar(500);

            Dsl.Clicar(webDriver, GlobalVariables.FiltroTipoMidia, "Campo Tipo Mídia Listar");
            Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.FiltroTipoMidia, tipoMidiaAtivo, "Campo Tipo Midia");
            Dsl.Clicar(webDriver, GlobalVariables.SelecionarTipoMidiaGrafica, "Campo Tipo Midia Selecionar");

            foreach (var nomeLoja in nomeLojas)
            {
                Dsl.Esperar(500);
                Dsl.Clicar(webDriver, GlobalVariables.FiltroLoja, "Campo Loja Listar");
                Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.FiltroLoja, nomeLoja, "Campo Loja");
                Dsl.Clicar(webDriver, GlobalVariables.SelecionarLojasInventario(nomeLoja), "Campo Loja Selecionar");
            }

            foreach (var nomeAtivo in ativosGraficos)
            {
                Dsl.Esperar(500);
                Dsl.Clicar(webDriver, GlobalVariables.FiltroAtivos, "Campo Ativos Listar");
                Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.FiltroAtivos, nomeAtivo, "Campo Ativo");
                Dsl.Clicar(webDriver, GlobalVariables.SelecionarAtivosInventario(nomeAtivo), "Campo Ativo Selecionar");
            }

            Dsl.Clicar(webDriver, GlobalVariables.ConfirmarFiltroInventario, "Botão Confirmar Ativos Selecionados no Filtro");
            Dsl.Esperar();
            AlocarAtivosNaSimulacao();
        }
        else
        {
            foreach (string nomeLoja in nomeLojas)
            {
                Dsl.Clicar(webDriver, GlobalVariables.FiltrarInventarios, "Botão Filtrar Inventários");
                Dsl.Esperar(500);

                if (nomeLoja.Equals("Loja 01"))
                {
                    Dsl.Clicar(webDriver, GlobalVariables.FiltroTipoMidia, "Campo Tipo Mídia Listar");
                    Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.FiltroTipoMidia, tipoMidiaAtivo, "Campo Tipo Midia");
                    Dsl.Clicar(webDriver, GlobalVariables.SelecionarTipoMidiaFisica, "Campo Tipo Midia Selecionar");
                }

                if (Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.FiltroLojaPreenchido) > 0)
                {
                    Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.FiltroLoja, Keys.Backspace);
                }

                Dsl.Esperar(500);
                Dsl.Clicar(webDriver, GlobalVariables.FiltroLoja, "Campo Loja Listar");
                Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.FiltroLoja, nomeLoja, "Campo Loja");
                Dsl.Clicar(webDriver, GlobalVariables.SelecionarLojasInventario(nomeLoja), "Campo Loja Selecionar");

                if (nomeLoja.Equals("Loja 01"))
                {
                    foreach (var nomeAtivo in ativosFisicos)
                    {
                        Dsl.Esperar(500);
                        Dsl.Clicar(webDriver, GlobalVariables.FiltroAtivos, "Campo Ativos Listar");
                        Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.FiltroAtivos, nomeAtivo, "Campo Ativo");
                        Dsl.Clicar(webDriver, GlobalVariables.SelecionarAtivosInventario(nomeAtivo), "Campo Ativo Selecionar");
                    }
                }

                Dsl.Clicar(webDriver, GlobalVariables.ConfirmarFiltroInventario, "Botão Confirmar Ativos Selecionados no Filtro");
                Dsl.Esperar();
                AlocarAtivosNaSimulacao(nomeLoja);
            }
        }

        return this;
    }

    /// <summary>
    /// Método para alocar o inventário na nova tela de simulação
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage AlocarAtivosNaSimulacao([Optional] string nomeLoja)
    {
        if (nomeTeste.Equals("TestCriarPlanoComAtivosTipoMidiaGrafica"))
            Dsl.Clicar(webDriver, GlobalVariables.AlocarTodosAtivos, "Botão Alocar Todos os Ativos (Cadeado)");
        else
        {
            foreach (var inventario in inventarios)
            {
                Dsl.ScrollParaElemento(webDriver, GlobalVariables.AlocarAtivo(nomeLoja, inventario), "Botão Alocar Ativo (Cadeado)");
                Dsl.Clicar(webDriver, GlobalVariables.AlocarAtivo(nomeLoja, inventario), "Botão Alocar Ativo (Cadeado)");
                Dsl.Esperar(500);
            }
        }

        Dsl.Esperar();

        return this;
    }

    /// <summary>
    /// Método para gerar o pré-plano, clicando no botão Gera Pré-Plano
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage GerarPrePlano()
    {
        Dsl.ScrollParaElemento(webDriver, GlobalVariables.GerarPrePlano, "Botão Gerar Pré-Plano");
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.GerarPrePlano, "Botão Gerar Pré-Plano");

        if (clienteUpSellAtual == ClienteUpSell.ClientePro)
        {
            Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadProcurandoEtapa, "Load Procurando Etapa Workflow");
            Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.PreencherUsuarioResponsavelEtapaWorkflow, "UserHomolog02Pro", "Campo Usuário Responsável");
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarUsuarioResponsavelEtapaWorkflowPro, "Campo Usuário Responsável Selecionar");
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.GerarPrePlanoComWorkflowSelecionado, "Botão Gerar Pré-Plano com Workflow");
        }
        else if (clienteUpSellAtual == ClienteUpSell.ClienteExpert)
        {
            Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadProcurandoEtapa, "Load Procurando Etapa Workflow");
            Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.PreencherUsuarioResponsavelEtapaWorkflow, "UserHomolog02Expert", "Campo Usuário Responsável");
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarUsuarioResponsavelEtapaWorkflowExpert, "Campo Usuário Responsável Selecionar");
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.GerarPrePlanoComWorkflowSelecionado, "Botão Gerar Pré-Plano com Workflow");
        }

        return this;
    }

    /// <summary>
    /// Método para validar a tela de plano após a criação do mesmo
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage ValidarPlanoCriado()
    {
        if (clienteUpSellAtual == ClienteUpSell.ClienteStart)
        {
            for (int i = 0; i < abasPlanoEsperado.Count - 1; i++)
            {
                var nomeAbaPlano = abasPlanoEsperado[i];
                Dsl.Esperar(500);
                var xpathElemento = $"//div[contains(@class,'ant-tabs-tab')]/div[contains(text(),'{nomeAbaPlano}')]";
                Dsl.Clicar(webDriver, xpathElemento, "Abas Edição Plano");

                var tituloAbaAtual = Dsl.ObterTextoDoElemento(webDriver, xpathElemento, "Abas Edição Plano");
                var tituloAbaEsperado = nomeAbaPlano;

                Dsl.ValidarTextosNoElemento(tituloAbaAtual, tituloAbaEsperado);

                if (nomeAbaPlano.Equals("Anexos"))
                    Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.LoadDeTelaAnexos, "Load Aba Anexos");
                else if (nomeAbaPlano.Equals("Book Fotográfico"))
                    Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.LoadDeTelaBookFotografico, "Load Aba Book Fotográfico");
                else if (nomeAbaPlano.Equals("Painel da indústria"))
                    Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.LoadDeTelaPainelIndustria, "Load Aba Painel da Indústria");
            }
        }
        else
        {
            Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.EtapasWorkflow, "Display Etapas Workflow do Plano");
            IList<IWebElement> elementos = Dsl.ObterListaDeElementos(webDriver, GlobalVariables.EtapasWorkflowPlano);
            IList<string> nomesEtapasWorkflowPlanoAtual = elementos.Select(elementos => elementos.Text).ToList();

            Dsl.ScrollParaElemento(webDriver, GlobalVariables.EtapasWorkflowGraficoPlano, "Gráfico Etapas Workflow do Plano");
            Dsl.Esperar();
            Dsl.ScrollParaElemento(webDriver, GlobalVariables.AbasPlano, "Abas do Plano");

            foreach (var (nomeEtapaWorkflowAtual, nomeEtapaWorkflowEsperado) in nomesEtapasWorkflowPlanoAtual.Zip(etapaNomeWorkflowPlanoEsperado, (nomesEtapasWorkflowPlanoAtual, nomeEtapaWorkflowPlanoEsperado) => (nomesEtapasWorkflowPlanoAtual, nomeEtapaWorkflowPlanoEsperado)))
            {
                Dsl.ValidarTextosNoElemento(nomeEtapaWorkflowAtual, nomeEtapaWorkflowEsperado);
            }

            foreach (var nomeAbaPlano in abasPlanoEsperado)
            {
                Dsl.Esperar(500);
                var xpathElemento = $"//div[contains(@class,'ant-tabs-tab')]/div[contains(text(),'{nomeAbaPlano}')]";
                Dsl.Clicar(webDriver, xpathElemento, "Abas Edição Plano");

                var tituloAbaAtual = Dsl.ObterTextoDoElemento(webDriver, xpathElemento, "Abas Edição Plano");
                var tituloAbaEsperado = nomeAbaPlano;

                Dsl.ValidarTextosNoElemento(tituloAbaAtual, tituloAbaEsperado);

                if (nomeAbaPlano.Equals("Anexos"))
                    Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaAnexos, "Load Aba Anexos");
                else if (nomeAbaPlano.Equals("Book Fotográfico"))
                    Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaBookFotografico, "Load Aba Book Fotográfico");
                else if (nomeAbaPlano.Equals("Painel da indústria"))
                    Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaPainelIndustria, "Load Aba Painel da Indústria");
                else if (nomeAbaPlano.Equals("Tarefas"))
                    Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaTarefas, "Load Aba Tarefas");
            }
        }
        return this;
    }

    /// <summary>
    /// Método para validar status e farol na lista de planos, após criação ou alteração
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage ValidarStatusFarolDoPlano()
    {
        IWebElement headers = Dsl.EncontrarElemento(webDriver, GlobalVariables.ColunasPlanosCadastrados, "Colunas Infos Planos Cadastrados");
        IList<IWebElement> theads = headers.FindElements(By.XPath("th"));

        foreach (IWebElement colun in theads)
        {
            string nomeColuna = colun.Text;
            if (nomeColuna.Equals("Status"))
            {
                int index = theads.IndexOf(colun) + 1;
                var statusPlanoAtual = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.StatusPlano(index), "Coluna Status Plano");
                Assert.That(statusPlanoAtual, Does.Contain(statusEsperado), "Status atual não corresponde com o esperado");
            }

            if (clienteUpSellAtual != ClienteUpSell.ClienteStart)
            {
                if (nomeColuna.Equals("Farol"))
                {
                    int index = theads.IndexOf(colun) + 1;
                    var farolPlanoAtual = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.FarolPlano(index), "Coluna Farol Plano");
                    Assert.That(farolPlanoAtual, Does.Contain(farolEsperado), "Farol atual não corresponde com o esperado");
                }
            }
        }

        return this;
    }

    /// <summary>
    /// Método para recarregar planos
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage RecarregarPlanos()
    {
        Dsl.Clicar(webDriver, GlobalVariables.RecarregarTela, "Botão Recarregar Tela");
        Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadListaPlanos, "Load Tela Lista de Planos");

        return this;
    }

    /// <summary>
    /// Método para buscar planos
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage BuscarPlanos()
    {
        Dsl.BuscarRegistros(webDriver, GlobalVariables.FiltrarPlanoPorCampanha, GlobalVariables.PesquisarNomeCampanha, GlobalVariables.BuscarRegistro, nomeCampanha);

        return this;
    }

    /// <summary>
    /// Método para filtrar ativos alocados dentro do plano existente
    /// </summary>
    /// <param name="nomeAtivo"></param>
    /// <returns></returns>
    public PlanosContratosPage BuscarAtivosAlocadosNoPlano(string nomeAtivo)
    {
        Dsl.Clicar(webDriver, GlobalVariables.FiltrarAtivoAlocado, "Botão Filtrar Ativo");

        IWebElement element = webDriver.FindElement(By.XPath(GlobalVariables.PreencherNomeAtivo));
        element.SendKeys(Keys.Control + "a" + Keys.Backspace);

        Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.PreencherNomeAtivo, nomeAtivo);

        Dsl.Clicar(webDriver, GlobalVariables.BuscarAtivoAlocado, "Botão Buscar Ativo");
        Dsl.Esperar();

        return this;
    }

    /// <summary>
    /// Método para abrir a edição de um ativo alocado dentro do plano existente
    /// </summary>
    /// <param name="nomeAtivo"></param>
    /// <returns></returns>
    public PlanosContratosPage AbrirEdicaoDoAtivoAlocado(string nomeAtivo)
    {
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.EditarAtivoAlocado(nomeAtivo), "Botão Editar Ativo Alocado");
        Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaAlocacaoPorLoja, "Load Tela Alocação por Loja");

        return this;
    }

    /// <summary>
    /// Método para abrir a edição de um plano existente
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage AbrirEdicaoDoPlano()
    {
        if (nomeTeste.Equals("TestCriarPlanoComAtivosTipoMidiaFisica"))
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.EditarPlano((string)DataLoader.ObterDados("negociacoes_planos", "TestCriarPlanoComAtivosTipoMidiaFisica", "nomeCampanha")), "Botão Editar Plano");
        else
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.EditarPlano((string)DataLoader.ObterDados("negociacoes_planos", "TestGlobalData", "nomeCampanha")), "Botão Editar Plano");

        Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaDadosPlano, "Load Aba Dados do Plano");
        Dsl.Esperar(6000);

        if (clienteUpSellAtual == ClienteUpSell.ClienteExpert)
        {
            var modalInventarioIndisponivel = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.ModalInventarioIndisponivel);
            if (modalInventarioIndisponivel > 0)
            {
                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.ModalInventarioIndisponivel, "Modal Inventário Indisponível");
                Dsl.Esperar(500);
                Dsl.Clicar(webDriver, GlobalVariables.ModalInventarioIndisponivelOKButton, "Botão Fechar Modal Inventário Indisponível");
                Dsl.Esperar(500);
            }

            Dsl.Esperar(1000);
            Dsl.ScrollParaElemento(webDriver, GlobalVariables.MaisInformacoesPlano, "Menu Suspenso Mais Informações do Plano");
            Dsl.Clicar(webDriver, GlobalVariables.MaisInformacoesPlano, "Menu Suspenso Mais Informações do Plano");
        }

        return this;
    }

    /// <summary>
    /// Método para selecionar a vigencia do plano
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage SelecionarVigenciaDoPlano()
    {
        int avancarMesCalendarioFimVigenciaEm;
        int avancarMesCalendarioInicioVigenciaEm;

        Dsl.CalcularAvancoMesesVigencia(webDriver, out avancarMesCalendarioFimVigenciaEm, out avancarMesCalendarioInicioVigenciaEm);

        Dsl.Clicar(webDriver, GlobalVariables.FimVigenciaPlano, "Campo Fim Vigencia Plano");
        Dsl.Esperar();
        Dsl.AvancarMesesNoCalendario(webDriver, avancarMesCalendarioFimVigenciaEm);
        Dsl.PreencherCalendarios(webDriver, "Calendário Fim Vigência Plano");

        Dsl.Clicar(webDriver, GlobalVariables.InicioVigenciaPlano, "Campo Início Vigencia Plano");
        Dsl.Esperar();
        Dsl.AvancarMesesNoCalendario(webDriver, avancarMesCalendarioInicioVigenciaEm);
        Dsl.PreencherCalendarios(webDriver, "Calendário Início Vigência Plano");

        return this;
    }

    /// <summary>
    /// Método para selecionar a vigencia do trade
    /// </summary>
    /// <param name="inicioVigenciaTrade"></param>
    /// <param name="fimVigenciaTrade"></param>
    /// <returns></returns>
    public PlanosContratosPage SelecionarVigenciaDoTrade(IWebElement inicioVigenciaTrade, IWebElement fimVigenciaTrade, string nomeLoja = "")
    {
        int avancarMesCalendarioFimVigenciaEm;
        int avancarMesCalendarioInicioVigenciaEm;

        switch (clienteUpSellAtual)
        {
            case ClienteUpSell.ClienteStart:
                Dsl.CalcularAvancoMesesVigenciaTradeLoja(webDriver, nomeLoja, out avancarMesCalendarioFimVigenciaEm, out avancarMesCalendarioInicioVigenciaEm);
                Dsl.ClicarNoElementoId(fimVigenciaTrade, "Campo Fim Vigência do Trade Loja");
                Dsl.Esperar();
                Dsl.AvancarMesesNoCalendario(webDriver, avancarMesCalendarioFimVigenciaEm);
                Dsl.PreencherCalendarios(webDriver, "Calendário Fim Vigência Trade");

                Dsl.ClicarNoElementoId(inicioVigenciaTrade, "Campo Início Vigência do Trade Loja");
                Dsl.Esperar();
                Dsl.AvancarMesesNoCalendario(webDriver, avancarMesCalendarioInicioVigenciaEm);
                Dsl.PreencherCalendarios(webDriver, "Calendário Início Vigência Trade");
                break;
            case ClienteUpSell.ClientePro:
            case ClienteUpSell.ClienteExpert:
                Dsl.ClicarNoElementoId(fimVigenciaTrade, "Campo Fim Vigência do Trade Acelerador");
                Dsl.Esperar();
                Dsl.AvancarMesesNoCalendario(webDriver, 3);
                Dsl.PreencherCalendarios(webDriver, "Calendário Fim Vigência Trade Acelerador");

                Dsl.ClicarNoElementoId(inicioVigenciaTrade, "Campo Início Vigência do Trade Acelerador");
                Dsl.Esperar();
                Dsl.AvancarMesesNoCalendario(webDriver, 2);
                Dsl.PreencherCalendarios(webDriver, "Calendário Início Vigência Trade Acelerador");
                break;
        }

        return this;
    }

    /// <summary>
    /// Método para editar a vigência dos ativos alocados no plano
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage EditarVigenciaDoAtivoAlocado()
    {
        BuscarAtivosAlocadosNoPlano(ativosGraficos.FirstOrDefault());
        Dsl.Esperar();

        AbrirEdicaoDoAtivoAlocado(ativosGraficos.FirstOrDefault());
        Dsl.Esperar();

        EditarVigenciaLoja();

        SalvarAtivoAlocado();

        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.ConfirmarAlteracaoPeriodo, "Botão Entendi Modal Confirmar Alteração de Período no Ativo Alocado");

        return this;
    }

    /// <summary>
    /// Método para editar a vigência das lojas para os ativos alocados no plano
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage EditarVigenciaLoja()
    {
        switch (clienteUpSellAtual)
        {
            case ClienteUpSell.ClienteStart:
                Dsl.ScrollHorizontalDentroDoElementoTabela(webDriver, GlobalVariables.ScrollHorizontalTabelaLojasAtivoAlocados, GlobalVariables.ColunaVeiculacaoTradeCheckbox);

                foreach (var loja in nomeLojas)
                {
                    Dsl.ScrollHorizontalDentroDoElementoTabela(webDriver, GlobalVariables.ScrollHorizontalTabelaLojasAtivoAlocados, GlobalVariables.ColunaVeiculacaoTradeCheckbox);

                    IWebElement inicioVigencia = Dsl.EncontrarElemento(webDriver, GlobalVariables.InicioVigenciaLoja(loja), "Campo Início Vigência da Loja");
                    IWebElement fimVigencia = Dsl.EncontrarElemento(webDriver, GlobalVariables.FimVigenciaLoja(loja), "Campo Fim Vigência da Loja");

                    SelecionarVigenciaDoTrade(inicioVigencia, fimVigencia, loja);
                }
                break;
            case ClienteUpSell.ClientePro:
            case ClienteUpSell.ClienteExpert:

                IWebElement iniciovigencia = Dsl.EncontrarElemento(webDriver, GlobalVariables.AceleradorInicioVigenciaTrade, "Campo Início Vigência Trade no Acelerador");
                IWebElement fimvigencia = Dsl.EncontrarElemento(webDriver, GlobalVariables.AceleradorFimVigenciaTrade, "Campo Fim Vigência Trade no Acelerador");

                SelecionarVigenciaDoTrade(iniciovigencia, fimvigencia);

                Dsl.Clicar(webDriver, GlobalVariables.AplicarAceleradorPorLojaAtivoAlocado, "Botão Aplicar Vigência para Todas as Lojas");

                List<MensagemFeedback> mensagensAtuais = Dsl.ObterMensagensDeFeedback(webDriver, GlobalVariables.MensagemDeFeedback);

                ValidarMensagensDoPlano(mensagensAtuais);
                break;
        }

        return this;
    }

    /// <summary>
    /// Método para salvar os dados plano com diferentes status
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage SalvarPlano()
    {
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.AbaDadosPlano, "Aba Dados Plano");

        if (clienteUpSellAtual == ClienteUpSell.ClienteExpert || clienteUpSellAtual == ClienteUpSell.ClientePro)
            Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.EtapasWorkflow, "Display Etapas Workflow do Plano");

        Dsl.ScrollParaElemento(webDriver, GlobalVariables.SalvarPlano, "Botão Salvar Plano");
        Dsl.Clicar(webDriver, GlobalVariables.SalvarPlano, "Botão Salvar Plano");
        Dsl.Esperar(1000);

        if (!string.IsNullOrEmpty(nomeTeste) && nomeTeste.Equals("TestCancelarPlano"))
            ConfirmarCancelamentoDoPlano();

        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeFeedback, "Mensagem de Feedback Após Salvar Plano");
        List<MensagemFeedback> mensagensAtuais = Dsl.ObterMensagensDeFeedback(webDriver, GlobalVariables.MensagemDeFeedback);
        ValidarMensagensDoPlano(mensagensAtuais);
        Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeFeedback, "Mensagem de Feedback Após Salvar Plano");

        if (clienteUpSellAtual == ClienteUpSell.ClienteStart || clienteUpSellAtual == ClienteUpSell.ClientePro)
            Dsl.ScrollParaElemento(webDriver, GlobalVariables.ReceitaAtivos, "Campo Receita de Ativos");
        else if (clienteUpSellAtual == ClienteUpSell.ClienteExpert)
        {
            Dsl.ScrollParaElemento(webDriver, GlobalVariables.MaisInformacoesPlano, "Menu Suspenso Mais Informações do Plano");
            Dsl.Clicar(webDriver, GlobalVariables.MaisInformacoesPlano, "Menu Suspenso Mais Informações do Plano");
        }

        return this;
    }

    /// <summary>
    /// Método para salvar os dados do ativo alocado
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage SalvarAtivoAlocado()
    {
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SalvarAlocacaoLoja, "Botão Salvar Quantidades Alocadas do Ativo por Loja");
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.LoadDeTelaSpiner, "Load Tela Salvar Ativo Alocado");
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeFeedback, "Mensagem de Feedback Após Salvar Ativo Alocado");

        List<MensagemFeedback> mensagensAtuais = Dsl.ObterMensagensDeFeedback(webDriver, GlobalVariables.MensagemDeFeedback);

        ValidarMensagensDoPlano(mensagensAtuais);

        Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.LoadDeTelaSpiner, "Load Tela Salvar Ativo Alocado");
        Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeFeedback, "Mensagem de Feedback Após Salvar Ativo Alocado");

        Dsl.Esperar(5000);

        return this;
    }

    /// <summary>
    /// Método para fechar a tela com os dados do plano
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage FecharDadosDoPlano()
    {
        Dsl.Clicar(webDriver, GlobalVariables.FecharPlano, "Botão Fechar Plano");
        Dsl.Esperar();

        var modalInventarioIndisponivel = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.ModalInventarioIndisponivel);
        if (clienteUpSellAtual == ClienteUpSell.ClienteExpert && modalInventarioIndisponivel > 0)
        {
            Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.ModalInventarioIndisponivel, "Modal Inventário Indisponível");
            Dsl.Esperar(500);
            Dsl.Clicar(webDriver, GlobalVariables.ModalInventarioIndisponivelOKButton, "Botão Fechar Modal Inventário Indisponível");
            Dsl.Esperar(500);
        }

        if (Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.FecharPlanoConfirmacao) > 0)
            Dsl.Clicar(webDriver, GlobalVariables.FecharPlanoConfirmacao, "Botão Confirmar Fechar Plano");

        Dsl.Esperar();

        return this;
    }

    /// <summary>
    /// Método para fechar a tela de alocação por loja
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage FecharAlocacaoPorLoja()
    {
        Dsl.Clicar(webDriver, GlobalVariables.FecharAlocacaoAtivoPorLoja, "Botão Fechar Alocação Por Loja");
        Dsl.Esperar();

        return this;
    }

    /// <summary>
    /// Método para editar as quantidades dos ativos por loja
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage EditarQuantidadesDosAtivosNoPlano()
    {
        string nomeAtivoAlocadoEsperado;
        string nomeAtivoAlocadoAtual;
        IList<string> nomesAtivosAlocadosEsperados = ativosGraficos;

        switch (clienteUpSellAtual)
        {
            case ClienteUpSell.ClienteStart:
                foreach (var nomeAtivo in ativosGraficos)
                {
                    AbrirEdicaoDoAtivoAlocado(nomeAtivo);
                    Dsl.Esperar();

                    nomeAtivoAlocadoAtual = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.NomeAtivoAlocacao, "Campo Nome Ativo");
                    nomeAtivoAlocadoEsperado = nomeAtivo;

                    Dsl.ValidarTextosNoElemento(nomeAtivoAlocadoAtual, nomeAtivoAlocadoEsperado);

                    AumentarQuantidadeAtivosPorLoja();

                    Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.SalvarAlocacaoLoja, "Botão Salvar Quantidades Alocadas do Ativo por Loja");
                    SalvarAtivoAlocado();
                }
                break;
            case ClienteUpSell.ClientePro:
            case ClienteUpSell.ClienteExpert:
                nomeAtivoAlocadoEsperado = nomesAtivosAlocadosEsperados.FirstOrDefault();
                BuscarAtivosAlocadosNoPlano(nomeAtivoAlocadoEsperado);
                Dsl.Esperar();

                AbrirEdicaoDoAtivoAlocado(nomeAtivoAlocadoEsperado);
                Dsl.Esperar(3000);

                string textoQuantidadeLojasAtivoAlocado = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.QuantidadeLojasPorAtivo, "Label Quantidade de Lojas no Ativo Alocado");
                var quantidadeLojasAtivoAlocadoAtual = Dsl.RemoverLetrasEspacosDeUmTexto(textoQuantidadeLojasAtivoAlocado, "Label Quantidade de Lojas no Ativo Alocado");
                Dsl.ValidarNumerosNoElemento((int)quantidadeLojasAtivoAlocadoAtual, 20, "Label Quantidade de Loja no Ativo Alocado");

                nomeAtivoAlocadoAtual = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.NomeAtivoAlocacao, "Campo Nome Ativo");

                Dsl.ValidarTextosNoElemento(nomeAtivoAlocadoAtual, nomeAtivoAlocadoEsperado);

                AumentarQuantidadeAtivosPorLoja();

                Dsl.ScrollParaElemento(webDriver, GlobalVariables.SalvarAlocacaoLoja, "Botão Salvar Quantidades Alocadas do Ativo por Loja");
                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.SalvarAlocacaoLoja, "Botão Salvar Quantidades Alocadas do Ativo por Loja");
                SalvarAtivoAlocado();
                break;
        }


        return this;
    }

    /// <summary>
    /// Método para abrir a aba de Ativos Alocados
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage AbrirAbaAtivosAlocados()
    {
        Dsl.ScrollParaElemento(webDriver, GlobalVariables.AbaAtivosAlocados, "Aba Ativos Alocados");
        Dsl.Clicar(webDriver, GlobalVariables.AbaAtivosAlocados, "Aba Ativos Alocados");
        Dsl.Esperar(2000);

        return this;
    }

    /// <summary>
    /// Método para alocar um novo ativo para as lojas do plano
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage AlocarNovosAtivosNoPlano()
    {
        string ativoNome = DataLoader.ObterDados("negociacoes_planos", "TestGlobalData", "ativoNome");

        switch (clienteUpSellAtual)
        {
            case ClienteUpSell.ClienteStart:
                var xpathElemento = $"//div[@class='rc-virtual-list']//*[text()='{ativoNome}']";

                Dsl.Clicar(webDriver, GlobalVariables.IncluirAlocacaoAtivo, "Botão Incluir Ativo");
                Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.BuscarAtivoAlocacao, ativoNome, "Campo Buscar Ativo");
                Dsl.EsperarVisibilidadeDoElemento(webDriver, xpathElemento, "Campo Buscar Ativo Listar");
                Dsl.Clicar(webDriver, xpathElemento, "Campo Buscar Ativo Selecionar");

                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.LinhaTabelaLojasAtivoAlocados, "Lista de Lojas no Ativo Alocado Linha 1");
                Dsl.Esperar();

                AumentarQuantidadeAtivosPorLoja();

                Dsl.ScrollParaElemento(webDriver, GlobalVariables.SalvarAlocacaoLoja, "Botão Salvar Quantidades Alocadas do Ativo por Loja");
                break;
            case ClienteUpSell.ClientePro:
            case ClienteUpSell.ClienteExpert:
                Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.IncluirAlocacaoAtivo, "Botão Incluir Ativo");
                Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.BuscarAtivoAlocacao, ativoNome, "Campo Buscar Ativo");

                var elementoAtivoNome = $"//div[@class='rc-virtual-list']//*[text()='{ativoNome}']";
                Dsl.EsperarVisibilidadeDoElemento(webDriver, elementoAtivoNome, "Campo Buscar Ativo Listar");
                Dsl.Clicar(webDriver, elementoAtivoNome, "Campo Buscar Ativo Selecionar");
                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.LinhaTabelaLojasAtivoAlocados, "Lista de Lojas no Ativo Alocado Linha 1");
                Dsl.Esperar(2000);

                var textoQuantidadeLojasAtivoAlocado = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.QuantidadeLojasPorAtivo, "Label Quantidade de Lojas no Ativo Alocado");
                var quantidadeLojasAtivoAlocadoAtual = Dsl.RemoverLetrasEspacosDeUmTexto(textoQuantidadeLojasAtivoAlocado, "Label Quantidade de Lojas no Ativo Alocado");
                Dsl.ValidarNumerosNoElemento((int)quantidadeLojasAtivoAlocadoAtual, 20, "Label Quantidade de Loja no Ativo Alocado");

                AumentarQuantidadeAtivosPorLoja();

                Dsl.ScrollParaElemento(webDriver, GlobalVariables.SalvarAlocacaoLoja, "Botão Salvar Quantidades Alocadas do Ativo por Loja");
                break;
        }

        SalvarAtivoAlocado();

        return this;
    }

    /// <summary>
    /// Método para aumentar as quantidades de ativos alocados por loja
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage AumentarQuantidadeAtivosPorLoja()
    {
        var teste = TestContext.CurrentContext.Test.MethodName;

        switch (clienteUpSellAtual)
        {
            case ClienteUpSell.ClienteStart:
                string texto = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.QuantidadeLojasPorAtivo, "Campo Total Lojas por Ativo");
                var quantidadeAtivosAlocadosLoja = Dsl.RemoverLetrasEspacosDeUmTexto(texto, "Campo Total Lojas por Ativo"); //Descobrindo a quantidade de lojas no plano para o ativo alocado

                int qtd = (int)quantidadeAtivosAlocadosLoja;
                for (var i = 1; i <= qtd; i++)
                {
                    //Aumentando a quantidade de alocação por loja
                    webDriver.FindElement(By.XPath($"//tr[{i + 1}]/td[22]/div//span[@aria-label='Increase Value']")).Click();
                }
                break;
            case ClienteUpSell.ClientePro:
            case ClienteUpSell.ClienteExpert:
                if (teste.Equals("TestEditarPlanoExistenteIncluindoNovoAtivoDisponivel"))
                    Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.AceleradorQuantidadeAlocarTrade, "1");
                else
                    Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.AceleradorQuantidadeAlocarTrade, "6");

                Dsl.Clicar(webDriver, GlobalVariables.AplicarAceleradorPorLojaAtivoAlocado, "Botão Aplicar Quantidade para Todas as Lojas");

                List<MensagemFeedback> mensagensAtuais = Dsl.ObterMensagensDeFeedback(webDriver, GlobalVariables.MensagemDeFeedback);

                ValidarMensagensDoPlano(mensagensAtuais);
                break;
        }

        return this;
    }

    /// <summary>
    /// Método para alterar a situação do plano para aprovado ou cancelado preenchendo os campo obrigatórios
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage EditarSituacaoDoPlano()
    {
        if (nomeTeste.Equals("TestAprovarPlano"))
        {
            //var mensagemAlertaInformarParcelaEsperada = "Salveasparcelascomostatusdoplanosimuladoparaaprovaroplano!";

            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SituacaoPlano, "Campo Situação Plano");
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SituacaoPlanoAprovar, "Campo Selecionar Situação Plano");
            Dsl.Esperar();
        }
        else if (nomeTeste.Equals("TestCancelarPlano"))
        {
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SituacaoPlano, "Campo Situação do Plano");
            Dsl.Clicar(webDriver, GlobalVariables.SituacaoPlanoCancelar, "Selecionar Situação Plano Cancelado");
        }

        return this;
    }

    /// <summary>
    /// Método para validar mensagens de comunicação apresentadas nas ações do plano
    /// </summary>
    /// <param name="mensagensAtuais"></param>
    /// <returns></returns>
    public PlanosContratosPage ValidarMensagensDoPlano(List<MensagemFeedback> mensagensAtuais)
    {
        string mensagem;
        string mensagemTradada;

        foreach (var mensagemAtual in mensagensAtuais)
        {
            mensagem = mensagemAtual.Mensagem;
            mensagemTradada = Dsl.RemoverApenasNumeros(mensagem, "Mensagem de Feedback do Plano");
            mensagemAtual.Mensagem = mensagemTradada;
        }

        Dsl.ValidarMensagemDeFeedbacak(mensagensEsperadas, mensagensAtuais);

        return this;
    }

    /// <summary>
    /// Método para validar mensagens de confirmação apresentadas em modal nas ações do plano
    /// </summary>
    /// <param name="mensagemEsperada"></param>
    /// <returns></returns>
    public PlanosContratosPage ValidarMensagensDeModalDoPlano(string mensagemEsperada)
    {
        var texto = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.TituloModalConfirmacao, "Mensagens de Confirmação");
        var mensagemAtual = Dsl.RemoverNumerosEspacosDeUmTexto(texto, "Mensagens de Confirmação");

        Dsl.ValidarMensagensDeConfirmacao(mensagemAtual, mensagemEsperada);

        return this;
    }

    /// <summary>
    /// Método para preencher a data de cancelemento e confirmar o cancelamento do plano
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage ConfirmarCancelamentoDoPlano()
    {
        var mensagemConfirmacaoEsperadaCancelarPlano = "DatadeCancelamentodoPlano";

        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.DataCancelamentoPlano, "Campo Data Cancelamento do Plano");
        ValidarMensagensDeModalDoPlano(mensagemConfirmacaoEsperadaCancelarPlano);
        Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.OkCancelamento, "Botão OK Cancelamento");

        Dsl.Clicar(webDriver, GlobalVariables.DataCancelamentoPlano, "Campo Data Cancelamento");
        Dsl.Clicar(webDriver, GlobalVariables.SelecionarDataCancelamentoPlano, "Botão Today Cancelamento");
        Dsl.Clicar(webDriver, GlobalVariables.OkCancelamento, "Botão OK Cancelamento");

        return this;
    }

    /// <summary>
    /// Método para confirmar a exclusão de um plano, validando as mensagens de alerta e sucesso
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage ConfirmarExclusaoDoPlano()
    {
        var mensagemConfirmacaoEsperadaExcluirPlano = "DesejarealmenteexcluirestePlano?";
        foreach (var nomeCampanha in nomeCampanhas)
        {
            Dsl.BuscarRegistros(webDriver, GlobalVariables.FiltrarPlanoPorCampanha, GlobalVariables.PesquisarNomeCampanha, GlobalVariables.BuscarRegistro, nomeCampanha);
            var quantidadeLinhasTabela = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.TabelaPlanos) - 1; //Contar linhas no elemento tbody da listagem de planos, ignorando a tag tr sem dados

            if (Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.AvisoInexistenciaDados) == 0)
            {
                for (var i = 1; i <= quantidadeLinhasTabela; i++)
                {
                    Dsl.Clicar(webDriver, GlobalVariables.ExcluirPlano(nomeCampanha), "Botão Excluir Plano");

                    Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.TituloModalConfirmacao, "Modal Confirmação Exclusão do Plano");
                    ValidarMensagensDeModalDoPlano(mensagemConfirmacaoEsperadaExcluirPlano);

                    Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.OkExclusao, "Botão OK Exclusão");

                    Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeFeedback, "Mensagem de Feedback Após Exclusão do Plano");
                    List<MensagemFeedback> mensagensAtuais = Dsl.ObterMensagensDeFeedback(webDriver, GlobalVariables.MensagemDeFeedback);
                    ValidarMensagensDoPlano(mensagensAtuais);
                    Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeFeedback, "Mensagem de Feedback Após Exclusão do Plano");
                }
            }
        }

        return this;
    }

    /// <summary>
    /// Método para validar os alertas apresentados, para a indisponibildiade do ativo no inventário, ao selecionar a loja
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage ValidarIndisponibilidadeDeInventario()
    {
        switch (clienteUpSellAtual)
        {
            case ClienteUpSell.ClienteStart:
            case ClienteUpSell.ClientePro:
                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MensagemIndisponibilidadeInventario, "Mensagem Indisponibilidade Inventário Loja");

                var contadorMensagemAlertaAtual = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.MensagemIndisponibilidadeInventario);
                var contadorIconeAlertaAtual = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.InventarioAlerta);

                if (contadorMensagemAlertaAtual == 1 && contadorIconeAlertaAtual == 5)
                {
                    var mensagemAlertaEsperada = "Algumaslojasnãoteminventáriosuficientedisponível";
                    var textoMensagemAlertaAtual = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.MensagemIndisponibilidadeInventario, "Mensagem Indisponibilidade Inventário Loja");
                    var mensagemAlertaAtual = Dsl.RemoverNumerosEspacosDeUmTexto(textoMensagemAlertaAtual, "Mensagem Indisponibilidade Inventário Loja");
                    Dsl.ValidarMensagemDeSucessoEAlerta(mensagemAlertaAtual, mensagemAlertaEsperada);
                }
                else
                {
                    Assert.Fail("Mensagem de indisponibilidade ou ícone de alerta não foram apresentados corretamente: " + "\n" + "Mensagem: " + contadorMensagemAlertaAtual + " Alertas: " + contadorIconeAlertaAtual);
                }
                break;
            case ClienteUpSell.ClienteExpert:
                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MatrizSimulacaoVazia, "Matriz de Simulação Vazia");

                List<MensagemFeedback> mensagensAtuais = Dsl.ObterMensagensDeFeedback(webDriver, GlobalVariables.MensagemAvisoInventarioIndisponivel);

                ValidarMensagensDoPlano(mensagensAtuais);

                Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.FiltrarPorDisponibilidade, "Ocupado", "Campo Disponibilidade");
                Dsl.Clicar(webDriver, GlobalVariables.SelecionarFiltroDisponibilidadeOcupado, "Filtro Ocupado no Inventário");
                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MatrizSimulacao, "Matriz de Simulação Após Filtro por Ocupado");

                IWebElement element = Dsl.EncontrarElemento(webDriver, GlobalVariables.AlocarAtivoOcupado, "Botão Alocar Ativo Ocupado");
                Actions acao = new Actions(webDriver);
                acao.MoveToElement(element).Perform();
                Dsl.Esperar(500);

                mensagensAtuais = Dsl.ObterMensagensDeFeedback(webDriver, GlobalVariables.MensagemAvisoAtivoOcupado);

                ValidarMensagensDoPlano(mensagensAtuais);
                break;
        }

        return this;
    }

    /// <summary>
    /// Método para validar o valor de receita dos ativos e do plano
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage ValidarReceitasDoPlano()
    {
        var tipoAtributo = "value";
        Dsl.Esperar();

        try
        {
            switch (clienteUpSellAtual)
            {
                case ClienteUpSell.ClienteStart:
                    if (nomeTeste.Equals("TestCriarPlanoComAtivosTipoMidiaGrafica"))
                    {
                        var valorReceitaAtivosEsperado = 789.75;
                        var valorReceitaAtivos = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaAtivos, "Campo Receita Ativos", tipoAtributo);
                        var valorReceitaAtivosAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaAtivos, "Campo Receita Ativos");

                        var valorReceitaPlanoEsperado = 904.50;
                        var valorReceitaPlano = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaPlano, "Campo Receita Plano", tipoAtributo);
                        var valorReceitaPlanoAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaPlano, "Campo Receita Ativos");

                        Dsl.ValidarNumerosNoElemento(valorReceitaAtivosAtual, valorReceitaAtivosEsperado, "Campo Receita Ativos");
                        Dsl.ValidarNumerosNoElemento(valorReceitaPlanoAtual, valorReceitaPlanoEsperado, "Campo Receita Plano");
                    }
                    else if (nomeTeste.Equals("TestCriarPlanoComAtivosTipoMidiaFisica"))
                    {
                        var valorReceitaAtivosEsperado = 526.50;
                        var valorReceitaAtivos = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaAtivos, "Campo Receita Ativos", tipoAtributo);
                        var valorReceitaAtivosAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaAtivos, "Campo Receita Ativos");

                        var valorReceitaPlanoEsperado = 603.00;
                        var valorReceitaPlano = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaPlano, "Campo Receita Plano", tipoAtributo);
                        var valorReceitaPlanoAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaPlano, "Campo Receita Plano");

                        Dsl.ValidarNumerosNoElemento(valorReceitaAtivosAtual, valorReceitaAtivosEsperado, "Campo Receita Ativos");
                        Dsl.ValidarNumerosNoElemento(valorReceitaPlanoAtual, valorReceitaPlanoEsperado, "Campo Receita Plano");
                    }
                    else if (nomeTeste.Equals("TestEditarPlanoExistenteAlterandoQuantidadeAlocadaDoAtivoDisponivel"))
                    {
                        var valorReceitaAtivosEsperado = 947.70;
                        var valorReceitaAtivos = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaAtivos, "Campo Receita Ativos", tipoAtributo);
                        var valorReceitaAtivosAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaAtivos, "Campo Receita Ativos");

                        var valorReceitaPlanoEsperado = 1085.40;
                        var valorReceitaPlano = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaPlano, "Campo Receita Plano", tipoAtributo);
                        var valorReceitaPlanoAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaPlano, "Campo Receita Plano");

                        Dsl.ValidarNumerosNoElemento(valorReceitaAtivosAtual, valorReceitaAtivosEsperado, "Campo Receita Ativos");
                        Dsl.ValidarNumerosNoElemento(valorReceitaPlanoAtual, valorReceitaPlanoEsperado, "Campo Receita Plano");
                    }
                    else if (nomeTeste.Equals("TestEditarPlanoExistenteIncluindoNovoAtivoDisponivel"))
                    {
                        var valorReceitaAtivosEsperado = 1105.65;
                        var valorReceitaAtivos = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaAtivos, "Campo Receita Ativos", tipoAtributo);
                        var valorReceitaAtivosAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaAtivos, "Campo Receita Ativos");

                        var valorReceitaPlanoEsperado = 1251.00;
                        var valorReceitaPlano = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaPlano, "Campo Receita Plano", tipoAtributo);
                        var valorReceitaPlanoAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaPlano, "Campo Receita Plano");

                        Dsl.ValidarNumerosNoElemento(valorReceitaAtivosAtual, valorReceitaAtivosEsperado, "Campo Receita Ativos");
                        Dsl.ValidarNumerosNoElemento(valorReceitaPlanoAtual, valorReceitaPlanoEsperado, "Campo Receita Plano");
                    }
                    break;
                case ClienteUpSell.ClientePro:
                case ClienteUpSell.ClienteExpert:
                    if (nomeTeste.Equals("TestCriarPlanoComAtivosTipoMidiaGrafica"))
                    {
                        var valorReceitaAtivosEsperado = 3000.00;
                        var valorReceitaAtivos = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaAtivos, "Campo Receita Ativos", tipoAtributo);
                        var valorReceitaAtivosAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaAtivos, "Campo Receita Ativos");

                        var valorReceitaPlanoEsperado = 3300.00;
                        var valorReceitaPlano = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaPlano, "Campo Receita Plano", tipoAtributo);
                        var valorReceitaPlanoAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaPlano, "Campo Receita Plano");

                        Dsl.ValidarNumerosNoElemento(valorReceitaAtivosAtual, valorReceitaAtivosEsperado, "Campo Receita Ativos");
                        Dsl.ValidarNumerosNoElemento(valorReceitaPlanoAtual, valorReceitaPlanoEsperado, "Campo Receita Plano");
                    }
                    else if (nomeTeste.Equals("TestCriarPlanoComAtivosTipoMidiaFisica"))
                    {
                        var valorReceitaAtivosEsperado = 2110.00;
                        var valorReceitaAtivos = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaAtivos, "Campo Receita Ativos", tipoAtributo);
                        var valorReceitaAtivosAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaAtivos, "Campo Receita Ativos");

                        var valorReceitaPlanoEsperado = 2420.00;
                        var valorReceitaPlano = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaPlano, "Campo Receita Plano", tipoAtributo);
                        var valorReceitaPlanoAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaPlano, "Campo Receita Plano");

                        Dsl.ValidarNumerosNoElemento(valorReceitaAtivosAtual, valorReceitaAtivosEsperado, "Campo Receita Ativos");
                        Dsl.ValidarNumerosNoElemento(valorReceitaPlanoAtual, valorReceitaPlanoEsperado, "Campo Receita Plano");
                    }
                    else if (nomeTeste.Equals("TestEditarPlanoExistenteAlterandoQuantidadeAlocadaDoAtivoDisponivel"))
                    {
                        var valorReceitaAtivosEsperado = 3200.00;
                        var valorReceitaAtivos = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaAtivos, "Campo Receita Ativos", tipoAtributo);
                        var valorReceitaAtivosAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaAtivos, "Campo Receita Ativos");

                        var valorReceitaPlanoEsperado = 3520.00;
                        var valorReceitaPlano = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaPlano, "Campo Receita Plano", tipoAtributo);
                        var valorReceitaPlanoAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaPlano, "Campo Receita Plano");

                        Dsl.ValidarNumerosNoElemento(valorReceitaAtivosAtual, valorReceitaAtivosEsperado, "Campo Receita Ativos");
                        Dsl.ValidarNumerosNoElemento(valorReceitaPlanoAtual, valorReceitaPlanoEsperado, "Campo Receita Plano");
                    }
                    else if (nomeTeste.Equals("TestEditarPlanoExistenteIncluindoNovoAtivoDisponivel"))
                    {
                        var valorReceitaAtivosEsperado = 3800.00;
                        var valorReceitaAtivos = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaAtivos, "Campo Receita Ativos", tipoAtributo);
                        var valorReceitaAtivosAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaAtivos, "Campo Receita Ativos");

                        var valorReceitaPlanoEsperado = 4140.00;
                        var valorReceitaPlano = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaPlano, "Campo Receita Plano", tipoAtributo);
                        var valorReceitaPlanoAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaPlano, "Campo Receita Plano");

                        Dsl.ValidarNumerosNoElemento(valorReceitaAtivosAtual, valorReceitaAtivosEsperado, "Campo Receita Ativos");
                        Dsl.ValidarNumerosNoElemento(valorReceitaPlanoAtual, valorReceitaPlanoEsperado, "Campo Receita Plano");
                    }
                    break;
                default:
                    throw new InvalidOperationException("Contexto de teste inválido");
            }
        }
        catch (Exception ex)
        { throw new Exception(ex.Message + "\n"); }

        return this;
    }
}