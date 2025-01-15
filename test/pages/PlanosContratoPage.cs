using System.Runtime.InteropServices;
using OpenQA.Selenium;

namespace MeuClienteWebTestProject;

/// <summary>
/// Classe com métodos específicos de manipulação\interação dos elementos, pertinentes a tela Planos da plataforma
/// </summary>
public class PlanosContratosPage
{
    private IWebDriver webDriver;
    private string[] nomesAtivos = { "Adesivo de Check Out", "Ponta de Gôndola", "Woobler" };
    private string[] abasPlano = { "Dados do Plano", "Ativos Alocados", "Fluxo de Pagamentos", "Histórico", "Anexos", "Book Fotográfico", "Painel da indústria" };

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
        Dsl.Clicar(webDriver, GlobalVariables.NovoRegistro, "Botão Nova Simulação");

        return this;
    }

    /// <summary>
    /// Métodos para preencher o campo Indústria
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage PreencherCampoIndustria()
    {
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.PesquisarIndustria);
        Dsl.ScrollParaElemento(webDriver, GlobalVariables.PesquisarIndustria);
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
    public PlanosContratosPage SelecionarAtivos()
    {
        Dsl.Clicar(webDriver, GlobalVariables.SelecionarAtivos, "Botão Selecionar Ativos");

        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.FiltrarAtivos);
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.FiltrarAtivos, "Botão Filtrar Ativo");

        foreach (var nomeAtivo in nomesAtivos)
        {
            Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.PesquisarAtivos, nomeAtivo);
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarAtivosFiltro, "Campo Seleciona Ativo Filtrado");
            webDriver.FindElement(By.XPath(GlobalVariables.PesquisarAtivos)).SendKeys(Keys.Control + "a" + Keys.Backspace); //Apagando o nome do ativo do campo de pesquisa
            Dsl.Clicar(webDriver, GlobalVariables.TelaFiltrarAtivo, "Tela Filtrar Ativos");
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
    public PlanosContratosPage PreencherQuantidadeAtivos(string contextoDeTestes)
    {
        if (contextoDeTestes.Contains("SemPlantaLoja"))
        {
            Dsl.ScrollParaElemento(webDriver, GlobalVariables.CarregarLojas);
            foreach (var nomeAtivo in nomesAtivos)
            {
                for (var i = 0; i < 5; i++)
                {
                    //Informando a quantidade de ativos por loja
                    webDriver.FindElement(By.XPath($"//*[text()='{nomeAtivo}']/following-sibling::td[12]//span[@aria-label='Increase Value']")).Click();
                }
            }
        }
        else if (contextoDeTestes.Contains("ComPlantaLoja"))
        {
            Dsl.ScrollParaElemento(webDriver, GlobalVariables.AplicarQuantidadePorLojaMassivamente);
            Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.QuantidadePorLoja, "5");
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.AplicarQuantidadePorLojaMassivamente, "Botão Aplicar Quantidade para Todas as Lojas na Simulação do Plano");
            Dsl.Esperar(500);
            Dsl.ScrollParaElemento(webDriver, GlobalVariables.CarregarLojas);
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
    public PlanosContratosPage GerarPrePlano()
    {
        Dsl.ScrollParaElemento(webDriver, GlobalVariables.GerarPrePlano);
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.GerarPrePlano, "Botão Gerar Pré-Plano");
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.SalvarRegistro);

        return this;
    }

    /// <summary>
    /// Método para validar a tela de plano após a criação do mesmo
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage ValidarPlanoCriado()
    {
        var contadorAbaPlano = 1;

        Dsl.Esperar();
        Dsl.ScrollParaElemento(webDriver, GlobalVariables.AbasPlano);

        foreach (var abaPlano in abasPlano)
        {
            webDriver.FindElement(By.XPath($"//div[@class='ant-tabs-nav-list']/div[{contadorAbaPlano}]")).Click();

            var tituloAbaAtual = webDriver.FindElement(By.XPath($"//div[@class='ant-tabs-nav-list']/div[{contadorAbaPlano}]")).Text;
            var tituloAbaEsperado = abaPlano;

            Assert.That(tituloAbaAtual, Does.Contain(tituloAbaEsperado));
            Thread.Sleep(500);

            contadorAbaPlano++;
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
        var statusPlanoAtual = webDriver.FindElement(By.XPath(GlobalVariables.StatusPlano)).Text;
        Assert.That(statusPlanoAtual, Does.Contain(statusPlanoEsperado), "Status atual não corresponde com o esperado");

        var farolPlanoAtual = webDriver.FindElement(By.XPath(GlobalVariables.FarolPlano)).Text;
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
        if (contextoDeExecucao.Equals("NovoPlano"))
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
        if (contextoDeExecucao.Equals("NovoPlano"))
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
    public PlanosContratosPage SalvarPlano(string contextoDeExecucao, [Optional] string contextoDeTeste)
    {
        var mensagemSucessoEsperada = "OPlanofoialteradocomsucesso!";

        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.AbaDadosPlano, "Aba Dados Plano");
        Dsl.ScrollParaElemento(webDriver, GlobalVariables.SalvarRegistro);
        Dsl.Clicar(webDriver, GlobalVariables.SalvarRegistro, "Botão Salvar Plano");

        if (contextoDeExecucao.Contains("CancelarPlano"))
            ConfirmarCancelamentoDoPlano();

        var mensagemSucessoAtual = Dsl.RemoverNumerosEspacosDeUmTexto(webDriver, GlobalVariables.Mensagens, "Mensagem Salvar Plano");
        ValidarMensagemDeSucessoEAlerta(mensagemSucessoAtual, mensagemSucessoEsperada);

        ValidarReceitasDoPlano(contextoDeExecucao, contextoDeTeste);

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
        var mensagemSucessoEsperada = "Alocaçãoatualizadacomsucesso!";

        if (contextoDeTestes.Contains("SemPlantaLoja"))
        {
            var qtdAtivosAlocados = Dsl.ObterQuantidadeLinhasNoElementoTabelaComLinhaInvisivel(webDriver, GlobalVariables.TabelaAtivosPlano);

            for (var i = 1; i <= qtdAtivosAlocados; i++)
            {
                var editarAtivo = $"//tr[{i + 1}]//button/span[@aria-label='edit']";

                Dsl.EsperarElementoParaClicar(webDriver, editarAtivo, "Botão Editar Ativo");

                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.TabelaLojasAtivoAlocados);
                Dsl.Esperar();
                var qtdAtivosAlocadosLoja = Dsl.RemoverLetrasEspacosDeUmTexto(webDriver, GlobalVariables.QuantidadeLojasPorAtivo, "Campo Total Lojas por Ativo", 1); //Descobrindo a quantidade de lojas no plano para o ativo alocado

                int qtd = (int)qtdAtivosAlocadosLoja;
                for (var j = 1; j <= qtd; j++)
                {
                    webDriver.FindElement(By.XPath($"//tbody//tr[{j + 1}]/td[17]/div//span[@aria-label='Increase Value']")).Click(); //Aumentando a quantidade de alocação por loja
                }

                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.SalvarAlocacaoLoja);
                Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SalvarAlocacaoLoja, "Botão Salvar Quantidades Alocadas do Ativo por Loja");

                var mensagemSucessoAtual = Dsl.RemoverNumerosEspacosDeUmTexto(webDriver, GlobalVariables.MensagemSucessoAlocacaoAtivo, "Mensagem Alocação Ativo");
                Dsl.ValidarMensagemDeSucessoEAlerta(mensagemSucessoAtual, mensagemSucessoEsperada);
                Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTela);
            }
        }
        else if (contextoDeTestes.Contains("ComPlantaLoja"))
        {
            BuscarAtivosAlocadosNoPlano(nomesAtivos[0]);
            Dsl.Esperar();

            var quantidadeAtivosAlocados = Dsl.ObterQuantidadeLinhasNoElementoTabelaComLinhaInvisivel(webDriver, GlobalVariables.TabelaAtivosPlano);

            for (var i = 1; i <= quantidadeAtivosAlocados; i++)
            {
                var editarAtivo = $"//tr[{i + 1}]//button/span[@aria-label='edit']";

                Dsl.EsperarElementoParaClicar(webDriver, editarAtivo, "Botão Editar Ativo");

                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.TabelaLojasAtivoAlocados);
                Dsl.Esperar();

                Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.QuantidadePorLojaAtivosAlocados, "6");
                Dsl.Clicar(webDriver, GlobalVariables.AplicarQuantidadePorLojaMassivamenteAtivosAlocados, "Botão Aplicar Quantidade para Todas as Lojas");
                Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.MensagemSucessoEditarQuantidadeAlocacaoAtivo);

                Dsl.ScrollParaElemento(webDriver, GlobalVariables.SalvarAlocacaoLoja);
                Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.SalvarAlocacaoLoja);
                Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SalvarAlocacaoLoja, "Botão Salvar Quantidades Alocadas do Ativo por Loja");

                var mensagemSucessoAtual = Dsl.RemoverNumerosEspacosDeUmTexto(webDriver, GlobalVariables.MensagemSucessoAlocacaoAtivo, "Mensagem Salvar Alocação Ativo");
                Dsl.ValidarMensagemDeSucessoEAlerta(mensagemSucessoAtual, mensagemSucessoEsperada);
                Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTela);
                Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.MensagemSucessoAlocacaoAtivo);
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
        var mensagemSucessoEsperada = "Alocaçãoatualizadacomsucesso!";

        if (contextoDeTeste.Contains("SemPlantaLoja"))
        {
            var nomeAtivo = "Cestão 01 - ";

            Dsl.Clicar(webDriver, GlobalVariables.IncluirAlocacaoAtivo, "Botão Incluir Ativo");
            Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.BuscarAtivoAlocacao, nomeAtivo);
            Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.SelecionarAtivoAlocacao);
            Dsl.Clicar(webDriver, GlobalVariables.SelecionarAtivoAlocacao, "Campo Selecionar Ativo");

            Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.TabelaLojasAtivoAlocados);
            Dsl.Esperar();

            AumentarQuantidadeAtivosPorLoja(contextoDeTeste);

            Dsl.ScrollParaElemento(webDriver, GlobalVariables.SalvarAlocacaoLoja);
            Dsl.Clicar(webDriver, GlobalVariables.SalvarAlocacaoLoja, "Botão Salvar Quantidades Alocadas do Ativo por Loja");

            var mensagemSucessoAtual = Dsl.RemoverNumerosEspacosDeUmTexto(webDriver, GlobalVariables.MensagemSucessoAlocacaoAtivo, "Mensagem Alocação Ativo");
            Dsl.ValidarMensagemDeSucessoEAlerta(mensagemSucessoAtual, mensagemSucessoEsperada);
            Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.MensagemSucessoAlocacaoAtivo);
        }
        else if (contextoDeTeste.Contains("ComPlantaLoja"))
        {
            string[] nomeAtivo = { "Cestão 01 - ", "Cestão 01 - E01", "Cestão 01 - E02", "Cestão 01 - E03" };

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

                var mensagemSucessoAtual = Dsl.RemoverNumerosEspacosDeUmTexto(webDriver, GlobalVariables.MensagemSucessoAlocacaoAtivo, "Mensagem Alocação Ativo");
                Dsl.ValidarMensagemDeSucessoEAlerta(mensagemSucessoAtual, mensagemSucessoEsperada);
                Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTela);
            }
        }

        return this;
    }

    /// <summary>
    /// Método para aumentar as quantidades de ativos alocados por loja
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage AumentarQuantidadeAtivosPorLoja(string contextoDeTeste)
    {
        if (contextoDeTeste.Contains("SemPlantaLoja"))
        {
            var quantidadeAtivosAlocadosLoja = Dsl.RemoverLetrasEspacosDeUmTexto(webDriver, GlobalVariables.QuantidadeLojasPorAtivo, "Campo Total Lojas por Ativo", 1);

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
            Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.MensagemAvisoEditarQuantidadeAlocacaoAtivo);
            Dsl.Esperar();
        }

        return this;
    }

    /// <summary>
    /// Método para alterar a situação do plano para aprovado ou cancelado
    /// </summary>
    /// <param name="situacaoPlano"></param>
    /// <returns></returns>
    public PlanosContratosPage EditarSituacaoDoPlano(string contextoSituacao)
    {
        var valorSetorDepartamentoCategoria = "Geral";

        if (contextoSituacao.Equals("Contrato Aprovado"))
        {
            var mensagemAlertaParcelaEsperado = "Salveasparcelascomostatusdoplanosimuladoparaaprovaroplano!";

            webDriver.FindElement(By.XPath(GlobalVariables.SituacaoPlano)).Click();
            webDriver.FindElement(By.XPath($"//*[text()='{contextoSituacao}']")).Click();

            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.TipoCampanha, "Campo Tipo Campanha");
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarTipoCampanha, "Campo Selecionar Tipo Campanha");

            webDriver.FindElement(By.XPath(GlobalVariables.QuantidadeParcelas)).SendKeys("1");
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.AbaDadosPlano, "Aba Dados Plano");

            var mensagemAlertaParcelaAtual = Dsl.RemoverNumerosEspacosDeUmTexto(webDriver, GlobalVariables.Mensagens, "Mensagem Parcela Pagamentos");
            ValidarMensagemDeSucessoEAlerta(mensagemAlertaParcelaAtual, mensagemAlertaParcelaEsperado);
            Thread.Sleep(3000);

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
            webDriver.FindElement(By.XPath($"//*[text()='{contextoSituacao}']")).Click();
        }

        return this;
    }

    /// <summary>
    /// Método para validar mensagens de sucesso e mensagens de alertas em telas
    /// </summary>
    /// <param name="mensagemAtual"></param>
    /// <param name="mensagemEsperada"></param>
    /// <returns></returns>
    public PlanosContratosPage ValidarMensagemDeSucessoEAlerta(string mensagemAtual, string mensagemEsperada)
    {
        Assert.That(mensagemAtual, Does.Contain(mensagemEsperada));

        return this;
    }

    /// <summary>
    /// Método para preencher a data de cancelemento e confirmar o cancelamento do plano
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage ConfirmarCancelamentoDoPlano()
    {
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.DataCancelamentoPlano);
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
        var mensagemAlertaExcluirPlanoEsperada = "DesejarealmenteexcluirestePlano?";
        var mensagemSucessoEsperada = "Planodeletadocomsucesso";
        var quantidadeLinhasTabela = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.TabelaPlanos) - 1; //Contar linhas no elemento tbody da listagem de planos, ignorando a tag tr sem dados

        for (var i = 1; i <= quantidadeLinhasTabela; i++)
        {
            Dsl.Clicar(webDriver, GlobalVariables.ExcluirPlano, "Botão Excluir Plano");

            var mensagemAlertaExcluirPlanoAtual = Dsl.RemoverNumerosEspacosDeUmTexto(webDriver, GlobalVariables.ExcluirPlanoMensagemConfirmacao, "Mensagem Confirmar Excluisão Plano");
            ValidarMensagemDeSucessoEAlerta(mensagemAlertaExcluirPlanoAtual, mensagemAlertaExcluirPlanoEsperada);

            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.OkExclusao, "Botão OK Exclusão");

            var mensagemSucessoAtual = Dsl.RemoverNumerosEspacosDeUmTexto(webDriver, GlobalVariables.Mensagens, "Mensagem Excluir Plano");
            ValidarMensagemDeSucessoEAlerta(mensagemSucessoAtual, mensagemSucessoEsperada);

            Dsl.Esperar(2000);
        }
        return this;
    }

    /// <summary>
    /// Método para validar os alertas apresentados com a indisponibildiade do ativo no inventário da loja
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage ValidarAlertaInventario()
    {
        var mensagemAlertaEsperada = "Algumaslojasnãoteminventáriosuficientedisponível";
        var mensagemAlertaAtual = Dsl.RemoverNumerosEspacosDeUmTexto(webDriver, GlobalVariables.MensagensDadosPlano, "Mensagem Inventário Loja");

        Dsl.ValidarDisponbilidadeDeInventarioParaLoja(webDriver, GlobalVariables.InventarioAlerta, GlobalVariables.TabelaLojasPlano);
        Dsl.ValidarMensagemDeSucessoEAlerta(mensagemAlertaAtual, mensagemAlertaEsperada);

        return this;
    }

    /// <summary>
    /// Método para validar as receitas dos ativos e do plano
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage ValidarReceitasDoPlano(string contextoDeExecucao, string contextoDeTeste)
    {
        switch (contextoDeTeste)
        {
            case "SemPlantaLoja":
                if (contextoDeExecucao.Contains("NovoPlano"))
                {
                    var valorReceitaAtivosEsperado = 750.00;
                    var valorReceitaPlanoEsperado = 825.00;
                    Dsl.ValidarNumerosNoElemento(webDriver, GlobalVariables.ReceitaAtivos, valorReceitaAtivosEsperado, "Campo Receita Ativos");
                    Dsl.ValidarNumerosNoElemento(webDriver, GlobalVariables.ReceitaPlano, valorReceitaPlanoEsperado, "Campo Receita Plano");
                }
                else if (contextoDeExecucao.Contains("EditarPlanoAlterandoQuantidadeAtivo"))
                {
                    var valorReceitaAtivosEsperado = 900.00;
                    var valorReceitaPlanoEsperado = 990.00;
                    Dsl.ValidarNumerosNoElemento(webDriver, GlobalVariables.ReceitaAtivos, valorReceitaAtivosEsperado, "Campo Receita Ativos");
                    Dsl.ValidarNumerosNoElemento(webDriver, GlobalVariables.ReceitaPlano, valorReceitaPlanoEsperado, "Campo Receita Plano");
                }
                else if (contextoDeExecucao.Contains("EditarPlanoIncluindoAtivo"))
                {
                    var valorReceitaAtivosEsperado = 950.00;
                    var valorReceitaPlanoEsperado = 1045.00;
                    Dsl.ValidarNumerosNoElemento(webDriver, GlobalVariables.ReceitaAtivos, valorReceitaAtivosEsperado, "Campo Receita Ativos");
                    Dsl.ValidarNumerosNoElemento(webDriver, GlobalVariables.ReceitaPlano, valorReceitaPlanoEsperado, "Campo Receita Plano");
                }
                break;
            case "ComPlantaLoja":
                if (contextoDeExecucao.Contains("NovoPlano"))
                {
                    var valorReceitaAtivosEsperado = 3000.00;
                    var valorReceitaPlanoEsperado = 3300.00;
                    Dsl.ValidarNumerosNoElemento(webDriver, GlobalVariables.ReceitaAtivos, valorReceitaAtivosEsperado, "Campo Receita Ativos");
                    Dsl.ValidarNumerosNoElemento(webDriver, GlobalVariables.ReceitaPlano, valorReceitaPlanoEsperado, "Campo Receita Plano");
                }
                else if (contextoDeExecucao.Contains("EditarPlanoAlterandoQuantidadeAtivo"))
                {
                    var valorReceitaAtivosEsperado = 3200.00;
                    var valorReceitaPlanoEsperado = 3520.00;
                    Dsl.ValidarNumerosNoElemento(webDriver, GlobalVariables.ReceitaAtivos, valorReceitaAtivosEsperado, "Campo Receita Ativos");
                    Dsl.ValidarNumerosNoElemento(webDriver, GlobalVariables.ReceitaPlano, valorReceitaPlanoEsperado, "Campo Receita Plano");
                }
                else if (contextoDeExecucao.Contains("EditarPlanoIncluindoAtivo"))
                {
                    var valorReceitaAtivosEsperado = 3800.00;
                    var valorReceitaPlanoEsperado = 4140.00;
                    Dsl.ValidarNumerosNoElemento(webDriver, GlobalVariables.ReceitaAtivos, valorReceitaAtivosEsperado, "Campo Receita Ativos");
                    Dsl.ValidarNumerosNoElemento(webDriver, GlobalVariables.ReceitaPlano, valorReceitaPlanoEsperado, "Campo Receita Plano");
                }
                break;
        }

        return this;
    }
}