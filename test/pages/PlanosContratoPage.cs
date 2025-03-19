using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using OpenQA.Selenium;

namespace MeuClienteWebTestProject;

/// <summary>
/// Classe com métodos específicos de manipulação\interação dos elementos, pertinentes a tela Planos da plataforma
/// </summary>
public class PlanosContratosPage
{
    private IWebDriver webDriver;
    private string atributoDataTestId = "data-testid";
    private string[] ativosGraficos = { "Adesivo de Check Out", "Display de Check Out", "Woobler" };
    private string[] ativosFisicos = { "Cestão 01", "Ponta de Gôndola" };
    private string[] nomesAbasPlanoEsperado = { "Dados do Plano", "Ativos Alocados", "Fluxo de Pagamentos", "Histórico", "Anexos", "Book Fotográfico", "Painel da indústria", "Tarefas" };
    private string[] nomesEtapasWorkflowPlanoEsperado = { "Arte", "Montagem", "Comprovação" };

    public PlanosContratosPage(IWebDriver webDriver)
    {
        this.webDriver = webDriver;
    }

    /// <summary>
    /// Método para realizar uma nova simulação de plano\contrato
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage NovaSimulacaoDePlano()
    {
        var mensagemConfirmacaoEsperadaReutilizarDadosSalvosAnteriormente = "Existemdadossalvosdaultimasimulação,desejareutiliza-los?";

        Dsl.Clicar(webDriver, GlobalVariables.NovoRegistro, "Botão Nova Simulação");
        Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTela1);

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
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.PesquisarIndustria);
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.PesquisarIndustria, "Campo Indústria");
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.SelecionarIndustria);

        Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.PreencherIndustria, "Indústria 01 F");
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarIndustria, "Campo Selecionar Indústria");

        return this;
    }

    /// <summary>
    /// Método para preencher o campo Campanha
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage PreencherCampoCampanha(string nomeCampanha)
    {
        Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.PreencherCampanha, nomeCampanha);

        return this;
    }

    /// <summary>
    /// Método para filtrar e selecionar os ativos
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage SelecionarAtivos(string tipoAtivoMidia)
    {
        Dsl.Clicar(webDriver, GlobalVariables.SelecionarAtivos, "Botão Selecionar Ativos");

        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.FiltrarAtivos);
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.FiltrarAtivos, "Botão Filtrar Ativo");

        switch (tipoAtivoMidia)
        {
            case "Grafica":
                foreach (var nomeAtivo in ativosGraficos)
                {
                    Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.PesquisarAtivos, nomeAtivo);
                    Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarAtivosFiltro, "Campo Seleciona Ativo Filtrado");
                    webDriver.FindElement(By.XPath(GlobalVariables.PesquisarAtivos)).SendKeys(Keys.Control + "a" + Keys.Backspace); //Apagando o nome do ativo do campo de pesquisa
                    Dsl.Clicar(webDriver, GlobalVariables.TelaFiltrarAtivo, "Tela Filtrar Ativos");
                }
                break;
            case "Fisica":
                foreach (var nomeAtivo in ativosFisicos)
                {
                    Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.PesquisarAtivos, nomeAtivo);
                    Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarAtivosFiltro, "Campo Seleciona Ativo Filtrado");
                    webDriver.FindElement(By.XPath(GlobalVariables.PesquisarAtivos)).SendKeys(Keys.Control + "a" + Keys.Backspace); //Apagando o nome do ativo do campo de pesquisa
                    Dsl.Clicar(webDriver, GlobalVariables.TelaFiltrarAtivo, "Tela Filtrar Ativos");
                }
                break;
        }

        Dsl.Clicar(webDriver, GlobalVariables.OkFiltroAtivos, "Botão OK Ativos Selecionados no Filtro");
        Dsl.Clicar(webDriver, GlobalVariables.SelecionarTodosAtivos, "Campo Selecionar Todos os Ativos");
        Dsl.Clicar(webDriver, GlobalVariables.AplicarAtivos, "Botão Aplicar Ativos Selecionados");

        return this;
    }

    /// <summary>
    /// Método para preencher as quantidades dos ativos por loja
    /// </summary>
    /// <param name="contextoDeTestes"></param>
    /// <returns></returns>
    public PlanosContratosPage PreencherQuantidadeAtivos(string contextoDeTestes, string tipoAtivoMidia)
    {
        switch (contextoDeTestes)
        {
            case "SemPlantaLoja":
                if (tipoAtivoMidia.Equals("Grafica"))
                {
                    Dsl.ScrollParaElemento(webDriver, GlobalVariables.CarregarLojas);
                    foreach (var nomeAtivo in ativosGraficos)
                    {
                        for (var i = 0; i < 4; i++)
                        {
                            //Informando a quantidade de ativos por loja
                            webDriver.FindElement(By.XPath($"//*[text()='{nomeAtivo}']/following-sibling::td[12]//span[@aria-label='Increase Value']")).Click();
                        }
                    }
                }
                else if (tipoAtivoMidia.Equals("Fisica"))
                {
                    Dsl.ScrollParaElemento(webDriver, GlobalVariables.CarregarLojas);
                    foreach (var nomeAtivo in ativosFisicos)
                    {
                        for (var i = 0; i < 4; i++)
                        {
                            //Informando a quantidade de ativos por loja
                            webDriver.FindElement(By.XPath($"//*[text()='{nomeAtivo}']/following-sibling::td[12]//span[@aria-label='Increase Value']")).Click();
                        }
                    }
                }
                break;
            case "ComPlantaLoja":
                Dsl.ScrollParaElemento(webDriver, GlobalVariables.AplicarQuantidadePorLojaMassivamente);
                Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.QuantidadePorLoja, "5");
                Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.AplicarQuantidadePorLojaMassivamente, "Botão Aplicar Quantidade para Todas as Lojas na Simulação do Plano");
                Dsl.Esperar(500);
                Dsl.ScrollParaElemento(webDriver, GlobalVariables.CarregarLojas);
                break;
        }

        return this;
    }

    /// <summary>
    /// Método para selecionar as lojas carregadas na simulação do plano, para alocação dos ativos
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage SelecionarLojas()
    {
        Dsl.Clicar(webDriver, GlobalVariables.CarregarLojas, "Botão Carregar Lojas");
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MenuLojas);
        Dsl.ScrollParaElemento(webDriver, GlobalVariables.GerarPrePlano);

        var quantidadeLojasCarregadas = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.TabelaLojasPlano) - 1; //Contar linhas no elemento tbody da listagem de lojas carregadas na simulação do plano, ignorando a tag tr sem dados

        for (var i = 1; i <= quantidadeLojasCarregadas; i++)
        {
            webDriver.FindElement(By.XPath($"//tbody/tr[{i + 1}]/td[9]//input[@class='ant-checkbox-input']")).Click();
        }

        return this;
    }

    /// <summary>
    /// Método para gerar o pré-plano, clicando no botão Gera Pré-Plano
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage GerarPrePlano(string contextoDeTeste, string tipoAtivoMidia)
    {
        switch (tipoAtivoMidia)
        {
            case "Grafica":
                Dsl.ScrollParaElemento(webDriver, GlobalVariables.GerarPrePlano);
                Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.GerarPrePlano, "Botão Gerar Pré-Plano");
                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.SalvarRegistro);
                break;
            case "Fisica":
                Dsl.ScrollParaElemento(webDriver, GlobalVariables.GerarPrePlano);
                Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.GerarPrePlano, "Botão Gerar Pré-Plano");

                Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.PesquisarUsuarioResponsavelEtapaWorkflow, "Campo Selecionar Usuário Responsável do Workflow no Plano");

                if (contextoDeTeste.Equals("SemPlantaLoja"))
                {
                    Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.PreencherUsuarioResponsavelEtapaWorkflow, "UserHomolog02SP");
                    Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarUsuarioResponsavelEtapaWorkflowSP, "Campo Selecionar Usuário Responsável");
                }
                else
                {
                    Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.PreencherUsuarioResponsavelEtapaWorkflow, "UserHomolog02CP");
                    Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarUsuarioResponsavelEtapaWorkflowCP, "Campo Selecionar Usuário Responsável");
                }

                Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.GerarPrePlanoComWorkflowSelecionado, "Botão Gerar Pré-Plano com Workflow");
                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.SalvarRegistro);
                break;
        }

        return this;
    }

    /// <summary>
    /// Método para validar a tela de plano após a criação do mesmo
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage ValidarPlanoCriado()
    {
        Dsl.ScrollParaElemento(webDriver, GlobalVariables.EtapasWorkflowGraficoPlano);
        Dsl.Esperar();
        Dsl.ScrollParaElemento(webDriver, GlobalVariables.AbasPlano);

        IList<IWebElement> elementos = Dsl.ObterListaDeElementos(webDriver, GlobalVariables.EtapasWorkflowPlano);
        IList<string> nomesEtapasWorkflowPlanoAtual = elementos.Select(elementos => elementos.Text).ToList();

        foreach (var (nomeEtapaWorkflowAtual, nomeEtapaWorkflowEsperado) in nomesEtapasWorkflowPlanoAtual.Zip(nomesEtapasWorkflowPlanoEsperado, (nomesEtapasWorkflowPlanoAtual, nomeEtapaWorkflowPlanoEsperado) => (nomesEtapasWorkflowPlanoAtual, nomeEtapaWorkflowPlanoEsperado)))
        {
            Dsl.ValidarTextosNoElemento(nomeEtapaWorkflowAtual, nomeEtapaWorkflowEsperado);
        }

        foreach (var nomeAbaPlano in nomesAbasPlanoEsperado)
        {
            Dsl.Esperar(500);
            var xpathElemento = $"//div[contains(@class,'ant-tabs-tab')]/div[contains(text(),'{nomeAbaPlano}')]";
            Dsl.Clicar(webDriver, xpathElemento, "Abas Edição Plano");

            var tituloAbaAtual = Dsl.ObterTextoDoElemento(webDriver, xpathElemento, "Abas Edição Plano");
            var tituloAbaEsperado = nomeAbaPlano;

            Dsl.ValidarTextosNoElemento(tituloAbaAtual, tituloAbaEsperado);
            if (nomeAbaPlano.Equals("Anexos") || nomeAbaPlano.Equals("Book Fotográfico") || nomeAbaPlano.Equals("Painel da indústria"))
                Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTela);
        }

        return this;
    }

    /// <summary>
    /// Método para validar status e farol na lista de planos, após criação ou alteração
    /// </summary>
    /// <param name="statusPlanoEsperado"></param>
    /// <param name="farolPlanoEsperado"></param>
    /// <returns></returns>
    public PlanosContratosPage ValidarStatusFarolDoPlano(string statusPlanoEsperado, string farolPlanoEsperado)
    {
        var statusPlanoAtual = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.StatusPlano, "Coluna Status Plano");
        Assert.That(statusPlanoAtual, Does.Contain(statusPlanoEsperado), "Status atual não corresponde com o esperado");

        var farolPlanoAtual = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.FarolPlano, "Coluna Farol Plano");
        Assert.That(farolPlanoAtual, Does.Contain(farolPlanoEsperado), "Farol atual não corresponde com o esperado");

        return this;
    }

    /// <summary>
    /// Método para buscar planos
    /// </summary>
    /// <param name="textoValor"></param>
    /// <returns></returns>
    public PlanosContratosPage BuscarPlanos(string textoValor)
    {
        Dsl.BuscarRegistros(webDriver, GlobalVariables.FiltrarPlanoPorCampanha, GlobalVariables.PesquisarNomeCampanha, GlobalVariables.BuscarRegistro, textoValor);

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
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.EditarPlano, "Botão Editar Plano");
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.AbasPlano);

        return this;
    }

    /// <summary>
    /// Método para preencher o campo Inicio Vigencia
    /// </summary>
    /// <param name="contextoDeExecucao"></param>
    /// <returns></returns>
    public PlanosContratosPage EditarInicioVigencia(string contextoDeExecucao)
    {
        if (contextoDeExecucao.Equals("CriarPlanoComWorkflowPadrao"))
        {
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.InicioVigenciaNovoPlano, "Campo Inicio Vigencia Novo Plano");
            Dsl.PreencherCalendariosInicioVigencia(webDriver, GlobalVariables.AvancarCalendarioMesInicioVigencia, 2);
        }
        else if (contextoDeExecucao.Equals("EditarPlano"))
        {
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.InicioVigenciaEditarPlano, "Campo Inicio Vigencia Editar Plano");
            Dsl.Esperar();
            Dsl.PreencherCalendariosInicioVigencia(webDriver, GlobalVariables.AvancarCalendarioMesInicioVigencia, 2);
        }

        return this;
    }

    /// <summary>
    /// Método para preencher o campo Fim vigencia
    /// </summary>
    /// <param name="contextoDeExecucao"></param>
    /// <returns></returns>
    public PlanosContratosPage EditarFimVigencia(string contextoDeExecucao)
    {
        if (contextoDeExecucao.Equals("CriarPlanoComWorkflowPadrao"))
        {
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.FimVigenciaNovoPlano, "Campo Fim Vigencia Novo Plano");
            Dsl.PreencherCalendariosFimVigencia(webDriver, GlobalVariables.AvancarCalendarioMesFimVigencia, 2);
        }
        else if (contextoDeExecucao.Equals("EditarPlano"))
        {
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.FimVigenciaEditarPlano, "Campo Fim Vigencia Editar Plano");
            Dsl.Esperar();
            Dsl.PreencherCalendariosFimVigencia(webDriver, GlobalVariables.AvancarCalendarioMesFimVigencia, 2);
        }

        return this;
    }

    /// <summary>
    /// Método para salvar os dados plano com diferentes status
    /// </summary>
    /// <param name="contextoDeExecucao"></param>
    /// <param name="contextoDeTeste"></param>
    /// <returns></returns>
    public PlanosContratosPage SalvarPlano([Optional] string contextoDeExecucao)
    {
        var mensagemPlanoAlteradoEsperada = "OPlanofoialteradocomsucesso!";

        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.AbaDadosPlano, "Aba Dados Plano");
        Dsl.ScrollParaElemento(webDriver, GlobalVariables.SalvarRegistro);
        Dsl.Clicar(webDriver, GlobalVariables.SalvarRegistro, "Botão Salvar Plano");

        if (contextoDeExecucao != null && contextoDeExecucao.Equals("CancelarPlano"))
            ConfirmarCancelamentoDoPlano();

        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);
        ValidarMensagensDoPlano(mensagemPlanoAlteradoEsperada);
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

    /// <summary>
    /// Método para editar as quantidades dos ativos por loja
    /// </summary>
    /// <param name="contextoDeTestes"></param>
    /// <returns></returns>
    public PlanosContratosPage EditarQuantidadesDosAtivosNoPlano(string contextoDeTestes)
    {
        var mensagemSucessoEsperadaAlocacaoAtualizada = "Alocaçãoatualizadacomsucesso!";
        var mensagemSucessoEsperadaProdutosInseridos = "Produtosinseridoscomsucesso!";

        if (contextoDeTestes.Contains("SemPlantaLoja"))
        {
            string[] nomesAtivosAlocadosEsperados = { "Adesivo de Check Out -", "Display de Check Out -", "Woobler -" };
            
            var qtdAtivosAlocados = Dsl.ObterQuantidadeLinhasNoElementoTabelaComLinhaInvisivel(webDriver, GlobalVariables.TabelaAtivosPlano);

            for (var i = 1; i <= qtdAtivosAlocados; i++)
            {
                var editarAtivo = $"//tr[{i + 1}]//button/span[@aria-label='edit']";

                Dsl.EsperarElementoParaClicar(webDriver, editarAtivo, "Botão Editar Ativo");

                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.TabelaLojasAtivoAlocados);
                Dsl.Esperar();

                var nomeAtivoAlocadoAtual = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.NomeAtivoAlocao, "Campo Nome Ativo");
                var nomeAtivoAlocadoEsperado = nomesAtivosAlocadosEsperados[i - 1];

                Dsl.ValidarTextosNoElemento(nomeAtivoAlocadoAtual, nomeAtivoAlocadoEsperado);

                var texto = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.QuantidadeLojasPorAtivo, "Campo Total Lojas por Ativo");
                var quantidadeAtivosAlocadosLoja = Dsl.RemoverLetrasEspacosDeUmTexto(texto, "Campo Total Lojas por Ativo"); //Descobrindo a quantidade de lojas no plano para o ativo alocado

                int qtd = (int)quantidadeAtivosAlocadosLoja;
                for (var j = 1; j <= qtd; j++)
                {
                    webDriver.FindElement(By.XPath($"//tbody//tr[{j + 1}]/td[17]/div//span[@aria-label='Increase Value']")).Click(); //Aumentando a quantidade de alocação por loja
                }

                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.SalvarAlocacaoLoja);
                Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SalvarAlocacaoLoja, "Botão Salvar Quantidades Alocadas do Ativo por Loja");

                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.LoadDeTela);
                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);

                ValidarMensagensDoPlano(mensagemSucessoEsperadaAlocacaoAtualizada);

                Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.LoadDeTela);
                Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);
            }
        }
        else if (contextoDeTestes.Contains("ComPlantaLoja"))
        {
            string[] nomesAtivosAlocadosEsperados = { "Adesivo de Check Out -", "Adesivo de Check Out - E01", "Adesivo de Check Out - E02", "Adesivo de Check Out - E03" };

            BuscarAtivosAlocadosNoPlano(ativosGraficos[0]);
            Dsl.Esperar();

            var quantidadeAtivosAlocados = Dsl.ObterQuantidadeLinhasNoElementoTabelaComLinhaInvisivel(webDriver, GlobalVariables.TabelaAtivosPlano);

            for (var i = 1; i <= quantidadeAtivosAlocados; i++)
            {
                var editarAtivo = $"//tr[{i + 1}]//button/span[@aria-label='edit']";

                Dsl.EsperarElementoParaClicar(webDriver, editarAtivo, "Botão Editar Ativo");

                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.TabelaLojasAtivoAlocados);
                Dsl.Esperar();

                var nomeAtivoAlocadoAtual = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.NomeAtivoAlocao, "Campo Nome Ativo");
                var nomeAtivoAlocadoEsperado = nomesAtivosAlocadosEsperados[i - 1];

                Dsl.ValidarTextosNoElemento(nomeAtivoAlocadoAtual, nomeAtivoAlocadoEsperado);

                Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.QuantidadePorLojaAtivosAlocados, "6");
                Dsl.Clicar(webDriver, GlobalVariables.AplicarQuantidadePorLojaMassivamenteAtivosAlocados, "Botão Aplicar Quantidade para Todas as Lojas");

                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);
                ValidarMensagensDoPlano(mensagemSucessoEsperadaProdutosInseridos);
                Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);

                Dsl.ScrollParaElemento(webDriver, GlobalVariables.SalvarAlocacaoLoja);
                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.SalvarAlocacaoLoja);
                Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SalvarAlocacaoLoja, "Botão Salvar Quantidades Alocadas do Ativo por Loja");

                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.LoadDeTela);
                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);

                ValidarMensagensDoPlano(mensagemSucessoEsperadaAlocacaoAtualizada);

                Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.LoadDeTela);
                Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);
            }
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
    /// <param name="contextoDeTeste"></param>
    /// <returns></returns>
    public PlanosContratosPage AlocarNovosAtivosNoPlano(string contextoDeTeste)
    {
        var mensagemSucessoEsperadaAlocacaoAtualizada = "Alocaçãoatualizadacomsucesso!";

        if (contextoDeTeste.Contains("SemPlantaLoja"))
        {
            var nomeAtivo = "Ilha 01 - ";
            var xpathElemento = $"//div[@class='rc-virtual-list']//*[text()='{nomeAtivo}']";

            Dsl.Clicar(webDriver, GlobalVariables.IncluirAlocacaoAtivo, "Botão Incluir Ativo");
            Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.BuscarAtivoAlocacao, nomeAtivo);
            Dsl.EsperarVisibilidadeDoElemento(webDriver, xpathElemento);
            Dsl.Clicar(webDriver, xpathElemento, "Campo Selecionar Ativo");

            Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.TabelaLojasAtivoAlocados);
            Dsl.Esperar();

            AumentarQuantidadeAtivosPorLoja(contextoDeTeste);

            Dsl.ScrollParaElemento(webDriver, GlobalVariables.SalvarAlocacaoLoja);
            Dsl.Clicar(webDriver, GlobalVariables.SalvarAlocacaoLoja, "Botão Salvar Quantidades Alocadas do Ativo por Loja");

            Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.LoadDeTela);
            Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);

            ValidarMensagensDoPlano(mensagemSucessoEsperadaAlocacaoAtualizada);

            Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.LoadDeTela);
            Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);
        }
        else if (contextoDeTeste.Contains("ComPlantaLoja"))
        {
            string[] nomeAtivo = { "Ilha 01 - ", "Ilha 01 - E01", "Ilha 01 - E02", "Ilha 01 - E03" };

            foreach (var nome in nomeAtivo)
            {
                Dsl.ScrollModalElemento(webDriver, GlobalVariables.ModalPlanos);
                Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.IncluirAlocacaoAtivo, "Botão Incluir Ativo");
                Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.BuscarAtivoAlocacao, nome);

                var elementoAtivoNome = $"//div[@class='rc-virtual-list']//*[text()='{nome}']";
                Dsl.EsperarVisibilidadeDoElemento(webDriver, elementoAtivoNome);
                Dsl.Clicar(webDriver, elementoAtivoNome, "Campo Selecionar Ativo");

                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.TabelaLojasAtivoAlocados);
                Dsl.Esperar();

                AumentarQuantidadeAtivosPorLoja(contextoDeTeste);

                Dsl.ScrollParaElemento(webDriver, GlobalVariables.SalvarAlocacaoLoja);
                Dsl.Clicar(webDriver, GlobalVariables.SalvarAlocacaoLoja, "Botão Salvar Quantidades Alocadas do Ativo por Loja");

                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.LoadDeTela);
                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);

                ValidarMensagensDoPlano(mensagemSucessoEsperadaAlocacaoAtualizada);

                Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.LoadDeTela);
                Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);
            }
        }

        return this;
    }

    /// <summary>
    /// Método para aumentar as quantidades de ativos alocados por loja
    /// </summary>
    /// <param name="contextoDeTeste"></param>
    /// <returns></returns>
    public PlanosContratosPage AumentarQuantidadeAtivosPorLoja(string contextoDeTeste)
    {
        var mensagemAlertaSalvarInformacoes = "Salvesuasinformaçõesparapoderincluirprodutofoco!";

        if (contextoDeTeste.Contains("SemPlantaLoja"))
        {
            var texto = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.QuantidadeLojasPorAtivo, "Campo Total Lojas por Ativo");
            var quantidadeAtivosAlocadosLoja = Dsl.RemoverLetrasEspacosDeUmTexto(texto, "Campo Total Lojas por Ativo"); //Descobrindo a quantidade de lojas no plano para o ativo alocado

            if (quantidadeAtivosAlocadosLoja is int)
            {
                int qtd = (int)quantidadeAtivosAlocadosLoja;
                for (var i = 1; i <= qtd; i++)
                {
                    //Aumentando a quantidade de alocação por loja
                    webDriver.FindElement(By.XPath($"//tr[{i + 1}]/td[17]/div//span[@aria-label='Increase Value']")).Click();
                }
            }
        }
        else if (contextoDeTeste.Contains("ComPlantaLoja"))
        {
            Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.QuantidadePorLojaAtivosAlocados, "1");
            Dsl.Clicar(webDriver, GlobalVariables.AplicarQuantidadePorLojaMassivamenteAtivosAlocados, "Botão Aplicar Quantidade para Todas as Lojas");

            Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);
            ValidarMensagensDoPlano(mensagemAlertaSalvarInformacoes);
            Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);
            Dsl.Esperar();
        }

        return this;
    }

    /// <summary>
    /// Método para alterar a situação do plano para aprovado ou cancelado
    /// </summary>
    /// <param name="contextoSituacao"></param>
    /// <returns></returns>
    public PlanosContratosPage EditarSituacaoDoPlano(string contextoSituacao)
    {
        var valorSetorDepartamentoCategoria = "Geral";

        if (contextoSituacao.Equals("Contrato Aprovado"))
        {
            var mensagemAlertaInformarParcelaEsperada = "Salveasparcelascomostatusdoplanosimuladoparaaprovaroplano!";

            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SituacaoPlano, "Campo Situação Plano");
            var xpathElemento = $"//*[text()='{contextoSituacao}']";
            Dsl.EsperarElementoParaClicar(webDriver, xpathElemento, "Campo Selecionar Situação Plano");

            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.TipoCampanha, "Campo Tipo Campanha");
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarTipoCampanha, "Campo Selecionar Tipo Campanha");

            Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.QuantidadeParcelas, "1");
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.AbaDadosPlano, "Aba Dados Plano");

            Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);
            ValidarMensagensDoPlano(mensagemAlertaInformarParcelaEsperada);
            Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);

            Dsl.ScrollParaElemento(webDriver, GlobalVariables.SalvarRegistro);

            Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.Setor, valorSetorDepartamentoCategoria);
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarSetor, "Campo Selecionar Setor");

            Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.Departamento, valorSetorDepartamentoCategoria);
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarDepartamento, "Campo Selecionar Departamento");

            Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.Categoria, valorSetorDepartamentoCategoria);
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarCategoria, "Campo Selecionar Categoria");
        }
        else if (contextoSituacao.Equals("Cancelado"))
        {
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SituacaoPlano, "Campo Situação do Plano");
            var xpathElemento = $"//*[text()='{contextoSituacao}']";
            Dsl.Clicar(webDriver, xpathElemento, "Selecionar Situação Plano Cancelado");
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
        var valorAtributoDataTestId = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao, "Mensagens de Comunicação", atributoDataTestId);

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
        var quantidadeLinhasTabela = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.TabelaPlanos) - 1; //Contar linhas no elemento tbody da listagem de planos, ignorando a tag tr sem dados

        for (var i = 1; i <= quantidadeLinhasTabela; i++)
        {
            Dsl.Clicar(webDriver, GlobalVariables.ExcluirPlano, "Botão Excluir Plano");

            Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.TituloModalConfirmacao);
            ValidarMensagensDeModalDoPlano(mensagemConfirmacaoEsperadaExcluirPlano);

            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.OkExclusao, "Botão OK Exclusão");

            Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);
            ValidarMensagensDoPlano(mensagemSucessoEsperadaPlanoDeletado);
            Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.MensagemDeComunicacao);
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
            Assert.Fail("Mensagem de indisponibilidade e ícone de alerta não foram apresentados corretamente: " + "\n" + "Mensagem: " + contadorMensagemAlertaAtual + " Alertas: " + contadorIconeAlertaAtual);
        }

        return this;
    }

    /// <summary>
    /// Método para validar o valor de receita dos ativos e do plano
    /// </summary>
    /// <param name="contextoDeTeste"></param>
    /// <param name="contextoDeExecucao"></param>
    /// <returns></returns>
    public PlanosContratosPage ValidarReceitasDoPlano(string contextoDeTeste, string contextoDeExecucao)
    {
        var tipoAtributo = "value";

        switch (contextoDeTeste)
        {
            case "SemPlantaLoja":
                if (contextoDeExecucao.Equals("CriarPlanoComWorkflowPadrao"))
                {
                    var valorReceitaAtivosEsperado = 750.00;
                    var valorReceitaAtivos = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaAtivos, "Campo Receita Ativos", tipoAtributo);
                    var valorReceitaAtivosAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaAtivos, "Campo Receita Ativos");

                    var valorReceitaPlanoEsperado = 825.00;
                    var valorReceitaPlano = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaPlano, "Campo Receita Plano", tipoAtributo);
                    var valorReceitaPlanoAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaPlano, "Campo Receita Ativos");

                    Dsl.ValidarNumerosNoElemento(valorReceitaAtivosAtual, valorReceitaAtivosEsperado, "Campo Receita Ativos");
                    Dsl.ValidarNumerosNoElemento(valorReceitaPlanoAtual, valorReceitaPlanoEsperado, "Campo Receita Plano");
                }
                else if (contextoDeExecucao.Equals("CriarPlanoComWorkflow"))
                {
                    var valorReceitaAtivosEsperado = 527.50;
                    var valorReceitaAtivos = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaAtivos, "Campo Receita Ativos", tipoAtributo);
                    var valorReceitaAtivosAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaAtivos, "Campo Receita Ativos");

                    var valorReceitaPlanoEsperado = 605.00;
                    var valorReceitaPlano = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaPlano, "Campo Receita Plano", tipoAtributo);
                    var valorReceitaPlanoAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaPlano, "Campo Receita Plano");

                    Dsl.ValidarNumerosNoElemento(valorReceitaAtivosAtual, valorReceitaAtivosEsperado, "Campo Receita Ativos");
                    Dsl.ValidarNumerosNoElemento(valorReceitaPlanoAtual, valorReceitaPlanoEsperado, "Campo Receita Plano");
                }
                else if (contextoDeExecucao.Equals("EditarPlanoAlterandoQuantidadeAtivo"))
                {
                    var valorReceitaAtivosEsperado = 900.00;
                    var valorReceitaAtivos = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaAtivos, "Campo Receita Ativos", tipoAtributo);
                    var valorReceitaAtivosAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaAtivos, "Campo Receita Ativos");

                    var valorReceitaPlanoEsperado = 990.00;
                    var valorReceitaPlano = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaPlano, "Campo Receita Plano", tipoAtributo);
                    var valorReceitaPlanoAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaPlano, "Campo Receita Plano");

                    Dsl.ValidarNumerosNoElemento(valorReceitaAtivosAtual, valorReceitaAtivosEsperado, "Campo Receita Ativos");
                    Dsl.ValidarNumerosNoElemento(valorReceitaPlanoAtual, valorReceitaPlanoEsperado, "Campo Receita Plano");
                }
                else if (contextoDeExecucao.Equals("EditarPlanoIncluindoAtivo"))
                {
                    var valorReceitaAtivosEsperado = 1050.00;
                    var valorReceitaAtivos = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaAtivos, "Campo Receita Ativos", tipoAtributo);
                    var valorReceitaAtivosAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaAtivos, "Campo Receita Ativos");

                    var valorReceitaPlanoEsperado = 1145.00;
                    var valorReceitaPlano = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaPlano, "Campo Receita Plano", tipoAtributo);
                    var valorReceitaPlanoAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaPlano, "Campo Receita Plano");

                    Dsl.ValidarNumerosNoElemento(valorReceitaAtivosAtual, valorReceitaAtivosEsperado, "Campo Receita Ativos");
                    Dsl.ValidarNumerosNoElemento(valorReceitaPlanoAtual, valorReceitaPlanoEsperado, "Campo Receita Plano");
                }
                break;
            case "ComPlantaLoja":
                if (contextoDeExecucao.Equals("CriarPlanoComWorkflowPadrao"))
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
                else if (contextoDeExecucao.Equals("CriarPlanoComWorkflow"))
                {
                    var valorReceitaAtivosEsperado = 2000.00;
                    var valorReceitaAtivos = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaAtivos, "Campo Receita Ativos", tipoAtributo);
                    var valorReceitaAtivosAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaAtivos, "Campo Receita Ativos");

                    var valorReceitaPlanoEsperado = 2200.00;
                    var valorReceitaPlano = Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.ReceitaPlano, "Campo Receita Plano", tipoAtributo);
                    var valorReceitaPlanoAtual = Dsl.RemoverLetrasEspacosDeUmTexto(valorReceitaPlano, "Campo Receita Plano");

                    Dsl.ValidarNumerosNoElemento(valorReceitaAtivosAtual, valorReceitaAtivosEsperado, "Campo Receita Ativos");
                    Dsl.ValidarNumerosNoElemento(valorReceitaPlanoAtual, valorReceitaPlanoEsperado, "Campo Receita Plano");
                }
                else if (contextoDeExecucao.Equals("EditarPlanoAlterandoQuantidadeAtivo"))
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
                else if (contextoDeExecucao.Equals("EditarPlanoIncluindoAtivo"))
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
        }
        return this;
    }
}