using OpenQA.Selenium;

namespace MeuClienteWebTestProject;

/// <summary>
/// Classe com métodos específicos de manipulação\interação dos elementos, pertinentes a tela Planos da plataforma
/// </summary>
public class PlanosContratosPage
{
    private IWebDriver webDriver;
    private readonly string atributoTesteId;
    private readonly string nomeCampanha;
    private string tipoMidiaAtivo;
    private readonly string statusEsperado;
    private readonly string farolEsperado;
    private ClienteUpSell clienteUpSellAtual;
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
        atributoTesteId = DataLoader.ObterDados("negociacoes_planos", "TestGlobalData", "atributoTesteId");
        ativosGraficos = DataLoader.ObterDadosEmLista("negociacoes_planos", "TestGlobalData", "ativosGraficos");
        ativosFisicos = DataLoader.ObterDadosEmLista("negociacoes_planos", "TestGlobalData", "ativosFisicos");
        abasPlanoEsperado = DataLoader.ObterDadosEmLista("negociacoes_planos", "TestGlobalData", "abasPlanoEsperado");
        mensagensEsperadas = DataLoader.ObterMensagensDeFeedback("negociacoes_planos", "TestGlobalData", "mensagensDeFeedback");

        var nomeTeste = TestContext.CurrentContext.Test.MethodName;
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

        Dsl.Clicar(webDriver, GlobalVariables.NovoRegistro, "Botão Nova Simulação");
        Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.SelecionarAtivos, "Botão Selecionar Ativos");

        if (Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.TituloModalConfirmacao) > 0)
        {
            ValidarMensagensDeModalDoPlano(mensagemConfirmacaoEsperadaReutilizarDadosSalvosAnteriormente);
            Dsl.Clicar(webDriver, GlobalVariables.CancelarAcao, "Botão Cancelar Reutilização Dados");
        }

        return this;
    }

    /// <summary>
    /// Métodos para preencher o campo Indústria
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage PreencherCampoIndustria()
    {
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.PreencherIndustria);
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.PreencherIndustria, "Campo Indústria");

        if (clienteUpSellAtual == ClienteUpSell.ClienteStart)
        {
            Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.SelecionarIndustriaClienteStart);
            Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.PreencherIndustria, "Indústria 01 F");
            Dsl.Clicar(webDriver, GlobalVariables.SelecionarIndustriaClienteStart, "Campo Selecionar Indústria");
        }
        else if (clienteUpSellAtual == ClienteUpSell.ClientePro)
        {
            Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.SelecionarIndustriaClientePro);
            Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.PreencherIndustria, "Indústria 01 F");
            Dsl.Clicar(webDriver, GlobalVariables.SelecionarIndustriaClientePro, "Campo Selecionar Indústria");
        }
        else if (clienteUpSellAtual == ClienteUpSell.ClienteExpert)
        {
            Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.SelecionarIndustriaClientExpert);
            Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.PreencherIndustria, "Indústria 01 F");
            Dsl.Clicar(webDriver, GlobalVariables.SelecionarIndustriaClientExpert, "Campo Selecionar Indústria");
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
    /// Método para filtrar e selecionar os ativos
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage SelecionarAtivos()
    {
        List<string> lojas = DataLoader.ObterDadosEmLista("negociacoes_planos", "TestGlobalData", "lojas");

        switch (clienteUpSellAtual)
        {
            case ClienteUpSell.ClienteStart:
            case ClienteUpSell.ClientePro:
                Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarAtivos, "Botão Selecionar Ativos");

                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.FiltrarAtivos);
                Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.FiltrarAtivos, "Botão Filtrar Ativo");

                if (tipoMidiaAtivo.Equals("Grafica"))
                {
                    foreach (var nomeAtivo in ativosGraficos)
                    {
                        Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.PesquisarAtivos, nomeAtivo);
                        Dsl.Esperar();
                        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarAtivosFiltro, "Campo Seleciona Ativo Filtrado");
                        webDriver.FindElement(By.XPath(GlobalVariables.PesquisarAtivos)).SendKeys(Keys.Control + "a" + Keys.Backspace); //Apagando o nome do ativo do campo de pesquisa
                        Dsl.Clicar(webDriver, GlobalVariables.TelaFiltrarAtivo, "Tela Filtrar Ativos");
                    }
                }
                else if (tipoMidiaAtivo.Equals("Fisica"))
                {
                    foreach (var nomeAtivo in ativosFisicos)
                    {
                        Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.PesquisarAtivos, nomeAtivo);
                        Dsl.Esperar();
                        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarAtivosFiltro, "Campo Seleciona Ativo Filtrado");
                        webDriver.FindElement(By.XPath(GlobalVariables.PesquisarAtivos)).SendKeys(Keys.Control + "a" + Keys.Backspace); //Apagando o nome do ativo do campo de pesquisa
                        Dsl.Clicar(webDriver, GlobalVariables.TelaFiltrarAtivo, "Tela Filtrar Ativos");
                    }
                }

                Dsl.Clicar(webDriver, GlobalVariables.OkFiltroAtivos, "Botão OK Ativos Selecionados no Filtro");
                Dsl.Clicar(webDriver, GlobalVariables.SelecionarTodosAtivos, "Campo Selecionar Todos os Ativos");
                Dsl.Clicar(webDriver, GlobalVariables.AplicarAtivos, "Botão Aplicar Ativos Selecionados");
                break;
            case ClienteUpSell.ClienteExpert:
                Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.FiltrarInvetario, "Botão Filtrar Invetários");
                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.TipoMidia);

                if (tipoMidiaAtivo.Equals("Grafica"))
                {
                    Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.TipoMidia, tipoMidiaAtivo);
                    Dsl.Esperar(500);
                    Dsl.Clicar(webDriver, GlobalVariables.SelecionarTipoMidiaGrafica, "Campo Seleciona Tipo Midia Filtrada");
                    Dsl.Clicar(webDriver, GlobalVariables.TelaFiltrarInventario, "Tela Filtrar Inventário");

                    Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.Loja, lojas[0]);
                    Dsl.Esperar(500);
                    Dsl.Clicar(webDriver, GlobalVariables.Loja, "Campo Seleciona Loja Filtrada");

                    foreach (var nomeAtivo in ativosGraficos)
                    {
                        Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.TipoMidia, nomeAtivo);
                        Dsl.Esperar(500);
                        string xpathElemento = $"//div[@class='rc-virtual-list']//*[text()='{nomeAtivo}']";
                        Dsl.Clicar(webDriver, xpathElemento, "Campo Seleciona Ativo Filtrado");
                        webDriver.FindElement(By.XPath(GlobalVariables.PesquisarAtivos)).SendKeys(Keys.Control + "a" + Keys.Backspace); //Apagando o nome do ativo do campo de pesquisa
                    }

                    Dsl.Clicar(webDriver, GlobalVariables.TelaFiltrarInventario, "Tela Filtrar Inventário");
                }
                else if (tipoMidiaAtivo.Equals("Fisica"))
                {
                    Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.TipoMidia, tipoMidiaAtivo);
                    Dsl.Esperar(500);
                    Dsl.Clicar(webDriver, GlobalVariables.SelecionarTipoMidiaGrafica, "Campo Seleciona Tipo Midia Filtrada");
                    Dsl.Clicar(webDriver, GlobalVariables.TelaFiltrarInventario, "Tela Filtrar Inventário");

                    foreach (var nomeAtivo in ativosFisicos)
                    {
                        Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.TipoMidia, nomeAtivo);
                        Dsl.Esperar(500);
                        string xpathElemento = $"//div[@class='rc-virtual-list']//*[text()='{nomeAtivo}']";
                        Dsl.Clicar(webDriver, xpathElemento, "Campo Seleciona Ativo Filtrado");
                        webDriver.FindElement(By.XPath(GlobalVariables.PesquisarAtivos)).SendKeys(Keys.Control + "a" + Keys.Backspace); //Apagando o nome do ativo do campo de pesquisa
                    }

                    Dsl.Clicar(webDriver, GlobalVariables.TelaFiltrarInventario, "Tela Filtrar Inventário");
                }

                Dsl.Clicar(webDriver, GlobalVariables.ConfirmarFiltroInventario, "Botão Confirmar Ativos Selecionados no Filtro");
                break;
        }

        return this;
    }

    /// <summary>
    /// Método para preencher as quantidades dos ativos por loja
    /// </summary>
    /// <param name="contextoDeTestes"></param>
    /// <returns></returns>
    public PlanosContratosPage PreencherQuantidadeAtivos()
    {
        switch (clienteUpSellAtual)
        {
            case ClienteUpSell.ClienteStart:
                if (tipoMidiaAtivo.Equals("Grafica"))
                {
                    Dsl.ScrollParaElemento(webDriver, GlobalVariables.CarregarLojas);
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
                    Dsl.ScrollParaElemento(webDriver, GlobalVariables.CarregarLojas);
                    foreach (var nomeAtivo in ativosFisicos)
                    {
                        var quantidade = webDriver.FindElement(By.XPath(GlobalVariables.QuantidadeAlocacaoAtivo(nomeAtivo)));
                        quantidade.SendKeys(Keys.Control + "a" + Keys.Backspace);
                        quantidade.SendKeys("5");
                    }
                }
                break;
            case ClienteUpSell.ClientePro:
                Dsl.ScrollParaElemento(webDriver, GlobalVariables.AplicarAceleradorPorLojaSimulacao);
                Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.AceleradorQuantidadeAlocarSimulacao, "5");
                Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.AplicarAceleradorPorLojaSimulacao, "Botão Aplicar Quantidade para Todas as Lojas na Simulação do Plano");
                Dsl.Esperar(500);
                Dsl.ScrollParaElemento(webDriver, GlobalVariables.CarregarLojas);
                break;
            case ClienteUpSell.ClienteExpert:
                break;
                /*case "SemPlantaLoja":
                        if (tipoMidiaAtivo.Equals("Grafica"))
                        {
                            Dsl.ScrollParaElemento(webDriver, GlobalVariables.CarregarLojas);
                            foreach (var nomeAtivo in ativosGraficos)
                            {
                                for (var i = 0; i < 4; i++)
                                {
                                    //Informando a quantidade de ativos por loja
                                    webDriver.FindElement(By.XPath($"//*[text()='{nomeAtivo}']/following-sibling::td[13]//span[@aria-label='Increase Value']")).Click();
                                }
                            }
                        }
                        else if (tipoMidiaAtivo.Equals("Fisica"))
                        {
                            Dsl.ScrollParaElemento(webDriver, GlobalVariables.CarregarLojas);
                            foreach (var nomeAtivo in ativosFisicos)
                            {
                                for (var i = 0; i < 4; i++)
                                {
                                    //Informando a quantidade de ativos por loja
                                    webDriver.FindElement(By.XPath($"//*[text()='{nomeAtivo}']/following-sibling::td[13]//span[@aria-label='Increase Value']")).Click();
                                }
                            }
                        }
                        break;*/
        }

        return this;
    }


    /// <summary>
    /// Método para selecionar as lojas carregadas na simulação do plano, para alocação dos ativos
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage SelecionarLojas()
    {
        List<string> lojas = DataLoader.ObterDadosEmLista("negociacoes_planos", "TestGlobalData", "lojas");

        switch (clienteUpSellAtual)
        {
            case ClienteUpSell.ClienteStart:
            case ClienteUpSell.ClientePro:
                Dsl.Clicar(webDriver, GlobalVariables.CarregarLojas, "Botão Carregar Lojas");
                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MenuLojas);
                Dsl.ScrollParaElemento(webDriver, GlobalVariables.GerarPrePlano);

                var quantidadeLojasCarregadas = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.TabelaLojasPlano) - 1; //Contar linhas no elemento tbody da listagem de lojas carregadas na simulação do plano, ignorando a tag tr sem dados

                for (var i = 1; i <= quantidadeLojasCarregadas; i++)
                {
                    webDriver.FindElement(By.XPath($"//tbody/tr[{i + 1}]/td[9]//input[@class='ant-checkbox-input']")).Click();
                }
                /*for (var i = 1; i <= lojas.Count; i++)
                {
                    var lojaAtual = webDriver.FindElements(By.XPath(GlobalVariables.SelecionarLojaCheckbox(lojas[i - 1])));
                    if (lojaAtual.Count > 0) lojaAtual[0].Click();
                }*/
                break;
            case ClienteUpSell.ClienteExpert:
                foreach (var loja in lojas)
                {
                    for (var j = 1; j <= 12; j++)
                    {
                        string xpathElemento = $"(//span[@aria-label='unlock'])[{j}]";
                        webDriver.FindElement(By.XPath(xpathElemento)).Click();
                        Dsl.Esperar(500);
                    }

                    Dsl.Clicar(webDriver, GlobalVariables.FiltrarInvetario, "Botão Filtrar Invetários");
                    Dsl.Esperar(500);
                    Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.Loja, loja);
                    Dsl.Clicar(webDriver, GlobalVariables.ConfirmarFiltroInventario, "Botão Confirmar Ativos Selecionados no Filtro");
                    Dsl.Esperar();
                }
                break;

        }

        return this;
    }

    /// <summary>
    /// Método para gerar o pré-plano, clicando no botão Gera Pré-Plano
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage GerarPrePlano()
    {
        Dsl.ScrollParaElemento(webDriver, GlobalVariables.GerarPrePlano);
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.GerarPrePlano, "Botão Gerar Pré-Plano");

        if (clienteUpSellAtual == ClienteUpSell.ClientePro)
        {
            Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadProcurandoEtapa);
            Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.PreencherUsuarioResponsavelEtapaWorkflow, "UserHomolog02Pro");
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarUsuarioResponsavelEtapaWorkflowPro, "Campo Selecionar Usuário Responsável");
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.GerarPrePlanoComWorkflowSelecionado, "Botão Gerar Pré-Plano com Workflow");
        }
        else if (clienteUpSellAtual == ClienteUpSell.ClienteExpert)
        {
            Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadProcurandoEtapa);
            Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.PreencherUsuarioResponsavelEtapaWorkflow, "UserHomolog02Expert");
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarUsuarioResponsavelEtapaWorkflowExpert, "Campo Selecionar Usuário Responsável");
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.GerarPrePlanoComWorkflowSelecionado, "Botão Gerar Pré-Plano com Workflow");
        }

        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.SalvarRegistro);

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
                    Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaAnexos);
                else if (nomeAbaPlano.Equals("Book Fotográfico"))
                    Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaBookFotografico);
                else if (nomeAbaPlano.Equals("Painel da indústria"))
                    Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaPainelIndustria);
            }
        }
        else
        {
            Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.EtapasWorkflow);
            IList<IWebElement> elementos = Dsl.ObterListaDeElementos(webDriver, GlobalVariables.EtapasWorkflowPlano);
            IList<string> nomesEtapasWorkflowPlanoAtual = elementos.Select(elementos => elementos.Text).ToList();

            Dsl.ScrollParaElemento(webDriver, GlobalVariables.EtapasWorkflowGraficoPlano);
            Dsl.Esperar();
            Dsl.ScrollParaElemento(webDriver, GlobalVariables.AbasPlano);

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
                    Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaAnexos);
                else if (nomeAbaPlano.Equals("Book Fotográfico"))
                    Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaBookFotografico);
                else if (nomeAbaPlano.Equals("Painel da indústria"))
                    Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaPainelIndustria);
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
        var statusPlanoAtual = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.StatusPlano, "Coluna Status Plano");
        Assert.That(statusPlanoAtual, Does.Contain(statusEsperado), "Status atual não corresponde com o esperado");

        var farolPlanoAtual = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.FarolPlano, "Coluna Farol Plano");
        Assert.That(farolPlanoAtual, Does.Contain(farolEsperado), "Farol atual não corresponde com o esperado");

        return this;
    }

    /// <summary>
    /// Método para recarregar planos
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage RecarregarPlanos()
    {
        Dsl.Clicar(webDriver, GlobalVariables.RecarregarTela, "Botão Recarregar Tela");
        Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadListaPlanos);

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
    /// Método para abrir a edição de um plano existente
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage AbrirEdicaoDoPlano()
    {
        var teste = TestContext.CurrentContext.Test.MethodName;

        if (teste.Equals("TestCriarPlanoComAtivosTipoMidiaFisica"))
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.EditarPlano((string)DataLoader.ObterDados("negociacoes_planos", "TestCriarPlanoComAtivosTipoMidiaFisica", "nomeCampanha")), "Botão Editar Plano");
        else
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.EditarPlano((string)DataLoader.ObterDados("negociacoes_planos", "TestGlobalData", "nomeCampanha")), "Botão Editar Plano");

        Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaDadosPlano);
        Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.Desconto, "Botão Abrir Modal Desconto");

        return this;
    }

    /// <summary>
    /// Método para selecionar a vigencia do plano
    /// </summary>
    /// <param name="contextoDeExecucao"></param>
    /// <returns></returns>
    public PlanosContratosPage SelecionarVigenciaDoPlano()
    {
        int avancarMesCalendarioEm = 2;

        var teste = TestContext.CurrentContext.Test.MethodName;

        if (teste.Equals("TestCriarPlanoComAlertaDeInventario"))
        {
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.FimVigenciaSimulacao, "Campo Fim Vigencia Novo Plano");
            Dsl.PreencherCalendariosFimVigencia(webDriver, avancarMesCalendarioEm - 1);

            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.InicioVigenciaSimulacao, "Campo Início Vigencia Novo Plano");
            Dsl.PreencherCalendariosInicioVigencia(webDriver, avancarMesCalendarioEm - 1);
        }
        else if (teste.Equals("TestEditarPlanoExistenteAlterandoVigenciaDoPlano"))
        {
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.InicioVigenciaPlano, "Campo Início Vigencia Editar Plano");
            Dsl.PreencherCalendariosInicioVigencia(webDriver, avancarMesCalendarioEm);

            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.FimVigenciaPlano, "Campo Fim Vigencia Editar Plano");
            Dsl.PreencherCalendariosFimVigencia(webDriver, avancarMesCalendarioEm);
        }

        return this;
    }

    /// <summary>
    /// Método para selecionar a vigencia do trade
    /// </summary>
    /// <param name="colunaTr"></param>
    /// <param name="avancoCalendarioInicioVigencia"></param>
    /// <param name="avancoCalendarioFimVigencia"></param>
    /// <returns></returns>
    public PlanosContratosPage SelecionarVigenciaDoTrade(IWebElement inicioVigenciaTrade, IWebElement fimVigenciaTrade)
    {
        switch (clienteUpSellAtual)
        {
            case ClienteUpSell.ClienteStart:
                var avancarMesCalendarioEm = 2;

                Dsl.ClicarNoElementoId(webDriver, fimVigenciaTrade, "Campo Fim Vigência do Trade");
                Dsl.PreencherCalendariosFimVigencia(webDriver, avancarMesCalendarioEm);

                Dsl.ClicarNoElementoId(webDriver, inicioVigenciaTrade, "Campo Início Vigência do Trade");
                Dsl.PreencherCalendariosInicioVigencia(webDriver, avancarMesCalendarioEm);
                break;
            case ClienteUpSell.ClientePro:
            case ClienteUpSell.ClienteExpert:
                var avancarMesCalendarioInicioVigenciaEm = 2;
                var avancarMesCalendarioFimVigenciaEm = 3;

                Dsl.ClicarNoElementoId(webDriver, fimVigenciaTrade, "Campo Fim Vigência do Trade");
                Dsl.PreencherCalendariosFimVigencia(webDriver, avancarMesCalendarioFimVigenciaEm);

                Dsl.ClicarNoElementoId(webDriver, inicioVigenciaTrade, "Campo Início Vigência do Trade");
                Dsl.PreencherCalendariosInicioVigencia(webDriver, avancarMesCalendarioInicioVigenciaEm);
                break;
        }

        return this;
    }

    public PlanosContratosPage EditarVigenciaDoAtivoAlocado()
    {
        var quantidadeAtivosAlocados = Dsl.ObterQuantidadeLinhasNoElementoTabelaComLinhaInvisivel(webDriver, GlobalVariables.TabelaAtivosPlano);
        string editarAtivo;

        switch (clienteUpSellAtual)
        {
            case ClienteUpSell.ClienteStart:
                for (var i = 1; i <= quantidadeAtivosAlocados; i++)
                {
                    editarAtivo = $"//tr[{i + 1}]//button/span[@aria-label='edit']";

                    Dsl.EsperarElementoParaClicar(webDriver, editarAtivo, "Botão Editar Ativo");

                    Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaAlocacaoPorLoja);

                    EditarVigenciaLoja();

                    SalvarAtivoAlocado();
                }
                break;
            case ClienteUpSell.ClientePro:
            case ClienteUpSell.ClienteExpert:
                BuscarAtivosAlocadosNoPlano(ativosGraficos[0]);
                Dsl.Esperar();

                editarAtivo = "//tr[2]//button/span[@aria-label='edit']";

                Dsl.EsperarElementoParaClicar(webDriver, editarAtivo, "Botão Editar Ativo");

                Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaSpiner);
                Dsl.Esperar();

                EditarVigenciaLoja();

                SalvarAtivoAlocado();
                break;
        }

        return this;
    }

    public PlanosContratosPage EditarVigenciaLoja()
    {

        switch (clienteUpSellAtual)
        {
            case ClienteUpSell.ClienteStart:
                Dsl.ScrollHorizontalDentroDoElementoTabela(webDriver, GlobalVariables.ScrollHorizontalTabelaLojasAtivoAlocados, GlobalVariables.ColunaVeiculacaoTradeCheckbox);
                IList<IWebElement> linhas = Dsl.ObterLinhasDoElementoTabela(webDriver, GlobalVariables.TabelaLojasAtivoAlocados);

                foreach (IWebElement linha in linhas)
                {
                    var valorAtributo = Dsl.ObterDadosDoAtributoDoElementoId(linha, "Lojas Alocadas No Ativo", "aria-hidden");
                    IList<IWebElement> colunas = Dsl.ObterColunasDoElementoTabela(linha);

                    if (valorAtributo == null || valorAtributo != "true")
                    {
                        Dsl.ScrollHorizontalDentroDoElementoTabela(webDriver, GlobalVariables.ScrollHorizontalTabelaLojasAtivoAlocados, GlobalVariables.ColunaVeiculacaoTradeCheckbox);

                        var inicioVigencia = colunas[7]; //mudar futuramente para pegar o xpath da vigencia inicioVigencia-<NomeLoja>
                        var fimVigencia = colunas[8]; //mudar futuramente para pegar o xpath da vigencia fimVigencia-<NomeLoja>

                        SelecionarVigenciaDoTrade(inicioVigencia, fimVigencia);
                    }
                }
                break;
            case ClienteUpSell.ClientePro:
            case ClienteUpSell.ClienteExpert:

                IWebElement iniciovigencia = Dsl.EncontrarElemento(webDriver, GlobalVariables.AceleradorInicioVigenciaTrade, "Campo Início Vigência no Acelerador");
                IWebElement fimvigencia = Dsl.EncontrarElemento(webDriver, GlobalVariables.AceleradorFimVigenciaTrade, "Campo Fim Vigência no Acelerador");

                SelecionarVigenciaDoTrade(iniciovigencia, fimvigencia);

                Dsl.Clicar(webDriver, GlobalVariables.AplicarAceleradorPorLojaAtivoAlocado, "Botão Aplicar Vigência para Todas as Lojas");

                List<MensagemFeedback> mensagensAtuais = Dsl.ObterMensagensDeFeedback(webDriver, GlobalVariables.MensagemDeFeedback);

                Dsl.ValidarMensagemDeFeedbacak(mensagensEsperadas, mensagensAtuais);
                break;
        }

        return this;
    }


    /// <summary>
    /// Método para salvar os dados plano com diferentes status
    /// </summary>
    /// <param name="contextoDeExecucao"></param>
    /// <param name="contextoDeTeste"></param>
    /// <returns></returns>
    public PlanosContratosPage SalvarPlano()
    {
        var mensagemPlanoAlteradoEsperada = "OPlanofoialteradocomsucesso!";
        var teste = TestContext.CurrentContext.Test.MethodName;

        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.AbaDadosPlano, "Aba Dados Plano");
        Dsl.ScrollParaElemento(webDriver, GlobalVariables.SalvarRegistro);
        Dsl.Clicar(webDriver, GlobalVariables.SalvarRegistro, "Botão Salvar Plano");

        if (!string.IsNullOrEmpty(teste) && teste.Equals("TestCancelarPlano"))
            ConfirmarCancelamentoDoPlano();

        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);
        ValidarMensagensDoPlano(mensagemPlanoAlteradoEsperada);
        Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);

        return this;
    }

    /// <summary>
    /// Método para salvar os dados do ativo alocado
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage SalvarAtivoAlocado()
    {
        var mensagemSucessoEsperadaAlocacaoAtualizada = "Alocaçãoatualizadacomsucesso!";

        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.SalvarAlocacaoLoja);
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SalvarAlocacaoLoja, "Botão Salvar Quantidades Alocadas do Ativo por Loja");

        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.LoadDeTelaSpiner);
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);

        ValidarMensagensDoPlano(mensagemSucessoEsperadaAlocacaoAtualizada);

        Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.LoadDeTelaSpiner);
        Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);

        return this;
    }

    /// <summary>
    /// Método para fechar a tela com os dados do plano
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage FecharDadosDoPlano()
    {
        Dsl.Clicar(webDriver, GlobalVariables.FecharTela, "Botão Fechar Plano");
        Dsl.Esperar();

        if (Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.FecharPlanoConfirmacao) > 0)
            Dsl.Clicar(webDriver, GlobalVariables.FecharPlanoConfirmacao, "Botão Confirmar Fechar Plano");

        Dsl.Esperar();

        return this;
    }

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
        string mensagemSucessoEsperadaAlocacaoAtualizada = "Alocaçãoatualizadacomsucesso!";
        string nomeAtivoAlocadoEsperado;
        string nomeAtivoAlocadoAtual;
        string editarAtivo;
        IList<string> nomesAtivosAlocadosEsperados = ativosGraficos;

        switch (clienteUpSellAtual)
        {
            case ClienteUpSell.ClienteStart:
                int qtdAtivosAlocados = Dsl.ObterQuantidadeLinhasNoElementoTabelaComLinhaInvisivel(webDriver, GlobalVariables.TabelaAtivosPlano);

                for (var i = 1; i <= qtdAtivosAlocados; i++)
                {
                    editarAtivo = $"//tr[{i + 1}]//button/span[@aria-label='edit']";

                    Dsl.EsperarElementoParaClicar(webDriver, editarAtivo, "Botão Editar Ativo");

                    Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.LinhaTabelaLojasAtivoAlocados);
                    Dsl.Esperar();

                    nomeAtivoAlocadoAtual = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.NomeAtivoAlocacao, "Campo Nome Ativo");
                    nomeAtivoAlocadoEsperado = nomesAtivosAlocadosEsperados[i - 1];

                    Dsl.ValidarTextosNoElemento(nomeAtivoAlocadoAtual, nomeAtivoAlocadoEsperado);

                    AumentarQuantidadeAtivosPorLoja();

                    Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.SalvarAlocacaoLoja);
                    Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SalvarAlocacaoLoja, "Botão Salvar Quantidades Alocadas do Ativo por Loja");

                    Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.LoadDeTelaSpiner);
                    Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);

                    ValidarMensagensDoPlano(mensagemSucessoEsperadaAlocacaoAtualizada);

                    Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.LoadDeTelaSpiner);
                    Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);
                }
                break;
            case ClienteUpSell.ClientePro:
            case ClienteUpSell.ClienteExpert:
                nomeAtivoAlocadoEsperado = nomesAtivosAlocadosEsperados.FirstOrDefault();
                BuscarAtivosAlocadosNoPlano(nomeAtivoAlocadoEsperado);
                Dsl.Esperar();

                editarAtivo = "//tr[2]//button/span[@aria-label='edit']";

                Dsl.EsperarElementoParaClicar(webDriver, editarAtivo, "Botão Editar Ativo");

                Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaSpiner);
                Dsl.Esperar(3000);

                string textoQuantidadeLojasAtivoAlocado = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.QuantidadeLojasPorAtivo, "Label Quantidade de Lojas no Ativo Alocado");
                var quantidadeLojasAtivoAlocadoAtual = Dsl.RemoverLetrasEspacosDeUmTexto(textoQuantidadeLojasAtivoAlocado, "Label Quantidade de Lojas no Ativo Alocado");
                Dsl.ValidarNumerosNoElemento(quantidadeLojasAtivoAlocadoAtual, 20, "Label Quantidade de Loja no Ativo Alocado");

                nomeAtivoAlocadoAtual = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.NomeAtivoAlocacao, "Campo Nome Ativo");

                Dsl.ValidarTextosNoElemento(nomeAtivoAlocadoAtual, nomeAtivoAlocadoEsperado);

                AumentarQuantidadeAtivosPorLoja();

                Dsl.ScrollParaElemento(webDriver, GlobalVariables.SalvarAlocacaoLoja);
                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.SalvarAlocacaoLoja);
                Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SalvarAlocacaoLoja, "Botão Salvar Quantidades Alocadas do Ativo por Loja");

                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.LoadDeTelaSpiner);
                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);

                ValidarMensagensDoPlano(mensagemSucessoEsperadaAlocacaoAtualizada);

                Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.LoadDeTelaSpiner);
                Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);
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
        Dsl.ScrollParaElemento(webDriver, GlobalVariables.AbaAtivosAlocados);
        Dsl.Clicar(webDriver, GlobalVariables.AbaAtivosAlocados, "Aba Ativos Alocados");

        return this;
    }

    /// <summary>
    /// Método para alocar um novo ativo para as lojas do plano
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage AlocarNovosAtivosNoPlano()
    {
        string mensagemSucessoEsperadaAlocacaoAtualizada = "Alocaçãoatualizadacomsucesso!";
        string ativoNome = DataLoader.ObterDados("negociacoes_planos", "TestGlobalData", "ativoNome");

        switch (clienteUpSellAtual)
        {
            case ClienteUpSell.ClienteStart:
                var xpathElemento = $"//div[@class='rc-virtual-list']//*[text()='{ativoNome}']";

                Dsl.Clicar(webDriver, GlobalVariables.IncluirAlocacaoAtivo, "Botão Incluir Ativo");
                Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.BuscarAtivoAlocacao, ativoNome);
                Dsl.EsperarVisibilidadeDoElemento(webDriver, xpathElemento);
                Dsl.Clicar(webDriver, xpathElemento, "Campo Selecionar Ativo");

                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.LinhaTabelaLojasAtivoAlocados);
                Dsl.Esperar();

                AumentarQuantidadeAtivosPorLoja();

                Dsl.ScrollParaElemento(webDriver, GlobalVariables.SalvarAlocacaoLoja);
                Dsl.Clicar(webDriver, GlobalVariables.SalvarAlocacaoLoja, "Botão Salvar Quantidades Alocadas do Ativo por Loja");

                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.LoadDeTelaSpiner);
                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);

                ValidarMensagensDoPlano(mensagemSucessoEsperadaAlocacaoAtualizada);

                Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.LoadDeTelaSpiner);
                Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);
                break;
            case ClienteUpSell.ClientePro:
            case ClienteUpSell.ClienteExpert:
                Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.IncluirAlocacaoAtivo, "Botão Incluir Ativo");
                Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.BuscarAtivoAlocacao, ativoNome);

                var elementoAtivoNome = $"//div[@class='rc-virtual-list']//*[text()='{ativoNome}']";
                Dsl.EsperarVisibilidadeDoElemento(webDriver, elementoAtivoNome);
                Dsl.Clicar(webDriver, elementoAtivoNome, "Campo Selecionar Ativo");

                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.LinhaTabelaLojasAtivoAlocados);
                Dsl.Esperar(2000);

                var textoQuantidadeLojasAtivoAlocado = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.QuantidadeLojasPorAtivo, "Label Quantidade de Lojas no Ativo Alocado");
                var quantidadeLojasAtivoAlocadoAtual = Dsl.RemoverLetrasEspacosDeUmTexto(textoQuantidadeLojasAtivoAlocado, "Label Quantidade de Lojas no Ativo Alocado");
                Dsl.ValidarNumerosNoElemento(quantidadeLojasAtivoAlocadoAtual, 20, "Label Quantidade de Loja no Ativo Alocado");

                AumentarQuantidadeAtivosPorLoja();

                Dsl.ScrollParaElemento(webDriver, GlobalVariables.SalvarAlocacaoLoja);
                Dsl.Clicar(webDriver, GlobalVariables.SalvarAlocacaoLoja, "Botão Salvar Quantidades Alocadas do Ativo por Loja");

                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.LoadDeTelaSpiner);
                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);

                ValidarMensagensDoPlano(mensagemSucessoEsperadaAlocacaoAtualizada);

                Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.LoadDeTelaSpiner);
                Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);
                break;
        }

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

                if (quantidadeAtivosAlocadosLoja is int)
                {
                    int qtd = (int)quantidadeAtivosAlocadosLoja;
                    for (var i = 1; i <= qtd; i++)
                    {
                        //Aumentando a quantidade de alocação por loja
                        webDriver.FindElement(By.XPath($"//tr[{i + 1}]/td[22]/div//span[@aria-label='Increase Value']")).Click();
                    }
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

                Dsl.ValidarMensagemDeFeedbacak(mensagensEsperadas, mensagensAtuais);
                break;
        }

        return this;
    }

    /// <summary>
    /// Método para alterar a situação do plano para aprovado ou cancelado
    /// </summary>
    /// <param name="contextoSituacao"></param>
    /// <returns></returns>
    public PlanosContratosPage EditarSituacaoDoPlano()
    {
        var valorSetorDepartamentoCategoria = "Geral";
        var teste = TestContext.CurrentContext.Test.MethodName;

        if (!string.IsNullOrEmpty(teste) && teste.Equals("TestAprovarPlano"))
        {
            //var mensagemAlertaInformarParcelaEsperada = "Salveasparcelascomostatusdoplanosimuladoparaaprovaroplano!";

            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SituacaoPlano, "Campo Situação Plano");
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SituacaoPlanoAprovar, "Campo Selecionar Situação Plano");

            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.TipoCampanha, "Campo Tipo Campanha");
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarTipoCampanha, "Campo Selecionar Tipo Campanha");

            Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.QuantidadeParcelas, "1");
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.AbaDadosPlano, "Aba Dados Plano");

            Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.Mensagens);
            //ValidarMensagensDoPlano(mensagemAlertaInformarParcelaEsperada);
            Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.Mensagens);

            Dsl.ScrollParaElemento(webDriver, GlobalVariables.SalvarRegistro);

            Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.Setor, valorSetorDepartamentoCategoria);
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarSetor, "Campo Selecionar Setor");

            Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.Departamento, valorSetorDepartamentoCategoria);
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarDepartamento, "Campo Selecionar Departamento");

            Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.Categoria, valorSetorDepartamentoCategoria);
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarCategoria, "Campo Selecionar Categoria");
        }
        else if (!string.IsNullOrEmpty(teste) && teste.Equals("TestCancelarPlano"))
        {
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SituacaoPlano, "Campo Situação do Plano");
            Dsl.Clicar(webDriver, GlobalVariables.SituacaoPlanoCancelar, "Selecionar Situação Plano Cancelado");
        }

        return this;
    }

    /// <summary>
    /// Método para validar mensagens de comunicação apresentadas nas ações do plano
    /// </summary>
    /// <param name="mensagemEsperada"></param>
    /// <returns></returns>
    public PlanosContratosPage ValidarMensagensDoPlano(string mensagemEsperada)
    {
        var texto = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao, "Mensagens de Comunicação");
        var mensagemAtual = Dsl.RemoverNumerosEspacosDeUmTexto(texto, "Mensagens de Comunicação");
        var valorAtributoDataTestId = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao, "Mensagens de Comunicação", atributoTesteId);

        Dsl.ValidarMensagemDeComunicacao(mensagemAtual, mensagemEsperada, valorAtributoDataTestId);

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

        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.DataCancelamentoPlano);
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
        var mensagemSucessoEsperadaPlanoDeletado = "Planodeletadocomsucesso";
        foreach (var nomeCampanha in nomeCampanhas)
        {
            Dsl.BuscarRegistros(webDriver, GlobalVariables.FiltrarPlanoPorCampanha, GlobalVariables.PesquisarNomeCampanha, GlobalVariables.BuscarRegistro, nomeCampanha);
            var quantidadeLinhasTabela = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.TabelaPlanos) - 1; //Contar linhas no elemento tbody da listagem de planos, ignorando a tag tr sem dados

            for (var i = 1; i <= quantidadeLinhasTabela; i++)
            {
                Dsl.Clicar(webDriver, GlobalVariables.ExcluirPlano(nomeCampanha), "Botão Excluir Plano");

                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.TituloModalConfirmacao);
                ValidarMensagensDeModalDoPlano(mensagemConfirmacaoEsperadaExcluirPlano);

                Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.OkExclusao, "Botão OK Exclusão");

                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);
                ValidarMensagensDoPlano(mensagemSucessoEsperadaPlanoDeletado);
                Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);
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
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MensagemIndisponibilidadeInventario);

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

        return this;
    }

    /// <summary>
    /// Método para validar o valor de receita dos ativos e do plano
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage ValidarReceitasDoPlano()
    {
        var tipoAtributo = "value";
        var teste = TestContext.CurrentContext.Test.MethodName;
        Dsl.Esperar();

        if (string.IsNullOrEmpty(teste))
            throw new ArgumentException("Variável teste vazia ou nulo");

        try
        {
            switch (clienteUpSellAtual)
            {
                case ClienteUpSell.ClienteStart:
                    if (teste.Equals("TestCriarPlanoComAtivosTipoMidiaGrafica"))
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
                    else if (teste.Equals("TestCriarPlanoComAtivosTipoMidiaFisica"))
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
                    else if (teste.Equals("TestEditarPlanoExistenteAlterandoQuantidadeAlocadaDoAtivoDisponivel"))
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
                    else if (teste.Equals("TestEditarPlanoExistenteIncluindoNovoAtivoDisponivel"))
                    {
                        var valorReceitaAtivosEsperado = 1000.35;
                        var valorReceitaAtivos = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaAtivos, "Campo Receita Ativos", tipoAtributo);
                        var valorReceitaAtivosAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaAtivos, "Campo Receita Ativos");

                        var valorReceitaPlanoEsperado = 1145.70;
                        var valorReceitaPlano = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaPlano, "Campo Receita Plano", tipoAtributo);
                        var valorReceitaPlanoAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaPlano, "Campo Receita Plano");

                        Dsl.ValidarNumerosNoElemento(valorReceitaAtivosAtual, valorReceitaAtivosEsperado, "Campo Receita Ativos");
                        Dsl.ValidarNumerosNoElemento(valorReceitaPlanoAtual, valorReceitaPlanoEsperado, "Campo Receita Plano");
                    }
                    break;
                case ClienteUpSell.ClientePro:
                case ClienteUpSell.ClienteExpert:
                    if (teste.Equals("TestCriarPlanoComAtivosTipoMidiaGrafica"))
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
                    else if (teste.Equals("TestCriarPlanoComAtivosTipoMidiaFisica"))
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
                    else if (teste.Equals("TestEditarPlanoExistenteAlterandoQuantidadeAlocadaDoAtivoDisponivel"))
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
                    else if (teste.Equals("TestEditarPlanoExistenteIncluindoNovoAtivoDisponivel"))
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